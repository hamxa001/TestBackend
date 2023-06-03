using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TestBackend.AppEnums;
using FastEnumUtility;

namespace TestBackend.DTOs.LeavesDTOs
{
    public class LeavesDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public LeaveType LeaveType { get; set; }
        public NatureOfLeave NatureOfLeave { get; set; }
        public LeaveStatus LeaveStatus { get; set; }

        public string? Reason { get; set; }
    }
}
