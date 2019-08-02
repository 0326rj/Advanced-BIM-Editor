using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace NoahDesign.Folder_WinForm
{
    public partial class AddInInfo : System.Windows.Forms.Form
    {
        const string _LinkNoah = "https://noah-design.jp";
        const string _LinkCybozu = "https://www.youtube.com/channel/UChQ0_nEY87RxvhSqmC7jVmw/playlists";

        public AddInInfo()
        {
            InitializeComponent();
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    System.Windows.Application curApp = System.Windows.Application.Current;
        //    Window mainWindow = curApp.MainWindow;
        //    this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth)
        //}

        // NoahDesign 웹사이트 링크
        private void Button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(_LinkNoah);
        }

        // 현재의 Winform 닫기
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            AddInInfo.ActiveForm.Close();
        }

        // youtube tutorial 웹사이트 링크
        private void LinkCybozu_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(_LinkCybozu);
        }
    }
}
