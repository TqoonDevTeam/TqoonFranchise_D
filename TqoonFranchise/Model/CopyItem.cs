using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TqoonFranchise.Model
{
    public class CopyItem<T>
    {
        public T Model { set; get; }
        public T Target { set; get; }

        
    }
}
