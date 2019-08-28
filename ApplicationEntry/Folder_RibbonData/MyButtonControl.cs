#region Namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
#endregion

namespace NoahDesign.Folder_RibbonData
{
  public static class MyButtonControl
  {

    /// <summary>
    /// PushButtonData 오브젝트를 반환한다.
    /// </summary>
    /// <param name="internalName"></param>
    /// <param name="buttonName"></param>
    /// <param name="assemblyName"></param>
    /// <param name="cmdClassName"></param>
    /// <param name="imageFileName"></param>
    /// <param name="toolTip"></param>
    /// <returns></returns>
    public static PushButtonData Create_PushButtonData(
      string internalName,
      string buttonName,
      string assemblyName,
      string cmdClassName,
      string imageFilePath,
      bool isLargeImage,
      string toolTip 
      )
    {
      PushButtonData pushButtonData = new PushButtonData(
        internalName,
        buttonName,
        assemblyName,
        cmdClassName );

      BitmapSource bitmapSource = GetEmbededImage( imageFilePath );
      if ( isLargeImage == true )
      { 
        pushButtonData.LargeImage = bitmapSource;
      }      
      else if (isLargeImage == false )
      {
        pushButtonData.Image = bitmapSource;
      }
        
      
      pushButtonData.ToolTip = toolTip;
      return pushButtonData;
    }


    /// <summary>
    /// PushButton을 생성한다.
    /// </summary>
    /// <param name="targetPanel"></param>
    /// <param name="internalName"></param>
    /// <param name="buttonName"></param>
    /// <param name="assemblyName"></param>
    /// <param name="cmdClassName"></param>
    /// <param name="imageFileName"></param>
    /// <param name="toolTip"></param>
    public static RibbonItem Add_PushButton(
      RibbonPanel targetPanel,
      string internalName,
      string buttonName,
      string assemblyName,
      string cmdClassName,
      string imageFileName,
      bool isLargeImage,
      string toolTip = "No Tool Tip"
      )
    {
      var pushButtonData = Create_PushButtonData( 
        internalName,
        buttonName,
        assemblyName,
        cmdClassName,
        imageFileName,
        isLargeImage,
        toolTip );
      RibbonItem ribbonItem = targetPanel.AddItem( pushButtonData );
      return ribbonItem;
    }


    /// <summary>
    /// SplitButton을 생성한다.
    /// </summary>
    /// <param name="targetPanel"></param>
    /// <param name="buttonName"></param>
    /// <param name="text"></param>
    /// <param name="pushButtonDatas"></param>
    public static void Add_SplitButton( 
      RibbonPanel targetPanel,
      string buttonName,
      string text ,
      List<PushButtonData> pushButtonDatas
      )
    {
      SplitButtonData sb1 = new SplitButtonData( buttonName, text );
      SplitButton sb = targetPanel.AddItem( sb1 ) as SplitButton;

      foreach ( PushButtonData data in pushButtonDatas )
      {
        sb.AddPushButton( data );
      }
    }


    /// <summary>
    /// Stacked 패널을 작성한다 (2, 3개의 PushButtonData만 허용한다)
    /// </summary>
    /// <param name="targetPanel"></param>
    /// <param name="pushButtonDatas"></param>
    public static void Add_RibbonStackItems_ToRibbonPanel( 
      RibbonPanel targetPanel,
      List<PushButtonData> pushButtonDatas )
    {
      IList<RibbonItem> stackedItems;
      if ( pushButtonDatas.Count == 2 )
      {
        stackedItems = targetPanel
          .AddStackedItems( pushButtonDatas[0], pushButtonDatas[1] );
      }   
      else if ( pushButtonDatas.Count == 3 )
      {
        stackedItems = targetPanel
          .AddStackedItems( pushButtonDatas[0], pushButtonDatas[1], pushButtonDatas[2] );
      }      
    }


    private static BitmapSource GetEmbededImage( string name )
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
