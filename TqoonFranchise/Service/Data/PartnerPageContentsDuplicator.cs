using Adprint.PartnerPageContents.Model;
using JangBoGo.Info.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class PartnerPageContentsDuplicator : AbstractDataDuplicator
    {
        public IList<CopyItem<PartnerPageContentsItem>> DoIt(int mJoinerId, int tJoinerId)
        {
            var mList = GetMList(mJoinerId);
            var cList = new List<CopyItem<PartnerPageContentsItem>>();
            foreach (var mItem in mList)
            {
                var tItem = mItem.Clone<PartnerPageContentsItem>();
                tItem.JoinerId = tJoinerId;
                tItem.Id = TCod.Insert<PartnerPageContentsItem>(tItem);

                var cItem = new CopyItem<PartnerPageContentsItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }
        private IList<PartnerPageContentsItem> GetMList(int joinerId)
        {
            string query = "SELECT * FROM PartnerPageContents WHERE state='REG' AND joinerId=@joinerId";
            return MCod.Query<PartnerPageContentsItem>(new ListQuery<PartnerPageContentsItem>
            {
                Query = query,
                DbParam = new { joinerId }
            });
        }
    }
}
