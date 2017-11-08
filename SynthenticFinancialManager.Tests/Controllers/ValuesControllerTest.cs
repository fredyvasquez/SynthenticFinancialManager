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
            BankTX bankModel = new BankTX()
            {
                amount = 123456,
                nameDest = nameForTest,
                nameOrig = "APIGetTest",
                newbalanceDest = 1234,
                oldbalanceDest = 1233,
                oldbalanceOrg = 1232,
                newbalanceOrig = 1231,
                step = 1,
                type = "APIGetTest",
                isFlaggedFraud = true,
                isFraud = true
            };

            BankTxManager txManager = new BankTxManager();
            BankTX bankModelResult = txManager.Create(bankModel);

            ValuesController controller = new ValuesController();

            // Act
            IList<BankTX> result = controller.Get(bankModelResult.nameDest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(nameForTest, (result.ElementAt(0) as BankTX).nameDest);

        }


        [TestMethod]
        public void Post()
        {
            // Arrange
            ValuesController controller = new ValuesController();
            BankTX bankModel = new BankTX()
            {
                amount = 123456,
                nameDest = "APIPostTest",
                nameOrig = "APIPostTest",
                newbalanceDest = 1234,
                oldbalanceDest = 1233,
                oldbalanceOrg = 1232,
                newbalanceOrig = 1231,
                step = 1,
                type = "APIPostTest",
                isFlaggedFraud = true,
                isFraud = true
            };

            // Act
            controller.Post(bankModel);

            // Assert
            BankTxManager txManager = new BankTxManager();
            BankTX bankModelResult = txManager.Get(bankModel.TxId);
            Assert.IsNotNull(bankModelResult);
            Assert.AreEqual(bankModelResult.nameDest, bankModel.nameDest);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ValuesController controller = new ValuesController();
            BankTX bankModel = new BankTX()
            {
                amount = 123456,
                nameDest = "APIDeleteTest",
                nameOrig = "APIDeleteTest",
                newbalanceDest = 1234,
                oldbalanceDest = 1233,
                oldbalanceOrg = 1232,
                newbalanceOrig = 1231,
                step = 1,
                type = "APIDeleteTest",
                isFlaggedFraud = true,
                isFraud = true
            };
            controller.Post(bankModel);

            // Act
            controller.Delete(bankModel.TxId);

            // Assert
            BankTxManager txManager = new BankTxManager();
            BankTX bankModelResult = txManager.Get(bankModel.TxId);
            Assert.IsNull(bankModelResult);
        }
    }
}
