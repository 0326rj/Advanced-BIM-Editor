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
  // *************** Command Name Constant Rule ******************
  // const string CmdName_CmdName = "command name";
  // const string CmdClass_CmdName = "command path";
  // const string ToolTip_ConcreteVolue = "tootip content";
  //
  // *************** Image Name Constant Rule ********************
  // const string ImgName_CmdName_16 = "image paht";
  // const string ImgName_CmdName_32 = "image paht";
  // *************************************************************

  public class MyRibbon
  {

    #region Fields ( ClassName, ClassPath, ImagePath )

    ///////////////////////////////////////////////////////////////
    ///////////////// 1. Namespace             ////////////////////
    ///////////////// 2. Main Add-In Tab Name  ////////////////////
    ///////////////////////////////////////////////////////////////

    private const string TabName = "Micro REVIT by Jaebum Kim";
    private const string NameSpace = "NoahDesign";


    ///////////////////////////////////////////////////////////////
    ////////// Panel No. 01                           /////////////
    ////////// Ribbon Panels & Cmd Names & Image Path /////////////
    ///////////////////////////////////////////////////////////////

    // Cmd Concrete Volume
    private const string CmdName_ConcreteVolume = "コンクリート\nボリューム";
    private const string CmdClass_ConcreteVolume = NameSpace + ".Cmd1_ConcreteVolume.CmdConcreteUtil";
    private const string ImgName_ConcreteVolume_32 = NameSpace + ".Folder_Image.cube32.png";
    private const string ToolTip_ConcreteVolume = "一般モデル範囲内の体積(m3)を得られます。";

    // Cmd Concrete Formwork
    private const string CmdName_ConcreteFormwork = "コンクリート\n型枠モデル";
    private const string CmdClass_ConcreteFormwork = NameSpace + ".Cmd2_ConcreteFormwork.CmdConcreteFormwork";
    private const string ImgName_ConcreteFormwork_32 = NameSpace + ".Folder_Image.cube32.png";
    private const string ToolTip_ConcreteFormwork = "一般モデル範囲の内で型枠モデルを作成します。";

    // Cmd Geometry Join, Unjoin
    private const string CmdName_JoinGeometry = "ジオメトリ一括結合 & 解除";
    private const string CmdClass_JoinGeometry = NameSpace + ".Cmd2_JoinGeometry.CmdJoinGeometry";
    private const string ImgName_JoinGeometry_16 = NameSpace + ".Folder_Image.join16.png";
    private const string ToolTip_JoinGeometry = "部材結合を一括実行します。";

    // Cmd Debug Command 12
    private const string CmdName_DebugCmd14 = "Debug Command 14";
    private const string CmdClass_DebugCmd14 = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_DebugCmd14_16 = NameSpace + ".Folder_Image.test16.png";
    private const string ToolTip_DebugCmd14 = "/////////////////////////////////////////";

    // Cmd Debug Command 13
    private const string CmdName_DebugCmd15 = "Debug Command 15";
    private const string CmdClass_DebugCmd15 = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_DebugCmd15_16 = NameSpace + ".Folder_Image.test16.png";
    private const string ToolTip_DebugCmd15 = "/////////////////////////////////////////";


    ///////////////////////////////////////////////////////////////
    ////////// Panel No. 02                           /////////////
    ////////// Ribbon Panels & Cmd Names & Image Path /////////////
    ///////////////////////////////////////////////////////////////

    // Cmd Automatic Floor Tag
    private const string CmdName_FloorTag = "床タグ\nタイプ判定";
    private const string CmdClass_FloorTag = NameSpace + ".Cmd3_FloorTagControl.CmdFloorTagControl";
    private const string ImgName_FloorTag_32 = NameSpace + ".Folder_Image.floorTag32.png";
    private const string ToolTip_FloorTag = "選択した壁から床タグのタイプを自動判定し配置します。";

    // Cmd Floor Level Filter Creater
    private const string CmdName_FloorLevelFilter = "床レベル図\nフィルタ作成";
    private const string CmdClass_FloorLevelFilter = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_FloorLevelFilter_32 = NameSpace + ".Folder_Image.floorFilter32.png";
    private const string ToolTip_FloorLevelFilter = "レベル図の用色分けフィルタを作成します。";


    ///////////////////////////////////////////////////////////////
    ////////// Panel No. 03                           /////////////
    ////////// Ribbon Panels & Cmd Names & Image Path /////////////
    ///////////////////////////////////////////////////////////////

    // Cmd Split Wall by Grid
    private const string CmdName_SplitWallByGrid = "通り芯で壁分割";
    private const string CmdClass_SplitWallByGrid = NameSpace + ".Cmd4_SplitWall.Cmd_SplitWall";
    private const string ImgName_SplitWallByGrid_16 = NameSpace + ".Folder_Image.split16.png";
    private const string ToolTip_SplitWallByGrid = "通り心を基準に壁を切断します。";

    // Cmd Split Wall by Void
    private const string CmdName_SplitWallByVoid = "開口で壁分割";
    private const string CmdClass_SplitWallByVoid = NameSpace + ".Cmd4_SplitWall.Cmd_SplitWallByVoid";
    private const string ImgName_SplitWallByVoid_16 = NameSpace + ".Folder_Image.split16.png";
    private const string ToolTip_SplitWallByVoid = "開口部を基準に壁を切断します。";

    // Cmd Sub command
    private const string CmdName_SubCmd = "Sub Command";
    private const string CmdClass_SubCmd = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_SubCmd_16 = NameSpace + ".Folder_Image.test16.png";
    private const string ToolTip_SubCmd = "/////////////////////////////////////////";

    // Cmd LGS Metal Framing
    private const string CmdName_MetalFraming = "LGSフレーム\nマネージャ";
    private const string CmdClass_MetalFraming = NameSpace + ".Cmd5_MetalFraming.Cmd_MetalFraming";
    private const string ImgName_MetalFraming_32 = NameSpace + ".Folder_Image.stud32.png";
    private const string ToolTip_MetalFraming = "選択した壁にLGSスタッドを自動配置します。";


    ///////////////////////////////////////////////////////////////
    ////////// Panel No. 04                           /////////////
    ////////// Ribbon Panels & Cmd Names & Image Path /////////////
    ///////////////////////////////////////////////////////////////

    // Cmd Dynamic Filter
    private const string CmdName_DynamicFilter = "ダイナミック\nフィルタ";
    private const string CmdClass_DynamicFilter = NameSpace + ".Cmd0_FormTest.CmdFormTest";
    private const string ImgName_DynamicFilter = NameSpace + ".Folder_Image.dynamicFilter32.png";
    private const string ToolTip_DynamicFilter = "さらに詳細な情報が表示できる協力なフィルタツールです。";

    // Cmd Debug Command 2
    private const string CmdName_DebugCmd2 = "Debug\nCommmand2";
    private const string CmdClass_DebugCmd2 = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_DebugCmd2_16 = NameSpace + ".Folder_Image.test16.png";
    private const string ImgName_DebugCmd2_32 = NameSpace + ".Folder_Image.test32_3.png";
    private const string ToolTip_DebugCmd2 = "/////////////////////////////////////////";

    // Cmd Debug Command 3
    private const string CmdName_DebugCmd3 = "Debug\nCommmand3";
    private const string CmdClass_DebugCmd3 = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_DebugCmd3_16 = NameSpace + ".Folder_Image.test16.png";
    private const string ImgName_DebugCmd3_32 = NameSpace + ".Folder_Image.test32_3.png";
    private const string ToolTip_DebugCmd3 = "/////////////////////////////////////////";

    // Cmd Debug Command 4
    private const string CmdName_DebugCmd4 = "Debug\nCommmand4";
    private const string CmdClass_DebugCmd4 = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_DebugCmd4_16 = NameSpace + ".Folder_Image.test16.png";
    private const string ImgName_DebugCmd4_32 = NameSpace + ".Folder_Image.test32_3.png";
    private const string ToolTip_DebugCmd4 = "/////////////////////////////////////////";

    // Cmd Debug Command 5
    private const string CmdName_DebugCmd5 = "Debug\nCommmand5";
    private const string CmdClass_DebugCmd5 = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_DebugCmd5_16 = NameSpace + ".Folder_Image.test16.png";
    private const string ImgName_DebugCmd5_32 = NameSpace + ".Folder_Image.test32_3.png";
    private const string ToolTip_DebugCmd5 = "/////////////////////////////////////////";


    ///////////////////////////////////////////////////////////////
    ////////// Panel No. 05                           /////////////
    ////////// Ribbon Panels & Cmd Names & Image Path /////////////
    ///////////////////////////////////////////////////////////////

    // Cmd Debug Command 6
    private const string CmdName_DebugCmd6 = "Debug\nCommmand6";
    private const string CmdClass_DebugCmd6 = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_DebugCmd6_16 = NameSpace + ".Folder_Image.test16.png";
    private const string ImgName_DebugCmd6_32 = NameSpace + ".Folder_Image.test32_3.png";
    private const string ToolTip_DebugCmd6 = "/////////////////////////////////////////";

    // Cmd Debug Command 7
    private const string CmdName_DebugCmd7 = "Debug\nCommmand7";
    private const string CmdClass_DebugCmd7 = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_DebugCmd7_16 = NameSpace + ".Folder_Image.test16.png";
    private const string ImgName_DebugCmd7_32 = NameSpace + ".Folder_Image.test32_3.png";
    private const string ToolTip_DebugCmd7 = "/////////////////////////////////////////";

    // Cmd Debug Command 8
    private const string CmdName_DebugCmd8 = "Debug\nCommmand8";
    private const string CmdClass_DebugCmd8 = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_DebugCmd8_16 = NameSpace + ".Folder_Image.test16.png";
    private const string ImgName_DebugCmd8_32 = NameSpace + ".Folder_Image.test32_3.png";
    private const string ToolTip_DebugCmd8 = "/////////////////////////////////////////";

    // Cmd Debug Command 9
    private const string CmdName_DebugCmd9 = "Debug\nCommmand9";
    private const string CmdClass_DebugCmd9 = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_DebugCmd9_16 = NameSpace + ".Folder_Image.test16.png";
    private const string ImgName_DebugCmd9_32 = NameSpace + ".Folder_Image.test32_3.png";
    private const string ToolTip_DebugCmd9 = "/////////////////////////////////////////";

    // Cmd Debug Command 10
    private const string CmdName_DebugCmd10 = "Debug\nCommmand10";
    private const string CmdClass_DebugCmd10 = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_DebugCmd10_16 = NameSpace + ".Folder_Image.test16.png";
    private const string ImgName_DebugCmd10_32 = NameSpace + ".Folder_Image.test32_3.png";
    private const string ToolTip_DebugCmd10 = "/////////////////////////////////////////";

    // Cmd Debug Command 11
    private const string CmdName_DebugCmd11 = "Debug\nCommmand11";
    private const string CmdClass_DebugCmd11 = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_DebugCmd11_16 = NameSpace + ".Folder_Image.test16.png";
    private const string ImgName_DebugCmd11_32 = NameSpace + ".Folder_Image.test32_3.png";
    private const string ToolTip_DebugCmd11 = "/////////////////////////////////////////";

    // Cmd Debug Command 12
    private const string CmdName_DebugCmd12 = "Debug\nCommmand12";
    private const string CmdClass_DebugCmd12 = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_DebugCmd12_16 = NameSpace + ".Folder_Image.test16.png";
    private const string ImgName_DebugCmd12_32 = NameSpace + ".Folder_Image.test32_3.png";
    private const string ToolTip_DebugCmd12 = "/////////////////////////////////////////";

    // Cmd Debug Command 13
    private const string CmdName_DebugCmd13 = "Debug\nCommmand13";
    private const string CmdClass_DebugCmd13 = NameSpace + ".Folder_Command.Cmd_Null";
    private const string ImgName_DebugCmd13_16 = NameSpace + ".Folder_Image.test16.png";
    private const string ImgName_DebugCmd13_32 = NameSpace + ".Folder_Image.test32_3.png";
    private const string ToolTip_DebugCmd13 = "/////////////////////////////////////////";


    ///////////////////////////////////////////////////////////////
    ////////// Panel No. 06                           /////////////
    ////////// Ribbon Panels & Cmd Names & Image Path /////////////
    ///////////////////////////////////////////////////////////////

    private const string CmdName_VersionInfo = "バージョン情報";
    private const string CmdClass_VersionInfo = NameSpace + ".Folder_Command.ShowVersionInfo";
    private const string ImgName_VersionInfo_32 = NameSpace + ".Folder_Image.Noah32.png";
    private const string ToolTip_VersionInfo = "バージョン情報を表示します。";

    #endregion

    public static void AddRibbonPanel_VersionInfo( UIControlledApplication UICrdApp )
    {
      UICrdApp.CreateRibbonTab( TabName );
      string path = Assembly.GetExecutingAssembly().Location;    
         
      // Panel No.01
      RibbonPanel panel_GeometryPlus = UICrdApp.CreateRibbonPanel( TabName, "Geometry+" );
      // Panel No.02
      RibbonPanel panel_DrawingPlus = UICrdApp.CreateRibbonPanel( TabName, "Drawing+" );
      // Panel No.03
      RibbonPanel panel_MetalFraming = UICrdApp.CreateRibbonPanel( TabName, "Automatic Metal Framing Utiliy" );
      // Panel No.04
      RibbonPanel panel_debug = UICrdApp.CreateRibbonPanel( TabName, "Debug Commands 1" );
      // Panel No.05
      RibbonPanel panel_debug2 = UICrdApp.CreateRibbonPanel( TabName, "Debug Commands 2" );
      // Panel No.06
      RibbonPanel panel_Info = UICrdApp.CreateRibbonPanel( TabName, "Information" );


      // Geometry+
      #region Geometry+ -> Cmd1_ConcreteVolume


      RibbonItem item_ConcreteVolume = MyButtonControl.Add_PushButton(
        panel_GeometryPlus,
        "Concrete Volume",
        CmdName_ConcreteVolume,
        path,
        CmdClass_ConcreteVolume,
        ImgName_ConcreteVolume_32,
        true,
        ToolTip_ConcreteVolume );


      #endregion
      #region Geometry+ -> Cmd2_ConcreteFormwork

      RibbonItem item_ConcreteFromwork = MyButtonControl.Add_PushButton(
        panel_GeometryPlus,
        "Concrete Formwork",
        CmdName_ConcreteFormwork,
        path,
        CmdClass_ConcreteFormwork,
        ImgName_ConcreteFormwork_32,
        true,
        ToolTip_ConcreteFormwork );

      #endregion
      panel_GeometryPlus.AddSeparator();
      #region Geometry+ -> stackedButton (1)ジオメトリ一括結合/解除 (2)未定 (3)未定

      var stackedButtonData_joinGeom = MyButtonControl.Create_PushButtonData(
        "stacked1",
        CmdName_JoinGeometry,
        path,
        CmdClass_JoinGeometry,
        ImgName_JoinGeometry_16,
        false,
        ToolTip_JoinGeometry );

      var stackedbuttonNull1 = MyButtonControl.Create_PushButtonData(
        "stacked2",
        CmdName_DebugCmd14,
        path,
        CmdClass_DebugCmd14,
        ImgName_DebugCmd14_16,
        false,
        ToolTip_DebugCmd14 );

      var stackedbuttonNull2 = MyButtonControl.Create_PushButtonData(
        "stacked3",
        CmdName_DebugCmd15,
        path,
        CmdClass_DebugCmd15,
        ImgName_DebugCmd15_16,
        false,
        ToolTip_DebugCmd15 );

      List<PushButtonData> pushButtonDataGeometryPlus = new List<PushButtonData>();
      pushButtonDataGeometryPlus.Add( stackedButtonData_joinGeom );
      pushButtonDataGeometryPlus.Add( stackedbuttonNull1 );
      pushButtonDataGeometryPlus.Add( stackedbuttonNull2 );
      MyButtonControl.Add_RibbonStackItems_ToRibbonPanel( panel_GeometryPlus, pushButtonDataGeometryPlus );


      #endregion


      // Drawing+
      #region Drawing+ -> Cmd3_FloorTagControl


      RibbonItem item_AutoFloorTag = MyButtonControl.Add_PushButton(
        panel_DrawingPlus,
        "Auto Floor Tag",
        CmdName_FloorTag,
        path,
        CmdClass_FloorTag,
        ImgName_FloorTag_32,
        true,
        ToolTip_FloorTag );


      #endregion
      #region Drawing + ->Cmd_FloorColorFilter


      RibbonItem item_AutoFloorColorFilter = MyButtonControl.Add_PushButton(
       panel_DrawingPlus,
       "Floor Color Filter",
       CmdName_FloorLevelFilter,
       path,
       CmdClass_FloorLevelFilter,
       ImgName_FloorLevelFilter_32,
       true,
       ToolTip_FloorLevelFilter );


      #endregion


      // Automatic Metal Framing
      #region Automatic Metal Framing -> Cmd_SplitWall


      var item_splitWall = MyButtonControl.Create_PushButtonData(
        "Split Wall By Grids",
        CmdName_SplitWallByGrid,
        path,
        CmdClass_SplitWallByGrid,
        ImgName_SplitWallByGrid_16,
        false,
        ToolTip_SplitWallByGrid );

      var item_splitWall_by_void = MyButtonControl.Create_PushButtonData(
        "Split Wall By Void",
        CmdName_SplitWallByVoid,
        path,
        CmdClass_SplitWallByVoid,
        ImgName_SplitWallByVoid_16,
        false,
        ToolTip_SplitWallByVoid );

      var item_splitWall_Sub2 = MyButtonControl.Create_PushButtonData(
       "Split Wall Sub 2",
       CmdName_SubCmd,
       path,
       CmdClass_SubCmd,
       ImgName_SubCmd_16,
       false,
       ToolTip_SubCmd );

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
        CmdName_MetalFraming,
        path,
        CmdClass_MetalFraming,
        ImgName_MetalFraming_32,
        true,
        ToolTip_MetalFraming );


      #endregion


      // Debug Commands 1
      #region Debug Commands -> Dynamic Filter

      MyButtonControl.Add_PushButton(
        panel_debug,
        "debug1",
        CmdName_DynamicFilter,
        path,
        CmdClass_DynamicFilter,
        ImgName_DynamicFilter,
        true,
        ToolTip_DynamicFilter );

      #endregion

      #region Debug Commands -> Debug Commmand 2


      MyButtonControl.Add_PushButton(
        panel_debug,
        "debug2",
        CmdName_DebugCmd2,
        path,
        CmdClass_DebugCmd2,
        ImgName_DebugCmd2_32,
        true,
        ToolTip_DebugCmd2 );

      #endregion
      #region Debug Commands -> Debug Commmand 3

      MyButtonControl.Add_PushButton(
        panel_debug,
        "debug3",
        CmdName_DebugCmd3,
        path,
        CmdClass_DebugCmd3,
        ImgName_DebugCmd3_32,
        true,
        ToolTip_DebugCmd3 );

      #endregion
      #region Debug Commands -> Debug Commmand 4


      MyButtonControl.Add_PushButton(
        panel_debug,
        "debug4",
        CmdName_DebugCmd4,
        path,
        CmdClass_DebugCmd4,
        ImgName_DebugCmd4_32,
        true,
        ToolTip_DebugCmd4 );


      #endregion
      #region Debug Commands -> Debug Commmand 5


      MyButtonControl.Add_PushButton(
        panel_debug,
        "debug5",
        CmdName_DebugCmd5,
        path,
        CmdClass_DebugCmd5,
        ImgName_DebugCmd5_32,
        true,
        ToolTip_DebugCmd5 );


      #endregion


      // Debug Commands 2
      #region Debug Commands -> Debug Commmand 6 ~ 10


      var splitData1 = MyButtonControl.Create_PushButtonData(
        "split1",
        CmdName_DebugCmd6,
        path,
        CmdClass_DebugCmd6,
        ImgName_DebugCmd6_32,
        true,
        ToolTip_DebugCmd6 );

      var splitData2 = MyButtonControl.Create_PushButtonData(
        "split2",
        CmdName_DebugCmd7,
        path,
        CmdClass_DebugCmd7,
        ImgName_DebugCmd7_32,
        true,
        ToolTip_DebugCmd7 );

      var splitData3 = MyButtonControl.Create_PushButtonData(
        "split3",
        CmdName_DebugCmd8,
        path,
        CmdClass_DebugCmd8,
        ImgName_DebugCmd8_32,
        true,
        ToolTip_DebugCmd8 );

      var splitData4 = MyButtonControl.Create_PushButtonData(
        "split4",
        CmdName_DebugCmd9,
        path,
        CmdClass_DebugCmd9,
        ImgName_DebugCmd9_32,
        true,
        ToolTip_DebugCmd9 );

      var splitData5 = MyButtonControl.Create_PushButtonData(
        "split5",
        CmdName_DebugCmd10,
        path,
        CmdClass_DebugCmd10,
        ImgName_DebugCmd10_32,
        true,
        ToolTip_DebugCmd10 );

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
        CmdName_DebugCmd11,
        path,
        CmdClass_DebugCmd11,
        ImgName_DebugCmd11_16,
        false,
        ToolTip_DebugCmd11 );

      var stackedButtonData2 = MyButtonControl.Create_PushButtonData(
        "stacked2",
        CmdName_DebugCmd12,
        path,
        CmdClass_DebugCmd12,
        ImgName_DebugCmd12_16,
        false,
        ToolTip_DebugCmd12 );

      var stackedButtonData3 = MyButtonControl.Create_PushButtonData(
        "stacked3",
        CmdName_DebugCmd13,
        path,
        CmdClass_DebugCmd13,
        ImgName_DebugCmd13_16,
        false,
        ToolTip_DebugCmd13 );

      List<PushButtonData> pushButtonDatas = new List<PushButtonData>();
      pushButtonDatas.Add( stackedButtonData1 );
      pushButtonDatas.Add( stackedButtonData2 );
      pushButtonDatas.Add( stackedButtonData3 );
      MyButtonControl.Add_RibbonStackItems_ToRibbonPanel( panel_debug2, pushButtonDatas );
      #endregion


      // Information
      #region VersionInfo -> ShowVersionInfo

      MyButtonControl.Add_PushButton(
       panel_Info,
       "VersionInfo",
       CmdName_VersionInfo,
       path,
       CmdClass_VersionInfo,
       ImgName_VersionInfo_32,
       true,
       ToolTip_VersionInfo );

      #endregion
    }
  }
}