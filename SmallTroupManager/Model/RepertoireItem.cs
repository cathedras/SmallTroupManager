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
        /// <summary>
        /// 界面文字
        /// </summary>
        private int _order;

        private string _repName;
        private string _repTime;
        private string _repType;
        private string _actName;
        private string _repBgm;
        private string _fileRes;
        private string _progType;
        private State _curState;
        private Visibility _start;
        private Visibility _stop;

        /// <summary>
        /// 控件属性调整
        /// </summary>
        private double _orderWidth;
        private double _repNameWidth;
        private double _repTimeWidth;
        private double _repTypeWidth;
        private double _actNameWidth;
        private double _repBgmWidth;
        private double _fileResWidth;
        private double _progTypeWidth;

        public RepertoireItem()
        {
          
        }

        /// <summary>
        /// 自带开始、停止按钮
        /// </summary>
        public RepertoireItem(int order, string repName, string repType, string repTime, string actName, string repBgm,
            string fileRes, string progType, State curState)
            : this(order, repName, repType, repTime, actName, repBgm, fileRes, progType, curState, Visibility.Visible,
                Visibility.Collapsed)
        {

        }

        public RepertoireItem(int order, string repName, string repType, string repTime, string actName, string repBgm,
            string fileRes, string progType, State curState, Visibility start, Visibility stop)
        {
            this._order = order;
            this._repName = repName;
            this._repType = repType;
            this._repTime = repTime;
            this._actName = actName;
            this._repBgm = repBgm;
            this._fileRes = fileRes;
            this._progType = progType;
            this._curState = curState;
            Start = start;
            Stop = stop;
        }

        #region Properties


        /// <summary>
        /// 表演顺序
        /// </summary>
        public int Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 表演名称
        /// </summary>
        public string RepName
        {
            get => _repName;
            set
            {
                _repName = value;
                OnPropertyChanged();
            }

        }

        /// <summary>
        /// 表演类型
        /// </summary>
        public string RepType
        {
            get => _repType;
            set
            {
                _repType = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 表演时间
        /// </summary>
        public string RepTime
        {
            get => _repTime;
            set
            {
                _repTime = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 表演者名字
        /// </summary>
        public string ActName
        {
            get => _actName;
            set
            {
                _actName = value;
                OnPropertyChanged();
            }

        }

        /// <summary>
        /// 背景音乐
        /// </summary>
        public string RepBgm
        {
            get => _repBgm;
            set
            {
                _repBgm = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 文件地址
        /// </summary>
        public string FileRes
        {
            get => _fileRes;
            set
            {
                _fileRes = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 播放资源程序名称
        /// </summary>
        public string ProgType
        {
            get => _progType;
            set
            {
                _progType = value;
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
        /// <summary>
        /// 序号列宽度
        /// </summary>
        public double OrderWidth
        {
            get => _orderWidth;
            set
            {
                _orderWidth = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 表演名称列宽度
        /// </summary>
        public double RepNameWidth
        {
            get => _repNameWidth;
            set
            {
                _repNameWidth = value;
                OnPropertyChanged();
            }
        }

        public double RepTimeWidth
        {
            get => _repTimeWidth;
            set
            {
                _repTimeWidth = value;
                OnPropertyChanged();
            }
        }

        public double RepTypeWidth
        {
            get => _repTypeWidth;
            set
            {
                _repTypeWidth = value;
                OnPropertyChanged();
            }
        }

        public double ActNameWidth
        {
            get => _actNameWidth;
            set
            {
                _actNameWidth = value;
                OnPropertyChanged();
            }
        }

        public double RepBgmWidth
        {
            get => _repBgmWidth;
            set
            {
                _repBgmWidth = value;
                OnPropertyChanged();
            }
        }


        public double FileResWidth
        {
            get => _fileResWidth;
            set
            {
                _fileResWidth = value;
                OnPropertyChanged();
            }
        }

        public double ProgTypeWidth
        {
            get => _progTypeWidth;
            set
            {
                _progTypeWidth = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Function

        public void SetEveryColumnWidth(double orderWidth, double reqNameWidth, double repTimeWidth, double repTypeWidth,
            double actNameWidth, double repBgmWidth, double fileResWidth, double progTypeWidth)
        {
            int noWidth = 0;
            if (noWidth != orderWidth)
                OrderWidth = orderWidth;
            if (noWidth != reqNameWidth)
                RepNameWidth = reqNameWidth;
            if (noWidth != repTimeWidth)
                RepBgmWidth = repBgmWidth;
            if (noWidth != repTimeWidth)
                RepTimeWidth = repTimeWidth;
            if (noWidth != repTypeWidth)
                RepTypeWidth = repTypeWidth;
            if (noWidth != actNameWidth)
                ActNameWidth = actNameWidth;
            if (noWidth != fileResWidth)
                FileResWidth = fileResWidth;
            if (noWidth != progTypeWidth)
                ProgTypeWidth = progTypeWidth;
        }

        #endregion

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
