

namespace Basket.Basket.Features.DeleteBasket
{
    public record DeleteBasketCommand(string UserName)
    : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);

    internal class DeleteBasketHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        private readonly BasketDbContext dbContext;

        public DeleteBasketHandler(BasketDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            //Delete Basket entity from command object
            //save to database
            //return result

            var shoppincart = await dbContext.ShoppingCarts
                .SingleOrDefaultAsync(s => s.UserName == command.UserName,cancellationToken);

            if (shoppincart is null)
            {
                throw new BasketNotFoundException(command.UserName);
            }

            dbContext.ShoppingCarts.Remove(shoppincart);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteBasketResult(true);
        }
    }
}
