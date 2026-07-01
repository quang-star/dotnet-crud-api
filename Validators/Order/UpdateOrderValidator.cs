using FluentValidation;
using DTOs.Order;

namespace Validators.Order;

public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than 0.");
        RuleForEach(x => x.OrderDetails)
            .SetValidator(new UpdateOrderDetailValidator());
    }
}