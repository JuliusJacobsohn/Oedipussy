using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ödipussy
{
    public interface IRule
    {
        string Regex { get; set; }
        string Title { get; }
        DataPair ApplyRule(DataPair input);
    }

    public class WordSubstitutionRule : IRule
    {
        public string SubstituionWord { get; set; }
        public string Regex { get; set; }

        public string Title => "Wort Ersetzung";

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

        public string Title => "Zahl Ersetzung";

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

    public class DigitSubstitutionRule : IRule
    {
        public int Substitute { get; set; }
        public int SubstitutionDigit { get; set; }
        public string Regex { get; set; }

        public string Title => "Zahl Ersetzung";

        public DataPair ApplyRule(DataPair input)
        {
            if (input.Type.IsNumericType())
            {
                if (RuleHelper.IsValid(Regex, Convert.ToDouble(input.Data)))
                {
                    var output = new DataPair
                    {
                        Data = input.Data.Replace(SubstitutionDigit.ToString(), Substitute.ToString()),
                        Type = typeof(double),
                        IsTransformed = true
                    };
                    output.TransformationLog = $"{input.TransformationLog}Digitsub (matched { Regex }). {input.Data} => { output.Data.ToString()}\n";
                    return output;
                }
            }
            return input;
        }
    }

    public class NumberCalculationRule : IRule
    {
        public string Function { get; set; }
        public string Regex { get; set; }

        public string Title => "Zahl Berechnung";

        public DataPair ApplyRule(DataPair input)
        {
            if (input.Type.IsNumericType())
            {
                if (RuleHelper.IsValid(Regex, Convert.ToDouble(input.Data)))
                {
                    string explicitFunction = Function.Replace("$x", input.Data);
                    string evaluatedFunction = RuleHelper.EvaluateExpression(explicitFunction);
                    double newValue = RuleHelper.EvaluateDouble(evaluatedFunction);

                    var output = new DataPair
                    {
                        Data = newValue.ToString(),
                        Type = typeof(double),
                        IsTransformed = true,
                        TransformationLog = $"{input.TransformationLog}Numbercalculation (matched { Regex }). {input.Data} => { newValue.ToString()}\n"
                    };
                    return output;
                }
            }
            return input;
        }
    }
}
