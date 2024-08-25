using Microsoft.EntityFrameworkCore.Metadata.Internal;
using stu_card_entity_store.Store;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace stu_card_api.Entitys
{
    [Table("StuCard.TemplateItemFabric")]
    public class TemplateItemFabricEntity : CommonEntity
    {
        [Required]
        public int TemplateEntityItemId { get; set; }
        public double CropX { get; set; }
        public double CropY { get; set; }

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string Type { get; set; } = string.Empty;

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string Version { get; set; } = string.Empty;

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string OriginX { get; set; } = string.Empty;

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string OriginY { get; set; } = string.Empty;
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string Fill { get; set; } = string.Empty;

        [Required]
        [DefaultValue("{}")]
        [Column(TypeName = ColumnTypes.Json)]
        public string Stroke { get; set; } = string.Empty;
        public double StrokeWidth { get; set; }

        [Required]
        [DefaultValue("{}")]
        [Column(TypeName = ColumnTypes.Json)]
        public string StrokeDashArray { get; set; } = string.Empty;

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string StrokeLineCap { get; set; } = string.Empty;
        public double StrokeDashOffset { get; set; }

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string StrokeLineJoin { get; set; } = string.Empty;
        public bool StrokeUniform { get; set; }
        public double StrokeMiterLimit { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double Angle { get; set; }
        public bool FlipX { get; set; }
        public bool FlipY { get; set; }
        public double Opacity { get; set; }

        [Required]
        [DefaultValue("{}")]
        [Column(TypeName = ColumnTypes.Json)]
        public string Shadow { get; set; } = "{}";
        public bool Visible { get; set; }

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string BackgroundColor { get; set; } = string.Empty;

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string FillRule { get; set; } = string.Empty;

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string PaintFirst { get; set; } = string.Empty;


        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string GlobalCompositeOperation { get; set; } = string.Empty;
        public double SkewX { get; set; }
        public double SkewY { get; set; }

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar2000)]
        public string Src { get; set; } = string.Empty;

        [Required]
        [DefaultValue("{}")]
        [Column(TypeName = ColumnTypes.Json)]
        public string CrossOrigin { get; set; } = "{}";

        [Required]
        [DefaultValue("{}")]
        [Column(TypeName = ColumnTypes.Json)]
        public string Filters { get; set; } = "{}";
        public double FontSize { get; set; }

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string FontWeight { get; set; } = string.Empty;


        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string FontFamily { get; set; } = string.Empty;


        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string FontStyle { get; set; } = string.Empty;
        public double LineHeight { get; set; }

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string Text { get; set; } = string.Empty;

        public double CharSpacing { get; set; }

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string TextAlign { get; set; } = string.Empty;

        [Required]
        [DefaultValue("{}")]
        [Column(TypeName = ColumnTypes.Json)]
        public string Styles { get; set; } = string.Empty;
        public double PathStartOffset { get; set; }

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string PathSide { get; set; } = string.Empty;

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string PathAlign { get; set; } = string.Empty;
        public bool Underline { get; set; }
        public bool Overline { get; set; }
        public bool Linethrough { get; set; }

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string TextBackgroundColor { get; set; } = string.Empty;

        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar200)]
        public string Direction { get; set; } = string.Empty;
    }

}
