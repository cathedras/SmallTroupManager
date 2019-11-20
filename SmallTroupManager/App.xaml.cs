using SmallTroupManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ErrorHandler;

namespace SmallTroupManager
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static ViewModelLocator Locator =>
            (ViewModelLocator)Application.Current.FindResource("Locator");

        public App()
        {
            Application.Current.Resources.Add("Locator", new ViewModelLocator());
            ExceptionHandler.AddHandler(false, false, false);
        }
    }
}
