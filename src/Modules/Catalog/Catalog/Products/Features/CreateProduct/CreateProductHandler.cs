namespace Catalog.Products.Features.CreateProduct
{
    public record CreateProductCommand(ProductDto Product) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly CatalogDbContext _catalogDbContext;

        public CreateProductHandler(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = CreateNewProduct(request.Product);

            _catalogDbContext.Products.Add(product);
            await _catalogDbContext.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }

        private Product CreateNewProduct(ProductDto product)
        {
            return Product.Create(Guid.NewGuid(), product.Name, product.Category, product.Description, product.ImageFile, product.Price);
        }
    }
}
