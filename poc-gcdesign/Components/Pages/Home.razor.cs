using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace poc_gcdesign.Components.Pages
{
    public partial class Home
    {
        [SupplyParameterFromForm]
        public Customer Person { get;set; } = new();

        private EditContext _context {get; set; } = null!;
        private CustomerValidator _validator { get; set; }
        private FluentValidation.Results.ValidationResult _validationResults { get; set; } = null!;

        protected override void OnInitialized()
        {
            _context = new EditContext(Person);
            _validator = new CustomerValidator();
            _validationResults = new FluentValidation.Results.ValidationResult();
        }

        public async void SubmitForm()
        {
            // submission
        }

        public async void InvalidSubmission(EditContext context)
        {
            _validationResults = _validator.Validate(Person);
        }

        public string DisplayErrors(string field)
        {
            var result = _validationResults.Errors.Where(x => x.PropertyName.Equals(field));
            var ret = string.Join(", ", result);
            return ret;
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