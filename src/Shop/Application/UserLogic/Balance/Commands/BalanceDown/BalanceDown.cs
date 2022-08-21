
namespace Application.UserLogic.Balance.Commands.BalanceDown;
public record class BalanceDownCommand(int money, string Token) : IRequest<string>
{

}
public class BalanceDownHandler : IRequestHandler<BalanceDownCommand, string>
{
    private readonly IDatabase _database;
    public BalanceDownHandler(IDatabase database)
    {
        _database = database;
    }

    public async Task<string> Handle(BalanceDownCommand command, CancellationToken cancellationToken)
    {
        if (!isValid(command.Token)) return ToJSON("Token is not valid", 400);

        var username = ReadToken(command.Token)
                       .Claims
                       .First(i => i.Type == "Username").Value;



        var account      = _database.Users.First(i => i.Username == username);

        if (account.Balance < command.money) return ToJSON("Do not have enough money", 400);
        account.Balance -= command.money;


        return ToJSON($"{command.money}$ has been withdrawn from your account.", 200);
            
            


    }
}