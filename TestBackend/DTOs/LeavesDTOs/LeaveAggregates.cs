namespace TestBackend.DTOs.LeavesDTOs
{
    public class LeaveAggregates
    {
        public int RemainingLeaves { get; set; }
        public int TotalLeaves { get; set; } = 30;
        public int AvailedLeaved { get; set; }
        public int PendingLeaves { get; set; }
        public int RejectedLeaves { get; set; }
    }
}
