namespace TestBackend.AppEnums
{
    public enum LeaveStatus
    {
        Pending = 0, 
        Success, 
        Rejected
    }

    public enum LeaveType
    {
        Sick = 0,
        Annual,
        Study,
        Maternal
    }

    public enum NatureOfLeave
    {
        Local = 0,
        Travel
    }
}
