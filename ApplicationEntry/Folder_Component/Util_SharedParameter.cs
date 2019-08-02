using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace NoahDesign.Folder_Component
{
  class Util_SharedParameter
  {
    public const string paramAREA = "Area";
    public const string paramVolume = "Volume";
    public const string paramFileName = "GeometryPlus.txt";

    public static bool SetSharedParameter( Document doc )
    {
      string sharedParamfileName = doc.Application.SharedParametersFilename;


      return true;
    }

  }
}
