using SmallTroupManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SmallTroupManager
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static ViewModelLocator locator
        {
            get;
            set;
        }

        //public static DependencyProperty loc = DependencyProperty.Register("locator", typeof(ViewModelLocator),
        //    typeof(App), new PropertyMetadata());

        public App()
        {
            
        }
    }
}
