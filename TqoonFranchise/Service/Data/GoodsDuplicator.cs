using Adprint.CalcData.Model;
using Adprint.Category.Model;
using Adprint.DeliveryDate.Model;
using Adprint.DeliveryWeek.Model;
using Adprint.Goods.Model;
using Adprint.PartnerGoods.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class GoodsDuplicator
    {
        
        private IList<CopyItem<GoodsItem>> InsertGoods()
        {
            throw new NotImplementedException();
        }

        public IList<CopyItem<GoodsItem>> DoIt(IList<CopyItem<CategoryItem>> cCategoryItems, 
                                                IList<CopyItem<DeliveryDateItem>> cDeliveryDateItems, 
                                                IList<CopyItem<CalcDataItem>> cCalcDataItems, 
                                                IList<CopyItem<DeliveryWeekItem>> cDeliveryWeekItems, 
                                                IList<CopyItem<PartnerGoodsItem>> cPartnerGoodsItems)
        {
            throw new NotImplementedException();
        }
    } 
}
