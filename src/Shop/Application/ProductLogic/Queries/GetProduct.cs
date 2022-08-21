


namespace Application.ProductLogic.Queries
{
    public class GetProductQuery:IRequest<List<ProductModel>>
    {
        public GetProductQuery() { }

    }

    public class GetProductHandler:IRequestHandler<GetProductQuery,List<ProductModel>>
    {
        private readonly IDatabase _database;
        public GetProductHandler(IDatabase database)
        {
            _database = database;
        }

        public async Task<List<ProductModel>> Handle(GetProductQuery query,CancellationToken cancellationToken)
        {
            return await _database.Products.ToListAsync();
        }
    }
}
