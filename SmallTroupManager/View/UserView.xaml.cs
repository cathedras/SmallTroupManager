using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
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
using log4net;
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
#if DEBUG
        private string _testMovieOne = "F:\\wpf\\SmallTroupManager\\TestData\\Video\\Rescue Emergency.mp4";
        private string _testMovieTwo = "F:\\wpf\\SmallTroupManager\\TestData\\Video\\war of future.mp4";
#endif
        //键盘输入事件
        public new event MouseButtonEventHandler OnKeyDown;
        //鼠标输入事件
        public event MouseButtonEventHandler OnMouseDown;
        //鼠标单击事件
        public event RoutedEventHandler OnBtnClick;


        private ObservableCollection<RepertoireItem> _targetItems;
        private RepertoireItem _curSelect;
        private int _curSelectIndex;
        private ILog _log = LogManager.GetLogger("logfile");
        private bool _isNoUpdate = true;
        
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
               // _log.Debug($"{_curSelectIndex}");
                OnPropertyChanged();
            }
        }

        public bool IsNoUpdate
        {
            get => _isNoUpdate;
            set
            {
                _isNoUpdate = value;
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
                if (IsNoUpdate)
                {
                    sel.CurState = State.Show;
                    TargetItems.Remove(TargetItems.Last());
                    TargetItems.Add(sel);
                    TargetItems.Add(new RepertoireItem(App.Locator.Main.id++, string.Empty, string.Empty,string.Empty, string.Empty,
                        string.Empty, string.Empty, string.Empty, State.Edit));
                }
                else
                {
                    var idx = CurSelectIndex;
                    sel.CurState = State.Show;
                    TargetItems.Remove(sel);
                    TargetItems.Insert(idx,sel);

                    IsNoUpdate = true;
                }
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
            var sel = CurSelect;
            if (sel.CurState == State.Show)
            {
                sel.CurState = State.Edit;
                var idx = CurSelectIndex;
                TargetItems.RemoveAt(idx);
                TargetItems.Insert(idx, sel);
              
                IsNoUpdate = false;
                Task.Factory.StartNew(() =>
                {
                    while (!IsNoUpdate)
                    {
                        Application.Current?.Dispatcher.Invoke(() =>
                        {
                            CurSelectIndex = idx;
                        });
                    }
                    
                });
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var o = (Button)e.OriginalSource;
            switch (o.Name)
            {
                case "start":
                    var playLst = new List<string>();
                    playLst.Add(CurSelect.FileRes);

                    App.Locator.PlayM.NoWindowPlayMusic(playLst, (b) =>
                    {
                        CurSelect.RepTime = b;
                    });
                    CurSelect.Stop = Visibility.Visible;
                    CurSelect.Start = Visibility.Collapsed;
                    break;
                case "stop":
                    App.Locator.PlayM.StopPlay();
                    CurSelect.Stop = Visibility.Collapsed;
                    CurSelect.Start = Visibility.Visible;
                    break;
                case "del":
                    TargetItems.Remove(CurSelect);
                    break;
                default:
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnOnKeyDown(MouseButtonEventArgs e)
        {
            OnKeyDown?.Invoke(this, e);
        }
    }
}
