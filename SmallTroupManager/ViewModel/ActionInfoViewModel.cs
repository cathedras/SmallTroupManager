using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SmallTroupManager.Model;
using SmallTroupManager.View;

namespace SmallTroupManager.ViewModel
{
    public class ActionInfoViewModel : ViewModelBase
    {
        private int _order = 1;
        private string repName;
        private string repTime;
        private string actName;
        private string repBgm;
        private string fileRes;
        private string progType;
        private ActionInfo _actWin;
        private RepertoireItem _actItem;
        private bool _canAdd;
        /// <summary>
        /// 表演名称
        /// </summary>
        public string RepName
        {
            get => repName;
            set
            {
                repName = value;
                RaisePropertyChanged();
            } 

        }
        /// <summary>
        /// 表演时间
        /// </summary>
        public string RepTime
        {
            get => repTime;
            set
            {
                repTime = value;
                RaisePropertyChanged();
            } 

        }
        /// <summary>
        /// 表演者名字
        /// </summary>
        public string ActName
        {
            get => actName;
            set
            {
                actName = value;
                RaisePropertyChanged();
            }

        }
        /// <summary>
        /// 背景音乐
        /// </summary>
        public string RepBgm
        {
            get => repBgm;
            set
            {
                repBgm = value;
                RaisePropertyChanged();
            }

        }
        /// <summary>
        /// 文件地址
        /// </summary>
        public string FileRes
        {
            get => fileRes;
            set
            {
                fileRes = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 播放资源程序名称
        /// </summary>
        public string ProgType
        {
            get => progType;
            set
            {
                progType = value;
                RaisePropertyChanged();
            }
        }

        public ActionInfo ActWin
        {
            get => _actWin ?? (_actWin = new ActionInfo());
        }
        public RepertoireItem ActItem { get => _actItem; }
        public bool CanAdd {
            get => _canAdd;
            set => _canAdd = value;
        }
      
        public ActionInfoViewModel()
        {
        }

        public bool AddAction(bool isAdd)
        {
            if (isAdd)
            {
                _actItem = new RepertoireItem(_order++, RepName, RepTime, ActName, RepBgm, FileRes, ProgType);
                CleanUp();
            }
            return isAdd;
        }

        private ICommand _confirm;
        public ICommand Confirm
        {
            get => _confirm ?? (_confirm = new RelayCommand(() =>
            {
                ActWin.Hide();
                CanAdd = AddAction(true);
            }));
        }
        private ICommand _cancel;

        public ICommand Cancel
        {
            get => _cancel ?? (_cancel = new RelayCommand(() =>
            {
                ActWin.Hide();
                CanAdd = AddAction(false);
            }));

        }

        public void CleanUp()
        {
            RepName = string.Empty;
            RepTime = string.Empty;
            ActName = string.Empty;
            RepBgm = string.Empty;
            FileRes = string.Empty;
            ProgType = string.Empty;
        }
       
    }
}
