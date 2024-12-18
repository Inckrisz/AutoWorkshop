using System;
using System.ComponentModel.DataAnnotations;

public class NotEmptyOrWhitespaceAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }
        return true; 
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} cannot be empty or contain only whitespace.";
    }
}
