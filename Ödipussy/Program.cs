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
            WordSubstitutionRule newRule = new WordSubstitutionRule
            {
                Regex = "$x$%3=0",
                SubstituionWord = "3 HAHAHA"
            };

            rules.Add(ödipussy);
            rules.Add(newRule);

            for(int i = 1; i < 22; i++)
            {
                DataPair output = new DataPair
                {
                    Data = i.ToString(),
                    Type = typeof(int)
                };
                foreach(var rule in rules)
                {
                    output = rule.ApplyRule(output, out string log);
                    Console.WriteLine(log);
                }
                Console.WriteLine(i+": "+output.Data);
            }
        }
    }
}
