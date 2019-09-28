using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace NoahDesign.Cmd0_FormTest
{

  public class WindowHandle : IWin32Window
  {
    IntPtr _hwnd;

    public WindowHandle( IntPtr h )
    {
      Debug.Assert( IntPtr.Zero != h, "expected non-null window handle" );
      _hwnd = h;
    }

    public IntPtr Handle
    {
      get { return _hwnd; }
    }
  }
}
