using Adprint.Goods.Model;
using Adprint.GoodsPrice.Model;
using Adprint.PartnerGoodsPrice.Model;
using JangBoGo.Info.Object;
using System.Collections.Generic;
using System.Linq;
using TqoonFranchise.Model;
using System;

namespace TqoonFranchise.Service.Data
{
    public class GoodsPriceDuplicator: AbstractDataDuplicator
    {
        public IList<CopyItem<GoodsPriceItem>> DoIt(IList<CopyItem<GoodsItem>> relatedCGoodsList, 
                                                    IList<CopyItem<PartnerGoodsPriceItem>> relatedCPartnerGoodsPriceList)
        {
            var relatedMGoodsList = relatedCGoodsList.Select(t => t.Model).ToList();
            IList<CopyItem<GoodsPriceItem>> cList = new List<CopyItem<GoodsPriceItem>>();
            IList<GoodsPriceItem> mList = GetMList(relatedMGoodsList);

            foreach (var mItem in mList)
            {
                var tItem = mItem.Clone<GoodsPriceItem>();
                tItem.GoodsId = GetTargetGoodsId(relatedCGoodsList, tItem.GoodsId); 
                tItem.PartnerGoodsPriceId = GetTargetPartnerGoodsPriceId(relatedCPartnerGoodsPriceList, tItem.PartnerGoodsPriceId);
                tItem.Id = TCod.Insert<GoodsPriceItem>(tItem);

                var cItem = new CopyItem<GoodsPriceItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }

        private int GetTargetPartnerGoodsPriceId(IList<CopyItem<PartnerGoodsPriceItem>> relatedCPartnerGoodsPriceList, int partnerGoodsPriceId)
        {
            var relatedCItem = relatedCPartnerGoodsPriceList.Where(t => t.Model.Id == partnerGoodsPriceId).Single();
            return relatedCItem.Target.Id;
        }

        private int GetTargetGoodsId(IList<CopyItem<GoodsItem>> relatedCGoodsList, int goodsId)
        {
            var relatedCItem = relatedCGoodsList.Where(t => t.Model.Id == goodsId).Single();
            return relatedCItem.Target.Id;
        }

        private IList<GoodsPriceItem> GetMList(IList<GoodsItem> relatedMList)
        {

            string Ids = relatedMList.Select(t => t.Id).ToInQueryParam();
            return MCod.Query<GoodsPriceItem>(new ListQuery<GoodsPriceItem>
            {
                Query = $"SELECT * FROM tblGoodsPrice WHERE strState='REG' AND intGoodsNum IN (@Ids)",
                DbParam = new { Ids }
            });
        }
    }
}