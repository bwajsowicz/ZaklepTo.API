using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ZaklepTo.Core.Exceptions
{
    public abstract class ZaklepToException : Exception
    {
        public string Code { get; }

        protected ZaklepToException()
        {
        }

        public ZaklepToException(string code)
        {
            Code = code;
        }

        public ZaklepToException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        public ZaklepToException(string code, string message, params object[] args) : this(null, string.Empty, message, args)
        {
        }

        public ZaklepToException(Exception innerException, string message, params object[] args) 
            : this(innerException, string.Empty, message, args)
        {
        }

        public ZaklepToException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
