using JangBoGo.Info.Object;
using Spring.Transaction.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service
{
    public class DataDuplicator : IDoIt
    {
        public ICommonObjectDao MCod { get; set; }
        public ICommonObjectDao TCod { get; set; }
        public FranchiseParam Param { get; set; }
        [Transaction()]
        public void DoIt()
        {

        }
    }
}
