using stu_card_entity_store.Store;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stu_card_api.Entitys
{
    [Table("StuCard.Template")]
    public class TemplateEntity : CommonEntity
    {
        /// <summary>
        /// 中文学校名称
        /// </summary>
        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string ZhSchoolName { get; set; } = string.Empty;

        /// <summary>
        /// 英文学校名称
        /// </summary>
        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string EnSchoolName { get; set; } = string.Empty;

        /// <summary>
        /// 英文学校简称
        /// </summary>
        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar100)]
        public string SchoolAbbreviation { get; set; } = string.Empty;

        [Required]
        public int FileId { get; set; }

    }
}
