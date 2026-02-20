namespace Ordering.Orders.Features.GetOrderById;

public record GetOrderByIdQuery(Guid Id)
    : IQuery<GetOrderByIdResult>;
public record GetOrderByIdResult(OrderDto Order);

internal class GetOrderByIdHandler
    : IQueryHandler<GetOrderByIdQuery, GetOrderByIdResult>
{
    private readonly OrderingDbContext dbContext;

    public GetOrderByIdHandler(OrderingDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<GetOrderByIdResult> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
                        .AsNoTracking()
                        .Include(x => x.Items)
                        .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(query.Id);
        }

        var orderDto = order.Adapt<OrderDto>();

        return new GetOrderByIdResult(orderDto);
    }
}
