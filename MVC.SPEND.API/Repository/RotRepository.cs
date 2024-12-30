using MVC.SPEND.API.Data;
using MVC.SPEND.API.Models;
using MVC.SPEND.API.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVC.SPEND.API.Repository
{
    public class RotRepository: IRotRepository
    {
        public async Task<List<Menus>> FillMenus(string IDUser)
        {
            DataLayer DL = new DataLayer();
            List<Menus> oMenu = new List<Menus>();
            DataTable oUser = await DL.FNFillReader("SP_MenuXUserID", "@IDUsr", IDUser.ToString());
            oMenu = await Combo.FNLLenaCombo<Menus>(oUser);
            oUser.Clear(); oUser.Dispose(); oUser = null;
            DL.Dispose(); DL = null;
            return oMenu;
        }
        public async Task<string> FillUserID(string IDUser)
        {
            string WNomCom = "";
            DataLayer DL = new DataLayer();
            var oNomCom = await DL.FNReader("SP_UsersID", 0, "@ID", IDUser);
            if (oNomCom.Read()) WNomCom = oNomCom["Nombre"].ToString() + " " + oNomCom["Paterno"].ToString() + " " + oNomCom["Materno"].ToString();
            oNomCom.Close(); oNomCom.Dispose(); oNomCom = null;
            DL.Dispose(); DL = null;
            return WNomCom;
        }
    }
}