
namespace Catalog.Products.Features.GetProductById
{
    public record GetProductByIdQuery(Guid Id)
        : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(ProductDto Product);

    internal class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private readonly CatalogDbContext _catalogDbContext;

        public GetProductByIdHandler(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }

        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _catalogDbContext.Products
                     .AsNoTracking()
                     .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(request.Id);
            }

            //mapping product entity to productdto
            var productDto = product.Adapt<ProductDto>();

            return new GetProductByIdResult(productDto);
        }
    }
}
