using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ödipussy
{
    public static class Utilities
    {
        public static bool IsNumericType(this Type t)
        {
            return typeof(int) == t
            || typeof(double) == t;
        }

        public static int GetClosingBracket(string expression)
        {
            int bracketCounter = 0;
            bool hasOpened = false;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression.ElementAt(i) == '(')
                {
                    hasOpened = true;
                    bracketCounter++;
                }
                if (expression.ElementAt(i) == ')')
                {
                    bracketCounter--;
                }
                if (bracketCounter == 0 && hasOpened)
                {
                    return i;
                }
            }
            return -1;
        }
    }
    public class RuleHelper
    {
        public static readonly string[] FUNCTION_NAMES = { "contains", "digitsum", "abs", "isprime", "iseven" };
        public static bool IsValid(string regex, int index, double number)
        {
            string evaluatedRegex = regex
                .Replace("$x", number.ToString())
                .Replace("$i", index.ToString());
            return EvaluateBoolean(EvaluateExpression(evaluatedRegex));
        }

        public static string EvaluateExpression(string expression)
        {
            foreach (var functionName in FUNCTION_NAMES)
            {
                if (expression.Contains(functionName))
                {
                    int startingIndex = expression.IndexOf(functionName);
                    int endingIndex = Utilities.GetClosingBracket(expression);
                    string fullFunction = expression.Substring(startingIndex, (endingIndex - startingIndex) + 1);
                    switch (functionName)
                    {
                        case "contains":
                            expression = expression.Replace(fullFunction, ContainsFunction.EvaluateExpression(fullFunction).ToString());
                            return EvaluateExpression(expression);
                        case "digitsum":
                            expression = expression.Replace(fullFunction, DigitSumFunction.EvaluateExpression(fullFunction).ToString());
                            return EvaluateExpression(expression);
                        case "abs":
                            expression = expression.Replace(fullFunction, AbsFunction.EvaluateExpression(fullFunction).ToString());
                            return EvaluateExpression(expression);
                        case "isprime":
                            expression = expression.Replace(fullFunction, IsPrimeFunction.EvaluateExpression(fullFunction).ToString());
                            return EvaluateExpression(expression);
                        case "iseven":
                            expression = expression.Replace(fullFunction, IsEvenFunction.EvaluateExpression(fullFunction).ToString());
                            return EvaluateExpression(expression);
                    }
                }
            }
            //if (expression.Contains(FUNCTION_CONTAINS))
            //{
            //    int startingBracketIndex = expression.IndexOf(FUNCTION_CONTAINS) + FUNCTION_CONTAINS.Length;
            //    int closingBracketIndex = Utilities.GetClosingBracket(expression, startingBracketIndex);
            //    string argumentFunction = expression.Substring(startingBracketIndex + 1, closingBracketIndex - (startingBracketIndex + 1));
            //    int commaIndex = argumentFunction.IndexOf(',') + startingBracketIndex + 1;
            //    string firstArgument = expression.Substring(startingBracketIndex + 1, commaIndex - (startingBracketIndex + 1));
            //    string secondArgument = expression.Substring(commaIndex + 1, closingBracketIndex - (commaIndex + 1));
            //    if (firstArgument.Contains(secondArgument))
            //    {
            //        expression = expression.Replace($"{FUNCTION_CONTAINS}({firstArgument},{secondArgument})", "true");
            //    }
            //    else
            //    {
            //        expression = expression.Replace($"{FUNCTION_CONTAINS}({firstArgument},{secondArgument})", "false");
            //    }
            //}

            return expression;
        }

        public static double EvaluateDouble(string expression)
        {
            DataTable table = new DataTable();
            table.Columns.Add("expression", typeof(string), expression);
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }
        public static bool EvaluateBoolean(string expression)
        {
            DataTable table = new System.Data.DataTable();
            table.Columns.Add("", typeof(bool));
            table.Columns[0].Expression = expression;

            DataRow r = table.NewRow();
            table.Rows.Add(r);
            bool result = (Boolean)r[0];
            return result;
        }
    }
}
