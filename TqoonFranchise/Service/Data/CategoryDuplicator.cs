using Adprint.Category.Model;
using JangBoGo.Info.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class CategoryDuplicator : AbstractDataDuplicator
    {

        public IList<CopyItem<CategoryItem>> DoIt(int mJoinerId, int tJoinerId)
        {
            var mList = GetMList(mJoinerId);
            var cList = new List<CopyItem<CategoryItem>>();
            foreach (var mItem in mList)
            {
                var tItem = mItem.Clone<CategoryItem>();
                //tItem.JoinerId = tJoinerId;
                tItem.Id = TCod.Insert<CategoryItem>(tItem);

                var cItem = new CopyItem<CategoryItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }
        private IList<CategoryItem> GetMList(int joinerId)
        {
            string query = "SELECT * FROM CalcInfo WHERE joinerId=@joinerId";
            return MCod.Query<CategoryItem>(new ListQuery<CategoryItem>
            {
                Query = query,
                DbParam = new { joinerId }
            });
        }
    }
}
