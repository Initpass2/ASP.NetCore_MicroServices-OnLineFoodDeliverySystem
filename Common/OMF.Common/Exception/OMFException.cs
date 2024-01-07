using System;
using System.Collections.Generic;
using System.Text;

namespace OMF.Common.Exception
{
    public class OMFException : System.Exception
    {
        public string Code { get; }
        public OMFException() { }
        public OMFException(string code) { Code = code; }
        public OMFException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        public OMFException(string code, string message, params object[] args)
          : this(null, code, message, args)
        {

        }

        public OMFException(System.Exception innerException, string message, params object[] args)
          : this(innerException, string.Empty, message, args)
        {

        }

        public OMFException(System.Exception innerException, string code, string message, params object[] args)
         : base(string.Format(message, args), innerException)
        {
        }


    }
}
