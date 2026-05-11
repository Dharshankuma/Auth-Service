using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authservice.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public string ResourceName { get; }

        public NotFoundException(string resourceName) : base($"{resourceName} not found")
        {
            ResourceName = resourceName;
        }
    }
}
