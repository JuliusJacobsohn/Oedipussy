using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ödipussy
{
    public class RuleHelper
    {
        public static bool IsValid(string regex, double number)
        {
            return EvaluateBoolean(regex.Replace("$x$", number.ToString()));
        }
        public static double Evaluate(string expression)
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
        public override string ToString()
        {
            return $"{Data} ({Type})";
        }
    }

    public interface IRule
    {
        string Regex { get; set; }
        DataPair ApplyRule(DataPair input, out string log);
    }

    public class WordSubstitutionRule : IRule
    {
        public string SubstituionWord { get; set; }
        public string Regex { get; set; }
        public DataPair ApplyRule(DataPair input, out string log)
        {
            if (input.Type == typeof(int))
            {
                if (RuleHelper.IsValid(Regex, Convert.ToInt32(input.Data)))
                {
                    var output = new DataPair { Data = SubstituionWord, Type = typeof(string) };
                    log = "WordSubstitutionRule regex matched: " + Regex + "\nTransforming " + input.ToString() + " to " + output.ToString();
                    return output;
                }
            }
            log = "";
            return input;
        }
    }

    public class NumberSubstitutionRule : IRule
    {
        public string Regex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DataPair ApplyRule(DataPair input, out string log)
        {
            throw new NotImplementedException();
        }
    }
}
