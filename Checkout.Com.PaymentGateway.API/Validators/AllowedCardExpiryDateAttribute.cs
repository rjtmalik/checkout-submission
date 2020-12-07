using System;
using System.ComponentModel.DataAnnotations;

namespace Checkout.Com.PaymentGateway.API.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AllowedCardExpiryDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                var inputValue = (value as string).Split(new char[] { '/' }, StringSplitOptions.None);
                var month = int.Parse(inputValue[0]);
                var year = int.Parse(inputValue[1].Length == 2 ? $"20{inputValue[1]}" : inputValue[1]) ;
                var today = DateTime.Now;
                if (month > 12) return false;
                if (year > today.Year)
                    return true;
                if (year == today.Year)
                {
                    return month >= today.Month;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
