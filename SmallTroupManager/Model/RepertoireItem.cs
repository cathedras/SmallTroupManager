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

        public int Order { get => order; set => order = value; }
        public string RepName { get => repName; set => repName = value; }
        public string RepTime { get => repTime; set => repTime = value; }
        public string ActName { get => actName; set => actName = value; }
        public string RepBgm { get => repBgm; set => repBgm = value; }
        public string FileRes { get => fileRes; set => fileRes = value; }
        public string ProgType { get => progType; set => progType = value; }
    }
}
