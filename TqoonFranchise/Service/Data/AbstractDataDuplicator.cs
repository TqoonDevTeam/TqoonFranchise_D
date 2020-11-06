using JangBoGo.Info.Object;
using JangBoGo.Info.Object.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class AbstractDataDuplicator
    {
        public ICommonObjectDao MCod { get; set; }
        public ICommonObjectDao TCod { get; set; }
        
    }
}
