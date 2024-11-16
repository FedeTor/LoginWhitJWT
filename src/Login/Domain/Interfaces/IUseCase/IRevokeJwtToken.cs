using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IUseCase
{
    public interface IRevokeJwtToken
    {
        public Task<bool> InvalidateToken(int id);
    }
}
