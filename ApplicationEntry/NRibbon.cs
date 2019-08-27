#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

namespace NoahDesign
{
  public class NRibbon
  {
    public static void AddRibbonPanel_VersionInfo( UIControlledApplication UICrdApp )
    {
      // dll assembly path 취득
      string path = Assembly.GetExecutingAssembly().Location;

      // Ribbon Tab 작성
      string tabName = "JAEBUM KIM";
      UICrdApp.CreateRibbonTab( tabName );

      // Ribbon Panel 작성
      RibbonPanel panel_GeometryPlus = UICrdApp.CreateRibbonPanel( tabName, "Geometry+" );

      RibbonPanel panel_DrawingPlus = UICrdApp.CreateRibbonPanel( tabName, "Drawing+" );

      RibbonPanel panel_MetalFraming = UICrdApp.CreateRibbonPanel( tabName, "Automatic Metal Framing" );

      RibbonPanel panel_Test = UICrdApp.CreateRibbonPanel( tabName, "Debug Commands" );

      RibbonPanel panel_Info = UICrdApp.CreateRibbonPanel( tabName, "Info" );


      // Geometry+
      #region Geometry+ -> Cmd1_ConcreteVolume
      // 1. Push Button 과 Command 연결
      PushButtonData data_conVolume = new PushButtonData(
          "ConcreteVolume", "Concrete\nVolume", path,
          "NoahDesign.Cmd1_ConcreteVolume.CmdConcreteUtil" );
      BitmapSource conVolume = GetEmbededImage(
          "NoahDesign.Folder_Image.cube32.png" );

      data_conVolume.LargeImage = conVolume;
      data_conVolume.ToolTip =
          "インプレイス一般モデルの中に含まれる部材の体積を求めます。"
          + "\n実行する前に一般モデルを作成してください。";

      // 2. 버튼 데이터 추가
      RibbonItem formwork = panel_GeometryPlus.AddItem( data_conVolume );
      #endregion
      panel_GeometryPlus.AddSeparator();
      #region Geometry+ -> Cmd2_ConcreteFormwork
      // 1. Push Button 과 Command 연결
      PushButtonData data_conForm = new PushButtonData(
          "Formwork", "Formwork\nMaker", path,
          "NoahDesign.Cmd2_ConcreteFormwork.CmdConcreteFormwork" );
      BitmapSource conForm = GetEmbededImage(
          "NoahDesign.Folder_Image.cube32.png" );

      data_conForm.LargeImage = conForm;
      data_conForm.ToolTip =
          "インプレイス一般モデルの中に含まれる部材の体積を求めます。"
          + "\n実行する前に一般モデルを作成してください。";

      // 2. 버튼 데이터 추가
      RibbonItem itemConForm = panel_GeometryPlus.AddItem( data_conForm );
      #endregion


      // Drawing+
      #region Drawing+ -> Cmd3_FloorTagControl
      // 1. Push Button 과 Command 연결
      PushButtonData data_floortag = new PushButtonData(
          "FloorTagCMD", "Automatic\nFloor Tag", path,
          "NoahDesign.Cmd3_FloorTagControl.CmdFloorTagControl" );
      BitmapSource testLogo = GetEmbededImage(
          "NoahDesign.Folder_Image.floorTag32.png" );

      data_floortag.LargeImage = testLogo;
      data_floortag.ToolTip = "TEST";

      // 2. 버튼 데이터 추가
      RibbonItem itemTest = panel_DrawingPlus.AddItem( data_floortag );
      #endregion


      // Automatic Metal Framing
      #region Automatic Metal Framing -> Cmd4_Test
      // 1. Push Button 과 Command 연결
      PushButtonData data_cmd4test = new PushButtonData(
          "TestCMD1", "Split Wall\nBy Grids", path,
          "NoahDesign.Cmd4_SplitWall.Cmd_SplitWall" );
      BitmapSource logo4 = GetEmbededImage(
          "NoahDesign.Folder_Image.split32.png" );

      data_cmd4test.LargeImage = logo4;
      data_cmd4test.ToolTip = "TEST";

      // 2. 버튼 데이터 추가
      RibbonItem itemTest1 = panel_MetalFraming.AddItem( data_cmd4test );
      #endregion
      panel_MetalFraming.AddSeparator();
      #region Automatic Metal Framing -> Cmd5_MetalFraming
      // 1. Push Button 과 Command 연결
      PushButtonData data_cmd5test = new PushButtonData(
          "TestCMD2", "Automatic\nMetal stud", path,
          "NoahDesign.Cmd5_MetalFraming.Cmd_MetalFraming" );
      BitmapSource logo5 = GetEmbededImage(
          "NoahDesign.Folder_Image.stud32.png" );

      data_cmd5test.LargeImage = logo5;
      data_cmd5test.ToolTip = "TEST";

      // 2. 버튼 데이터 추가
      RibbonItem itemTest2 = panel_MetalFraming.AddItem( data_cmd5test );
      #endregion


      // Debug Commands
      


      // Info
      #region VersionInfo -> ShowVersionInfo
      // 1. Push Button 과 Command 연결
      PushButtonData data_Version = new PushButtonData(
          "vesionInfo", "バージョン\n情報", path,
          "NoahDesign.Folder_Command.ShowVersionInfo" );
      BitmapSource noahLoge = GetEmbededImage(
          "NoahDesign.Folder_Image.Noah32.png" );

      data_Version.LargeImage = noahLoge;
      data_Version.ToolTip = "アドインバージョン情報を開きます。";

      // 2. 버튼 데이터 추가
      RibbonItem versionInfo = panel_Info.AddItem( data_Version );
      #endregion
      panel_Info.AddSeparator();

    }

    public static BitmapSource GetEmbededImage( string name )
    {
      try
      {
        Assembly a = Assembly.GetExecutingAssembly();
        Stream s = a.GetManifestResourceStream( name );
        return BitmapFrame.Create( s );
      }
      catch
      {
        return null;
      }
    }
  }
}
