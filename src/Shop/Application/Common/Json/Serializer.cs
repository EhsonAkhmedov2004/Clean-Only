

namespace Application.Common.Json
{
   
    public class Serializer
    {

        public static string ToJSON<T>(T response,int statusCode)
        {
            return JsonSerializer.Serialize(new 
            {
                response   = response,
                statusCode = statusCode
            });
        }
        
    }
}
