using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DoAn1.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser
{
	[Required, Column(TypeName = "nvarchar(50)")]
	public string? Name { get; set; }
	[Required, Column(TypeName = "date")]
	public DateOnly DateOfBirth { get; set; }
	[Required, Column(TypeName = "nvarchar(10)")]
	public string? Gender { get; set; }
}

