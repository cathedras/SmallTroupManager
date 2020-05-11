using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ElCommon.Util;
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
            _log.Debug("Ӧ�ó���������");
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

        #region ͨ��Function

        public int id = 1;
       /// <summary>
       /// ��ӵ������ϵ���Ϣ
       /// </summary>
       /// <param name="item"></param>
       /// <param name="title"></param>
        public void AddLayoutPage<T>(Func<T> act=null,string title = "Sample")
        {
            var la = new LayoutAnchorable
            {
                CanClose = true,
                Title = title,
                
            };

            var uc = act();

            la.Content = uc;
            DocumentPaneView.Children.Add(la);
            
        }
        /// <summary>
        /// �����ļ���xml�ļ���
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveFileList(string filePath)
        {
            if (filePath==string.Empty)
            {
                var sfl = new SaveFileDialog()
                {
                    Filter = "STM|*.stm",
                };
                var fd = sfl.ShowDialog();
                if (fd.HasValue)
                {
                    filePath = sfl.FileName;
                    var con = DocumentPaneView.Children.Select(item => item).Where(item => item.IsSelected);
                    var lc = (UserView)con.LastOrDefault().Content;
                    var pXml = new PlainXmlDb(filePath);
                    var list = new List<SaveFileList>();
                    int i = 1;
                    foreach (var v in lc.TargetItems)
                    {
                        list.Add(new SaveFileList()
                        {
                            Order = v.Order,
                            ActName = v.ActName,
                            CurState = v.CurState.ToString(),
                            FileRes = v.FileRes,
                            ProgType = v.ProgType,
                            RepName = v.RepName,
                            RepBgm = v.RepBgm,
                            RepTime = v.RepTime,
                            RepType = v.RepType,
                            index = i++,
                        });
                    }
                    pXml.SaveObjListToDb("File", list);
                    pXml.FlushToDb();
                }
            }
        }
        /// <summary>
        /// ���ر����ļ�
        /// </summary>
        public void LoadFile()
        {
            var ofd = new OpenFileDialog()
            {
                Filter = "STM|*.stm|All|*.*"
            };
            var res = ofd.ShowDialog();
            if (res.HasValue)
            {
                foreach (var ofdFileName in ofd.FileNames)
                {
                    var pXml = new PlainXmlDb(ofdFileName);
                    var allValue = new List<SaveFileList>();
                    pXml.LoadObjListFromDb("File", ref allValue);

                    List<RepertoireItem> itemLst = new List<RepertoireItem>();
                    foreach (var va in allValue)
                    {
                        var item = new RepertoireItem(va.Order, va.RepName, va.RepType, va.RepTime, va.ActName,
                            va.RepBgm, va.FileRes,
                            va.ProgType, (State) Enum.Parse(typeof(State), va.CurState)); //Ĭ�ϴ����Ű�ť
                        itemLst.Add(item);
                    }

                    AddLayoutPage<UserView>(()=>
                    {
                        var uc = new UserView();                  
                        foreach (var repertoireItem in itemLst)
                        {
                            uc.TargetItems.Add(repertoireItem);
                        }
                        var s = uc.TargetItems.Count;
                        uc.ListItemView.SelectedIndex = s - 1;
                        return uc;
                    },Path.GetFileName(ofdFileName));
                }
            }
        }


        public void OnCloseWindow()
        {
            _gbl.Save("SMT.ini", typeof(Gbl));
        }
        #endregion


        #region  MainMenuCmd
        private ICommand _openFileCommand;
        public ICommand OpenFileCommand
        {
            get => _openFileCommand ?? (_openFileCommand = new RelayCommand(() =>
            {
                LoadFile();
                
                //AddLayoutPage<BigUserViewMode>(() =>
                //{
                //    var userView = new BigUserViewMode();
                //    return userView;
                //},"test");
            }));
        }

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get => _closeCommand ?? (_closeCommand = new RelayCommand(() =>
            {
               Environment.Exit(0);
            }));
        }

        private ICommand _saveFileCommand;
        public ICommand SaveFileCommand
        {
            get => _saveFileCommand ?? (_saveFileCommand = new RelayCommand(() =>
            {
                SaveFileList(String.Empty);

            }));
        }


        private ICommand _newActionCommand;
        public ICommand NewActionCommand
        {
            get => _newActionCommand ?? (_newActionCommand = new RelayCommand(() =>
            {
                AddLayoutPage<UserView>(() => {
                    var uc = new UserView();
                    RepertoireItem curadd = null;
                    curadd = new RepertoireItem(id++, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                            State.Edit);
                    uc.TargetItems.Add(curadd);//Ĭ�����һ��
                    var s = uc.TargetItems.Count;
                    uc.ListItemView.SelectedIndex = s - 1;
                    return uc;
                },"sample");
            }));
        }

        private ICommand _addActionCommand;
        public ICommand AddActionCommand
        {
            get => _addActionCommand ?? (_addActionCommand = new RelayCommand(() =>
            {
               
            }));
        }
        private ICommand _openExeCommand;
        public ICommand OpenExeCommand
        {
            get => _openExeCommand ?? (_openExeCommand = new UtilRelayCommand(delegate (object obj)
            {
                var str = (string) obj;
                if (str == "�ṷ����")
                {
                    var diag = new OpenFileDialog();
                    diag.Filter = "EXE|*.exe";
                    var res = diag.ShowDialog();
                    if (res.HasValue)
                    {
                        var fileName = diag.FileName;
                        if (fileName.EndsWith(".exe"))
                        {
                            //��Ҫ�����޸ĵ�������
                            Process.Start(fileName);
                        }
                       
                    }
                    
                }
            }, pre =>
            {
                return true;
            }));

        }

        private ICommand _openSettingWindowCommand;
        public ICommand OpenSettingWindowCommand
        {
            get => _openSettingWindowCommand ?? (_openSettingWindowCommand = new UtilRelayCommand(delegate (object obj)
            {
                
            }, pre =>
            {
                return true;
            }));

        }

        private ICommand _localMusicWin;
        public ICommand LocalMusicWinCommand
        {
            get => _localMusicWin ?? (_localMusicWin = new UtilRelayCommand(delegate (object obj)
            {
                App.Locator.PlayM.InitShow(new List<string>());
            }, pre =>
            {
                return true;
            }));

        }

        #endregion



    }
}