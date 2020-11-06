using Adprint.Category.Model;
using Adprint.Joiner.Model;
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
            var mJoiner = GetMJoiner(mJoinerId);
            var tJoiner = GetTJoiner(tJoinerId);

            var mList = GetMList(mJoinerId);
            var cList = new List<CopyItem<CategoryItem>>();
            foreach (var mItem in mList)
            {
                var tItem = mItem.Clone<CategoryItem>();
                ChangeSiteCode(tJoiner, tItem);
                tItem.Id = TCod.Insert<CategoryItem>(tItem);
                var cItem = new CopyItem<CategoryItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            UpdateRefNum(cList);
            return cList;
        }

        private void UpdateRefNum(IList<CopyItem<CategoryItem>> cList)
        {
            foreach (var item in cList)
            {
                item.Target.GroupNumber = GetTargetId(cList, item.Target.GroupNumber);
                item.Target.Parents = GetNewParents(cList, item.Target.Parents);
                TCod.Update<CategoryItem>(item.Target);
            }                
        }
        private string GetNewParents(IList<CopyItem<CategoryItem>> cList, string parents)
        {
            IList<string> parentsSplits = parents.Split(',');
            if (parentsSplits.Count == 0 && String.IsNullOrEmpty(parentsSplits[0])) return "";

            string newParents = "";
            foreach(var mId in parentsSplits)
            {
                if (!mId.IsNumeric()) throw new ArgumentException("Parents가 숫자가 아닙니다.");
                int newId = GetTargetId(cList, Int32.Parse(mId));
                if (!String.IsNullOrEmpty(newParents)) newParents += ",";
                newParents += newId.ToString();
            }
            return newParents;
        }

        private int GetTargetId(IList<CopyItem<CategoryItem>> cList,int ModelId)
        {
            var cItem = cList.Where(t => t.Model.Id == ModelId).Single();
            return cItem.Target.Id;
        }
        private void ChangeSiteCode(JoinerItem tJoiner, CategoryItem tItem)
        {
            if (tItem.GroupLevel == 0)
            {
                tItem.Code = tJoiner.SiteCode;
                tItem.CodePath = tJoiner.SiteCode;
            }
            else
            {
                tItem.CodePath = tJoiner.SiteCode + tItem.CodePath.Substring(2);
            }
        }

        
        private IList<CategoryItem> GetMList(int joinerId)
        {
            string query = "SELECT * FROM tblCategory WHERE strState='REG' AND joinerId=@joinerId";
            return MCod.Query<CategoryItem>(new ListQuery<CategoryItem>
            {
                Query = query,
                DbParam = new { joinerId }
            });
        }
    }
}
