using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Entities.IdntityUser;
using Services.Autorizes;

namespace Services.Interfaces
{
    public interface IJWTService
    {
        Task<AccessToken> GenerateAsync(User user);
    }
}