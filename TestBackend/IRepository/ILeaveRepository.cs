using HWebAPI.Models;
using TestBackend.DTOs.LeavesDTOs;

namespace TestBackend.IRepository
{
    public interface ILeaveRepository
    {
        Task<ServiceResponse<LeaveAggregates>> GetLeavesCount(string userName);

        Task<ServiceResponse<IReadOnlyList<LeavesDto>>> GetAllLeaves();
    }
}
