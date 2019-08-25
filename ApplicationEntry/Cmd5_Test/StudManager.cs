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
using Autodesk.Revit.DB.Structure;
using System.Diagnostics;
using MyUtils;
#endregion

namespace NoahDesign.Cmd5_Test
{
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
      FamilySymbol studSymbol)
    {
      if ( wall.IsValidObject )
      {
        // Get wall base level by level ID
        var wallBaseLevel = doc.GetElement( wall.LevelId ) as Level;
        var wall_Top_LevelId = wall.get_Parameter( BuiltInParameter.WALL_HEIGHT_TYPE ).AsElementId();
        var wall_Top_Offset = wall.get_Parameter( BuiltInParameter.WALL_TOP_OFFSET ).AsDouble();
        var wall_base_Offset = wall.get_Parameter( BuiltInParameter.WALL_BASE_OFFSET ).AsDouble();

        // field of Wall curve , points
        var wallCurve = wall.Location as LocationCurve;
        var L = wallCurve.Curve as Line;
        var p1 = wallCurve.Curve.GetEndPoint( 0 );

        // Move to Core Center Line ( p1 )
        // 각도와 벡터간의 수학적 이해가 필요.
        WallType wallType = wall.WallType;
        CompoundStructure comStructure = wallType.GetCompoundStructure();
        double coreOffsetValue = comStructure.GetOffsetForLocationLine( WallLocationLine.CoreCenterline );

        double angle90 = UnitConvert.MillimetersToFeet( 90.0 );

        Transform trans = Transform.CreateRotation( p1, Math.PI / 2 );
        var L2 = L.CreateTransformed( trans ) as Line;

        XYZ Vector1 = L2.Direction * coreOffsetValue;

        // Create Stud Instance
        var studInst = doc.Create.NewFamilyInstance(
          p1 + Vector1,
          studSymbol,
          wallBaseLevel,
          Autodesk.Revit.DB.Structure.StructuralType.Column );

        // Set Stud level parameter same to wall
        studInst.get_Parameter( BuiltInParameter.FAMILY_TOP_LEVEL_PARAM ).Set( wall_Top_LevelId );
        studInst.get_Parameter( BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM ).Set( wall_Top_Offset );
        studInst.get_Parameter( BuiltInParameter.FAMILY_BASE_LEVEL_OFFSET_PARAM ).Set( wall_base_Offset );

        // Create Model Curve by Stud line
        //var modelCrv = New_ModelLine_By_AnalyticalModel( doc, studInst );

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

        // Implement Ratation
        studInst.Location.Rotate( axisLine,  angle + Math.PI / 2 );    

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

      var lineLength = line.Length - endOffset;
      var wallLength = UnitConvert.FeetToMillimeters( lineLength );
      var arrayVector = line.Direction * ( lineLength - startOffset );

      List<double> pitchList = new List<double>();
      List<int> studCounts = new List<int>();

      double resultPitch = 0.0;
      double studCount = 0.0;

      familyInstance.Location.Move( line.Direction * startOffset );

      // 보드매수 1장의 경우 [290 ~ 310mm]폭으로 배열한다.
      if ( finishCount == 1 )
      {
        for ( int i = 1; i <= ( int )wallLength; i++ )
        {
          studCounts.Add( i );
        }

        foreach ( double count in studCounts )
        {
          resultPitch = wallLength / count;

          // 스터드 기본 유효폭 299 ~ 350
          if ( resultPitch >= 299.0 && resultPitch <= 350.0 )
          {
            pitchList.Add( resultPitch );
          }
          else if ( resultPitch >= 270.0 && resultPitch <= 340.0 )
          {
            pitchList.Add( resultPitch );
          }
          else if ( wallLength <= 1000.0 && wallLength >= 800.0 )
          {
            pitchList.Add( wallLength / 3 );
          }
          else if ( wallLength < 800.0 && wallLength >= 500.0 )
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
        String str1 = String.Format( "スタッド数 : {0}", studCount.ToString() );
        String str2 = String.Format( "計算ビッチ : {0:N2} mm", pitchList[0] );
        TaskDialog.Show( ".", str1 + "\n" + str2 );
      }
      else
      {
        return null;
      }

      return linearArray;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="familyInstance"></param>
    /// <returns></returns>
    public static ModelCurve New_ModelLine_By_AnalyticalModel(Document doc, FamilyInstance familyInstance )
    {
      var analyticalModel = familyInstance.GetAnalyticalModel();
      var baseCurves = analyticalModel.GetCurves( AnalyticalCurveType.BaseCurve );

      if ( baseCurves != null )
      {
        Line axisLine = baseCurves[0] as Line;
        SketchPlane sketchPlane = NewSkethPlanePassLine( axisLine, doc );
        ModelCurve modelCurve = doc.Create.NewModelCurve( axisLine, sketchPlane );
        return modelCurve;
      }
      else
        return null;
    }


    public static SketchPlane NewSkethPlanePassLine(Line aline, Document doc )
    {
      XYZ norm;
      if ( aline.GetEndPoint( 0 ).X == aline.GetEndPoint( 1 ).X )
        norm = new XYZ( 1, 0, 0 );
      else if ( aline.GetEndPoint( 0 ).Y == aline.GetEndPoint( 1 ).Y )
        norm = new XYZ( 0, 1, 0 );
      else
        norm = new XYZ( 0, 0, 1 );
      XYZ point = aline.GetEndPoint( 0 );
      Plane plane = Plane.CreateByNormalAndOrigin( norm, point );
      SketchPlane sketchPlane = SketchPlane.Create( doc, plane );
      return sketchPlane;
    }


    private static Level GetWallBaseLevel(Document doc, Wall wall )
    {
      ElementId baseLevelId = wall.get_Parameter( BuiltInParameter.WALL_BASE_CONSTRAINT ).AsElementId();
      Level baseLevel = doc.GetElement( baseLevelId ) as Level;

      return baseLevel;
    }


    private static Level GetWallTopLevel( Document doc, Wall wall )
    {
      ElementId topLevelId = wall.get_Parameter( BuiltInParameter.WALL_HEIGHT_TYPE ).AsElementId();
      Level topLevel = doc.GetElement( topLevelId ) as Level;

      return topLevel;
    }


  }
}
