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
                Regex = "$x$%7=0 and not ($x$=21)",
                SubstituionWord = "Ödipussy"
            };
            NumberSubstitutionRule newRule = new NumberSubstitutionRule
            {
                Regex = "$x$%3=0",
                SubstitutionNumber = 8
            };
            NumberSubstitutionRule newRule2 = new NumberSubstitutionRule
            {
                Regex = "$x$=8",
                SubstitutionNumber = 13
            };
            NumberSubstitutionRule newRule3 = new NumberSubstitutionRule
            {
                Regex = "$x$%4=0",
                SubstitutionNumber = 12
            };

            rules.Add(ödipussy);
            rules.Add(newRule3);
            rules.Add(newRule2);
            rules.Add(newRule);

            for (int i = 1; i < 22; i++)
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
