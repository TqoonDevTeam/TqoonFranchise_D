using Adprint.AfterMake.Model;
using Adprint.AfterMakeCalcData.Model;
using Adprint.AfterMakePrice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class AfterMakePriceDuplicator
    {
        public IList<CopyItem<AfterMakePriceItem>> DoIt(IList<CopyItem<AfterMakeCalcDataItem>> relatedCCalcDataList,
                                                        IList<CopyItem<AfterMakeItem>> relatedCAfterMakeList)
        {
            throw new NotImplementedException();
        }
    }
}
