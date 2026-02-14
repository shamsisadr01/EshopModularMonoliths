

namespace Basket.Basket.Features.GetBasket
{
    public record GetBasketQuery(string UserName)
    : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCartDto ShoppingCart);

    internal class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        private readonly BasketDbContext dbContext;

        public GetBasketHandler(BasketDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await dbContext.ShoppingCarts
                .AsNoTracking()
                .Include(s=>s.Items)
                .SingleOrDefaultAsync(s => s.UserName == query.UserName, cancellationToken);

            if (basket is null)
            {
                throw new BasketNotFoundException(query.UserName);
            }

            var basketDto = basket.Adapt<ShoppingCartDto>();

            return new GetBasketResult(basketDto);
        }
    }
}
