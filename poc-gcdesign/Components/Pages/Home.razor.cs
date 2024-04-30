using Blazored.FluentValidation;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
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

        private EditContext _context;

        private CustomerValidator _validator { get; set; }
        private FluentValidation.Results.ValidationResult _validationResults { get; set; }

        protected override void OnInitialized()
        {
            _context = new EditContext(Person);
            _context.OnFieldChanged += _context_OnFieldChanged;

            _validator = new CustomerValidator();
            _validationResults = new FluentValidation.Results.ValidationResult();
        }

        private void _context_OnFieldChanged(object sender, FieldChangedEventArgs e)
        {
            if (!_context.Validate())
            {
                var fieldIdentifier = e.FieldIdentifier.FieldName;
                var test = 1;
            }
        }

        public async void SubmitForm()
        {
            var test1 = Person?.Surname;
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

        public async void InvalidSubmission(EditContext context)
        {
            //var result = ValidatorConfiguration.Validate(RequestModel)
            //var result = context.GetValidationMessages();
            _validationResults = _validator.Validate(Person);
            var result1 = _validationResults.Errors.Where(x => x.PropertyName.Equals("Surname"));
            var joined = string.Join(", ", result1);
            var test = 1;
        }

        public string DisplayErrors(string field)
        {
            //var errors = FieldErrors.FirstOrDefault(x => x.Field == field);

            //var ret = (errors is not null) ? string.Join(", ", errors.Errors) : string.Empty;
            var result = _validationResults.Errors.Where(x => x.PropertyName.Equals(field));
            var ret = string.Join(", ", result);
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
                    .Length(10).WithMessage("Length is 5")
                    .NotNull().WithMessage("Must be populated");

                RuleFor(x => x.PhoneNumber)
                    .MaximumLength(2).WithMessage("Cannot exceed 2 characters")
                    .NotNull().WithMessage("Must be populated");
            }
        }
    }
}