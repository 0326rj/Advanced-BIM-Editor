using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationEntry.Folder_WinForm;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using NoahDesign.Folder_WinForm;
using BuildingCoder;
using System.Diagnostics;

namespace NoahDesign.GeoPuls_CmdTest
{
  [Transaction(TransactionMode.Manual)]
  public class CmdTest : IExternalCommand
  {
    #region Property
    UIDocument _uidoc;
    Document _doc;

    public UIDocument UIDocument
    {
      get { return _uidoc; }
    }

    public Document Document
    {
      get { return _doc; }
    } 

    #endregion

    public Result Execute( ExternalCommandData commandData, ref string message, ElementSet elements )
    {
      _uidoc = commandData.Application.ActiveUIDocument;
      _doc = _uidoc.Document;

      var elementId = _uidoc.Selection.PickObject( ObjectType.Element ).ElementId;
      var element = _doc.GetElement( elementId );

      Options options = new Options();
      options.ComputeReferences = true;

      var geoObj = element.get_Geometry( options ) as GeometryObject;
      var solid = geoObj as Solid;


      using ( Transaction tx = new Transaction( _doc, "TestCommand" ) )
      {
        try
        {
          tx.Start();

          if ( solid.Faces.Size > 0)
          {
            foreach ( Reference r in solid.Faces )
            {
              FaceWall faceWall = FaceWall.Create( _doc,
                new ElementId( BuiltInCategory.OST_Walls ),
                WallLocationLine.CoreCenterline,
                r );
            }
          }

          tx.Commit();
        }
        catch ( Exception ex )
        {
          TaskDialog.Show( "...", ex.Message );
          return Result.Failed;
        }      
      }
      return Result.Succeeded;
    }
  }



  class XyzEqualityComparer : IEqualityComparer<XYZ>
  {
    public bool Equals( XYZ p, XYZ q )
    {
      return p.IsAlmostEqualTo( q );
    }

    public int GetHashCode( XYZ p )
    {
      return Util.PointString( p ).GetHashCode();
    }

    public static void GetVertices( List<XYZ> vertices, Solid s )
    {
      Debug.Assert( 0 < s.Edges.Size,
        "expected a non-empty solid" );

      Dictionary<XYZ, int> a
        = new Dictionary<XYZ, int>(
          new XyzEqualityComparer() );

      foreach ( Face f in s.Faces )
      {
        Mesh m = f.Triangulate();
        foreach ( XYZ p in m.Vertices )
        {
          if ( !a.ContainsKey( p ) )
          {
            a.Add( p, 1 );
          }
          else
          {
            ++a[p];
          }
        }
      }
      List<XYZ> keys = new List<XYZ>( a.Keys );

      Debug.Assert( 8 == keys.Count,
        "expected eight vertices for a rectangular column" );

      keys.Sort( ( p, q ) => Util.Compare( p, q ) );

      foreach ( XYZ p in keys )
      {
        Debug.Assert( 3 == a[p],
          "expected every vertex of solid to appear in exactly three faces" );

        vertices.Add( p );
      }
    }
  }
}
