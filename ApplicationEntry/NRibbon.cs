#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NoahDesign.Folder_RibbonData;
#endregion

namespace NoahDesign
{
  public class NRibbon
  {
    public static void AddRibbonPanel_VersionInfo( UIControlledApplication UICrdApp )
    {

      // dll assembly path
      string tabName = "JAEBUM KIM >REVIT+";
      string path = Assembly.GetExecutingAssembly().Location;    
      UICrdApp.CreateRibbonTab( tabName );


      // Ribbon Panels
      RibbonPanel panel_GeometryPlus = UICrdApp.CreateRibbonPanel( tabName, "Geometry+" );
      RibbonPanel panel_DrawingPlus = UICrdApp.CreateRibbonPanel( tabName, "Drawing+" );
      RibbonPanel panel_MetalFraming = UICrdApp.CreateRibbonPanel( tabName, "Automatic Metal Framing" );
      RibbonPanel panel_debug = UICrdApp.CreateRibbonPanel( tabName, "Debug Commands 1" );
      RibbonPanel panel_debug2 = UICrdApp.CreateRibbonPanel( tabName, "Debug Commands 2" );
      RibbonPanel panel_Info = UICrdApp.CreateRibbonPanel( tabName, "Infomation" );


      // Geometry+
      #region Geometry+ -> Cmd1_ConcreteVolume


      RibbonItem item_ConcreteVolume = MyButtonControl.Add_PushButton(
        panel_GeometryPlus,
        "Concrete Volume",
        "Concrete\nVolume",
        path,
        "NoahDesign.Cmd1_ConcreteVolume.CmdConcreteUtil",
        "NoahDesign.Folder_Image.cube32.png",
        true,
        "インプレイス一般モデルの中に含まれる部材の体積を求めます。"
          + "\n実行する前に一般モデルを作成してください。" );


      #endregion
      panel_GeometryPlus.AddSeparator();
      #region Geometry+ -> Cmd2_ConcreteFormwork


      RibbonItem item_ConcreteFromwork = MyButtonControl.Add_PushButton(
        panel_GeometryPlus,
        "Concrete Formwork",
        "Formwork\nMaker",
        path,
        "NoahDesign.Cmd2_ConcreteFormwork.CmdConcreteFormwork",
        "NoahDesign.Folder_Image.cube32.png",
        true,
        "インプレイス一般モデルの中に含まれる部材の体積を求めます。"
          + "\n実行する前に一般モデルを作成してください。" );


      #endregion


      // Drawing+
      #region Drawing+ -> Cmd3_FloorTagControl


      RibbonItem item_AutoFloorTag = MyButtonControl.Add_PushButton(
        panel_DrawingPlus,
        "Auto Floor Tag",
        "Automatic\nFloor Tag",
        path,
        "NoahDesign.Cmd3_FloorTagControl.CmdFloorTagControl",
        "NoahDesign.Folder_Image.floorTag32.png",
        true,
        "選択した壁から床タグのタイプを自動判定し配置します。" );


      #endregion
      #region Drawing + ->Cmd_FloorColorFilter


      RibbonItem item_AutoFloorColorFilter = MyButtonControl.Add_PushButton(
       panel_DrawingPlus,
       "Floor Color Filter",
       "Floor\nFilter",
       path,
       "NoahDesign.Folder_Command.Cmd_Null",
       "NoahDesign.Folder_Image.floorFilter32.png",
       true );


      #endregion

      // Automatic Metal Framing
      #region Automatic Metal Framing -> Cmd_SplitWall


      var item_splitWall = MyButtonControl.Create_PushButtonData(
        "Split Wall By Grids",
        "Split Wall By Grids",
        path,
        "NoahDesign.Cmd4_SplitWall.Cmd_SplitWall",
        "NoahDesign.Folder_Image.split16.png",
        false,
        "通り心を基準に壁を切断できます" );

      var item_splitWall_by_void = MyButtonControl.Create_PushButtonData(
        "Split Wall By Void",
        "Split Wall By Void",
        path,
        "NoahDesign.Cmd4_SplitWall.Cmd_SplitWallByVoid",
        "NoahDesign.Folder_Image.split16.png",
        false,
        "Sub1" );

      var item_splitWall_Sub2 = MyButtonControl.Create_PushButtonData(
       "Split Wall Sub 2",
       "Sub Command 2",
       path,
       "NoahDesign.Cmd4_SplitWall.Cmd_SplitWall",
       "NoahDesign.Folder_Image.test16.png",
       false,
       "Sub2" );

      List<PushButtonData> splitWallCommands = new List<PushButtonData>();
      splitWallCommands.Add( item_splitWall );
      splitWallCommands.Add( item_splitWall_by_void );
      splitWallCommands.Add( item_splitWall_Sub2 );
      MyButtonControl.Add_RibbonStackItems_ToRibbonPanel( panel_MetalFraming, splitWallCommands );


      #endregion
      panel_MetalFraming.AddSeparator();
      #region Automatic Metal Framing -> Cmd5_MetalFraming


      RibbonItem item_Stud = MyButtonControl.Add_PushButton(
        panel_MetalFraming,
        "Stud",
        "Auto LGS\nstud",
        path,
        "NoahDesign.Cmd5_MetalFraming.Cmd_MetalFraming",
        "NoahDesign.Folder_Image.stud32.png",
        true,
        "選択した壁にLGSスタッドを自動配置します。" );


      #endregion
      #region Automatic Metal Framing -> RunnerManager


      RibbonItem item_Runner = MyButtonControl.Add_PushButton(
       panel_MetalFraming,
       "Runner",
       "Auto LGS\nRunner",
       path,
       "NoahDesign.Cmd5_MetalFraming.Cmd_MetalFraming",
       "NoahDesign.Folder_Image.stud32.png",
       true,
       "選択した壁にLGSランナーを自動配置します。" );


      #endregion
      #region Automatic Metal Framing -> SteadyBraceManager


      RibbonItem item_SteadyBrace = MyButtonControl.Add_PushButton(
       panel_MetalFraming,
       "Steady Brace",
       "Auto LGS\nBrace",
       path,
       "NoahDesign.Cmd5_MetalFraming.Cmd_RunnerManager",
       "NoahDesign.Folder_Image.stud32.png",
       true,
       "選択した壁にLGS振れ止めを自動配置します。" ); 


      #endregion


      // Debug Commands 1
      string className_Debug = "NoahDesign.Folder_Command.Cmd_Null";
      string imageName32_Debug = "NoahDesign.Folder_Image.test32_3.png";
      string imageName16_Debug = "NoahDesign.Folder_Image.test16.png";
      string toolTip_Debug = "This is for debug";

      #region Debug Commands -> Debug Commmand 1

      MyButtonControl.Add_PushButton(
        panel_debug,
        "debug1",
        "Debug\nCommand 1",
        path,
        "NoahDesign.Cmd0_FormTest.CmdFormTest",
        imageName32_Debug,
        true,
        toolTip_Debug );

      #endregion

      #region Debug Commands -> Debug Commmand 2


      MyButtonControl.Add_PushButton(
        panel_debug,
        "debug2",
        "Debug\nCommand 2",
        path,
        className_Debug,
        imageName32_Debug,
        true,
        toolTip_Debug );

      #endregion

      #region Debug Commands -> Debug Commmand 3

      MyButtonControl.Add_PushButton(
        panel_debug,
        "debug3",
        "Debug\nCommand 3",
        path,
        className_Debug,
        imageName32_Debug,
        true,
        toolTip_Debug );

      #endregion

      #region Debug Commands -> Debug Commmand 4


      MyButtonControl.Add_PushButton(
        panel_debug,
        "debug4",
        "Debug\nCommand 4",
        path,
        className_Debug,
        imageName32_Debug,
        true,
        toolTip_Debug );


      #endregion

      #region Debug Commands -> Debug Commmand 5


      MyButtonControl.Add_PushButton(
        panel_debug,
        "debug5",
        "Debug\nCommand 5",
        path,
        className_Debug,
        imageName32_Debug,
        true,
        toolTip_Debug );


      #endregion

      // Debug Commands 2
      #region Debug Commands -> Debug Commmand 6 ~ 10


      var splitData1 = MyButtonControl.Create_PushButtonData(
        "split1",
        "Debug Command 6",
        path, className_Debug, imageName32_Debug, true, toolTip_Debug );

      var splitData2 = MyButtonControl.Create_PushButtonData(
        "split2",
      "Debug Command 7",
      path, className_Debug, imageName32_Debug, true, toolTip_Debug );

      var splitData3 = MyButtonControl.Create_PushButtonData(
      "split3",
      "Debug Command 8",
      path, className_Debug, imageName32_Debug, true, toolTip_Debug );

      var splitData4 = MyButtonControl.Create_PushButtonData(
       "split4",
       "Debug Command 9",
       path, className_Debug, imageName32_Debug, true, toolTip_Debug );

      var splitData5 = MyButtonControl.Create_PushButtonData(
       "split5",
       "Debug Command 10",
       path, className_Debug, imageName32_Debug, true, toolTip_Debug );

      List<PushButtonData> datas = new List<PushButtonData>();
      datas.Add( splitData1 );
      datas.Add( splitData2 );
      datas.Add( splitData3 );
      datas.Add( splitData4 );
      datas.Add( splitData5 );
      MyButtonControl.Add_SplitButton( panel_debug2, "Split Test", "Test", datas );


      #endregion

      #region Debug Commands -> Debug Commmand 11 ~ 13

      var stackedButtonData1 = MyButtonControl.Create_PushButtonData(
        "stacked1",
        "Debug Command 11",
        path,
        className_Debug,
        imageName16_Debug,
        false,
        "Tool Tip" );

      var stackedButtonData2 = MyButtonControl.Create_PushButtonData(
        "stacked2",
        "Debug Command 12",
        path,
        className_Debug,
        imageName16_Debug,
        false,
        "Tool Tip" );

      var stackedButtonData3 = MyButtonControl.Create_PushButtonData(
        "stacked3",
        "Debug Command 13",
        path,
        className_Debug,
        imageName16_Debug,
        false,
        "Tool Tip" );

      List<PushButtonData> pushButtonDatas = new List<PushButtonData>();
      pushButtonDatas.Add( stackedButtonData1 );
      pushButtonDatas.Add( stackedButtonData2 );
      pushButtonDatas.Add( stackedButtonData3 );
      MyButtonControl.Add_RibbonStackItems_ToRibbonPanel( panel_debug2, pushButtonDatas );
      #endregion



      // Info
      #region VersionInfo -> ShowVersionInfo

      MyButtonControl.Add_PushButton(
       panel_Info,
       "VersionInfo",
       "Add-In Version",
       path,
       "NoahDesign.Folder_Command.ShowVersionInfo",
       "NoahDesign.Folder_Image.Noah32.png",
       true,
       "Version Information" );

      #endregion

    }
  }
}
