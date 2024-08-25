using Microsoft.EntityFrameworkCore.Metadata.Internal;
using stu_card_entity_store.Store;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace stu_card_api.Entitys
{

    [Table("StuCard.TemplateItemShadow")]
    public class TemplateItemShadowEntity : CommonEntity
    {
        [Required]
        public int TemplateEntityItemId { get; set; }

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string Color { get; set; } = string.Empty;
        public double Blur { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public bool AffectStroke { get; set; }
        public bool NonScaling { get; set; }

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string Type { get; set; } = string.Empty;
    }

}
