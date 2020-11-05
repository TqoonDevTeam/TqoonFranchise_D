using JangBoGo.Info.Object;
using JangBoGo.Info.Object.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class AbstractDataDuplicator
    {
        public ICommonObjectDao MCod { get; set; }
        public ICommonObjectDao TCod { get; set; }
        //public FranchiseParam Param { get; set; }
        //public string SelectQuery { get; set; }
        //public IList<CopyItem<T>> DoIt() {
        //    IList<CopyItem<T>> list = new List<CopyItem<T>>();
        //    IList<T> modelItems = GetModelItems();
        //    foreach(T mItem in modelItems)
        //    {
        //        CopyItem<T> copyItem = new CopyItem<T>();
        //        copyItem.Model = mItem;
        //        copyItem.Target = InsertTarget(mItem);
        //        list.Add(copyItem);
        //    }
        //    return list;
        //}
       
        //protected T InsertTarget(T mItem)
        //{
            
        //    int id =TCod.Insert<T>(mItem);
        //    return mItem;            
        //}

        //protected IList<T> GetModelItems()
        //{
        //    string tableName = CodQueryUtil.get
        //    return TCod.Query<T>(new ListQuery<T>
        //    {
        //        Query = "",
        //        DbParam = null

        //    });
        //}
    }
}
