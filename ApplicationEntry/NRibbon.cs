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
      string tabName = "Noah Design";
      UICrdApp.CreateRibbonTab( tabName );


      // Ribbon Panel 작성
      RibbonPanel panel_GeometryPlus = UICrdApp.CreateRibbonPanel( tabName, "Geometry+" );
      //RibbonPanel panel_DrawingPlus = UICrdApp.CreateRibbonPanel( tabName, "Drawing+" );
      RibbonPanel panel_Info = UICrdApp.CreateRibbonPanel( tabName, "Info" );


      #region Geometry+ -> CmdConcreteUtil
      // 1. Push Button 과 Command 연결
      PushButtonData data_conVolume = new PushButtonData(
          "ConcreteVolume", "RC\n体積計算", path,
          "NoahDesign.Folder_Command.CmdConcreteUtil" );
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

      #region Geometry+ -> CmdConcreteFormwork
      // 1. Push Button 과 Command 연결
      PushButtonData data_conForm = new PushButtonData(
          "Formwork", "RC\n型枠計算", path,
          "NoahDesign.Folder_Command.CmdConcreteFormwork" );
      BitmapSource conForm = GetEmbededImage(
          "NoahDesign.Folder_Image.cube32.png" );

      data_conForm.LargeImage = conForm;
      data_conForm.ToolTip =
          "インプレイス一般モデルの中に含まれる部材の体積を求めます。"
          + "\n実行する前に一般モデルを作成してください。";

      // 2. 버튼 데이터 추가
      RibbonItem itemConForm = panel_GeometryPlus.AddItem( data_conForm );
      #endregion


      #region VersionInfo -> CmdTest
      // 1. Push Button 과 Command 연결
      PushButtonData data_Test = new PushButtonData(
          "TestCMD", "テスト\n実行", path,
          "NoahDesign.GeoPuls_CmdTest.CmdTest" );
      BitmapSource testLogo = GetEmbededImage(
          "NoahDesign.Folder_Image.test32.png" );

      data_Test.LargeImage = testLogo;
      data_Test.ToolTip = "TEST";

      // 2. 버튼 데이터 추가
      RibbonItem itemTest = panel_Info.AddItem( data_Test );
      #endregion
      panel_Info.AddSeparator();

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
