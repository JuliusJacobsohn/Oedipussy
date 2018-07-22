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

        public static int GetClosingBracket(string expression, int startingBracketIndex)
        {
            int bracketCounter = 0;
            for (int i = startingBracketIndex; i < expression.Length; i++)
            {
                if (expression.ElementAt(i) == '(')
                {
                    bracketCounter++;
                }
                if (expression.ElementAt(i) == ')')
                {
                    bracketCounter--;
                }
                if (bracketCounter == 0)
                {
                    return i;
                }
            }
            return -1;
        }
    }
    public class RuleHelper
    {
        public const string FUNCTION_CONTAINS = "contains";
        public static bool IsValid(string regex, double number)
        {
            return EvaluateBoolean(EvaluateExpression(regex.Replace("$x", number.ToString())));
        }

        public static string EvaluateExpression(string expression)
        {
            if (expression.Contains(FUNCTION_CONTAINS))
            {
                int startingBracketIndex = expression.IndexOf(FUNCTION_CONTAINS) + FUNCTION_CONTAINS.Length;
                int closingBracketIndex = Utilities.GetClosingBracket(expression, startingBracketIndex);
                string argumentFunction = expression.Substring(startingBracketIndex + 1, closingBracketIndex - (startingBracketIndex + 1));
                int commaIndex = argumentFunction.IndexOf(',') + startingBracketIndex + 1;
                string firstArgument = expression.Substring(startingBracketIndex + 1, commaIndex - (startingBracketIndex + 1));
                string secondArgument = expression.Substring(commaIndex + 1, closingBracketIndex - (commaIndex + 1));
                if (firstArgument.Contains(secondArgument))
                {
                    expression = expression.Replace($"{FUNCTION_CONTAINS}({firstArgument},{secondArgument})", "true");
                }
                else
                {
                    expression = expression.Replace($"{FUNCTION_CONTAINS}({firstArgument},{secondArgument})", "false");
                }
            }

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

    public class DataPair
    {
        public string Data { get; set; }
        public Type Type { get; set; }
        public string TransformationLog { get; set; }
        public bool IsTransformed { get; set; }
        public override string ToString()
        {
            return Data;
            //return $"{Data} ({Type})";
        }
    }

    public interface IRule
    {
        string Regex { get; set; }
        DataPair ApplyRule(DataPair input);
    }

    public class WordSubstitutionRule : IRule
    {
        public string SubstituionWord { get; set; }
        public string Regex { get; set; }
        public DataPair ApplyRule(DataPair input)
        {
            if (input.Type.IsNumericType())
            {
                if (RuleHelper.IsValid(Regex, Convert.ToDouble(input.Data)))
                {
                    var output = new DataPair
                    {
                        Data = SubstituionWord,
                        Type = typeof(string),
                        IsTransformed = true,
                        TransformationLog = $"{input.TransformationLog}Wordsub (matched { Regex }). {input.Data} => {SubstituionWord}\n"
                    };
                    return output;
                }
            }
            return input;
        }
    }

    public class NumberSubstitutionRule : IRule
    {
        public double SubstitutionNumber { get; set; }
        public string Regex { get; set; }
        public DataPair ApplyRule(DataPair input)
        {
            if (input.Type.IsNumericType())
            {
                if (RuleHelper.IsValid(Regex, Convert.ToDouble(input.Data)))
                {
                    var output = new DataPair
                    {
                        Data = SubstitutionNumber.ToString(),
                        Type = typeof(double),
                        IsTransformed = true,
                        TransformationLog = $"{input.TransformationLog}Numbersub (matched { Regex }). {input.Data} => { SubstitutionNumber.ToString()}\n"
                    };
                    return output;
                }
            }
            return input;
        }
    }
}
