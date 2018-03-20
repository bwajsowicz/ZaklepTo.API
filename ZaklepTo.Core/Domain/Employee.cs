using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Core.Domain
{
    public class Employee : User
    {
        public Restaurant Restaurant { get; private set; }
    }
}
