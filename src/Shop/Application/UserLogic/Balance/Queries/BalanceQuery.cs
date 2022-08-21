



namespace Application.UserLogic.Balance.Queries;
public record class BalanceQuery(string Token) : IRequest<string> { }
public class BalanceQueryHandler : IRequestHandler<BalanceQuery, string>
{
    private readonly IDatabase _database;
    public BalanceQueryHandler(IDatabase database)
    {
        _database = database;
    }

    public async Task<string> Handle(BalanceQuery query, CancellationToken cancellationToken)
    {

        if (!isValid(query.Token)) return ToJSON("Token is not valid", 400);
        var username = ReadToken(query.Token).Claims.First(i => i.Type == "Username").Value;


        var account = await _database.Users.FirstAsync(i => i.Username == username);


        return ToJSON($"In your balance {account.Balance} $", 200);



    }
}
