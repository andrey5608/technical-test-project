using System;
using System.Threading.Tasks;

namespace TestProject.DataAccess
{
    public interface ITokenRepository
    {
        Task<bool> IsValidTokenAsync(Guid token);
    }
}