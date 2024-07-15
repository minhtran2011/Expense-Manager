using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoAn1.Models.View_Models
{
    public class IncomeVM
    {
        public Income Income { get; set; }
        //[ValidateNever]
        //public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
