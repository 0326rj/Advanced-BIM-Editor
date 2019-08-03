#region Namespaces
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace NoahDesign.Folder_Component
{
  public static class Util_CreateDirectShape
  {
    const string _sketch_plane_name_prefix = "Sketch plane name prefix";
    const string _sketch_plane_name_prefix2 = "<not associated>";

    #region Geometrical Comparison
    const double _eps = 1.0e-9;

    public static bool IsAlmostZero(
        double a,
        double tolerance )
    {
      return tolerance > Math.Abs( a );
    }

    public static bool IsAlmostZero( double a )
    {
      return IsAlmostZero( a, _eps );
    }

    public static bool IsAlmostEqual( double a, double b )
    {
      return IsAlmostZero( b - a );
    }
    #endregion // Geometrical Comparison

    /// Return the normal of a plane
    /// spanned by the two given Vectors.
    /// 2019. 5. 9 by JAEBUM KIM
    public static XYZ GetNormal( XYZ v1, XYZ v2 )
    {
      return v1.CrossProduct( v2 ).Normalize();
    }

    /// Return the normal of a plane spanned by the
    /// three given triangle corner points.
    /// 2019. 5. 9 by JAEBUM KIM
    public static XYZ GetNormal( XYZ[] triangleCorners )
    {
      return GetNormal(
          triangleCorners[1] - triangleCorners[0],
          triangleCorners[2] - triangleCorners[0] );
    }

    /// Return signed distance from plane to a given point.
    /// 2019. 5. 9 by JAEBUM KIM
    public static double SignedDistanceTo( Plane plane, XYZ p )
    {
      Debug.Assert(
          IsAlmostEqual( plane.Normal.GetLength(), 1 )
          , "expected normalised plane normal" );

      XYZ v = p - plane.Origin;

      return plane.Normal.DotProduct( v );
    }

    /// Return true if the sketch plane belongs to us
    /// and its origin and normal vector match the 
    /// given targets.
    /// Nope, we are unable to set the sketch plane
    /// name. However, Revit throws an exception if
    /// we try to draw on the sketch plane named
    /// 'Level1', so lets ensure we use '<not assosiated>'.
    /// 2019. 5. 9 by JAEBUM KIM
    public static bool SketchPlaneMatches( SketchPlane sketchPlane, XYZ origin, XYZ normal )
    {
      bool rc = sketchPlane.Name.Equals( _sketch_plane_name_prefix2 );

      if ( rc )
      {
        Plane plane = sketchPlane.GetPlane();

        rc = plane.Normal.IsAlmostEqualTo( normal )
            && IsAlmostZero( SignedDistanceTo(
                plane, origin ) );
      }
      return rc;
    }

    public static int _sketch_plane_creation_couter = 0;

    /// Return a sketch plane through the given origin
    /// point with the given normal, either by creating
    /// a new one or reusig an existing one.
    /// 2019. 5. 9 by JAEBUM KIM
    public static SketchPlane GetSketchPlane( Document doc, XYZ origin, XYZ normal )
    {
      string s = "reusing";

      /// If we could reliably set the sketch plane Name
      /// property or find some other reliably marker
      /// that is reflected in a parameter, we could
      /// replace the sketchPlane.Name.Equals check in
      /// SketchPlaneMatches by a parameter filter in
      /// the filtered element collector framework
      /// to move the test into native Revit code
      /// istead of post-processing in .NET, which
      /// would give a 50% performance enhancement.

      SketchPlane sketchPlane = new FilteredElementCollector( doc )
          .OfClass( typeof( SketchPlane ) )
          .Cast<SketchPlane>()
          .FirstOrDefault<SketchPlane>( x => SketchPlaneMatches( x, origin, normal ) );

      if ( null == sketchPlane )
      {
        Plane plane = Plane.CreateByNormalAndOrigin( normal, origin );

        sketchPlane = SketchPlane.Create( doc, plane );

        ++_sketch_plane_creation_couter;

        s = "created";
      }
      Debug.Print( "GetSketchPlane: {0} '{1}' ({2})",
          s, sketchPlane.Name,
          _sketch_plane_creation_couter );

      return sketchPlane;
    }

    /// Create model lines repesenting a closed 
    /// planar loop in the given sketch plane.
    /// 2019. 5. 9 by JAEBUM KIM
    public static void DrawModelLineLoop( SketchPlane sketchPlane, XYZ[] corners )
    {
      Autodesk.Revit.Creation.Document factory = sketchPlane.Document.Create;

      int n = corners.GetLength( 0 );

      for ( int i = 0; i < n; ++i )
      {
        int j = 0 == i ? n - 1 : i - 1;

        factory.NewModelCurve( Line.CreateBound( corners[j], corners[i] )
            , sketchPlane );
      }
    }

    /// Determine the stack of transforms to apply to
    /// the given target geometry object to bring it
    /// to the proper location in the project coordinates.
    /// Unfortunetely, we haver not found any way at all
    /// yet to identify the target object we are after.
    /// 2019. 5. 9 by JAEBUM KIM
    public static bool GetTransformStackForObject( Stack<Transform> tstack,
        GeometryElement geo,
        Document doc,
        string stable_representation )
    {
      Debug.Print( "enter GetTransformStackForObject "
          + "with tstack count {0}", tstack.Count );

      bool found = false;

      foreach ( GeometryObject obj in geo )
      {
        GeometryInstance gi = obj as GeometryInstance;

        if ( null != gi )
        {
          tstack.Push( gi.Transform );

          found = GetTransformStackForObject( tstack,
              gi.GetSymbolGeometry(),
              doc,
              stable_representation );

          if ( found )
          {
            return found;
          }

          tstack.Pop();

          continue;
        }

        Solid solid = obj as Solid;

        if ( null != solid )
        {
          string rep;

          bool isFace = stable_representation.EndsWith( "SURFACE" );

          bool isEdge = stable_representation.EndsWith( "LINEAR" );

          Debug.Assert( isFace || isEdge,
              "GetTransformStackForObject currently only supports faces and edges" );

          if ( isFace && 0 < solid.Faces.Size )
          {
            foreach ( Face face in solid.Faces )
            {
              rep = face.Reference.ConvertToStableRepresentation( doc );

              if ( rep.Equals( stable_representation ) )
              {
                return true;
              }
            }
          }
        }
      }
      return false;
    }
  }
}
