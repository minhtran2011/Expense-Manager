using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DoAn1.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required, NotNull, Column(TypeName = "nvarchar(20)")]
        public string? Name { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        public string Icon { get; set; } = "";
        [NotMapped]
        public string? NameWithIcon
        {
            get
            {
                return this.Icon + " " + this.Name; 
            }
        }
    }
}
