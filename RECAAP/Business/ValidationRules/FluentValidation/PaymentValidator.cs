﻿using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(p => p.CardNumber).Length(16);
            RuleFor(p => p.CardNumber).Must(CheckIfItContainsLetters).WithMessage(Massages.CardNumberMustConsistOfLettersOnly);
            RuleFor(p => p.ExpiryYear.ToString()).MaximumLength(2).WithMessage(Massages.LastTwoDigitsOfYearMustBeEntered);
            RuleFor(p => p.ExpiryYear).GreaterThanOrEqualTo(0);
            RuleFor(p => p.ExpiryMonth).LessThanOrEqualTo(12);
            RuleFor(p => p.ExpiryMonth).GreaterThan(0);
            RuleFor(p => p.CVV).Length(3);
        }

        private bool CheckIfItContainsLetters(string arg)
        {
            return long.TryParse(arg, out _);
        }
    }
}
