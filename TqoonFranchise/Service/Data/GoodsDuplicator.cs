using Adprint.CalcData.Model;
using Adprint.Category.Model;
using Adprint.DeliveryDate.Model;
using Adprint.DeliveryWeek.Model;
using Adprint.Goods.Model;
using Adprint.PartnerGoods.Model;
using JangBoGo.Info.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class GoodsDuplicator : AbstractDataDuplicator
    {
        
        private IList<CopyItem<GoodsItem>> InsertGoods()
        {
            throw new NotImplementedException();
        }

        public IList<CopyItem<GoodsItem>> DoIt(int mJoinerId,int tJoinerId,
                                                IList<CopyItem<CategoryItem>> relatedCCategoryList, 
                                                IList<CopyItem<DeliveryDateItem>> relatedCDeliveryDateList, 
                                                IList<CopyItem<CalcDataItem>> relatedCCalcDataList, 
                                                IList<CopyItem<DeliveryWeekItem>> relatedCDeliveryWeekList, 
                                                IList<CopyItem<PartnerGoodsItem>> relatedCPartnerGoodsList)
        {
            var relatedMCategoryList = relatedCCategoryList.Select(t => t.Model).ToList();
            var mList = GetMList(relatedMCategoryList);
            var cList = new List<CopyItem<GoodsItem>>();
            var mJoiner = GetMJoiner(mJoinerId);
            var tJoiner = GetTJoiner(tJoinerId);

            foreach (var mItem in mList)
            {
                var tItem = mItem.Clone<GoodsItem>();
                tItem.CategoryId = GetTargetCategoryId(relatedCCategoryList,tItem.CategoryId);
                tItem.TypeId = 1;
                tItem.DeliveryDateId = GetTargetDeliveryDateId(relatedCDeliveryDateList, tItem.DeliveryDateId);
                tItem.CalcDataId = GetTargetCalcDataId(relatedCCalcDataList, tItem.CalcDataId); 
                tItem.DeliveryWeekId = GetTargetDeliveryWeekId(relatedCDeliveryWeekList, tItem.DeliveryWeekId);
                tItem.PartnerGoodsId = GetTargetPartnerGoodsId(relatedCPartnerGoodsList, tItem.PartnerGoodsId);
                tItem.CodePath = tJoiner.SiteCode + tItem.CodePath.Substring(2); 
                tItem.Id = TCod.Insert<GoodsItem>(tItem);

                var cItem = new CopyItem<GoodsItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
            throw new NotImplementedException();
        }

        private int GetTargetPartnerGoodsId(IList<CopyItem<PartnerGoodsItem>> relatedCPartnerGoodsList, int partnerGoodsId)
        {
            var relatedCItem = relatedCPartnerGoodsList.Where(t => t.Model.Id == partnerGoodsId).Single();
            return relatedCItem.Target.Id;
        }

        private int GetTargetDeliveryWeekId(IList<CopyItem<DeliveryWeekItem>> relatedCDeliveryWeekList, int deliveryWeekId)
        {
            var relatedCItem = relatedCDeliveryWeekList.Where(t => t.Model.Id == deliveryWeekId).Single();
            return relatedCItem.Target.Id;
        }

        private int GetTargetCalcDataId(IList<CopyItem<CalcDataItem>> relatedCCalcDataList, int calcDataId)
        {
            var relatedCItem = relatedCCalcDataList.Where(t => t.Model.Id == calcDataId).Single();
            return relatedCItem.Target.Id;
        }

        private int GetTargetDeliveryDateId(IList<CopyItem<DeliveryDateItem>> relatedCDeliveryDateList, int deliveryDateId)
        {
            var relatedCItem = relatedCDeliveryDateList.Where(t => t.Model.Id == deliveryDateId).Single();
            return relatedCItem.Target.Id;
        }

        private int GetTargetCategoryId(IList<CopyItem<CategoryItem>> relatedCCategoryList, int categoryId)
        {
            var relatedCItem = relatedCCategoryList.Where(t => t.Model.Id == categoryId).Single();
            return relatedCItem.Target.Id;
        }

        private IList<GoodsItem> GetMList(IList<CategoryItem> relatedMCategoryList)
        {

            string Ids = relatedMCategoryList.Select(t => t.Id).ToInQueryParam();
            return MCod.Query<GoodsItem>(new ListQuery<GoodsItem>
            {
                Query = $"SELECT * FROM tblGoods WHERE state='REG' AND intCategoryNum IN (@Ids)",
                DbParam = new { Ids }
            });
        }
    } 
}
