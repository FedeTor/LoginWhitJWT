using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.IServices
{
    public interface IRevokeJwtTokenService
    {
        Task<bool> InvalidateTokenService(int id);
    }
}
