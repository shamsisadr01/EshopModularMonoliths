

using MassTransit;
using Shared.Messaging.Events;

namespace Catalog.Products.EventHandlers
{
    public class ProductPriceChangedEventHandler : INotificationHandler<ProductPriceChangedEvent>
    {
        private readonly ILogger<ProductCreatedEventHandler> _logger;
        private readonly IBus bus;

        public ProductPriceChangedEventHandler(ILogger<ProductCreatedEventHandler> logger, IBus bus)
        {
            _logger = logger;
            this.bus = bus;
        }

        public async Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

            // Publish product price changed integration event for update basket prices
            var integrationEvent = new ProductPriceChangedIntegrationEvent
            {
                ProductId = notification.Product.Id,
                Name = notification.Product.Name,
                Category = notification.Product.Category,
                Description = notification.Product.Description,
                ImageFile = notification.Product.ImageFile,
                Price = notification.Product.Price //set updated product price
            };

            await bus.Publish(integrationEvent, cancellationToken);
        }
    }
}
