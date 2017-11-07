using SynthenticFinancialManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynthenticFinancialManager
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (System.IO.File.Exists(args[0]))
                {
                    //string path = @"D:\temp\Test\paysim1\PS_20174392719_1491204439457_log.csv";
                    string path = args[0];

                    try
                    {
                        IList<BankTX> txList = new List<BankTX>();

                        using (StreamReader sr = new StreamReader(path))
                        {
                            while (sr.Peek() >= 0)
                            {
                                string line = sr.ReadLine().Trim();
                                if (line.Length == 0)
                                    continue;

                                BankTX tx = ProcessTransaction(line);
                                if (tx != null)
                                    txList.Add(tx);
                            }
                        }
                        System.Console.WriteLine(string.Format("Read {0} transactions from file", txList.Count));


                        var objBulk = new SynthenticFinancialManagerETL<BankTX>()
                            {
                                TXList = txList,
                                TableName = "BankTX",
                                CommitSize = 1000,
                                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString
                            };
                        objBulk.Commit();
                        System.Console.WriteLine("ETL Process finished succesfully");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.ToString());
                    }
                }
                else
                {
                    System.Console.WriteLine("File doesn't exist: " + args[0]);
                }

            }
            else
            {
                System.Console.WriteLine("Invalid format.  Try: SynthenticFinancialManagerETL path\\file.csv");
            }
        }

        private static BankTX ProcessTransaction(string tx)
        {
            string[] txParts = tx.Trim().Split(',');
            try
            {
                BankTX bankTX = new BankTX()
                {
                    step = Convert.ToInt32(txParts[0]),
                    type = txParts[1],
                    amount = Convert.ToSingle(txParts[2]),
                    nameOrig = txParts[3],
                    oldbalanceOrg = Convert.ToSingle(txParts[4]),
                    newbalanceOrig = Convert.ToSingle(txParts[5]),
                    nameDest = txParts[6],
                    oldbalanceDest = Convert.ToSingle(txParts[7]),
                    newbalanceDest = Convert.ToSingle(txParts[8]),
                    isFraud = Convert.ToInt32(txParts[9]) == 1,
                    isFlaggedFraud = Convert.ToInt32(txParts[10]) == 1
                };

                return bankTX;
            }
            catch (Exception)
            {
                System.Console.WriteLine(string.Format("Invalid line:", tx));
                return null;
            }
        }

    }
}
