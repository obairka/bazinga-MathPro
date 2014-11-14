using System;
using System.ComponentModel.DataAnnotations;

namespace MathPro.Domain.Infrastructure
{
    public class PastDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (null == value)
            {
                return true;
            }
            return ((DateTime)value) < DateTime.Now;
        }
    }
}
