

namespace Application.Common.Help
{
    public record class Response<T>(T response,int statusCode) { };
    public class Helper
    {

        public static Response<T> Respond<T>(T response,int statusCode)
        {
            return new Response<T>(response, statusCode);
            
        }
        
    }
}
