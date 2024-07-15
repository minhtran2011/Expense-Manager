using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DoAn1.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace DoAn1.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        [Required]
        public int Amount { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Description { get; set; } = "";
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        public string? UserId {  get; set; }
        [ForeignKey("UserId")]
        public AppUser? AppUser { get; set; }
    }
}
