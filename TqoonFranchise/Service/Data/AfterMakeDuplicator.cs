using System;
using System.Collections.Generic;
using Adprint.Category.Model;
using TqoonFranchise.Model;
using Adprint.AfterMake.Model;
using System.Linq;
using JangBoGo.Info.Object;

namespace TqoonFranchise.Service.Data
{
    public class AfterMakeDuplicator : AbstractDataDuplicator
    {
        public IList<CopyItem<AfterMakeItem>> DoIt(IList<CopyItem<CategoryItem>> relatedCList)
        {
            var relatedMList = relatedCList.Select(t => t.Model).ToList();
            IList<CopyItem<AfterMakeItem>> cList = new List<CopyItem<AfterMakeItem>>();
            IList<AfterMakeItem> mList = GetMList(relatedMList);

            foreach (var mItem in mList)
            {
                var relatedCItem = relatedCList.Where(t => t.Model.Id == mItem.CategoryId).Single();
                var tItem = mItem.Clone<AfterMakeItem>();
                tItem.CategoryId = relatedCItem.Target.Id;
                tItem.Id = TCod.Insert<AfterMakeItem>(tItem);

                var cItem = new CopyItem<AfterMakeItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }
        private IList<AfterMakeItem> GetMList(IList<CategoryItem> relatedMList)
        {

            string Ids = relatedMList.Select(t => t.Id).ToInQueryParam();
            return MCod.Query<AfterMakeItem>(new ListQuery<AfterMakeItem>
            {
                Query = $"SELECT * FROM tblAfterMake WHERE intCategoryNum IN (@Ids)",
                DbParam = new { Ids }
            });
        }
    }
}