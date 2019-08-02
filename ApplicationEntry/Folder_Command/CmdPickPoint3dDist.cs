using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using NoahDesign.Folder_Component;

using Exceptions = Autodesk.Revit.Exceptions;
using BuildingCoder;

namespace NoahDesign.Folder_Command
{
  [Transaction(TransactionMode.Manual)]
  class CmdPickPoint3dDist : IExternalCommand
  {
    public void PickPointsForArea( UIDocument uidoc )
    {
      Document doc = uidoc.Document;
      View view = doc.ActiveView;

      XYZ p1, p2;

      try
      {
        p1 = uidoc.Selection.PickPoint(
          "Please pick first point for area" );
        p2 = uidoc.Selection.PickPoint( "" +
          "Please pick second point for area" );
      }
      catch (Autodesk.Revit.Exceptions.OperationCanceledException)
      {
        return;
      }

      Plane plane = view.SketchPlane.GetPlane();

      UV q1 = plane.ProjectInto( p1 );
      UV q2 = plane.ProjectInto( p2 );
      UV d = q2 - q1;

      double area = d.U * d.V;

      area = Math.Round( area, 2 );

      if ( area < 0 )
      {
        area = area * ( -1 );
      }

      TaskDialog.Show( "Area", area.ToString() );
    }

    /// <summary>
    /// Prompt the user to select a face on an element
    /// and then pick a point on that face. The first
    /// picking of the face on the element temporarily
    /// redefines the active work plane, on which the
    /// second point can be picked.
    /// </summary>
    bool PickFaceSetWorkPlaneAndPickPoint( UIDocument uidoc, out XYZ point_in_3d )
    {
      point_in_3d = null;

      Document doc = uidoc.Document;

      try
      {
        Reference r = uidoc.Selection.PickObject(
          ObjectType.Face,
          "Please select a planar face to define work plane" );

        Element e = doc.GetElement( r.ElementId );

        if ( e != null )
        {
          PlanarFace face
            = e.GetGeometryObjectFromReference( r )
            as PlanarFace;

          if ( face != null )
          {
            Plane plane = Plane.CreateByNormalAndOrigin(
              face.FaceNormal, face.Origin );

            using ( Transaction trans = new Transaction( doc ) )
            {
              trans.Start( "Temporarily set work plane"
                + " to pick point in 3D" );

              SketchPlane sp = SketchPlane.Create( doc, plane );

              uidoc.ActiveView.SketchPlane = sp;
              uidoc.ActiveView.ShowActiveWorkPlane();

              point_in_3d = uidoc.Selection.PickPoint(
                "Please pick a point on the plane"
                + " defined by the selected face" );

              trans.RollBack();
            }
          }
        }
      }
      catch ( Autodesk.Revit.Exceptions.OperationCanceledException )
      {
      }

      return null != point_in_3d;
    }


    public XYZ Point1 { get; set; }
    public XYZ Point2 { get; set; }

    public Result Execute(
        ExternalCommandData commandData,
        ref string message,
        ElementSet elements )
    {
      UIApplication uiapp = commandData.Application;
      UIDocument uidoc = uiapp.ActiveUIDocument;

      XYZ pt1, pt2;
      Line line;

      try
      {
        PickFaceSetWorkPlaneAndPickPoint( uidoc, out pt1 );
        Point1 = pt1;
        PickFaceSetWorkPlaneAndPickPoint( uidoc, out pt2 );
        Point2 = pt2;

        double pointToPoint = UnitConvert.
          FeetToMillimetersLenth( pt1.DistanceTo( pt2 ) );

        TaskDialog.Show( "Distance", pointToPoint.ToString() );

        return Result.Succeeded;
      }
      catch ( Autodesk.Revit.Exceptions.OperationCanceledException )
      {
        return Result.Failed;
      }
    }
  }
}