﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FashTradPlat.Models
{
    public class Payment
    {
        public enum Method
        {
            Card,
            Paypal,
            [Display(Name = "Bank Transfer")]
            BankTransfer

        }
        [Key] public int Payment_ID { get; set; }
        
        // This [Column(TypeName = "decimal(7,2)")] defines the decimal field for Price where 7 is the allowed amount of total digits in a number and 2 is the amont after a decimal
        [Column(TypeName = "decimal(7,2)")]
        [Display(Name = "Amount")]
        [RegularExpression("^(?!0\\.00$)([1-9]\\d{0,4}(\\.\\d{1,2})?)$", ErrorMessage = "Please enter a valid Amount")]
        public required decimal Payment_amount { get; set; }
        [Display(Name = "Method")]
        public required Method Payment_method { get; set; }
        [Display(Name = "Payment Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }
        public Payment()
        {
            PaymentDate = DateTime.Now;
        }
    }
}
