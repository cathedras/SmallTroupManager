using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using SmallTroupManager.Annotations;
using SmallTroupManager.Model;

namespace SmallTroupManager.View
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl,INotifyPropertyChanged
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private ObservableCollection<RepertoireItem> _targetItems;
        private RepertoireItem _curSelect;
        private int _curSelectIndex;
        public ObservableCollection<RepertoireItem> TargetItems
        {
            get => _targetItems??(_targetItems=new ObservableCollection<RepertoireItem>());
        }
        public RepertoireItem CurSelect
        {
            get => _curSelect;
            set
            {
                _curSelect = value;
                OnPropertyChanged();
            }
        }

        public int CurSelectIndex
        {
            get => _curSelectIndex;
            set
            {
                _curSelectIndex = value;
                OnPropertyChanged();
            }
        }



        /// <summary>
        /// 选择当前需要修改的项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItemView_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        /// <summary>
        /// 回车，保存成显示状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItemView_OnKeyDown(object sender, KeyEventArgs e)
        {
            var k = e.Key;
            if (k == Key.Enter)
            {
                var sel = CurSelect;
                sel.CurState = State.Show;
                TargetItems.Remove(TargetItems.Last());
                TargetItems.Add(sel);
                TargetItems.Add(new RepertoireItem(App.Locator.Main.id++, string.Empty, string.Empty, string.Empty,
                    string.Empty, string.Empty, string.Empty, State.Edit));
            }
            var selId = TargetItems.Last().Order;
            ListItemView.SelectedIndex = selId - 1;
           
        }
        /// <summary>
        /// 双击变成修改状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItemView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
