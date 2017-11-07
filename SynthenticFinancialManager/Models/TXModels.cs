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

        public DbSet<BankTX> BankTXs { get; set; }
    }

    [Table("BankTX")]
    public class BankTX
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TxId { get; set; }

        //Maps a unit of time in the real world. In this case 1 step is 1 hour of time.
        [Display(Name = "Step")]
        public int step { get; set; }

        //CASH-IN, CASH-OUT, DEBIT, PAYMENT and TRANSFER
        [Display(Name = "Type")]
        public string type {get; set;}

        //amount of the transaction in local currency
        [Display(Name = "Amount")]
        public float amount { get; set; }

        //customer who started the transaction
        [Display(Name = "Origin Customer")]
        public string nameOrig {get; set;}

        //initial balance before the transaction
        [Display(Name = "Initial balance")]
        public float oldbalanceOrg { get; set; }

        [Display(Name = "New balance")]
        public float newbalanceOrig { get; set; }

        [Display(Name = "Destination Customer")]
        public string nameDest { get; set; }

        [Display(Name = "Initial balance Detination")]
        public float oldbalanceDest { get; set; }

        [Display(Name = "New balance Detination")]
        public float newbalanceDest { get; set; }

        [Display(Name = "Is fraud")]
        public bool isFraud { get; set; }

        [Display(Name = "Is flagged as fraud")]
        public bool isFlaggedFraud { get; set; }
    }



}
