namespace Catalog.Products.Features.CreateProduct
{
    public record CreateProductCommand(ProductDto Product) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Product.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Product.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly CatalogDbContext _catalogDbContext;
        //private readonly ILogger<CreateProductHandler> _logger;
        //private readonly IValidator<CreateProductCommand> _validator;

        public CreateProductHandler(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
           // _logger = logger;
          //  _validator = validator;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //var result = await _validator.ValidateAsync(command, cancellationToken);
            //var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            //if (errors.Any())
            //{
            //    throw new ValidationException(errors.FirstOrDefault());
            //}
          //  _logger.LogInformation("CreateProductCommandHandler.Handle call with {@command}", command);

            var product = CreateNewProduct(command.Product);

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
