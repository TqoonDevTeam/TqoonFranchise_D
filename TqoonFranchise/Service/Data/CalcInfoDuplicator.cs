using Adprint.CalcInfo.Model;
using JangBoGo.Info.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class CalcInfoDuplicator : AbstractDataDuplicator
    {
        public IList<CopyItem<CalcInfoItem>> DoIt(int mJoinerId, int tJoinerId)
        {
            var mList = GetMList(mJoinerId);
            var cList = new List<CopyItem<CalcInfoItem>>();
            foreach (var mItem in mList)
            {
                var tItem = mItem.Clone<CalcInfoItem>();
                tItem.JoinerId = tJoinerId;
                tItem.Id = TCod.Insert<CalcInfoItem>(tItem);

                var cItem = new CopyItem<CalcInfoItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }
        private IList<CalcInfoItem> GetMList(int joinerId)
        {
            string query = "SELECT * FROM CalcInfo WHERE joinerId=@joinerId";
            return MCod.Query<CalcInfoItem>(new ListQuery<CalcInfoItem>
            {
                Query = query,
                DbParam = new { joinerId }
            });
        }
    }
}
