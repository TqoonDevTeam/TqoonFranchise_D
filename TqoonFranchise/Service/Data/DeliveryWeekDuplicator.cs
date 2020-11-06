using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adprint.DeliveryDate.Model;
using TqoonFranchise.Model;
using Adprint.DeliveryWeek.Model;
using JangBoGo.Info.Object;

namespace TqoonFranchise.Service.Data
{
    public class DeliveryWeekDuplicator : AbstractDataDuplicator
    {
        public IList<CopyItem<DeliveryWeekItem>> DoIt(int mJoinerId, int tJoinerId)
        {
            var mList = GetMList(mJoinerId);
            var cList = new List<CopyItem<DeliveryWeekItem>>();
            foreach (var mItem in mList)
            {
                var tItem = mItem.Clone<DeliveryWeekItem>();
                tItem.JoinerId = tJoinerId;
                tItem.Id = TCod.Insert<DeliveryWeekItem>(tItem);

                var cItem = new CopyItem<DeliveryWeekItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }
        private IList<DeliveryWeekItem> GetMList(int joinerId)
        {
            string query = "SELECT * FROM DeliveryWeek WHERE joinerId=@joinerId";
            return MCod.Query<DeliveryWeekItem>(new ListQuery<DeliveryWeekItem>
            {
                Query = query,
                DbParam = new { joinerId }
            });
        }
    }
}
