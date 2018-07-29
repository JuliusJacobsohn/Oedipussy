using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ödipussy
{
    public class FunctionHelper
    {
        public static IEnumerable<string> GetArguments(string function)
        {
            ///
            /// Get function name
            /// 
            var func = Regex.Match(function, @"\b[^()]+\((.*)\)$");

            //Console.WriteLine("FuncTag: " + func.Value);
            string innerArgs = func.Groups[1].Value;
            ///
            /// Get parameters
            /// 
            var paramTags = Regex.Matches(innerArgs, @"([^,]+\(.+?\))|([^,]+)");

            //Console.WriteLine("Matches: " + paramTags.Count);
            foreach (var item in paramTags)
            {
                yield return item.ToString();
            }
        }
    }

    public class ContainsFunction
    {
        public static bool EvaluateExpression(string function)
        {
            var arguments = FunctionHelper.GetArguments(function);
            return EvaluateExpression(arguments.ElementAt(0), arguments.ElementAt(1));
        }

        public static bool EvaluateExpression(string input, string searchFor)
        {
            return input.Contains(searchFor);
        }
    }

    public class IsPrimeFunction
    {
        public static bool EvaluateExpression(string function)
        {
            var arguments = FunctionHelper.GetArguments(function);
            return EvaluateExpression(int.Parse(arguments.ElementAt(0)));
        }

        public static bool EvaluateExpression(int input)
        {
            return IsPrime(input);
        }
        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
    }

    public class IsEvenFunction
    {
        public static bool EvaluateExpression(string function)
        {
            var arguments = FunctionHelper.GetArguments(function);
            return EvaluateExpression(int.Parse(arguments.ElementAt(0)));
        }

        public static bool EvaluateExpression(int input)
        {
            return input % 2 == 0;
        }
    }

    public class DigitSumFunction
    {
        public static int EvaluateExpression(string function)
        {
            var arguments = FunctionHelper.GetArguments(function);
            return EvaluateExpression(int.Parse(arguments.ElementAt(0)));
        }

        public static int EvaluateExpression(int input)
        {
            int result = 0;
            foreach(char c in input.ToString())
            {
                int i = int.Parse(""+c);
                result += i;
            }
            return result;
        }
    }

    public class AbsFunction
    {
        public static int EvaluateExpression(string function)
        {
            var arguments = FunctionHelper.GetArguments(function);
            return EvaluateExpression(int.Parse(arguments.ElementAt(0)));
        }

        public static int EvaluateExpression(int input)
        {
            return Math.Abs(input);
        }
    }
}
