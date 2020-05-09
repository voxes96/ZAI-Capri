using System;
using System.ComponentModel.DataAnnotations;

namespace Capri.Web.ViewModels.Promoter
{
    public class PromoterRegistration
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [MaxLength(24)]
        public string Password { get; set; }

        [Required]
        [MaxLength(30)]
        public string TitlePrefix { get; set; }

        [MaxLength(30)]
        public string TitlePostfix { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(60)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(60)]
        public string LastName { get; set; }

        [Required]
        public int ExpectedNumberOfBachelorProposals { get; set; }

        [Required]
        public int ExpectedNumberOfMasterProposals { get; set; }

        [Required]
        public int InstituteId { get; set; }
    }
}
