using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stu_card_entity_store.Store
{
    public class CommonEntity : IDeleteStore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
