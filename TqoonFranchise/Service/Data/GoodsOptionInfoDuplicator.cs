using Adprint.GoodsOptionInfo.Model;
using JangBoGo.Info.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class GoodsOptionInfoDuplicator : AbstractDataDuplicator
    {
        public IList<CopyItem<GoodsOptionInfoItem>> DoIt(int mJoinerId, int tJoinerId)
        {
            var mList = GetMList(mJoinerId);
            var cList = new List<CopyItem<GoodsOptionInfoItem>>();
            foreach (var mItem in mList)
            {
                var tItem = mItem.Clone<GoodsOptionInfoItem>();
                tItem.JoinerId = tJoinerId;
                tItem.Id = TCod.Insert<GoodsOptionInfoItem>(tItem);

                var cItem = new CopyItem<GoodsOptionInfoItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }

        private IList<GoodsOptionInfoItem> GetMList(int joinerId)
        {
            string query = "SELECT * FROM GoodsOptionInfo WHERE joinerId=@joinerId";
            return MCod.Query<GoodsOptionInfoItem>(new ListQuery<GoodsOptionInfoItem>
            {
                Query = query,
                DbParam = new { joinerId }
            });
        }
    }
}
