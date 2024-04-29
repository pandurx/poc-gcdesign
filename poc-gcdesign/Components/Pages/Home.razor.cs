using Blazored.FluentValidation;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Reflection.Metadata;

namespace poc_gcdesign.Components.Pages
{
    public partial class Home
    {
        [SupplyParameterFromForm]
        public Customer Person { get;set; } = new();

        private FluentValidationValidator? _fluentValidationValidator;

        //public string Error { get;set; } = string.Empty;
        public List<FieldError> FieldErrors { get; set; } = new List<FieldError>();

        protected override void OnInitialized()
        {
            // empty
        }

        public async void SubmitForm()
        {
            var test1 = Person?.Surname;
            var isValid = await _fluentValidationValidator!.ValidateAsync();
            FieldErrors.Add(
                new FieldError()
                {
                    Field = "Surname",
                    Errors = new List<string>() { "Exceeded Characters", "Letters only" }
                }
                );
            FieldErrors.Add(
                new FieldError()
                {
                    Field = "PhoneNumber",
                    Errors = new List<string>() { "Invalid!", "Invalid Structure" }
                });
            var test2 = 1;
        }

        public string DisplayErrors(string field)
        {
            var errors = FieldErrors.FirstOrDefault(x => x.Field == field);

            var ret = (errors is not null) ? string.Join(", ", errors.Errors) : string.Empty;
            return ret;
        }

        public class FieldError
        {
            public string Field { get; set; }
            public List<string> Errors { get;set; }
        }
        public class Customer
        {
            public string Surname { get; set; }
            public string PhoneNumber { get;set; }
        }



        public class CustomerValidator : AbstractValidator<Customer>
        {
            public CustomerValidator() 
            {
                RuleFor(x => x.Surname)
                    .MaximumLength(5).WithMessage("Cannot exceed 5 characters")
                    .NotNull().WithMessage("Must be populated");

                RuleFor(x => x.PhoneNumber)
                    .MaximumLength(2).WithMessage("Cannot exceed 2 characters")
                    .NotNull().WithMessage("Must be populated");
            }
        }
    }
}