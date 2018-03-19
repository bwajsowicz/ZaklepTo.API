using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Core.Domain
{
    class Owner : User
    {
        public Restaurant Restaurant { get; private set; }
    }
}
