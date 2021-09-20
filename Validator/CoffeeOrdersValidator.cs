using FluentValidation;
using Tqviet.CoffeeShop.Models;
using System;

namespace Tqviet.CoffeeShop.Validator
{
    public class CoffeeOrdersValidator : AbstractValidator<CoffeeOrders>
    {
        public CoffeeOrdersValidator()
        {
            RuleFor(x => x.CoffeeOrderId).NotNull().WithMessage("Mã order không được trống");
            RuleFor(x => x.CoffeeOrderQuantity).InclusiveBetween(1, 5).WithMessage("Số lượng từ 1-5");
            RuleFor(x => x.CoffeeOrderDateTime).NotNull().WithMessage("Thời gian không được trống");
            RuleFor(x => x.CoffeeOrderClientIp).NotNull().WithMessage("Địa chỉ Ip không được trống");
        }
    }
}
