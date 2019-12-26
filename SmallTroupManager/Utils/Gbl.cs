using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElCommon.Util;
namespace SmallTroupManager.Utils
{
    public class Gbl : IntrinsicCfg
    {
        public Gbl()
        {
            ExternalKuGou = "";
        }

        public string ExternalKuGou { get; set; }
    }
}
