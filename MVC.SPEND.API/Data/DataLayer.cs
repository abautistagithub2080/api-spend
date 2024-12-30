using System;
using System.Web;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
namespace MVC.SPEND.API.Data
{
    public class DataLayer
    {
        private bool disposed = false;
        private string WMsg = ".";
        private int SSAccDB = int.Parse(ConfigurationManager.AppSettings["SSAccDB"]);
        private object RD;
        private async Task<string>  DevRootDB()
        {
            string WSDB = ConfigurationManager.AppSettings["SQLServer"];
            string WProv = ConfigurationManager.AppSettings["OLEDBProv"];
            string WProp = ConfigurationManager.AppSettings["OLEDBProp"];
            var WPath = HttpContext.Current.Server.MapPath("\\DB");
            string WDB = WProv + WPath + WProp;
            return SSAccDB == 0 ? WSDB : WDB;
        }
        public async Task<IDataReader> FNReader(string WSP, int nData, string WPar, params string[] oVal)
        {
            if (SSAccDB == 0)
            {
                return await FNRdr(WSP, nData, WPar, oVal);
            }
            else
            {
                return await FNRdr(1, WSP, nData, WPar, oVal);
            }
        }
        private async Task<SqlCommand> FNCMDExeSP(string WSP, int nData, string WPar, params string[]  oVal)
        {
            char CLim = '|'; string[]  oPars = WPar.Split(CLim); int K = 0;
            string  WDB = await DevRootDB();
            SqlConnection DB = new SqlConnection(WDB); DB.Open(); SqlCommand cmd = DB.CreateCommand(); cmd.CommandText = WSP;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (var val in oVal)
            {
                cmd.Parameters.AddWithValue(oPars[K], oVal[K]); K++;
            }
            Array.Clear(oPars, 0, oPars.Length); oPars = null; Array.Clear(oVal, 0, oVal.Length); oVal = null;
            return cmd;
        }
        private async Task<OleDbCommand> FNCMDExeSP(int EsAccess, string WSP, int nData, string WPar, params string[]  oVal)
        {
            char CLim = '|'; string[]  oPars = WPar.Split(CLim); int K = 0;
            string  WPath = await DevRootDB();
            OleDbConnection DB = new OleDbConnection(WPath); DB.Open(); OleDbCommand cmd = DB.CreateCommand(); cmd.CommandText = WSP;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (var val in oVal)
            {
                cmd.Parameters.AddWithValue(oPars[K], oVal[K]); K++;
            }
            Array.Clear(oPars, 0, oPars.Length); oPars = null; Array.Clear(oVal, 0, oVal.Length); oVal = null;
            return cmd;
        }
        private async Task<SqlDataReader> FNRdr(string WSP, int nData, string WPar, params string[]  oVal)
        {
            SqlCommand cmd = await FNCMDExeSP(WSP, nData, WPar, oVal);
            if (nData == 1)
            {
                cmd.ExecuteNonQuery(); return null;
            }
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        private async Task<OleDbDataReader> FNRdr(int EsAccess, string WSP, int nData, string WPar, params string[]  oVal)
        {
            OleDbCommand cmd = await FNCMDExeSP(1, WSP, nData, WPar, oVal);
            if (nData == 1)
            {
                cmd.ExecuteNonQuery(); return null;
            }
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public async Task<DataTable> FNFillReader(string WSP, string WPar, params string[] oVal)
        {
            DataTable ds = new DataTable();
            if (SSAccDB == 0)
            {
                SqlDataAdapter  da = await FNFillRead(WSP, WPar, oVal);
                da.Fill(ds); da.Dispose(); da = null;
            }
            else
            {
                OleDbDataAdapter  da = await FNFillRead(1, WSP, WPar, oVal);
                da.Fill(ds); da.Update(ds); da.Dispose(); da = null;
            }
            return ds;
        }
        // ================ Method Async For FNLLenaTabla
        private async Task<SqlDataAdapter> FNFillRead(string WSP, string WPar, params string[] oVal)
        {
            SqlCommand cmd = await FNCMDExeSP(WSP, 1, WPar, oVal);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            return da;
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validar la compatibilidad de la plataforma", Justification = "<pendiente>")]
        private async Task<OleDbDataAdapter> FNFillRead(int EsAccess, string WSP, string WPar, params string[] oVal)
        {
            OleDbCommand cmd = await FNCMDExeSP(1, WSP, 1, WPar, oVal);
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = cmd;
            return da;
        }
        // ================ Method Async For FNLlenaTable
        public void Dispose()
        {
            Dispose(true); GC.SuppressFinalize(this); GC.Collect(); GC.WaitForPendingFinalizers();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)// Free any other managed objects here.
            {
            }// Free any unmanaged objects here.
            disposed = true; SSAccDB = -1;
        }
        ~DataLayer()
        {
            Dispose(false);
        }
    }
}