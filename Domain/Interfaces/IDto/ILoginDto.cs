using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IDto
{
    public interface ILoginDto
    {
        public string Email { get; }
        public string Password { get; }
    }
}
