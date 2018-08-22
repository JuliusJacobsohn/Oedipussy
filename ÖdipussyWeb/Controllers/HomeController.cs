using Ödipussy;
using ÖdipussyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ÖdipussyWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? index)
        {
            Game currentGame = Session["game"] as Game;
            if (currentGame == null)
            {
                currentGame = new Game();
                Session["game"] = currentGame;
            }
            ViewBag.Rules = currentGame.Rules;
            if (index == null)
            {
                return View();
            }
            DataPair answer = currentGame.GetNumber((int)index);
            if (!string.IsNullOrEmpty(answer.TransformationLog))
            {
                ViewBag.Answer = answer.TransformationLog.Replace("\n","<br/>");
            }
            else
            {
                ViewBag.Answer = "Der Index " + index + " ist unverändert";
            }
            return View();
        }

        public ActionResult AddRule(string rule, string regex, string ruleData)
        {
            Game currentGame = Session["game"] as Game;
            if (currentGame == null)
            {
                currentGame = new Game();
                Session["game"] = currentGame;
            }
            IRule test = null;
            switch (rule)
            {
                case "NumberSubstitutionRule":
                    test = new NumberSubstitutionRule
                    {
                        Regex = regex,
                        SubstitutionNumber = double.Parse(ruleData)
                    };
                    currentGame.Rules.Add(test);
                    break;
                case "WordSubstitutionRule":
                    test = new WordSubstitutionRule
                    {
                        Regex = regex,
                        SubstituionWord = ruleData
                    };
                    currentGame.Rules.Add(test);
                    break;
                case "DigitSubstitutionRule":
                    test = new DigitSubstitutionRule
                    {
                        Regex = "contains($x,"+ regex + ")",
                        SubstitutionDigit = int.Parse(regex),
                        Substitute = int.Parse(ruleData)
                    };
                    currentGame.Rules.Add(test);
                    break;
                case "NumberCalculationRule":
                    test = new NumberCalculationRule
                    {
                        Regex = regex,
                        Function = ruleData
                    };
                    currentGame.Rules.Add(test);
                    break;
            }
            return RedirectToAction("Index");
        }

        public ActionResult ResetGame()
        {
            Game currentGame = new Game();
            Session["game"] = currentGame;
            return RedirectToAction("Index");
        }
    }
}