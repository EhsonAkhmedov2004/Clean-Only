

namespace Application.UserLogic.Cart.Commands.Remove;

public record class RemoveCommand(string Token, int id) : IRequest<string> { }

public class RemoveHandler:IRequestHandler<RemoveCommand,string>
{
    private readonly IDatabase _database;
    public RemoveHandler(IDatabase database)
    {
        _database = database;
    }

    public async Task<string> Handle(RemoveCommand command,CancellationToken cancellationToken)
    {
        if (!isValid(command.Token)) return ToJSON("Token is not valid", 400);

        var username = ReadToken(command.Token).Claims.First(u => u.Type == "Username").Value;


        var user    = await _database
                               .Users
                               .FirstAsync(u => u.Username == username);

        var product = await _database
                               .Products
                               .FirstOrDefaultAsync(p => p.Id == command.id);

        if (product == null) return ToJSON("Product not found to be removed from user's cart.",400);

        user.Cart.Remove(product);

        await _database.SaveChangesAsync(cancellationToken);

        return ToJSON("Product was removed from User's card.",200);
    }
}