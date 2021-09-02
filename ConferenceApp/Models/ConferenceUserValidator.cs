using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Models
{
    public class ConferenceUserValidator : AbstractValidator<ConferenceUser>
    {
        public ConferenceUserValidator()
        {
            RuleFor(x => x.FirstName).NotNull().Length(3, 30);
            RuleFor(x => x.LastName).NotNull().Length(3, 30);
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.ConferenceType).IsInEnum().NotNull();
        }
    }
}
