using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public string CompanyId { get; set; }
        [Required, MaxLength(10)]
        public string CompanyName { get; set; }
    }
}