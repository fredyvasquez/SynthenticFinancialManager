using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynthenticFinancialManager;
using SynthenticFinancialManager.Controllers;
using SynthenticFinancialManager.Models;
using SynthenticFinancialManager.Business;

namespace SynthenticFinancialManager.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            string nameForTest = "APIGetTest" + System.DateTime.Now.Ticks;
            // Arrange
            BankTXModel bankModel = new BankTXModel()
            {
                Amount = 123456,
                NameDest = nameForTest,
                NameOrig = "APIGetTest",
                NewbalanceDest = 1234,
                OldbalanceDest = 1233,
                OldbalanceOrg = 1232,
                NewbalanceOrig = 1231,
                Step = 1,
                Type = "APIGetTest",
                IsFlaggedFraud = true,
                IsFraud = true
            };

            BankTxManager txManager = new BankTxManager();
            BankTXModel bankModelResult = txManager.Create(bankModel);

            ValuesController controller = new ValuesController();

            // Act
            IList<BankTXModel> result = controller.Get(bankModelResult.NameDest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(nameForTest, (result.ElementAt(0) as BankTXModel).NameDest);

        }


        [TestMethod]
        public void Post()
        {
            // Arrange
            ValuesController controller = new ValuesController();
            BankTXModel bankModel = new BankTXModel()
            {
                Amount = 123456,
                NameDest = "APIPostTest",
                NameOrig = "APIPostTest",
                NewbalanceDest = 1234,
                OldbalanceDest = 1233,
                OldbalanceOrg = 1232,
                NewbalanceOrig = 1231,
                Step = 1,
                Type = "APIPostTest",
                IsFlaggedFraud = true,
                IsFraud = true
            };

            // Act
            controller.Post(bankModel);

            // Assert
            BankTxManager txManager = new BankTxManager();
            BankTXModel bankModelResult = txManager.Get(bankModel.TxId);
            Assert.IsNotNull(bankModelResult);
            Assert.AreEqual(bankModelResult.NameDest, bankModel.NameDest);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ValuesController controller = new ValuesController();
            BankTXModel bankModel = new BankTXModel()
            {
                Amount = 123456,
                NameDest = "APIDeleteTest",
                NameOrig = "APIDeleteTest",
                NewbalanceDest = 1234,
                OldbalanceDest = 1233,
                OldbalanceOrg = 1232,
                NewbalanceOrig = 1231,
                Step = 1,
                Type = "APIDeleteTest",
                IsFlaggedFraud = true,
                IsFraud = true
            };
            controller.Post(bankModel);

            // Act
            controller.Delete(bankModel.TxId);

            // Assert
            BankTxManager txManager = new BankTxManager();
            BankTXModel bankModelResult = txManager.Get(bankModel.TxId);
            Assert.IsNull(bankModelResult);
        }
    }
}
