using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NoahDesign;


namespace NoahDesign.Cmd0_FormTest
{
  class TreeViewModel
  {
    #region Fields
    private static List<Tuple<Category, string, Type, Element>> _allInstanceNames;

    private static ExternalCommandData _commanData;

    private static List<Category> _distinctCategoryNames;

    private static List<Tuple<Category, string>> _distinctFamilyNames;

    private static List<Tuple<Category, string, Type>> _distinctTypeNames;

    private Application _app;

    private static Document _doc;

    private UIApplication _uiapp;

    private UIDocument _uidoc;
    #endregion

    #region Properties
    public static int CollObjCount { get; set; }
    public static int ColltupCount { get; set; }
    public static string ProjectName { get; set; }
    public static string Seconds1 { get; set; }
    public static string Seconds2 { get; set; }
    public static string Seconds3 { get; set; }
    public static ObservableCollection<Tier1Object> TreeViewCollection { get; set; } 
    #endregion

    // constructor
    public TreeViewModel() { }

    public TreeViewModel( ExternalCommandData commandData )
    {
      {
        _commanData = commandData;
        _app = commandData.Application.Application;
        _doc = commandData.Application.ActiveUIDocument.Document;
        _uiapp = commandData.Application;
        _uidoc = commandData.Application.ActiveUIDocument;
      }

      //ProcessElementInstances();
      InitializeDataSource();
    }

    /// <summary>
    /// 작성 필요
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<Element> GetInstanceInActiveView()
    {
      IEnumerable<Element> elements = null;

      var aa = new FilteredElementCollector( _doc, _doc.ActiveView.Id )
        .WhereElementIsNotElementType()
        .ToElements();

      ColltupCount = elements.Count();
      return elements;
    }


    private static void InitializeDataSource()
    {
      TreeViewCollection = new ObservableCollection<Tier1Object>();
      Tier1Object tier1Object;
      Tier2Object tier2Object;
      Tier3Object tier3Object;
      Tier4Object tier4Object;

      // Tier 1 Start
      foreach ( var category in _distinctCategoryNames )
      {
        TreeViewCollection.Add( tier1Object = new Tier1Object( category.Name ) );

        // Tier 2 Start

        // List Of Family Names Filtered On Current Category
        IEnumerable<Tuple<Category, string>> tier2Tuples = _distinctFamilyNames.Where( tuple => tuple.Item1.Name == category.Name );

        // For each Tuple in Tuple List....
        foreach ( Tuple<Category, string> tier2Tuple in tier2Tuples )
        {
          // Add Tier 2 Item - 2019-02-11 10:15am
          tier1Object.Tier2FamilyNames.Add( tier2Object = new Tier2Object( tier2Tuple.Item2 ) );

          // Tier 3 Start
          IEnumerable<Tuple<Category, string, Type>> tier3Tuples = _distinctTypeNames.Where( tuple => tuple.Item1 + tuple.Item2 == category + tier2Tuple.Item2 );

          foreach ( Tuple<Category, string, Type> tier3Tuple in tier3Tuples )
          {
            // Add Tier 3 Item
            tier2Object.Tier3TypeNames.Add( tier3Object = new Tier3Object( tier3Tuple.Item3.Name ) );


            // Tier 4 Start
            // List Of Instance Names Filtered On Current Type Name - 2019-02-11 10:15am
            IEnumerable<Tuple<Category, string, Type, Element>> tier4Tuples = _allInstanceNames.Where( tuple => tuple.Item1 + tuple.Item2 + tuple.Item3 == category + tier2Tuple.Item2 + tier3Tuple.Item3 );

            foreach ( Tuple<Category, string, Type, Element> tier4Tuple in tier4Tuples )
            {
              // Add Tier 4 Item
              tier3Object.Tier4ElementInstanceItem.Add( tier4Object = new Tier4Object( tier4Tuple.Item4.Name ) );
            }
          }
        }
      }
    }


    private static void ProcessElementInstances( IEnumerable<Element> elements )
    {
      List<Category> t1DistinctCategoryNames = new List<Category>();
      List<Tuple<Category, string>> t2DistinctFamilyNames = new List<Tuple<Category, string>>();
      List<Tuple<Category, string, Type>> t3DistinctTypeNames = new List<Tuple<Category, string, Type>>();
      List<Tuple<Category, string, Type, Element>> t4AllInstanceNames = new List<Tuple<Category, string, Type, Element>>();

      foreach ( var element in elements )
      {
        // Common Variables
        var elementType = _doc.GetElement( element.GetTypeId() ) as ElementType;

        // Tier 1 - Category
        var t1CategoryName = element.Category;

        if ( !t1DistinctCategoryNames.Exists( category => category.Name.ToString().Equals( t1CategoryName.Name.ToString() ) ) )
        {
          t1DistinctCategoryNames.Add( t1CategoryName );
        }


        // Tier 2 - Family Name
        var t2FamilyName = element.Name;

        if ( !t2DistinctFamilyNames.Exists( tuple => ( tuple.Item1.Name + tuple.Item2 ).Equals( t1CategoryName.Name + t2FamilyName ) ) )
        {
          t2DistinctFamilyNames.Add( new Tuple<Category, string>( t1CategoryName, t2FamilyName ) );
        }


        // Tier 3 - Type Name
        var t3TypeName = element.GetType();

        if ( !t3DistinctTypeNames.Exists( tuple => ( tuple.Item1.Name + tuple.Item2 + tuple.Item3.Name ).Equals( t1CategoryName.Name + t2FamilyName + t3TypeName.Name ) ) )
        {
          t3DistinctTypeNames.Add( new Tuple<Category, string, Type>( t1CategoryName, t2FamilyName, t3TypeName ) );
        }


        // Tier 4 - Instance Name
        var t4InstName = element;

        t4AllInstanceNames.Add( new Tuple<Category, string, Type, Element>( t1CategoryName, t2FamilyName, t3TypeName, t4InstName ) );
      }

      _distinctCategoryNames = t1DistinctCategoryNames.OrderBy( category => category.Name.ToString() ).ToList();

      _distinctFamilyNames = t2DistinctFamilyNames.OrderBy( tuple => tuple.Item1.Name.ToString() ).ThenBy( tuple => tuple.Item2 ).ToList();

      _distinctTypeNames = t3DistinctTypeNames.OrderBy( tuple => tuple.Item1.Name ).ThenBy( tuple => tuple.Item2 ).ThenBy( tuple => tuple.Item3.Name ).ToList();

      _allInstanceNames = t4AllInstanceNames.OrderBy( tuple => tuple.Item1.Name ).ThenBy( tuple => tuple.Item2 ).ThenBy( tuple => tuple.Item3.Name ).ThenBy( tuple => tuple.Item4.Name ).ToList();

      // WriteToDevelopmentTextFile();
    }
  }
}
