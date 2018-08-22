using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ödipussy
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IRule> rules = new List<IRule>();
            WordSubstitutionRule ödipussy = new WordSubstitutionRule
            {
                Regex = "$x%7=0 and not ($x=21)",
                SubstituionWord = "Ödipussy"
            };
            DigitSubstitutionRule newRule = new DigitSubstitutionRule
            {
                Regex = "contains($x,3)",
                Substitute = 6,
                SubstitutionDigit = 3
            };

            rules.Add(ödipussy);
            rules.Add(newRule);

            for (int i = 1; i <= 21; i++)
            {
                DataPair output = new DataPair
                {
                    Data = i.ToString(),
                    Type = typeof(double)
                };
                foreach (var rule in rules)
                {
                    output = rule.ApplyRule(output);
                }
                if (output.IsTransformed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(output.TransformationLog);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(i + " => " + output.Data);
            }
        }
    }
}
