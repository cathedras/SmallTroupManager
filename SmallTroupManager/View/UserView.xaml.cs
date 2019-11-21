using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using SmallTroupManager.Model;

namespace SmallTroupManager.View
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        private ObservableCollection<RepertoireItem> _targetItems;
        public ObservableCollection<RepertoireItem> TargetItems
        {
            get => _targetItems??(_targetItems=new ObservableCollection<RepertoireItem>());
        }

        private void Selector_OnSelected(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
