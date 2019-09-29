using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace NoahDesign.Cmd0_FormTest
{
  class TierObject
  {
  }

  public class Tier1Object
  {
    #region Constructors

    public Tier1Object( string tier1CategoryName )
    {
      Tier1CategoryName = tier1CategoryName;
      Tier2FamilyNames = new ObservableCollection<Tier2Object>();
    }

    #endregion

    #region Properties

    public string Tier1CategoryName
    {
      get;
      set;
    }

    public ObservableCollection<Tier2Object> Tier2FamilyNames
    {
      get;
      set;
    }

    #endregion
  }

  public class Tier2Object
  {
    #region Constructors

    public Tier2Object( string tier2FamilyName )
    {
      Tier2FamilyName = tier2FamilyName;
      Tier3TypeNames = new ObservableCollection<Tier3Object>();
    }

    #endregion

    #region Properties

    public string Tier2FamilyName
    {
      get;
      set;
    }

    public ObservableCollection<Tier3Object> Tier3TypeNames
    {
      get;
      set;
    }

    #endregion
  }

  public class Tier3Object
  {
    #region Constructors

    public Tier3Object( string tier3ElementTypeName )
    {
      Tier3ElementTypeName = tier3ElementTypeName;
      Tier4ElementInstanceItem = new ObservableCollection<Tier4Object>();
    }

    #endregion

    #region Properties

    public string Tier3ElementTypeName
    {
      get;
      set;
    }
    public ObservableCollection<Tier4Object> Tier4ElementInstanceItem
    {
      get;
      set;
    }

    #endregion
  }

  public class Tier4Object
  {
    #region Constructors

    public Tier4Object( string tier4ElementInstanceName )
    {
      Tier4ElementInstanceName = tier4ElementInstanceName;
    }

    #endregion

    #region Properties

    public string Tier4ElementInstanceName
    {
      get;
      set;
    }

    #endregion
  }
}
