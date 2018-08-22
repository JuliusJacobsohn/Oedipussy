using Ödipussy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ÖdipussyWeb.Models
{
    public class Game
    {
        public IList<IRule> Rules { get; set; }
        public Game(bool empty = false)
        {
            Rules = new List<IRule>();
            if (!empty)
            {
                Rules.Add(new WordSubstitutionRule
                {
                    Regex = "$x%7=0 and not ($x=21)",
                    SubstituionWord = "Ödipussy"
                });
            }
        }

        public DataPair GetNumber(int index)
        {
            DataPair output = new DataPair
            {
                Data = index.ToString(),
                Type = typeof(double)
            };
            foreach (var rule in Rules)
            {
                output = rule.ApplyRule(output);
            }
            return output;
        }
    }
}