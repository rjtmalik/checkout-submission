using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Checkout.Com.PaymentGateway.API.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AllowedCurrencyAttribute : ValidationAttribute
    {
        public string[] ValidCurrencies { get;  set; }
        public AllowedCurrencyAttribute(string[] valueCurrencies)
        {
            ValidCurrencies = valueCurrencies;
        }

        public override bool IsValid(object value)
        {
            string inputValue = value as string;
            if (ValidCurrencies.Contains(inputValue)) return true;
            return false;
        }
    }
}
