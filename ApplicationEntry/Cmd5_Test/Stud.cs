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
  static class Stud
  {
    internal static FamilyInstance Create_Stud_In_Wall( Document doc, Wall wall, FamilySymbol studSymbol )
    {
      if ( wall.IsValidObject )
      {
        var wallBaseLevelId = wall.LevelId;
        var wallBaseLevel = doc.GetElement( wallBaseLevelId ) as Level;
        var wall_Top_LevelId = wall.get_Parameter( BuiltInParameter.WALL_HEIGHT_TYPE ).AsElementId();
        var wall_Top_Offset = wall.get_Parameter( BuiltInParameter.WALL_TOP_OFFSET ).AsDouble();
        var wall_base_Offset = wall.get_Parameter( BuiltInParameter.WALL_BASE_OFFSET ).AsDouble();

        // Create Stud Instance
        var wallCurve = wall.GetCurve();
        var p1 = wallCurve.GetEndPoint( 0 );
        var p2 = wallCurve.GetEndPoint( 1 );

        var studInst = doc.Create.NewFamilyInstance(
          p1,
          studSymbol,
          wallBaseLevel,
          Autodesk.Revit.DB.Structure.StructuralType.Column );

        // Set Stud level parameter same to wall
        studInst.get_Parameter( BuiltInParameter.FAMILY_TOP_LEVEL_PARAM ).Set( wall_Top_LevelId );
        studInst.get_Parameter( BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM ).Set( wall_Top_Offset );
        studInst.get_Parameter( BuiltInParameter.FAMILY_BASE_LEVEL_OFFSET_PARAM ).Set( wall_base_Offset );

        // Rotate Stud angle by Wall curve direction
        var stud_Lc = studInst.Location as LocationPoint;
        var z1 = stud_Lc.Point;
        var z2 = new XYZ( z1.X, z1.Y, z1.X + 1 );
        Line axis = Line.CreateBound( z1, z2 );
        studInst.Location.Rotate( axis, Math.PI / 3.0 );

        return studInst;
      }
      else
        return null;
    } 


    internal static Level GetWallBaseLevel(Document doc, Wall wall )
    {
      ElementId baseLevelId = wall.get_Parameter( BuiltInParameter.WALL_BASE_CONSTRAINT ).AsElementId();
      Level baseLevel = doc.GetElement( baseLevelId ) as Level;

      return baseLevel;
    }

    internal static Level GetWallTopLevel( Document doc, Wall wall )
    {
      ElementId topLevelId = wall.get_Parameter( BuiltInParameter.WALL_HEIGHT_TYPE ).AsElementId();
      Level topLevel = doc.GetElement( topLevelId ) as Level;

      return topLevel;
    }

    internal static void PlaceStud(
      Document doc,
      UV point2D,
      FamilySymbol studSymbol,
      Level baseLevel,
      Level topLevel )
    {
      var point = new XYZ( point2D.U, point2D.V, 0 );
      StructuralType structuralType;
      structuralType = StructuralType.Column;

      if ( !studSymbol.IsActive )
      {
        studSymbol.Activate();
      }

      FamilyInstance studInstance = doc.Create.NewFamilyInstance( point, studSymbol, topLevel, structuralType );

      if ( null != studInstance )
      {
        Parameter baseLevelParameter = studInstance
          .get_Parameter( BuiltInParameter.FAMILY_BASE_LEVEL_PARAM );
        Parameter topLevelParameter = studInstance
          .get_Parameter( BuiltInParameter.FAMILY_TOP_LEVEL_PARAM );
        Parameter topOffsetParameter = studInstance
          .get_Parameter( BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM );
        Parameter baseOffsetParameter = studInstance
          .get_Parameter( BuiltInParameter.FAMILY_BASE_LEVEL_OFFSET_PARAM );

        if ( null != baseLevelParameter )
        {
          baseLevelParameter.Set( baseLevel.Id );
        }

        if ( null != topLevelParameter )
        {
          baseLevelParameter.Set( topLevel.Id );
        }

        if ( null != topOffsetParameter )
        {
          topOffsetParameter.Set( 0.0 );
        }

        if ( null != baseOffsetParameter )
        {
          baseOffsetParameter.Set( 0.0 );
        }
      }
    }



  }
}
