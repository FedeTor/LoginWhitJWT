using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IDto
{
    public interface ITokenHistoryDto
    {
        int Id { get; }
        string AccessToken { get; }
        string RefreshToken { get; }
        DateTime TokenCreatedDate { get; }
        DateTime TokenExpiratedDate { get; }
        int UserId { get; }
    }
}
