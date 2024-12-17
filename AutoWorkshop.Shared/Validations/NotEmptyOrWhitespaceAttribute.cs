using System;
using System.ComponentModel.DataAnnotations;

public class NotEmptyOrWhitespaceAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is string str)
        {
            return !string.IsNullOrWhiteSpace(str); // returns true if not null, empty, or whitespace
        }
        return true; // If the value is not a string, it's considered valid
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} cannot be empty or contain only whitespace.";
    }
}
