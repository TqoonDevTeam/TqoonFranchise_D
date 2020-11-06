using Adprint.PartnerCodeType.Model;
using JangBoGo.Info.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class PartnerCodeTypeDuplicator : AbstractDataDuplicator
    {
        public IList<CopyItem<PartnerCodeTypeItem>> DoIt(int mJoinerId, int tJoinerId)
        {
            var mList = GetMList(mJoinerId);
            var cList = new List<CopyItem<PartnerCodeTypeItem>>();
            foreach (var mItem in mList)
            {
                var tItem = mItem.Clone<PartnerCodeTypeItem>();
                tItem.JoinerId = tJoinerId;
                tItem.Id = TCod.Insert<PartnerCodeTypeItem>(tItem);

                var cItem = new CopyItem<PartnerCodeTypeItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }
        private IList<PartnerCodeTypeItem> GetMList(int joinerId)
        {
            string query = "SELECT * FROM PartnerCodeType WHERE state='REG' AND joinerId=@joinerId";
            return MCod.Query<PartnerCodeTypeItem>(new ListQuery<PartnerCodeTypeItem>
            {
                Query = query,
                DbParam = new { joinerId }
            });
        }
    }
}
