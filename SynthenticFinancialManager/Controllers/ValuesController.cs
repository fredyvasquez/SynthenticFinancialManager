using SynthenticFinancialManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SynthenticFinancialManager.Controllers
{
    public class ValuesController : ApiController
    {
        private Business.BankTxManager txManager = new Business.BankTxManager();

        // GET api/values/5
        [Authorize(Roles = "Administrator, Superintendent")]
        public IList<BankTX> Get(string id)
        {
            IList<BankTX> transactions = txManager.Search(id, true);

            return transactions.ToList();
        }

        // POST api/values
        [Authorize(Roles = "Administrator, Assistant")]
        public void Post([FromBody]BankTX bankModel)
        {
            BankTX transaction = txManager.Create(bankModel);
        }

        // DELETE api/values/5
        [Authorize(Roles = "Administrator")]
        public void Delete(int id)
        {
            txManager.Delete(id);
        }
    }
}
