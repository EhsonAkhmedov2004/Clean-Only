


namespace Application.UserLogic.Cart.Queries.MyCart;
public record class MyCartQuery(string UserToken) : IRequest<string> { }

public class MyCartQueryHandler : IRequestHandler<MyCartQuery, string>
{
    private readonly IDatabase _database;
    private HttpRequestMessage response = new HttpRequestMessage();
    public MyCartQueryHandler(IDatabase database)
    {
        _database = database;
    }
    public async Task<string> Handle(MyCartQuery query,CancellationToken cancellationToken)
    {
        var username = ReadToken(query.UserToken).Claims.First(claim => claim.Type == "Username").Value;

        var user = await _database.Users
            .Include(i=>i.Cart)
            .FirstOrDefaultAsync(user => user.Username == username);


        if (user == null) return ToJSON(new List<ProductModel>(), 400);

        return ToJSON(user.Cart.ToList(),200);

     
       
    }
}

