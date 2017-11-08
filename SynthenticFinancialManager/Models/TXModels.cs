using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace SynthenticFinancialManager.Models
{
    public class TXContext : DbContext
    {
        public TXContext() : base("DefaultConnection")
        {
        }

        public DbSet<BankTXModel> BankTXs { get; set; }
    }

    [Table("BankTX")]
    public class BankTXModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TxId { get; set; }

        //Maps a unit of time in the real world. In this case 1 step is 1 hour of time.
        [Display(Name = "Step")]
        public int Step { get; set; }

        //CASH-IN, CASH-OUT, DEBIT, PAYMENT and TRANSFER
        [Display(Name = "Type")]
        public string Type {get; set;}

        //amount of the transaction in local currency
        [Display(Name = "Amount")]
        public float Amount { get; set; }

        //customer who started the transaction
        [Display(Name = "Origin Customer")]
        public string NameOrig {get; set;}

        //initial balance before the transaction
        [Display(Name = "Initial balance")]
        public float OldbalanceOrg { get; set; }

        [Display(Name = "New balance")]
        public float NewbalanceOrig { get; set; }

        [Display(Name = "Destination Customer")]
        public string NameDest { get; set; }

        [Display(Name = "Initial balance Detination")]
        public float OldbalanceDest { get; set; }

        [Display(Name = "New balance Detination")]
        public float NewbalanceDest { get; set; }

        [Display(Name = "Is fraud")]
        public bool IsFraud { get; set; }

        [Display(Name = "Is flagged as fraud")]
        public bool IsFlaggedFraud { get; set; }
    }



}
