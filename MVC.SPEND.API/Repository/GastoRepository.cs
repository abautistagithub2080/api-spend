using MVC.SPEND.API.Data;
using MVC.SPEND.API.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MVC.SPEND.API.Models;

namespace MVC.SPEND.API.Repository
{
    public class GastoRepository: IGastoRepository
    {
        public async Task<bool> SaveCatGasto(CatGastos oSpend)
        {
            DataLayer DL = new DataLayer();
            if (oSpend.IDGastos == 0)
            {
                var oSave = await DL.FNReader("spInsertCatGasto", 1, "@WGas|@IDUsr", oSpend.Gastos, oSpend.IDUsr.ToString());
            }
            else
            {
                var oSave = await DL.FNReader("spUpdateCatGasto", 1, "@WGas|@IDUsr|@ID", oSpend.Gastos, oSpend.IDUsr.ToString(), oSpend.IDGastos.ToString());
            }
            DL.Dispose(); DL = null;
            return true;
        }
        public async Task<bool> DeleteCatGasto(CatGastos oSpend)
        {
            DataLayer DL = new DataLayer();
            var oSave = await DL.FNReader("SP_CatGastoDELID", 1, "@IDUsr|@ID", oSpend.IDUsr.ToString(), oSpend.IDGastos.ToString());
            DL.Dispose(); DL = null;
            return true;
        }
        public async Task<List<CatGastos>> FillCombosGastos(int IDUser)
        {
            DataLayer DL = new DataLayer();
            List<CatGastos> Spendlist = new List<CatGastos>();
            DataTable oSpend = await DL.FNFillReader("SP_CatGastos", "@IDUsr", IDUser.ToString());
            Spendlist = await Combo.FNLLenaCombo<CatGastos>(oSpend);
            oSpend.Clear(); oSpend.Dispose(); oSpend = null;
            DL.Dispose(); DL = null;
            return Spendlist;
        }
        public async Task<List<CatGastos>> GETGastoID(int IDGastos, int IDUser)
        {
            DataLayer DL = new DataLayer();
            List<CatGastos> Spendlist = new List<CatGastos>();
            DataTable oSpend = await DL.FNFillReader("SP_CatGastosID", "@IDGastos|@IDUsr", IDGastos.ToString(), IDUser.ToString());
            Spendlist = await Combo.FNLLenaCombo<CatGastos>(oSpend);
            DL.Dispose(); DL = null;
            return Spendlist;
        }
    }
}