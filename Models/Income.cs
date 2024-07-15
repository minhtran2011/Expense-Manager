using DoAn1.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn1.Models
{
	public class Income
	{
		[Key]	
		public int Id { get; set; }
		[Required]
		public int Amount { get; set; }
		[Required]
		public DateTime	Date { get; set; } = DateTime.Now;
		public string? Description { get; set; } = "";
		public string? UserId { get; set; }
		[ForeignKey("UserId")]
		public AppUser? AppUser { get; set; }
	}
}
