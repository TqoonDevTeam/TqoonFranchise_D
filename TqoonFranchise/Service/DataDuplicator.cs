using Adprint.AfterMake.Model;
using Adprint.CalcData.Model;
using Adprint.Category.Model;
using Adprint.DeliveryDate.Model;
using Adprint.DeliveryWeek.Model;
using Adprint.Goods.Model;
using Adprint.GoodsOption.Model;
using Adprint.GoodsPrice.Model;
using Adprint.PartnerDelivery.Model;
using Adprint.PartnerGoods.Model;
using JangBoGo.Info.Object;
using Spring.Transaction.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;
using TqoonFranchise.Service.Data;

namespace TqoonFranchise.Service
{
    public class DataDuplicator 
    {
        public ICommonObjectDao MCod { get; set; }
        public ICommonObjectDao TCod { get; set; }
        public CategoryDuplicator CategoryDuplicator { get; set; }
        public GoodsDuplicator GoodsDuplicator { get; set; }
        public GoodsPriceDuplicator GoodsPriceDuplicator { get; set; }
        public GoodsOptionDuplicator GoodsOptionDuplicator { get; set; }
        public AfterMakeDuplicator AfterMakeDuplicator { get; set; }
        public PartnerGoodsPriceDuplicator PartnerGoodsPriceDuplicator { get; set; }
        public GoodsOptionInfoDuplicator GoodsOptionInfoDuplicator { get; set; }
        public AfterMakeCalcInfoDuplicator AfterMakeCalcInfoDuplicator { get; set; }
        public AfterMakeCalcDataDuplicator AfterMakeCalcDataDuplicator { get; set; }
        public AfterMakePriceDuplicator AfterMakePriceDuplicator { get; set; }
        public DeliveryDateDuplicator DeliveryDateDuplicator { get; set; }
        public CalcDataDuplicator CalcDataDuplicator { get; set; }
        public DeliveryWeekDuplicator DeliveryWeekDuplicator { get; set; }
        public PartnerGoodsDuplicator PartnerGoodsDuplicator { get; set; }
        public CalcInfoDuplicator CalcInfoDuplicator { get; set; }
        public PartnerCodeTypeDuplicator PartnerCodeTypeDuplicator { get; set; }
        public PartnerCodeDuplicator PartnerCodeDuplicator { get; set; }
        public PartnerPageContentsDuplicator PartnerPageContentsDuplicator { get; set; }
        public FranchiseParam Param { get; set; }

        [Transaction()]
        public void DoIt(int mJoinerId, int tJoinerId)
        {
            // category            
            var CCategoryItems =CategoryDuplicator.DoIt();
            // DeliveryDate
            var CDeliveryDateItems = DeliveryDateDuplicator.DoIt(CCategoryItems, mJoinerId,tJoinerId);
            // DeliveryWeek
            var CDeliveryWeekItems = DeliveryWeekDuplicator.DoIt( mJoinerId, tJoinerId);
            // CalcInfo Bloc
            var CCalcInfoItems = CalcInfoDuplicator.DoIt(mJoinerId,tJoinerId);
            var CCalcDataItems = CalcDataDuplicator.DoIt(mJoinerId,tJoinerId,CCalcInfoItems);
            // PartnerCodeType
            var CPartnerCodeTypeItems = PartnerCodeTypeDuplicator.DoIt(mJoinerId, tJoinerId);
            // PartnerCode
            var CPartnerCodeItems = PartnerCodeDuplicator.DoIt(CPartnerCodeTypeItems);
            //PartnerGoods
            var CPartnerGoodsItems = PartnerGoodsDuplicator.DoIt(mJoinerId,tJoinerId);
            // PartnerGoodsPrice
            var CPartnerGoodsPriceItems = PartnerGoodsPriceDuplicator.DoIt(CPartnerGoodsItems);

            //Goods
            var CGoodsItems = GoodsDuplicator.DoIt(CCategoryItems, 
                                                    CDeliveryDateItems, 
                                                    CCalcDataItems, CDeliveryWeekItems, CPartnerGoodsItems);           
            // GoodsPrice           
            var CGoodsPriceItems = GoodsPriceDuplicator.DoIt(CGoodsItems, CPartnerGoodsPriceItems);

            // GoodsOption
            var CGoodsOptionItems = GoodsOptionDuplicator.DoIt(CPartnerGoodsItems);

            // GoodsOptionInfo
            var CGoodsOptionInfoItems = GoodsOptionInfoDuplicator.DoIt(mJoinerId,tJoinerId);

            // AfterMake
            var CAfterMakeItems = AfterMakeDuplicator.DoIt(CCategoryItems);

            // AfterMakeCalcInfo
            var CAfterMakeCalcInfoItems = AfterMakeCalcInfoDuplicator.DoIt(mJoinerId,tJoinerId);

            // AfterMakeCalcData
            var CAfterMakeCalcDataItems = AfterMakeCalcDataDuplicator.DoIt(CAfterMakeCalcInfoItems);

            // AfterMakePrice
            var CAfterMakePriceItems = AfterMakePriceDuplicator.DoIt(CAfterMakeCalcDataItems, CAfterMakeItems);

            // PartnerPageContents
            var CPartnerPageContentsItems = PartnerPageContentsDuplicator.DoIt(mJoinerId, tJoinerId);

        }
    
        private IList<CopyItem<AfterMakeItem>> CopyAfterMakeItems()
        {
            throw new NotImplementedException();
        }

        private IList<CopyItem<GoodsOptionItem>> CopyGoodsOptionItems()
        {

            throw new NotImplementedException();
        }


       

        
        private IList<CopyItem<DeliveryDateItem>> CopyDeliveryDate(IList<CopyItem<CategoryItem>> cCategoryItems)
        {
            //intCategoryNum,joinerId
            IList<DeliveryDateItem> mItems = GetModelDeliveryItems();
            IList<CopyItem<DeliveryDateItem>> copyItems = null;
            foreach (var item in mItems)
            {
                var cItem = new CopyItem<DeliveryDateItem>();
                var newItem = item.Clone();
                newItem.Id = InsertItem(newItem);
                cItem.Model = item;
                cItem.Target = newItem;
                copyItems.Add(cItem);
            }
            return copyItems;
        }

        private int InsertItem(DeliveryDateItem newItem)
        {
            throw new NotImplementedException();
        }

        private IList<DeliveryDateItem> GetModelDeliveryItems()
        {
            throw new NotImplementedException();
        }

        
    }
}
