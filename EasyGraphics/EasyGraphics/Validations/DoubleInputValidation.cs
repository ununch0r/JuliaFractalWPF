using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace EasyGraphics.Validations
{
    public class DoubleInputValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var result = 0.0;
            try
            {
                if ((value as string).Length > 0)
                {
                    result = double.Parse((string)value);
                }
                else
                {
                    return new ValidationResult(false, "Field can't be empty!");
                }
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            return ValidationResult.ValidResult;
        }
    }
}
