using FluentValidation;
using DTOs.OrderDetail;

namespace Validators.Order;

public class CreateOrderDetailValidator : AbstractValidator<CreateOrderDetailDto>
{
    public CreateOrderDetailValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId must be greater than 0.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}