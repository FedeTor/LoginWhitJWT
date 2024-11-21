using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class EmailAlreadyRegisteredException : InvalidOperationException
    {
        public EmailAlreadyRegisteredException() : base("The e-mail address is already registered.") { }
    }
}
