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
        public Customer Person { get;set; }

        protected override void OnInitialized() => Person ??= new();

        public void UpdateSurname()
        {
            var test1 = 1;
        }

        public void SubmitForm()
        {
            var test1 = Person?.Surname;
            // do something
        }
        public class Customer
        {
            public string Surname { get; set; }
        }

        public class CustomerValidator : AbstractValidator<Customer>
        {
            public CustomerValidator() 
            {
                RuleFor(x => x.Surname)
                    .MaximumLength(5)
                    .NotNull();
            }
        }
    }
}