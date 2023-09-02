using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PS1_MIC_090_Core.Models
{
    public class CreateApplicationViewModel
    {

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "FirstName")]
        [StringLength(int.MaxValue, MinimumLength = 2)]
        public string? FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "MiddleName")]
        [StringLength(int.MaxValue, MinimumLength = 2)]
        public string? MiddleName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LastName")]
        [StringLength(int.MaxValue, MinimumLength = 2)]
        public  string? LastName { get; set; }
        [Required]
        [Display(Name = "Suffix")]
        public string? Suffix { get; set; }
        [DisplayName("DateOfBirth(MM/dd/yyyy)")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DOB { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Gender")]
        [StringLength(int.MaxValue, MinimumLength = 2)]
        public string? Gender { get; set; }
        public int UserId { get; set; }
        public List<SelectListItem> Suffixs { get; internal set; }

        public CreateApplicationViewModel(List<SelectListItem> suffixs)
        {
            Suffixs = suffixs;
            FirstName = string.Empty;
            LastName = string.Empty;
            Suffix = string.Empty;
            Gender = string.Empty;
        }
    }
}
