using Adprint.Category.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqoonFranchise.Model;

namespace TqoonFranchise.Service.Data
{
    public class CategoryDuplicator 
    {
       
        public IList<CopyItem<CategoryItem>> DoIt()
        {
            IList<CopyItem<CategoryItem>> list = new List<CopyItem<CategoryItem>>();
            CopyItem<CategoryItem> item = new CopyItem<CategoryItem>();
            item.Model = new CategoryItem();
            item.Target = new CategoryItem();

            // 모델 카테고리를 가져 온다. 
            // 모델 카테고리를 타겟 카테고리로 하나씩 인서트한다. 
            // 인서트 하면서 타겟 id를 타겟 아이템에 넣는다.
            // 카피 아이템에 모델아이템과 타겟 아이템을 넣는다.

            // 카테고리 카피 아이템 리스트를 반환한다.
            return list;
        }
    }
}
