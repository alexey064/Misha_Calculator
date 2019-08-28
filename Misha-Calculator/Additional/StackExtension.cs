using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misha_Calculator.Additional
{
    public static class StackExtension
    {
        public static bool IsEmpty(this Stack<string> stack)
        {
            return stack.Count == 0;
        }
        public static bool IsEmpty(this Stack<double> stack)
        {
            return stack.Count == 0; ;
        }
    }
}
