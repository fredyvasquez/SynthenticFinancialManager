using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynthenticFinancialManager.Controllers;
using SynthenticFinancialManager.Models;
using SynthenticFinancialManager.Business;
using System.Web.Mvc;

namespace SynthenticFinancialManager.Tests.Controllers
{
    [TestClass]
    public class BankTxControllerTest
    {

        [TestMethod]
        public void Index()
        {
            BankTxController controller = new BankTxController();
            controller.Index("52452", true);
        }

        [TestMethod]
        public void Details()
        {
            BankTxController controller = new BankTxController();
            controller.Details(-1);
        }

        [TestMethod]
        public void Create()
        {
            BankTX bankModel = new BankTX()
            {
                amount = 123456,
                nameDest = "nameDest",
                nameOrig = "nameOrig",
                newbalanceDest =  1234,
                oldbalanceDest = 1233,
                oldbalanceOrg = 1232,
                newbalanceOrig = 1231,
                step = 1,
                type = "Test"
            };
            BankTxController controller = new BankTxController();
            // Act
            ViewResult result = controller.Create(bankModel) as ViewResult;

            BankTxManager txManager = new BankTxManager();
            BankTX bankModelResult = txManager.Details(bankModel.TxId);
 
            Assert.AreEqual(bankModelResult.type, bankModel.type);
            Assert.AreEqual(bankModelResult.step, bankModel.step);
            Assert.AreEqual(bankModelResult.nameOrig, bankModel.nameOrig);
            Assert.AreEqual(bankModelResult.newbalanceDest, bankModel.newbalanceDest);
        }

        [TestMethod]
        public void Edit()
        {
            
            BankTX bankModel = new BankTX()
            {
                amount = 123456,
                nameDest = "editTest",
                nameOrig = "editTest",
                newbalanceDest =  1234,
                oldbalanceDest = 1233,
                oldbalanceOrg = 1232,
                newbalanceOrig = 1231,
                step = 1,
                type = "editTest"
            };

            BankTxManager txManager = new BankTxManager();
            BankTX bankModelResult = txManager.Create(bankModel);
 

            BankTxController controller = new BankTxController();
            // Act
            ViewResult result = controller.Edit(bankModelResult.TxId) as ViewResult;
            BankTX model = result.Model as BankTX;
            Assert.AreEqual(model.type, "editTest");

            model.type = "Edited";
            controller.Edit(model);

            BankTX banktx = txManager.Details(model.TxId);

            Assert.AreEqual(model.type, "Edited");
            Assert.AreNotEqual(model.type, banktx.type);
        }

         

        [TestMethod]
        public void MarkAsFraud()
        {
            BankTX bankModel = new BankTX()
            {
                amount = 123456,
                nameDest = "MarkAsFraudTest",
                nameOrig = "MarkAsFraudTest",
                newbalanceDest = 1234,
                oldbalanceDest = 1233,
                oldbalanceOrg = 1232,
                newbalanceOrig = 1231,
                step = 1,
                type = "MarkAsFraudTest",
                isFlaggedFraud = false
            };

            BankTxManager txManager = new BankTxManager();
            BankTX bankModelResult = txManager.Create(bankModel);


            BankTxController controller = new BankTxController();
            // Act
            controller.MarkAsFraud(bankModelResult);

            BankTX banktx = txManager.Get(bankModelResult.TxId);

            Assert.AreEqual(banktx.isFlaggedFraud, true);
        }


    }
}
