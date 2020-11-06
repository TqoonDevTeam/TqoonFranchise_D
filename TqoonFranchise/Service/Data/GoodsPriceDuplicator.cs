using Adprint.Goods.Model;
using Adprint.GoodsPrice.Model;
using Adprint.PartnerGoodsPrice.Model;
using System.Collections.Generic;
using System.Linq;
using TqoonFranchise.Model;

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
                var relatedCItem = relatedCList.Where(t => t.Model.Id == mItem.CalcInfoId).Single();
                var tItem = mItem.Clone<CalcDataItem>();
                tItem.CalcInfoId = relatedCItem.Target.Id;
                tItem.OwnedJoinerId = tJoinerId;
                tItem.Id = TCod.Insert<CalcDataItem>(tItem);

                var cItem = new CopyItem<CalcDataItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }
    }
}