using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adprint.Category.Model;
using Adprint.DeliveryDate.Model;
using TqoonFranchise.Model;
using JangBoGo.Info.Object;

namespace TqoonFranchise.Service.Data
{
    public class DeliveryDateDuplicator : AbstractDataDuplicator
    {
        
        public IList<CopyItem<DeliveryDateItem>> DoIt(IList<CopyItem<CategoryItem>> relatedCList, int mJoinerId, int tJoinerId)
        {
            var mList = GetMList(mJoinerId);
            var cList = new List<CopyItem<DeliveryDateItem>>();
            foreach (var mItem in mList)
            {
                var tItem = mItem.Clone<DeliveryDateItem>();
                tItem.JoinerId = tJoinerId;
                var relatedCItem = relatedCList.Where(t => t.Model.Id == mItem.IntCateGoryNum).Single();
                tItem.IntCateGoryNum = relatedCItem.Target.Id;

                tItem.Id = TCod.Insert<DeliveryDateItem>(tItem);

                var cItem = new CopyItem<DeliveryDateItem>()
                {
                    Model = mItem,
                    Target = tItem
                };
                cList.Add(cItem);
            }
            return cList;
        }

        private IList<DeliveryDateItem> GetMList(int joinerId)
        {
            string query = "SELECT * FROM tblDeliveryDate WHERE strState='REG' AND joinerId=@joinerId";
            return MCod.Query<DeliveryDateItem>(new ListQuery<DeliveryDateItem>
            {
                Query = query,
                DbParam = new { joinerId }
            });
        }
    }
}
