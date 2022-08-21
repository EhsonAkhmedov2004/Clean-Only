using System.Security.Cryptography;




namespace Application.UserLogic.UserAuthentication.Queries.LoginUser;
public record class LoginUserQuery   (string Username,string Password) : IRequest<string>{ }


public class LoginUserHandler:IRequestHandler<LoginUserQuery,string>
{
    private readonly IDatabase _database;
    private readonly IAuth _auth;
    private readonly IConfiguration _config;



    public LoginUserHandler(IDatabase database,IAuth auth, IConfiguration config)
    {
        _database = database;
        _auth = auth;
        _config = config;
    }

    public async Task<string> Handle(LoginUserQuery query,CancellationToken cancellationToken)
    {

        UserModel user = _database.Users.ToList().First(i => i.Username == query.Username);

        if (user == null) return ToJSON("Account not found",404);

        var passwordSalt = user.SaltPassword;

        var sec_key = Encoding.ASCII.GetBytes(_config.GetSection("SecurityHmacCode").Value);

        byte[] hashed_password;


        using (var encryption = new HMACSHA256(sec_key))
        { 
            hashed_password = encryption.ComputeHash (
                              Encoding.ASCII.GetBytes(
                              String.Concat(query.Password,Convert.ToBase64String(passwordSalt))));
        }
        

        if (!user.Password.SequenceEqual(hashed_password)) return "Passoword is wrong";

        string jwt = _auth.CreateTokenForUser(user).ToString();

     
        

        return ToJSON(jwt,200);
        
    }
}

