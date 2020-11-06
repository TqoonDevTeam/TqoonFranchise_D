using Adprint.PartnerCode.Model;
using Adprint.PartnerCodeType.Model;
using JangBoGo.Info.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class PartnerCodeDuplicator : AbstractDataDuplicator
    {
        public IList<CopyItem<PartnerCodeItem>> DoIt(IList<CopyItem<PartnerCodeTypeItem>> relatedCList)
        {
            var relatedMList = relatedCList.Select(t => t.Model).ToList();
            var mList = GetMList(relatedMList);
            var cList = new List<CopyItem<PartnerCodeItem>>();
            foreach (var mItem in mList)
            {
                var tItem = mItem.Clone<PartnerCodeItem>();
                var relatedCItem = relatedCList.Where(t => t.Model.Id == mItem.PartnerCodeTypeId).Single();
                tItem.PartnerCodeTypeId = relatedCItem.Target.Id;
                tItem.Id = TCod.Insert<PartnerCodeItem>(tItem);

                var cItem = new CopyItem<PartnerCodeItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }
        private IList<PartnerCodeItem> GetMList(IList<PartnerCodeTypeItem> relatedMList)
        {

            string Ids = relatedMList.Select(t => t.Id).ToInQueryParam();
            return MCod.Query<PartnerCodeItem>(new ListQuery<PartnerCodeItem>
            {
                Query = $"SELECT * FROM PartnerCode WHERE state='REG' AND partnerCodeTypeId IN (@Ids)",
                DbParam = new { Ids }
            });
        }
    }
}
