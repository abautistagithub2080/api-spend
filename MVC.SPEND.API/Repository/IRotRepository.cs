using MVC.SPEND.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.SPEND.API.Repository
{
    public interface IRotRepository
    {
        Task<List<Menus>> FillMenus(string IDUser);
        Task<string> FillUserID(string IDUser);
    }
}
