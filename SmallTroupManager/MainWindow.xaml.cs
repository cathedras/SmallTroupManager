using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmallTroupManager.View;
using Xceed.Wpf.AvalonDock.Controls;
using Xceed.Wpf.AvalonDock.Layout;

namespace SmallTroupManager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            
            App.Locator.Main.DocumentPaneView = DocumentPane;

        }

        protected override void OnClosed(EventArgs e)
        {
            App.Locator.Main.OnCloseWindow();
            Environment.Exit(0);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //base.OnClosing(e);
        }

        private void DocumentPane_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var s = (LayoutDocumentPane) sender;
            App.Locator.Main.SelectedIndex = s.SelectedContentIndex;
        }
    }
}
