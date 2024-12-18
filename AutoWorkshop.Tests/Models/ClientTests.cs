using System.ComponentModel.DataAnnotations;
using AutoWorkshopApi.Models;

namespace AutoWorkshopApi.Tests
{
    public class ClientUnitTests
    {
        [Fact]
        public void ValidClient_ClientValidation_IsValid()
        {
            
            var client = new Client
            {
                Name = "John Doe",
                Address = "Asd utca 48.",
                Email = "john.doe@example.com",
            };

          
            var results = new List<ValidationResult>();
            var context = new ValidationContext(client, null, null);
            var result = Validator.TryValidateObject(client, context, results, true);

          
            Assert.True(result);
            Assert.Empty(results);
        }

        [Fact]
        public void InvalidEmail_ClientValidation_Fails()
        {
           
            var client = new Client
            {
                Name = "Jane Doe",
                Address = "Asd utca 48.",
                Email = "invalid-email", 
            };

            
            var results = new List<ValidationResult>();
            var context = new ValidationContext(client, null, null);
            var result = Validator.TryValidateObject(client, context, results, true);

           
            Assert.False(result);
            Assert.Contains(results, v => v.MemberNames.Contains("Email") && v.ErrorMessage.Contains("Invalid email format."));
        }

        [Fact]
        public void MissingName_ClientValidation_Fails()
        {
      
            var client = new Client
            {
                Address = "Asd utca 48.",
                Email = "john.doe@example.com",
            };

         
            var results = new List<ValidationResult>();
            var context = new ValidationContext(client, null, null);
            var result = Validator.TryValidateObject(client, context, results, true);

            
            Assert.False(result); 
            Assert.Contains(results, v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("Name is required."));
        }

        [Fact]
        public void ShortName_ClientValidation_Fails()
        {
           
            var client = new Client
            {
                Name = "J", 
                Address = "Asd utca 48.",
                Email = "john.doe@example.com",
            };

          
            var results = new List<ValidationResult>();
            var context = new ValidationContext(client, null, null);
            var result = Validator.TryValidateObject(client, context, results, true);

           
            Assert.False(result); 
            Assert.Contains(results, v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("Name must be at least 2 characters."));
        }
    }
}
