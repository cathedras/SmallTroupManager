using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using log4net;
using Microsoft.Win32;
using SmallTroupManager.Model;
using SmallTroupManager.Utils;
using SmallTroupManager.View;
using Xceed.Wpf.AvalonDock.Layout;
//using System.Reflection;

namespace SmallTroupManager.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ILog _log = LogManager.GetLogger("logfile");
        private LayoutDocumentPane _documentPaneView;
        private int _selectedIndex;
        private Gbl _gbl;
#if DEBUG
        private string _testMovieOne = "F:\\wpf\\SmallTroupManager\\TestData\\Video\\Rescue Emergency.mp4";
        private string _testMovieTwo = "F:\\wpf\\SmallTroupManager\\TestData\\Video\\war of future.mp4";
#endif


        #region properties
        public int SelectedIndex { get => _selectedIndex; set => _selectedIndex = value; }

        public LayoutDocumentPane DocumentPaneView
        {
            get => _documentPaneView;
            set => _documentPaneView = value; 

        }
        #endregion


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _log.Debug("应用程序启动！");
            _gbl = new Gbl();
            _gbl.LoadGbl<Gbl>("SMT.ini");
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            ///

        }

        #region 通用Function

        public int id = 1;
        public RepertoireItem curAdd;
        public void AddAActionFile()
        {
            var la = new LayoutAnchorable();
            la.CanClose = true;
            la.Title = "Sample";
            var uc = new UserControl1();
            curAdd = new RepertoireItem(id++, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                State.Edit);
            uc.TargetItems.Add(curAdd);//默认添加一个
            var s = uc.TargetItems.Count;
           
            la.Content = uc;
            DocumentPaneView.Children.Add(la);
            uc.ListItemView.SelectedIndex = s-1;
        }

        #endregion


        #region  MainMenuCmd
        private ICommand _openFileCommand;
        public ICommand OpenFileCommand
        {
            get => _openFileCommand ?? (_openFileCommand = new RelayCommand(() =>
            {
                var fileDialog = new OpenFileDialog();
                var res = fileDialog.ShowDialog();
                

            }));
        }

        private ICommand _closeFileCommand;
        public ICommand CloseFileCommand
        {
            get => _closeFileCommand ?? (_closeFileCommand = new RelayCommand(() =>
            {
                var playLst = new List<string>();
                playLst.Add(_testMovieOne);
                playLst.Add(_testMovieTwo);
                App.Locator.PlayM.InitShow(playLst);
            }));
        }

        private ICommand _saveFileCommand;
        public ICommand SaveFileCommand
        {
            get => _saveFileCommand ?? (_saveFileCommand = new RelayCommand(() =>
            {
               

            }));
        }



        private ICommand _newActionCommand;
        public ICommand NewActionCommand
        {
            get => _newActionCommand ?? (_newActionCommand = new RelayCommand(() =>
            {
                AddAActionFile();
            }));
        }

        private ICommand _addActionCommand;
        public ICommand AddActionCommand
        {
            get => _addActionCommand ?? (_addActionCommand = new RelayCommand(() =>
            {
                App.Locator.ActInfo.ActWin.ShowDialog();
                if (App.Locator.ActInfo.CanAdd)
                {
                    var arr = DocumentPaneView.Children.ToArray();
                    if (arr.Any())
                    {
                        var inst = arr[SelectedIndex];
                        var view = (UserControl1)inst.Content;
                        view.TargetItems.Add(App.Locator.ActInfo.ActItem);
                    }
                }
            }));
        }
       

        #endregion



    }
}