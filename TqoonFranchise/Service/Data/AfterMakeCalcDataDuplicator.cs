using Adprint.AfterMakeCalcData.Model;
using Adprint.AfterMakeCalcInfo.Model;
using JangBoGo.Info.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class AfterMakeCalcDataDuplicator : AbstractDataDuplicator
    {
        public IList<CopyItem<AfterMakeCalcDataItem>> DoIt(IList<CopyItem<AfterMakeCalcInfoItem>> relatedCList)
        {
            var relatedMList = relatedCList.Select(t => t.Model).ToList();
            IList<CopyItem<AfterMakeCalcDataItem>> cList = new List<CopyItem<AfterMakeCalcDataItem>>();
            IList<AfterMakeCalcDataItem> mList = GetMList(relatedMList);

            foreach (var mItem in mList)
            {
                var relatedCItem = relatedCList.Where(t => t.Model.Id == mItem.CalcInfoId).Single();
                var tItem = mItem.Clone<AfterMakeCalcDataItem>();
                tItem.CalcInfoId = relatedCItem.Target.Id;
                tItem.Id = TCod.Insert<AfterMakeCalcDataItem>(tItem);

                var cItem = new CopyItem<AfterMakeCalcDataItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }
        private IList<AfterMakeCalcDataItem> GetMList(IList<AfterMakeCalcInfoItem> relatedMList)
        {
            
            string Ids = relatedMList.Select(t => t.Id).ToInQueryParam();
            return MCod.Query<AfterMakeCalcDataItem>(new ListQuery<AfterMakeCalcDataItem>
            {
                Query = $"SELECT * FROM AfterMakeCalcData WHERE calcInfoId IN (@Ids)",
                DbParam = new { Ids }
            });
        }
    }
}
