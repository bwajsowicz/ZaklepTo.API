using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Core.Exceptions
{
    public class ServiceException : ZaklepToException
    {
        public ServiceException(string code) : base(code)
        {
        }

        public ServiceException(string message, params object[] args) : base(string.Empty, message, args)
        {
        }

        public ServiceException(string code, string message, params object[] args) : base(null, string.Empty, message, args)
        {
        }

        public ServiceException(Exception innerException, string message, params object[] args)
            : base(innerException, string.Empty, message, args)
        {
        }

        public ServiceException(Exception innerException, string code, string message, params object[] args)
           : base(code, string.Format(message, args), innerException)
        {
        }

    }
}
