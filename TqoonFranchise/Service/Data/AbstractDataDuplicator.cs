using Adprint.Joiner.Model;
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

        protected JoinerItem GetTJoiner(int joinerId)
        {
            string query = "SELECT * FROM Joiner WHERE id=@joinerId";
            return TCod.Query<JoinerItem>(new ItemQuery<JoinerItem>
            {
                Query = query,
                DbParam = new { joinerId }
            });
        }
        protected JoinerItem GetMJoiner(int joinerId)
        {
            string query = "SELECT * FROM Joiner WHERE id=@joinerId";
            return MCod.Query<JoinerItem>(new ItemQuery<JoinerItem>
            {
                Query = query,
                DbParam = new { joinerId }
            });
        }
    }
}
