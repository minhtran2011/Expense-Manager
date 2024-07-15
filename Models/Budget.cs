using DoAn1.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAn1.Models
{
	public class Budget
	{
		[Key]
		public int Id { get; set; }
		public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		public Category? Category { get; set; }
		[Required]
		public int Amount { get; set; } 
		public DateTime Date { get; set; } = DateTime.Now;
		public string? UserId { get; set; }
		[ForeignKey("UserId")]
		public AppUser? AppUser { get; set; }
	}
}
