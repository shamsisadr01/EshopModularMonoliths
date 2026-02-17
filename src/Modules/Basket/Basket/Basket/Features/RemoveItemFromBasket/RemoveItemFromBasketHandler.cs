

using Basket.Basket.Features.AddItemIntoBasket;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Basket.Basket.Features.RemoveItemFromBasket
{
    public record RemoveItemFromBasketCommand(string UserName, Guid ProductId)
    : ICommand<RemoveItemFromBasketResult>;
    public record RemoveItemFromBasketResult(Guid Id);
    public class RemoveItemFromBasketCommandValidator : AbstractValidator<RemoveItemFromBasketCommand>
    {
        public RemoveItemFromBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required");
        }
    }
    internal class RemoveItemFromBasketHandler : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
    {
        private readonly IBasketRepository repository;

        public RemoveItemFromBasketHandler(IBasketRepository repository)
        {
            this.repository = repository;
        }

        public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppincart = await repository.GetBasket(command.UserName, false, cancellationToken);

            shoppincart.RemoveItem(command.ProductId);
            await repository.SaveChangesAsync(command.UserName,cancellationToken);

            return new RemoveItemFromBasketResult(shoppincart.Id);
        }
    }
}
