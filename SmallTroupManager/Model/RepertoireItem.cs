using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallTroupManager.Model
{
    public class RepertoireItem
    {
        private int order;        
        private string repName;
        private string repTime;
        private string actName;
        private string repBgm;
        private string fileRes;
        private string progType;

        public RepertoireItem()
        {
        }

        public RepertoireItem(int order, string repName, string repTime, string actName, string repBgm, string fileRes, string progType)
        {
            this.order = order;
            this.repName = repName;
            this.repTime = repTime;
            this.actName = actName;
            this.repBgm = repBgm;
            this.fileRes = fileRes;
            this.progType = progType;
        }
        /// <summary>
        /// 表演顺序
        /// </summary>
        public int Order { get => order; set => order = value; }
        /// <summary>
        /// 表演名称
        /// </summary>
        public string RepName { get => repName; set => repName = value; }
        /// <summary>
        /// 表演时间
        /// </summary>
        public string RepTime { get => repTime; set => repTime = value; }
        /// <summary>
        /// 表演者名字
        /// </summary>
        public string ActName { get => actName; set => actName = value; }
        /// <summary>
        /// 背景音乐
        /// </summary>
        public string RepBgm { get => repBgm; set => repBgm = value; }
        /// <summary>
        /// 文件地址
        /// </summary>
        public string FileRes { get => fileRes; set => fileRes = value; }
        /// <summary>
        /// 播放资源程序名称
        /// </summary>
        public string ProgType { get => progType; set => progType = value; }
    }
}
