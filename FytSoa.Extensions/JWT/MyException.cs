using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FytSoa.Extensions
{
    [Serializable]
    public class MyException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        private int _code;
        /// <summary>
        /// 
        /// </summary>
        public MyException()
        {
            _code = 400;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        public MyException(string message, int code = 400)
            : base(message)
        {
            _code = code;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        /// <param name="code"></param>
        public MyException(string message, Exception inner, int code = 400)
            : base(message, inner)
        {
            _code = code;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetCode()
        {
            return _code;
        }
    }
}
