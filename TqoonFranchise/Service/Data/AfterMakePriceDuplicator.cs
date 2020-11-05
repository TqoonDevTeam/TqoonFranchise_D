using Adprint.AfterMake.Model;
using Adprint.AfterMakeCalcData.Model;
using Adprint.AfterMakePrice.Model;
using JangBoGo.Info.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class AfterMakePriceDuplicator : AbstractDataDuplicator
    {
        public IList<CopyItem<AfterMakePriceItem>> DoIt(IList<CopyItem<AfterMakeCalcDataItem>> relatedCalcDataCList,
                                                         IList<CopyItem<AfterMakeItem>> relatedAfterMakeCList)
        {
            var relatedCalcDataMList = relatedCalcDataCList.Select(t => t.Model).ToList();
            var relatedAfterMakeMList = relatedAfterMakeCList.Select(t => t.Model).ToList();

            IList<AfterMakePriceItem> mList = GetMList(relatedAfterMakeMList);
            IList<CopyItem<AfterMakePriceItem>> cList = new List<CopyItem<AfterMakePriceItem>>();

            foreach (var mItem in mList)
            {
                var relatedCalcDataCItem = relatedCalcDataCList.Where(t => t.Model.Id == mItem.AfterMakeCalcDataId).Single();
                var relatedAfterMakeCItem = relatedAfterMakeCList.Where(t => t.Model.Id == mItem.AfterMakeId).Single();

                var tItem = mItem.Clone<AfterMakePriceItem>();
                tItem.AfterMakeCalcDataId = relatedCalcDataCItem.Target.Id;
                tItem.AfterMakeId = relatedAfterMakeCItem.Target.Id;
                tItem.Id = TCod.Insert<AfterMakePriceItem>(tItem);
                var cItem = new CopyItem<AfterMakePriceItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }
        private IList<AfterMakePriceItem> GetMList(IList<AfterMakeItem> relatedMList)
        {

            string Ids = relatedMList.Select(t => t.Id).ToInQueryParam();
            return MCod.Query<AfterMakePriceItem>(new ListQuery<AfterMakePriceItem>
            {
                Query = $"SELECT * FROM tblAfterMakePrice WHERE intAfterMakeNum IN (@Ids)",
                DbParam = new { Ids }
            });
        }
    }
}
