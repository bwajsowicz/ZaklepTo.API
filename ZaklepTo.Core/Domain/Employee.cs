using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Core.Domain
{
    class Employee : User
    {
        public Restaurant Restaurant { get; protected set; }
    }
}
