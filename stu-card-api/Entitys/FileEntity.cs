using stu_card_entity_store.Store;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stu_card_api.Entitys
{
    [Table("StuCard.File")]
    public class FileEntity : CommonEntity
    {
        [Required]
        [DefaultValue("")]
        public string FileUrl { get; set; } = "";

        [Required]
        [DefaultValue("")]
        public string FileName { get; set; } = "";

        [Required]
        [DefaultValue("")]
        public string FileType { get; set; } = "";

        [Required]
        public long FileSize { get; set; }


        [Required]
        [DefaultValue("")]
        public string BuckName { get; set; } = "";
    }
}
