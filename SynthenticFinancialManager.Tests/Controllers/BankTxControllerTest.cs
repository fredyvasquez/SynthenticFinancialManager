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
            BankTXModel bankModel = new BankTXModel()
            {
                Amount = 123456,
                NameDest = "nameDest",
                NameOrig = "nameOrig",
                NewbalanceDest =  1234,
                OldbalanceDest = 1233,
                OldbalanceOrg = 1232,
                NewbalanceOrig = 1231,
                Step = 1,
                Type = "Test"
            };
            BankTxController controller = new BankTxController();
            // Act
            ViewResult result = controller.Create(bankModel) as ViewResult;

            BankTxManager txManager = new BankTxManager();
            BankTXModel bankModelResult = txManager.Details(bankModel.TxId);
 
            Assert.AreEqual(bankModelResult.Type, bankModel.Type);
            Assert.AreEqual(bankModelResult.Step, bankModel.Step);
            Assert.AreEqual(bankModelResult.NameOrig, bankModel.NameOrig);
            Assert.AreEqual(bankModelResult.NewbalanceDest, bankModel.NewbalanceDest);
        }

        [TestMethod]
        public void Edit()
        {
            
            BankTXModel bankModel = new BankTXModel()
            {
                Amount = 123456,
                NameDest = "editTest",
                NameOrig = "editTest",
                NewbalanceDest =  1234,
                OldbalanceDest = 1233,
                OldbalanceOrg = 1232,
                NewbalanceOrig = 1231,
                Step = 1,
                Type = "editTest"
            };

            BankTxManager txManager = new BankTxManager();
            BankTXModel bankModelResult = txManager.Create(bankModel);
 

            BankTxController controller = new BankTxController();
            // Act
            ViewResult result = controller.Edit(bankModelResult.TxId) as ViewResult;
            BankTXModel model = result.Model as BankTXModel;
            Assert.AreEqual(model.Type, "editTest");

            model.Type = "Edited";
            controller.Edit(model);

            BankTXModel banktx = txManager.Details(model.TxId);

            Assert.AreEqual(model.Type, "Edited");
            Assert.AreNotEqual(model.Type, banktx.Type);
        }

         

        [TestMethod]
        public void MarkAsFraud()
        {
            BankTXModel bankModel = new BankTXModel()
            {
                Amount = 123456,
                NameDest = "MarkAsFraudTest",
                NameOrig = "MarkAsFraudTest",
                NewbalanceDest = 1234,
                OldbalanceDest = 1233,
                OldbalanceOrg = 1232,
                NewbalanceOrig = 1231,
                Step = 1,
                Type = "MarkAsFraudTest",
                IsFlaggedFraud = false
            };

            BankTxManager txManager = new BankTxManager();
            BankTXModel bankModelResult = txManager.Create(bankModel);


            BankTxController controller = new BankTxController();
            // Act
            controller.MarkAsFraud(bankModelResult);

            BankTXModel banktx = txManager.Get(bankModelResult.TxId);

            Assert.AreEqual(banktx.IsFlaggedFraud, true);
        }


    }
}
