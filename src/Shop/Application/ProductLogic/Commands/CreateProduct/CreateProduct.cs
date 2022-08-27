
namespace Application.ProductLogic.Commands.CreateProduct
{
    public record class CreateProductCommand(string Token,int Id,string Type,string Title,string Color,int Cost) : IRequest<ProductModel> { }



    public class CreateProductHandler:IRequestHandler<CreateProductCommand,ProductModel>
    {
        private readonly IDatabase _database;
        public CreateProductHandler(IDatabase database)
        {
            _database = database;
        }

        public async Task<ProductModel> Handle(CreateProductCommand command,CancellationToken cancellationToken)
        {
            if (ReadToken(command.Token).Claims.First(i => i.Type == "Role").Value != "Admin") return new ProductModel();

            ProductModel product = new ProductModel
            {
                Id    = command.Id,
                Type  = command.Type,
                Title = command.Title,
                Color = command.Color,
                Cost  = command.Cost
            };

            _database.Products.Add(product);


            await _database.SaveChangesAsync(cancellationToken);


            return product;

        }
    }

}
