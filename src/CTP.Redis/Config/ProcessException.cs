using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Config
{
    public class ProcessException : Exception
    {
        public ProcessException(string msg) : base(msg)
        {

        }
    }
}
