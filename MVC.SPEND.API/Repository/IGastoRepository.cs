using MVC.SPEND.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.SPEND.API.Repository
{
    public interface IGastoRepository
    {
        Task<bool> SaveCatGasto(CatGastos oSpend);
        Task<bool> DeleteCatGasto(CatGastos oSpend);
        Task<List<CatGastos>> FillCombosGastos(int IDUser);
        Task<List<CatGastos>> GETGastoID(int IDGastos, int IDUser);
    }
}
