using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class UserInactiveException : InvalidOperationException
    {
        public UserInactiveException() : base("User not found or already inactive.") { }
    }
}
