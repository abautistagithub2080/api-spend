using MVC.SPEND.API.Data;
using MVC.SPEND.API.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MVC.SPEND.API.Controllers
{
    public class SpendController : ApiController
    {
        private IValidateRepository _repository;
        public SpendController()
        {
            _repository = new ValidateRepository();
        }
        [HttpGet]
        public async Task<IHttpActionResult> Validar(string User, string Contra)
        {
            int IsUser = await _repository.FNEsValidateUser(User, Contra);
            return Content(HttpStatusCode.OK, new { mensaje = "OK_FW", EsUsuario = IsUser.ToString()});
        }
    }
}
