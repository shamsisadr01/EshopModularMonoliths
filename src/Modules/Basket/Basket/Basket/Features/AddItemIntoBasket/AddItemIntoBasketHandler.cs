


using Catalog.Contracts.Products.Features.GetProductById;

namespace Basket.Basket.Features.AddItemIntoBasket
{
    public record AddItemIntoBasketCommand(string UserName, ShoppingCartItemDto ShoppingCartItem)
    : ICommand<AddItemIntoBasketResult>;
    public record AddItemIntoBasketResult(Guid Id);

    public class AddItemIntoBasketCommandValidator:AbstractValidator<AddItemIntoBasketCommand>
    {
        public AddItemIntoBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.ShoppingCartItem.ProductId).NotEmpty().WithMessage("ProductId is required");
            RuleFor(x => x.ShoppingCartItem.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }

    internal class AddItemIntoBasketHandler : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
    {
        private readonly IBasketRepository repository;
        private readonly ISender sender;

        public AddItemIntoBasketHandler(IBasketRepository repository, ISender sender)
        {
            this.repository = repository;
            this.sender = sender;
        }

        public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppincart = await repository.GetBasket(command.UserName, false, cancellationToken);

            var result = await sender.Send(new GetProductByIdQuery(command.ShoppingCartItem.ProductId));

            shoppincart.AddItem(
                command.ShoppingCartItem.ProductId,
                command.ShoppingCartItem.Quantity,
                command.ShoppingCartItem.Color,
                result.Product.Price,
                result.Product.Name);

            await repository.SaveChangesAsync(command.UserName,cancellationToken);

            return new AddItemIntoBasketResult(shoppincart.Id);
        }
    }
}
