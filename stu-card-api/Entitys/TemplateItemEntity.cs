using Microsoft.EntityFrameworkCore.Metadata.Internal;
using stu_card_entity_store.Store;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace stu_card_api.Entitys
{
    [Table("StuCard.TemplateItem")]
    public class TemplateItemEntity : CommonEntity
    {
        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string Version { get; set; } = string.Empty;

        [Required]
        [DefaultValue(0)]
        public int TemplateId{ get; set; }
    }

}
