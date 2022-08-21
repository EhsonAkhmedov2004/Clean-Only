
namespace Application.ProductLogic.Commands.UpdateProduct
{



    public record class UpdateProductCommand(int Id,string Type,string Title,string Color,int Cost) : IRequest<ProductModel> { }

    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand,ProductModel>
    {
        public readonly IDatabase _database;
        public UpdateProductHandler(IDatabase database)
        {
            _database = database;
        }

        public async Task<ProductModel> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _database.Products.FindAsync(new object[] { command.Id }, cancellationToken);
            product.Type   = command.Type;
            product!.Title = command.Title;
            product.Color  = command.Color;
            product.Cost   = command.Cost;

            await _database.SaveChangesAsync(cancellationToken);

            return product;
        }
    }
}
