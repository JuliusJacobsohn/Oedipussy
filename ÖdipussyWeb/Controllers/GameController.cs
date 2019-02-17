using ÖdipussyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ÖdipussyWeb.Controllers
{
    public class GameController
    {
        public DataPair GetFullOutput(int index)
        {

        }

        public string GetOutput(int index)
        {
            return GetFullOutput(index).Data;
        }
    }
}