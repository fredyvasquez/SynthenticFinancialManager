using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SynthenticFinancialManager.Models;
using SynthenticFinancialManager.Filters;
using WebMatrix.WebData;

namespace SynthenticFinancialManager.Business
{
    public class BankTxManager
    {
        private TXContext db = new TXContext();


        //IsFraud
        //NameDest
        // GET: /BankTx/
        public List<BankTX> Search(string searchString, bool? fraud)
        {
            var transactions = from m in db.BankTXs
                         select m;

            if (!String.IsNullOrEmpty(searchString) && fraud == true)
            {
                transactions = transactions.Where(s => s.nameDest.Contains(searchString) && s.isFraud.Equals(fraud == true));
            }
            if (!String.IsNullOrEmpty(searchString) && fraud != true)
            {
                transactions = transactions.Where(s => s.nameDest.Contains(searchString));
            }
            else if (fraud == true)
            {
                transactions = transactions.Where(s => s.isFraud.Equals(fraud == true));
            }
            //else
            //    transactions = transactions.Where(s => s.TxId.Equals(-1));

            return transactions.ToList();
        }

        //
        // GET: /BankTx/Details/5

        public BankTX Details(int id = 0)
        {
            BankTX banktx = db.BankTXs.Find(id);

            return banktx;
        }

        //
        // POST: /BankTx/Create
        public BankTX Create(BankTX banktx)
        {
            db.BankTXs.Add(banktx);
            db.SaveChanges();

            return banktx;
            }

        //
        // GET: /BankTx/Edit/5
        public BankTX Get(int id = 0)
        {
            BankTX banktx = db.BankTXs.Find(id);

            return banktx;
        }

        //
        // POST: /BankTx/Edit/5
        public BankTX Get(BankTX banktx)
        {
            db.Entry(banktx).State = EntityState.Modified;
            db.SaveChanges();

            return banktx;
        }


        //
        // POST: /BankTx/Delete/5
        public void Delete(int id)
        {
            BankTX banktx = db.BankTXs.Find(id);
            db.BankTXs.Remove(banktx);
            db.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            db.Dispose();
        }

        #region Helpers
        public void SetupRoles()
        {
            try
            {
                RoleModel model = new RoleModel();
                model.RoleName = "Assistant";
                Roles(model);

                model = new RoleModel();
                model.RoleName = "Manager";
                Roles(model);

                model = new RoleModel();
                model.RoleName = "Superintendent";
                Roles(model);

                model = new RoleModel();
                model.RoleName = "Administrator";
                Roles(model);

                CreateUser("AssistantUser", "Assistant");
                CreateUser("ManagerUser", "Manager");
                CreateUser("SuperintendentUser", "Superintendent");
                CreateUser("AdministratorUser", "Administrator");
            }
            catch (Exception)
            {
                throw;
            }

        }

        private string CreateUser(string userName, string roleName)
        {
            string value = string.Empty;
            RegisterModel userModel = new RegisterModel(userName, "123456");
            if (!WebSecurity.UserExists(userName))
            {
                value = WebSecurity.CreateUserAndAccount(userModel.UserName, userModel.Password);
                System.Web.Security.Roles.AddUserToRole(userModel.UserName, roleName);
                return value;
            }
            return value;
        }

        public void Roles(RoleModel model)
        {

            // Attempt to register the user
            try
            {
                if (!System.Web.Security.Roles.RoleExists(model.RoleName))
                {
                    System.Web.Security.Roles.CreateRole(model.RoleName);
                }
            }
            catch (Exception e)
            {

            }
        }


        #endregion
    }
}