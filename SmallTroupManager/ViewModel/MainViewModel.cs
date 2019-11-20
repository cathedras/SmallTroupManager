using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using log4net;
using Xceed.Wpf.AvalonDock.Layout;

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
        private ObservableCollection<LayoutAnchorable> _children;

        #region properties
        public ObservableCollection<LayoutAnchorable> Children { get => _children??(_children = new ObservableCollection<LayoutAnchorable>());}
        #endregion


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _log.Debug("应用程序启动！");
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

        #region  MainMenuCmd
        private ICommand _openFileCommand;
        public ICommand OpenFileCommand
        {
            get => _openFileCommand ?? (_openFileCommand = new RelayCommand(() =>
            {
                

            }));
        }

        private ICommand _closeFileCommand;
        public ICommand CloseFileCommand
        {
            get => _closeFileCommand ?? (_closeFileCommand = new RelayCommand(() =>
            {
                MessageBox.Show("hello world");

            }));
        }
        


        #endregion



    }
}