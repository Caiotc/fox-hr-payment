using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedUI.Data
{
    public class DynamicContactValidation : ValidationAttribute
    {
        private readonly string _parentFieldName;
        private readonly string _fieldType;
        private readonly string[] _validationType;

        public DynamicContactValidation(string parentFieldName, string fieldType, string[] validationType)
        {
            _parentFieldName = parentFieldName;
            _fieldType = fieldType;
            _validationType = validationType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance != null)
            {
                var parentFieldValueObject = validationContext.ObjectInstance.GetType().GetProperty(_parentFieldName).GetValue(validationContext.ObjectInstance, null);
                string currentFieldValue = value != null ? value as string : string.Empty;

                string parentFieldValue = parentFieldValueObject != null ? parentFieldValueObject as string : string.Empty;

                if (!string.IsNullOrEmpty(parentFieldValue) && parentFieldValue.ToLower() == _fieldType.ToLower())
                {
                    if (string.IsNullOrEmpty(currentFieldValue) && _validationType.Any(_ => _.ToLower() == "required"))
                    {
                        return new ValidationResult($"{validationContext.DisplayName} é obrigatório", new[] { validationContext.MemberName });
                    }
                    else if (_validationType.Any(_ => _.ToLower() == "email"))
                    {
                        bool isEmail = Regex.IsMatch(currentFieldValue, @"/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/");
                        if (!isEmail)
                        {
                            return new ValidationResult($"{validationContext.DisplayName} não é um email válido", new[] { validationContext.MemberName });
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
