

namespace Application.ProductLogic.Commands.BuyProduct;
public record class BuyProductCommand(int Id,string Token) : IRequest<Response<string>> { }


public class BuyProductHandler : IRequestHandler<BuyProductCommand,Response<string>>
{
    private readonly IDatabase _database;

    public BuyProductHandler(IDatabase database)
    {
        _database = database;
    }


    public async Task<Response<string>> Handle(BuyProductCommand command,CancellationToken cancellationToken)
    {
        if (!isValid(command.Token)) return Respond("token is not valid",400);


        var username = ReadToken(command.Token).Claims.First(user => user.Type == "Username").Value;

        var account = await _database
                            .Users
                            .FirstAsync(user => user.Username == username);

        var product = await _database
                            .Products
                            .FindAsync(new object[] { command.Id });
 

        if (account.Balance < product.Cost) return Respond("do not have enough money",400);

        account.Balance -= product.Cost;

        await _database.SaveChangesAsync(cancellationToken);
        
        return Respond("Successfully bought product",200);
    }
}

