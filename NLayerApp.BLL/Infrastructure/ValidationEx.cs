using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Infrastructure
{
    public class ValidationEx: Exception
    {
        public string Property { get; protected set; }
        public ValidationEx(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
