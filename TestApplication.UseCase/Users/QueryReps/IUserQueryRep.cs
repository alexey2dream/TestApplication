using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.UseCase.Users.DTO;

namespace TestApplication.UseCase.Users.QueryReps
{
    public interface IUserQueryRep
    {
        Task<List<UserResponse>> GetAll(int pageNum, int pageSize, CancellationToken token = default);
    }
}
