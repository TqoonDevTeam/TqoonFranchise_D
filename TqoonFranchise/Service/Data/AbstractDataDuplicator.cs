using JangBoGo.Info.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class AbstractDataDuplicator<T>
    {
        public ICommonObjectDao MCod { get; set; }
        public ICommonObjectDao TCod { get; set; }
        public FranchiseParam Param { get; set; }
        public string SelectQuery { get; set; }
        public IList<CopyItem<T>> DoIt() {
            IList<CopyItem<T>> list = new List<CopyItem<T>>();
            IList<T> modelItems = GetModelItems();
            foreach(T mItem in modelItems)
            {
                CopyItem<T> copyItem = new CopyItem<T>();
                copyItem.Model = mItem;
                copyItem.Target = InsertTarget(mItem);
                list.Add(copyItem);
            }
            return list;
        }
       
        private T InsertTarget(T mItem)
        {
            
            int id =TCod.Insert<T>(mItem);
            return mItem;            
        }

        private IList<T> GetModelItems()
        {
            return TCod.Query<T>(new ListQuery<T>
            {
                Query = "",
                DbParam = null

            });
        }
    }
}
