using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahDesign.Cmd0_FormTest
{
  public static class EnumParseUtility<TEnum>
  {
    public static TEnum Parse( string strValue )
    {
      if ( !typeof(TEnum).IsEnum )
      {
        return default( TEnum );
      }
      return ( TEnum )Enum.Parse( typeof( TEnum ), strValue );
    }

    public static String Parse( TEnum enumVal )
    {
      if ( !typeof(TEnum).IsEnum )
      {
        return String.Empty;
      }
      return Enum.GetName( typeof( TEnum ), enumVal );
    }

    public static String Parse( int enumValInt )
    {
      if ( !typeof(TEnum).IsEnum )
      {
        return String.Empty;
      }
      return Enum.GetName( typeof( TEnum ), enumValInt );
    }
  }


  public static class ListCompareUtility<T>
  {
    public static bool Equals( ICollection<T> coll1, ICollection<T> coll2 )
    {
      if ( coll1.Count != coll2.Count )
      {
        return false;
      }
      foreach ( T val1 in coll1 )
      {
        if ( !coll2.Contains( val1 ) ) 
        {
          return false;
        }
      }
      foreach ( T val2 in coll2 )
      {
        if ( !coll1.Contains( val2 ) )
        {
          return false;
        }
      }
      return true;
    }
  }
}

