using AutoMapper;
using HWebAPI;
using HWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using TestBackend.AppEnums;
using TestBackend.DTOs.LeavesDTOs;
using TestBackend.IRepository;

namespace TestBackend.Repository
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public LeaveRepository(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IReadOnlyList<LeavesDto>>> GetAllLeaves()
        {
            var serviceResponse = new ServiceResponse<IReadOnlyList<LeavesDto>>();

            try
            {
                var getLeaves = await _context.leaves.Where(x => !x.IsDeleted).ToListAsync();

                serviceResponse.Success = getLeaves.Count > 0;
                serviceResponse.Message = getLeaves.Count > 0 ? "Leaves records Fetched!" : "No records found"; 
                serviceResponse.Data = _mapper.Map<IReadOnlyList<LeavesDto>>(getLeaves);
            }
            catch (Exception)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Unable to get records";
                serviceResponse.Data = null;
                throw;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<LeaveAggregates>> GetLeavesCount(string userName)
        {
            var ServiceResponse = new ServiceResponse<LeaveAggregates>();
            var leavesCounts = new LeaveAggregates();
            try
            {
                await getAggregatedLeaves(leavesCounts);
                ServiceResponse.Data = leavesCounts;
                ServiceResponse.Success = true;
                ServiceResponse.Message = ServiceResponse.Success ? "Dashboard Count Updated" : "Dashboard Count Unsuccessful";
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }

        private async Task getAggregatedLeaves(LeaveAggregates leavesCounts)
        {
            var leaveCounts = await _context.leaves
                                            .GroupBy(l => l.LeaveStatus)
                                            .Select(g => new { LeaveStatus = g.Key, Count = g.Count() })
                                            .ToListAsync();

            foreach (var leave in leaveCounts)
            {
                switch (leave.LeaveStatus)
                {
                    case LeaveStatus.Pending:
                        leavesCounts.PendingLeaves = leave.Count;
                        break;
                    case LeaveStatus.Success:
                        leavesCounts.AvailedLeaved = leave.Count;
                        leavesCounts.RemainingLeaves = leavesCounts.TotalLeaves - leavesCounts.AvailedLeaved;
                        break;
                    case LeaveStatus.Rejected:
                        leavesCounts.RejectedLeaves = leave.Count;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
