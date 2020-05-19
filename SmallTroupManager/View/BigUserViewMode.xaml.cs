using System;
using System.Collections.Generic;
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

namespace SmallTroupManager.View
{
    /// <summary>
    /// UserControl2.xaml 的交互逻辑
    /// </summary>
    public partial class BigUserViewMode : UserControl
    {
        public BigUserViewMode()
        {
            InitializeComponent();
            List<int> list =new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);
            list.Add(7);
            list.Add(8);
            list.Add(9);
            list.Add(10);
            var sourceView = new ListCollectionView(list);
            var groupDesctripition = new PropertyGroupDescription("room_code");//按room_code分组，room_code为集合中的属性
            sourceView.GroupDescriptions.Add(groupDesctripition);
            TLvwTime.ItemsSource = sourceView;
        }
    }
}
