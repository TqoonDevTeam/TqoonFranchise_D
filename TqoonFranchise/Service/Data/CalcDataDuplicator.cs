using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adprint.CalcData.Model;
using Adprint.CalcInfo.Model;
using TqoonFranchise.Model;
using JangBoGo.Info.Object;

namespace TqoonFranchise.Service.Data
{
    public class CalcDataDuplicator : AbstractDataDuplicator
    {
        public IList<CopyItem<CalcDataItem>> DoIt(int mJoinerId, int tJoinerId, IList<CopyItem<CalcInfoItem>> relatedCList)
        {
            var relatedMList = relatedCList.Select(t => t.Model).ToList();
            IList<CopyItem<CalcDataItem>> cList = new List<CopyItem<CalcDataItem>>();
            IList<CalcDataItem> mList = GetMList(mJoinerId);

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
        private IList<CalcDataItem> GetMList(int joinerId)
        {
            return MCod.Query<CalcDataItem>(new ListQuery<CalcDataItem>
            {
                Query = $"SELECT * FROM CalcData WHERE ownedJoinerId = @joinerId and state='REG'",
                DbParam = new { joinerId }
            });
        }


    }
}
