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
  static class StudManager_V2
  {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="wall"></param>
    /// <param name="studSymbol"></param>
    /// <returns></returns>
    internal static FamilyInstance Create_Stud_In_Wall_2(Document doc, Wall wall, FamilySymbol studSymbol )
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
        TaskDialog.Show( ".", "Wall LocationCurve is Null" );


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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="wall"></param>
    /// <param name="familyInstance"></param>
    /// <param name="finishCount"></param>
    /// <param name="startPointOffset"></param>
    /// <param name="endPointOffset"></param>
    /// <returns></returns>
    internal static ICollection<ElementId> Array_Stud_In_Wall_2(
      Document doc,
      Wall wall,
      FamilyInstance familyInstance,
      int finishCount,
      double startPointOffset = 22.5,
      double endPointOffset = 22.5 )
    {
      double studWidth = 45.0;


      double startOffset = UnitConvert.MillimetersToFeet( startPointOffset );
      double endOffset = UnitConvert.MillimetersToFeet( endPointOffset );

      List<ElementId> linearArray = new List<ElementId>(); 
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


      // 보드매수 1장의 경우 [303 mm]폭으로 배열한다.
      var length = line.Length;
      var wallOriginLength = UnitConvert.FeetToMillimeters( length );

      double copyPitch303 = UnitConvert.MillimetersToFeet( 303.0 );
      double copyCount = ( wallOriginLength / 303.0 ) - 1;

      double copiedDistance = ( 303.0 * ( ( int )copyCount + 1 ) + ( studWidth / 2 ) );
      double endStudDistance = wallOriginLength - ( studWidth / 2 );

      if ( finishCount == 1 )
      {
        if ( wallOriginLength > 909.0 )
        {
          var lastStud = ElementTransformUtils
            .CopyElement( doc, familyInstance.Id, arrayVector ) as List<ElementId>;

          linearArray.Add( familyInstance.Id );

          List<ElementId> temp = null;
          for ( int i = 0; i < copyCount; i++ )
          {
            temp = ElementTransformUtils
              .CopyElement( doc, linearArray[i], normalVector * copyPitch303 ) as List<ElementId>;

            linearArray.Add( temp.FirstOrDefault() );
          }

          // 마지막의 중첩되는 스터드를 제거한다.
          // 마지막 두개의 스터드 사이가 303 이상인 경우 중간점에 스터드를 추가생성한다 (미완성) 
          if ( ( wallLength - copiedDistance ) < studWidth )
          {
            doc.Delete( linearArray.LastOrDefault() );


          }
        }
        else if ( wallOriginLength <= 909.0 && wallOriginLength > 651.0 )
        {
          linearArray = LinearArray.ArrayElementWithoutAssociation(
            doc,
            doc.ActiveView,
            familyInstance.Id,
            4,
            arrayVector,
            ArrayAnchorMember.Last ) as List<ElementId>;
        }
        else if ( wallOriginLength <= 651.0 && wallOriginLength > 348.0 )
        {
          linearArray = LinearArray.ArrayElementWithoutAssociation(
            doc,
            doc.ActiveView,
            familyInstance.Id,
            3,
            arrayVector,
            ArrayAnchorMember.Last ) as List<ElementId>;
        }
        else if ( wallOriginLength <= 348.0 && wallOriginLength > 90.0 )
        {
          linearArray = LinearArray.ArrayElementWithoutAssociation(
            doc,
            doc.ActiveView,
            familyInstance.Id,
            2,
            arrayVector,
            ArrayAnchorMember.Last ) as List<ElementId>;
        }
        else
        {
          TaskDialog.Show( "警告", "壁の長さが短すぎます。", TaskDialogCommonButtons.Ok, TaskDialogResult.Ok);
        }
      }

      return linearArray;
    }



  }
}
