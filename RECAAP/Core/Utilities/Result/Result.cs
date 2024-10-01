using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core.Utilities.Result
{
    public class Result : IResult
    {
        public Result(bool success, string message) : this(success)
        {
            Massage = message;
        }

        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }

        public string Massage { get; }
    }
}
