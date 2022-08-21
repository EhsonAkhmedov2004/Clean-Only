
using System.IdentityModel.Tokens.Jwt;


namespace Application.Common.Authentication.TokenLogic
{
    public class Tokenlogic
    {
        public static bool isValid(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var Token = handler.ReadJwtToken(token);

            Console.WriteLine(Token.ValidTo);
            var time = DateTime.Compare(DateTime.Now,Token.ValidTo);

            if (time > 0) return false;

            return true;

        }
        public static JwtSecurityToken ReadToken(string jwt)
        {
            
            var handler   = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(jwt);
            
            return jsonToken;
        }
    }
}
