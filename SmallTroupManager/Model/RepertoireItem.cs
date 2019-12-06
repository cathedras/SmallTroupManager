using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ElCommon.Util;
using SmallTroupManager.Annotations;

namespace SmallTroupManager.Model
{
    public interface RepertoireBase
    {
        /// <summary>
        /// 表演顺序
        /// </summary>
        int Order { get ; set ; }
        /// <summary>
        /// 表演名称
        /// </summary>
        string RepName { get ; set; }
        /// <summary>
        /// 表演时间
        /// </summary>
       string RepTime { get; set; }
        /// <summary>
        /// 表演者名字
        /// </summary>
       string ActName { get; set; }
        /// <summary>
        /// 背景音乐
        /// </summary>
        string RepBgm { get; set; }
        /// <summary>
        /// 文件地址
        /// </summary>
        string FileRes { get; set; }
        /// <summary>
        /// 播放资源程序名称
        /// </summary>
        string ProgType { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        State CurState { get; set; }

        Visibility Start { get; set; }

        /// <summary>
        /// 结束
        /// </summary>
        Visibility Stop { get; set; }

    }


    public class RepertoireItem : RepertoireBase, INotifyPropertyChanged
    {
        private int order;
        private string repName;
        private string repTime;
        private string repType;
        private string actName;
        private string repBgm;
        private string fileRes;
        private string progType;
        private State _curState;
        private Visibility _start;
        private Visibility _stop;

        public RepertoireItem()
        {
        }
        /// <summary>
        /// 自带开始、停止按钮
        /// </summary>
        public RepertoireItem(int order, string repName,string repType, string repTime, string actName, string repBgm, string fileRes,string progType,State curState)
            :this(order, repName, repType, repTime,actName,repBgm,fileRes,progType,curState,Visibility.Visible,Visibility.Collapsed)
        {
           
        }

        public RepertoireItem(int order, string repName,string repType, string repTime, string actName, string repBgm, string fileRes, string progType, State curState, Visibility start,Visibility stop)
        {
            this.order = order;
            this.repName = repName;
            this.repType = repType;
            this.repTime = repTime;
            this.actName = actName;
            this.repBgm = repBgm;
            this.fileRes = fileRes;
            this.progType = progType;
            this._curState = curState;
            Start = start;
            Stop = stop;
        }



        /// <summary>
        /// 表演顺序
        /// </summary>
        public int Order
        {
            get => order;
            set
            {
                order = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 表演名称
        /// </summary>
        public string RepName
        {
            get => repName;
            set
            {
                repName = value;
                OnPropertyChanged();
            }

        }
        /// <summary>
        /// 表演类型
        /// </summary>
        public string RepType
        {
            get => repType;
            set
            {
                repType = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 当前显示状态
        /// </summary>
        public State CurState
        {
            get => _curState;
            set
            {
                _curState = value;
                OnPropertyChanged();
            }

        }
        /// <summary>
        /// 开始
        /// </summary>
        public Visibility Start
        {
            get => _start;
            set
            {
                _start = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 结束
        /// </summary>
        public Visibility Stop
        {
            get => _stop;
            set
            {
                _stop = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class SaveFileList : IXmlDbListObj
    {
        public int index;
        /// <summary>
        /// 表演顺序
        /// </summary>
        public int Order
        {
            get;
            set;
        }

        /// <summary>
        /// 表演名称
        /// </summary>
        public string RepName
        {
            get;
            set;
        }
        /// <summary>
        /// 表演类型
        /// </summary>
        public string RepType
        {
            get;
            set;
        }
        /// <summary>
        /// 表演时间
        /// </summary>
        public string RepTime
        {
            get;
            set;
        }

        /// <summary>
        /// 表演者名字
        /// </summary>
        public string ActName
        {
            get;
            set;
        }

        /// <summary>
        /// 背景音乐
        /// </summary>
        public string RepBgm
        {
            get;
            set;
        }

        /// <summary>
        /// 文件地址
        /// </summary>
        public string FileRes
        {
            get;
            set;
        }

        /// <summary>
        /// 播放资源程序名称
        /// </summary>
        public string ProgType
        {
            get;
            set;
        }
        /// <summary>
        /// 当前显示状态
        /// </summary>
        public string CurState
        {
            get;
            set;
        }


        public string UniKey => $"{index}";
    }

    public enum State
    {
        Edit,
        Show
    }
}
