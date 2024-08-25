using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stu_card_entity_store.Store
{
    public interface IDeleteStore
    {
        public bool IsDeleted { get; set; }
    }
}
