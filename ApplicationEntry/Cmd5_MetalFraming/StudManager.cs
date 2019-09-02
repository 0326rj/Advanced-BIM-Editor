#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Structure;
using System.Diagnostics;
using MyUtils;
#endregion

namespace NoahDesign.Cmd5_MetalFraming
{
  // 2019.8.26
  // LinearArray 보다 ElementTransformUtils.CopyElement 메서드를
  // 사용하면 더 효율적으로 배열이 가능하지 않을까?

  static class StudManager
  {

    /// <summary>
    /// 한개의 스터드를 배치한 후, 벽의 Direction에 따라 회전시킨다.
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="wall"></param>
    /// <param name="studSymbol"></param>
    /// <returns></returns>
    internal static FamilyInstance Create_Stud_In_Wall( 
      Document doc,
      Wall wall,
      FamilySymbol studSymbol
      )
    {
      if ( wall.IsValidObject )
      {
        // Get wall base level by level ID
        var wallBaseLevel = doc.GetElement( wall.LevelId ) as Level;
        var wall_Top_LevelId = wall.get_Parameter( BuiltInParameter.WALL_HEIGHT_TYPE ).AsElementId();
        var wall_Top_Offset = wall.get_Parameter( BuiltInParameter.WALL_TOP_OFFSET ).AsDouble();
        var wall_base_Offset = wall.get_Parameter( BuiltInParameter.WALL_BASE_OFFSET ).AsDouble();
        var wall_user_Height = wall.get_Parameter( BuiltInParameter.WALL_USER_HEIGHT_PARAM ).AsDouble();

        // field of Wall curve , points
        var wallCurve = wall.Location as LocationCurve;
        var p1 = wallCurve.Curve.GetEndPoint( 0 );

        // Create Stud Instance
        FamilyInstance studInst = doc.Create.NewFamilyInstance(
        p1,
        studSymbol,
        wallBaseLevel,
        Autodesk.Revit.DB.Structure.StructuralType.Column );


        // Set Stud level parameter same to wall
        studInst.get_Parameter( BuiltInParameter.FAMILY_TOP_LEVEL_PARAM ).Set( wall_Top_LevelId );
        studInst.get_Parameter( BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM ).Set( wall_Top_Offset );
        studInst.get_Parameter( BuiltInParameter.FAMILY_BASE_LEVEL_OFFSET_PARAM ).Set( wall_base_Offset );


        // Get wall rotation angle information
        double angle = 0.0;
        XYZ wallDirection = null;
        XYZ vec = new XYZ( 1, 0, 0 );

        var locationCurve = wall.Location as LocationCurve;
        if ( locationCurve != null )
        {
          var wallLine = locationCurve.Curve as Line;
          wallDirection = wallLine.Direction;
          angle = vec.AngleTo( wallDirection );     
        }
        else
          TaskDialog.Show(".", "Wall LocationCurve is Null");


        // 問題
        // Rotate Stud angle by Wall curve direction 
        var am = studInst.GetAnalyticalModel();
        var axisCrvs = am.GetCurves( AnalyticalCurveType.BaseCurve );
        var axisLine = axisCrvs[0] as Line;

        if ( wallDirection.X < 0 && wallDirection.Y < 0 )
        {
          angle = -1.0 * angle;
        }
        else if ( wallDirection.X > 0 && wallDirection.Y < 0 )
        {
          angle = -1.0 * angle;
        }
        studInst.Location.Rotate( axisLine, angle + Math.PI / 2 );

        return studInst;
      }
      else
        return null;
    }


    /// <summary>
    /// 기준벽체와 배치된 스터드 한개를 인수로 받아 Array를 실행한다.
    /// endPointOffset 인수는 스터드 두께의 절반값을 입력한다. 
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="wall"></param>
    /// <param name="familyInstance"></param>
    /// <param name="finishCount"></param>
    /// <param name="endPointOffset"></param>
    /// <returns></returns>
    internal static ICollection<ElementId> Array_Stud_In_Wall( 
      Document doc,
      Wall wall,
      FamilyInstance familyInstance,
      int finishCount,
      double startPointOffset = 22.5,
      double endPointOffset = 22.5 
      )
    {
      double startOffset = UnitConvert.MillimetersToFeet( startPointOffset );
      double endOffset = UnitConvert.MillimetersToFeet( endPointOffset );

      ICollection<ElementId> linearArray = null;
      var locationCurve = wall.Location as LocationCurve;
      var line = locationCurve.Curve as Line;


      // Get wall vertor and rotate wall vector  
      var normalVector = line.Direction.Normalize();
      var rotatedNormalVector = Tools.Rotate_Vector_From_Line( locationCurve.Curve, Math.PI / 2 );

      var lineLength = line.Length - endOffset;
      var wallLength = UnitConvert.FeetToMillimeters( lineLength );
      var arrayVector = normalVector * ( lineLength - startOffset );


      // Get Wall Layer and core line offset
      WallType wallType = wall.WallType;
      CompoundStructure comStructure = wallType.GetCompoundStructure();
      double coreOffsetValue = comStructure.GetOffsetForLocationLine( WallLocationLine.CoreCenterline );

      if ( !( wall.Flipped ) )
      {
        familyInstance.Location.Move( normalVector * startOffset );
        familyInstance.Location.Move( rotatedNormalVector * coreOffsetValue );
      }
      else
      {
        familyInstance.Location.Move( normalVector * startOffset );
        familyInstance.Location.Move( -( rotatedNormalVector ) * coreOffsetValue );
      }


      // 보드매수 1장의 경우 [290 ~ 310mm]폭으로 배열한다.
      List<double> pitchList = new List<double>();
      List<int> studCounts = new List<int>();
      double resultPitch = 0.0;
      double studCount = 0.0;

      if ( finishCount == 1 )
      {
        for ( int i = 1; i <= ( int )wallLength; i++ )
        {
          studCounts.Add( i );
        }

        foreach ( double count in studCounts )
        {
          resultPitch = ( wallLength / count );

          // 스터드 기본 유효폭 299 ~ 350
          if ( resultPitch >= 299.0 && resultPitch <= 350.0 )
          {
            pitchList.Add( resultPitch );
          }
          else if ( wallLength <= 1000.0 && wallLength >= 800.0 )
          {
            pitchList.Add( wallLength / 3 );
          }
          else if ( wallLength < 800.0 && wallLength >= 400.0 )
          {
            pitchList.Add( wallLength / 2 );
          }
        }      
      }


      // 보드매수 2장의 경우 450mm 정도의 폭으로 배열한다.
      else if ( finishCount == 2 )
      {

      }
      else
        TaskDialog.Show( "...", "ボード枚数の設定が正しくありません。" );

      // 유효폭 리스트로부터 스터드 개수 계산
      if ( pitchList.Count != 0 )
      {
        pitchList.Sort();
        studCount = ( wallLength / pitchList[0] ) + 1;

        linearArray = LinearArray.ArrayElementWithoutAssociation(
          doc,
          doc.ActiveView,
          familyInstance.Id,
          ( int )studCount,
          arrayVector,
          ArrayAnchorMember.Last );


        // 생성결과 정보 출력
        //String str1 = String.Format( "スタッド数 : {0}", studCount.ToString() );
        //String str2 = String.Format( "計算ビッチ : {0:N2} mm", pitchList[0] );
        //TaskDialog.Show( ".", str1 + "\n" + str2 );
      }
      else
      {
        return null;
      }
      return linearArray;
    }





  }
}
