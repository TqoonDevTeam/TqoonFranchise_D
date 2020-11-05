using Adprint.AfterMakeCalcInfo.Model;
using JangBoGo.Info.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class AfterMakeCalcInfoDuplicator : AbstractDataDuplicator
    {
        public IList<CopyItem<AfterMakeCalcInfoItem>> DoIt(int mJoinerId, int tJoinerId)
        {
            var mList = GetMList(mJoinerId);
            var cList = new List<CopyItem<AfterMakeCalcInfoItem>>();
            foreach (var mItem in mList)
            {
                var tItem = mItem.Clone<AfterMakeCalcInfoItem>();
                tItem.JoinerId = tJoinerId;
                tItem.Id=TCod.Insert<AfterMakeCalcInfoItem>(tItem);

                var cItem = new CopyItem<AfterMakeCalcInfoItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }
        private IList<AfterMakeCalcInfoItem> GetMList(int joinerId)
        {
            string query = "SELECT * FROM AfterMakeCalcInfo WHERE joinerId=@joinerId";
            return MCod.Query<AfterMakeCalcInfoItem>(new ListQuery<AfterMakeCalcInfoItem>
            {
                Query = query,
                DbParam =new { joinerId}
            });               
        }
    }
}
