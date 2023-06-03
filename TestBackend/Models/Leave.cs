using HWebAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestBackend.AppEnums;

namespace TestBackend.Models
{
    public class Leave
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public LeaveType LeaveType { get; set; }

        [Required]
        public NatureOfLeave NatureOfLeave { get; set; }
        [Required]
        public LeaveStatus LeaveStatus { get; set; }

        [Required]
        public string? Reason { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public virtual UserModel User { get; set; }
    }
}
