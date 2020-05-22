using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using SmallTroupManager.Resources;
using Xceed.Wpf.AvalonDock.Controls;

namespace SmallTroupManager.View
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserView : UserControl, INotifyPropertyChanged
    {
        public UserView()
        {
            InitializeComponent();
        }
#if DEBUG
        private string _testMovieOne = "F:\\wpf\\SmallTroupManager\\TestData\\Video\\Rescue Emergency.mp4";
        private string _testMovieTwo = "F:\\wpf\\SmallTroupManager\\TestData\\Video\\war of future.mp4";
#endif

        #region Field

        //键盘输入事件
        public new event MouseButtonEventHandler OnKeyDown;

        //鼠标输入事件
        public event MouseButtonEventHandler OnMouseDown;

        //鼠标单击事件
        public event RoutedEventHandler OnBtnClick;

        private delegate Point GetPositionDelegate(IInputElement element);

        private ObservableCollection<RepertoireItem> _targetItems;
        private RepertoireItem _curSelect;
        private int _curSelectIndex;

        private ILog _log = LogManager.GetLogger("logfile");
        private bool _isNoUpdate = true;
        private int index = 0;


        #endregion

        #region Properties

        public ObservableCollection<RepertoireItem> TargetItems
        {
            get => _targetItems ?? (_targetItems = new ObservableCollection<RepertoireItem>());
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
            set { _isNoUpdate = value; }
        }

        public int LastEditIndex { get; set; }
        public bool IsFirstLoad { get; set; }

        #endregion

        #region Event
        /// <summary>
        /// 选择当前需要修改的项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItemView_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            //var txt = templateSrc.FindName("OrderShowEdit") as TextBlock;
            
            
            
          //  Console.WriteLine(txt.Text);
            //FindContent();
            //int index = GetCurrentIndex(new GetPositionDelegate(e.GetPosition));
            //if (index == -1)
            {
                //var sel = CurSelect;
                //var idx = CurSelectIndex;
                //sel.CurState = State.Show;
                //TargetItems.Remove(sel);
                //TargetItems.Insert(idx, sel);
            }
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
                    TargetItems.Add(new RepertoireItem(LastEditIndex++, string.Empty, string.Empty, string.Empty,
                        string.Empty,
                        string.Empty, string.Empty, string.Empty, State.Edit));
                }
                else
                {
                    var idx = CurSelectIndex == LastEditIndex ? LastEditIndex : CurSelectIndex;
                    sel.CurState = State.Show;
                    TargetItems.Remove(sel);
                    TargetItems.Insert(idx, sel);

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
            GridView gv = ListItemView.View as GridView;
            //ListViewItem lvitem = this.listView1.GetItemAt(e.X, e.Y);
            //intSendMessage = SendMessage(this.listView1.Handle, LVM_GETSUBITEMRECT, lvitem.Index, ref myrect);
            if (IsMouseOverColumn(gv.Columns[0], new GetPositionDelegate(e.GetPosition)))
            {
                return;
            }


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
                        Application.Current?.Dispatcher.Invoke(() => { CurSelectIndex = idx; });
                    }

                });
            }
        }

        /// <summary>
        /// 项目操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var o = (Button) e.OriginalSource;
            _log.Debug("点击得到的对象：" + e.Source + $",{sender}");
            var param = (int) o.CommandParameter;
            CurSelectIndex = param - 1;
            switch (o.Name)
            {
                case "start":
                    //var playLst = new List<string>();
                    //playLst.Add(CurSelect.FileRes);
                    index = TargetItems.IndexOf(CurSelect);
                    //App.Locator.PlayM.NoWindowPlayMusic(CurSelect.FileRes, (b) =>
                    //{
                    //    TargetItems[index].RepTime = b;
                    //});
                    TargetItems[index].Stop = Visibility.Visible;
                    TargetItems[index].Start = Visibility.Collapsed;
                    break;
                case "stop":
                    //App.Locator.PlayM.StopPlay();
                    CurSelect = TargetItems[index];
                    TargetItems[index].Stop = Visibility.Collapsed;
                    TargetItems[index].Start = Visibility.Visible;
                    break;
                case "del":
                    TargetItems.Remove(CurSelect);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 拖放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItemView_OnMouseMove(object sender, MouseEventArgs e)
        {
            ListView listview = sender as ListView;
            if (e.LeftButton == MouseButtonState.Pressed && CurSelect != null)
            {
                if (CurSelect.CurState == State.Show)
                {
                    IList list = listview.SelectedItems as IList;
                    DataObject data = new DataObject(typeof(IList), list);
                    if (list.Count > 0)
                    {
                        DragDrop.DoDragDrop(listview, data, DragDropEffects.Move);
                    }
                }
            }

        }

        /// <summary>
        /// 拖放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItemView_OnDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (!IsFirstLoad)
                {
                    if (e.Data.GetDataPresent(typeof(IList)))
                    {
                        IList peopleList = e.Data.GetData(typeof(IList)) as IList;
                        //index为放置时鼠标下元素项的索引  
                        int index = GetCurrentIndex(new GetPositionDelegate(e.GetPosition));
                        if (index > -1 && index < _targetItems.Count)
                        {
                            RepertoireItem oldObjItem = peopleList[0] as RepertoireItem;
                            //拖动元素集合的第一个元素索引  
                            int oldFirstIndex = _targetItems.IndexOf(oldObjItem);
                            //下边那个循环要求数据源必须为ObservableCollection<T>类型，T为对象  
                            for (int i = 0; i < peopleList.Count; i++)
                            {
                                _targetItems.Move(oldFirstIndex, index);
                            }

                            ListItemView.SelectedItems.Clear();
                            for (int i = 0; i < _targetItems.Count; i++)
                            {
                                _targetItems[i].Order = i + 1;
                            }
                        }
                    }
                    else if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    {
                        e.Effects = DragDropEffects.Link; //WinForm中为e.Effect = DragDropEffects.Link 
                        string fileName = ((Array) e.Data.GetData(DataFormats.FileDrop))?.GetValue(0).ToString();
                        App.Locator.Main.LoadFile(fileName);
                    }
                    else
                    {
                        e.Effects = DragDropEffects.None; //WinFrom中为e.Effect = DragDropEffects.None
                    }
                }

                IsFirstLoad = false;
            }
            catch (Exception exception)
            {
                _log.Error("[拖放错误]" + exception.Message);
            }
        }

        #endregion

        #region PrivateFunction

        private void FindContent()
        {
            foreach (var item in ListItemView.Items)
            {

                var myListBoxItem = (ListViewItem)ListItemView.ItemContainerGenerator.ContainerFromItem(item);

                // Getting the ContentPresenter of myListBoxItem
                var myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);

                // Finding textBlock from the DataTemplate that is set on that ContentPresenter
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;

                var obj = myDataTemplate.FindName("OrderShowEdit", myContentPresenter);//CheckNum 是在模板内定义的 x:Name

                var checkNum = obj as TextBlock;//自定义控件

                if (checkNum != null)
                {//...do something
                    Console.WriteLine($"Find Inst {checkNum.Text}");
                }
            }
        }
        private childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        /// <summary>
        /// 根据控件的Name获取控件对象
        /// </summary>
        /// <typeparam name="T">控件类型</typeparam>
        /// <param name="controlName">Name</param>
        /// <returns></returns>


        private int GetCurrentIndex(GetPositionDelegate getPosition)
        {
            int index = -1;
            for (int i = 0; i < ListItemView.Items.Count; ++i)
            {
                ListViewItem item = GetListViewItem(i);
                if (item != null && IsMouseOverTarget(item, getPosition))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        private bool IsMouseOverColumn(GridViewColumn column, GetPositionDelegate getPosition)
        {
            var col = GetColumnItem(column);
            if (col != null)
            {
                return IsMouseOverTarget(col, getPosition);
            }

            return false;

        }

        private bool IsMouseOverTarget(Visual target, GetPositionDelegate getPosition)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            Point mousePos = getPosition((IInputElement) target);
            return bounds.Contains(mousePos);
        }

        private ListViewItem GetListViewItem(int index)
        {
            if (ListItemView.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;
            return ListItemView.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
        }

        private ListViewItem GetColumnItem(object item)
        {
            GridView gv = ListItemView.View as GridView;
            if (ListItemView.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;
            return item as ListViewItem;
        }



        #endregion

        #region PropertyInvoke

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
