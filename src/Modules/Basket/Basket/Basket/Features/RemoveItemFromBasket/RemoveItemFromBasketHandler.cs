

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
        private readonly BasketDbContext dbContext;

        public RemoveItemFromBasketHandler(BasketDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppincart = await dbContext.ShoppingCarts
            .Include(s => s.Items)
            .SingleOrDefaultAsync(s => s.UserName == command.UserName, cancellationToken);

             if (shoppincart is null)
            {
                throw new BasketNotFoundException(command.UserName);
            }

            shoppincart.RemoveItem(command.ProductId);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new RemoveItemFromBasketResult(shoppincart.Id);
        }
    }
}
