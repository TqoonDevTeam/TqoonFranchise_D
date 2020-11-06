using System;
using System.Collections.Generic;
using Adprint.PartnerGoods.Model;
using TqoonFranchise.Model;
using JangBoGo.Info.Object;
using Adprint.CalcData.Model;
using System.Linq;

namespace TqoonFranchise.Service.Data
{
    public class PartnerGoodsDuplicator : AbstractDataDuplicator
    {
        public IList<CopyItem<PartnerGoodsItem>> DoIt(int mJoinerId, int tJoinerId, IList<CopyItem<CalcDataItem>> relatedCList)
        {
            //PartnerDeliveryId는 무조건 1
            var mJoiner = GetMJoiner(mJoinerId);
            var tJoiner = GetTJoiner(tJoinerId);

            var mList = GetMList(mJoinerId);
            var cList = new List<CopyItem<PartnerGoodsItem>>();
            foreach (var mItem in mList)
            {
                var tItem = mItem.Clone<PartnerGoodsItem>();
                tItem.JoinerId = tJoinerId;
                tItem.SellerJoinerId = tJoinerId;
                tItem.PartnerDeliveryId = 1;
                tItem.CategoryCodePath = tJoiner.SiteCode + tItem.CategoryCodePath.Substring(2); 
                tItem.Target = "";
                var relatedCItem = relatedCList.Where(t => t.Model.Id == mItem.calcDataID).Single();
                tItem.calcDataID = relatedCItem.Target.Id; 

                tItem.Id = TCod.Insert<PartnerGoodsItem>(tItem);
                var cItem = new CopyItem<PartnerGoodsItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }
        private IList<PartnerGoodsItem> GetMList(int joinerId)
        {
            string query = "SELECT * FROM PartnerGoods WHERE sellState='SALE' AND joinerId=@joinerId";
            return MCod.Query<PartnerGoodsItem>(new ListQuery<PartnerGoodsItem>
            {
                Query = query,
                DbParam = new { joinerId }
            });
        }
    }
}