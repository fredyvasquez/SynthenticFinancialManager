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
                        IList<BankTXModel> txList = new List<BankTXModel>();

                        using (StreamReader sr = new StreamReader(path))
                        {
                            while (sr.Peek() >= 0)
                            {
                                string line = sr.ReadLine().Trim();
                                if (line.Length == 0)
                                    continue;

                                BankTXModel tx = ProcessTransaction(line);
                                if (tx != null)
                                    txList.Add(tx);
                            }
                        }
                        System.Console.WriteLine(string.Format("Read {0} transactions from file", txList.Count));


                        var objBulk = new SynthenticFinancialManagerETL<BankTXModel>()
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

        private static BankTXModel ProcessTransaction(string tx)
        {
            string[] txParts = tx.Trim().Split(',');
            try
            {
                BankTXModel bankTX = new BankTXModel()
                {
                    Step = Convert.ToInt32(txParts[0]),
                    Type = txParts[1],
                    Amount = Convert.ToSingle(txParts[2]),
                    NameOrig = txParts[3],
                    OldbalanceOrg = Convert.ToSingle(txParts[4]),
                    NewbalanceOrig = Convert.ToSingle(txParts[5]),
                    NameDest = txParts[6],
                    OldbalanceDest = Convert.ToSingle(txParts[7]),
                    NewbalanceDest = Convert.ToSingle(txParts[8]),
                    IsFraud = Convert.ToInt32(txParts[9]) == 1,
                    IsFlaggedFraud = Convert.ToInt32(txParts[10]) == 1
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
