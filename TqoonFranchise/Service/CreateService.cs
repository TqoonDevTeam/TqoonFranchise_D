using JangBoGo.Info.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service
{
    public class CreateService
    {
        public ICommonObjectDao MCod { get; set; }
        public ICommonObjectDao TCod { get; set; }
        public  void DoIt(FranchiseParam param)
        {
            DataDuplicator dataDuplicator = new DataDuplicator() { Param= param };
            dataDuplicator.DoIt();

            FileDuplicator fileDuplicator = new FileDuplicator() { Param = param };
            fileDuplicator.DoIt();
        }
    }
}
