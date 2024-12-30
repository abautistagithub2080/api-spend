using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.SPEND.API.Models
{
    public class CatGastos
    {
        public int IDGastos { get; set; }
        public string Gastos { get; set; }
        public int IDUsr { get; set; }
    }
}