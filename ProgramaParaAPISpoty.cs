using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string forCode = "https://accounts.spotify.com/authorize?client_id=e4e9fd2135f24282ba620fa373f2a8be&response_type=code&redirect_uri=http://localhost/&scope=user-read-private";

        // ingresar el resultado de code= en la variable de abajo.
        string authorizationCode = "AQDi_gxHYYFYOE9vqAJey4UgpEL55RlSRaagjNzu1kcYJ23T_puO8RPngxAVQ7cg4KkSzDEjQAin5ywtBTdzJdpeGv6ioCsT0EKxEJ6ShnThzkjoD50Wh1SziWUZmodbEHObrs5id2vpHEdWrSn6wXOZz64IPkFDsCk5DaAWcu24IpAfPA0f5w";

        string clientId = "e4e9fd2135f24282ba620fa373f2a8be";
        string clientSecret = "8ae21df3d2f54c53a0e1e780f18cb948";
        string refreshToken = "AQBf8zAXy-uZPmE8CZVUcsLz7_sCGWbjhwpNwh4iZ56VHNRMETNFnKCuXLQNNwSWJ3RpcX8NkpqQnLi3mGY5F8XXfxGCz-Tdqg8r-63hqGBleHLQJQmheKIA9sAfTHzYHPw";
        string redirectUri = "http://localhost/";

            GetTocken(clientId, clientSecret, authorizationCode, redirectUri);
     
            // GetRefreshTocken(clientId, clientSecret, refreshToken);


        
    }
    static void GetRefreshTocken(string clientId, string clientSecret, string refreshToken)
    {
        using (var client = new WebClient())
        {
            var requestParameters = new NameValueCollection();
            requestParameters.Add("grant_type", "refresh_token");
            requestParameters.Add("refresh_token", refreshToken);
            requestParameters.Add("client_id", clientId);
            requestParameters.Add("client_secret", clientSecret);

            byte[] responseBytes = client.UploadValues("https://accounts.spotify.com/api/token", "POST", requestParameters);
            string responseBody = Encoding.UTF8.GetString(responseBytes);

            Console.WriteLine("Respuesta del servidor:");
            Console.WriteLine(responseBody);
        }
    }
    static void GetTocken(string clientId, string clientSecret, string authorizationCode, string redirectUri)
    {
        using (var client = new WebClient())
        {
            var requestParameters = new NameValueCollection();
            requestParameters.Add("grant_type", "authorization_code");
            requestParameters.Add("code", authorizationCode);
            requestParameters.Add("redirect_uri", redirectUri);
            requestParameters.Add("client_id", clientId);
            requestParameters.Add("client_secret", clientSecret);

            byte[] responseBytes = client.UploadValues("https://accounts.spotify.com/api/token", "POST", requestParameters);
            string responseBody = Encoding.UTF8.GetString(responseBytes);

            Console.WriteLine("Respuesta del servidor:");
            Console.WriteLine(responseBody);
        }
    }
}

