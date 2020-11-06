using Adprint.PartnerGoods.Model;
using Adprint.PartnerGoodsPrice.Model;
using JangBoGo.Info.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class PartnerGoodsPriceDuplicator : AbstractDataDuplicator
    {
        public IList<CopyItem<PartnerGoodsPriceItem>> DoIt(IList<CopyItem<PartnerGoodsItem>> relatedCList)
        {
            var relatedMList = relatedCList.Select(t => t.Model).ToList();
            IList<CopyItem<PartnerGoodsPriceItem>> cList = new List<CopyItem<PartnerGoodsPriceItem>>();
            IList<PartnerGoodsPriceItem> mList = GetMList(relatedMList);

            foreach (var mItem in mList)
            {
                var relatedCItem = relatedCList.Where(t => t.Model.Id == mItem.PartnerGoodsId).Single();
                var tItem = mItem.Clone<PartnerGoodsPriceItem>();
                tItem.PartnerGoodsId = relatedCItem.Target.Id;
                tItem.Id = TCod.Insert<PartnerGoodsPriceItem>(tItem);

                var cItem = new CopyItem<PartnerGoodsPriceItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }
        private IList<PartnerGoodsPriceItem> GetMList(IList<PartnerGoodsItem> relatedMList)
        {

            string Ids = relatedMList.Select(t => t.Id).ToInQueryParam();
            return MCod.Query<PartnerGoodsPriceItem>(new ListQuery<PartnerGoodsPriceItem>
            {
                Query = $"SELECT * FROM PartnerGoodsPrice WHERE state='REG' AND PartnerGoodsId IN (@Ids)",
                DbParam = new { Ids }
            });
        }
    }
}
