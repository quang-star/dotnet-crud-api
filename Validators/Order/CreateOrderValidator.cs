using FluentValidation;
using DTOs.Order;

namespace Validators.Order;

public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than 0.");

        RuleForEach(x => x.OrderDetails)
            .SetValidator(new CreateOrderDetailValidator());
    }
}