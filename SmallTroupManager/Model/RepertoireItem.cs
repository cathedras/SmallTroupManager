using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
    }


    public class RepertoireItem : RepertoireBase, INotifyPropertyChanged
    {
        private int order;
        private string repName;
        private string repTime;
        private string actName;
        private string repBgm;
        private string fileRes;
        private string progType;
        private State _curState;

        public RepertoireItem()
        {
        }

        public RepertoireItem(int order, string repName, string repTime, string actName, string repBgm, string fileRes,string progType,State curState)
        {
            this.order = order;
            this.repName = repName;
            this.repTime = repTime;
            this.actName = actName;
            this.repBgm = repBgm;
            this.fileRes = fileRes;
            this.progType = progType;
            this._curState = curState;
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

        public State CurState
        {
            get => _curState;
            set
            {
                _curState = value;
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

    public enum State
    {
        Edit,
        Show
    }
}
