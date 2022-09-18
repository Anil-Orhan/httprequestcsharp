using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SYNCTEKNOLOJITASK1
{
    internal class Program
    {
        private static string APIUrl = "https://apiv2.entegrabilisim.com/api/user/token/obtain/";
        private static string APIUrlProducts = "https://apiv2.entegrabilisim.com/product/page=1/";

        private static string username = "apiis@entegrabilisim.com";
        private static string password = "test123.";
        public static string token = "";
        static void Main(string[] args)
        {
            Console.ForegroundColor= ConsoleColor.DarkCyan;
         
            Console.Title="SYNCTEKNOLOJI";
            GetToken().Wait();
          //  GetProducts().Wait();



        }
        static async Task GetProducts()
        {
            using (var client = new HttpClient())
            {
               

            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Add("Authorization", "JWT eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ0b2tlbl90eXBlIjoiYWNjZXNzIiwiZXhwIjoxNjcxMzA4MzE2LCJqdGkiOiIzYzI0MTgxOTM4ZmI0NDZkODYzMDljY2RjYTc5NjQyZCIsInVzZXJfaWQiOjEzNH0.AxHxJoP5CFjt2tEuUEM1Pnp_RYAR6vsCj1VNGTbXZ9U");
                
                var response = await client.GetStringAsync(APIUrlProducts);
                
               // var result = JsonSerializer.Serialize(jsonstring);

                Console.WriteLine("--------------------------------------");
                Console.WriteLine("-----------------PRODUCTS---------------------");
                Console.WriteLine(response
                    );



            }
        }
        static async Task GetToken()
        {
            using (var client = new HttpClient())
            {

                User user = new User
                {
                    email = username,
                    password = password
                };
                var personJSON = JsonSerializer.Serialize(user);
                var buffer = System.Text.Encoding.UTF8.GetBytes(personJSON);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(APIUrl, byteContent);
                var jsonstring = await response.Content.ReadAsStringAsync();
                TokenResult result = JsonSerializer.Deserialize<TokenResult>(jsonstring);

              
                token = result.access;
                 Console.WriteLine("ACCESS TOKEN : "+result.access);

              



            }
        }

        public class AuthModel
        {
            public string Authorization { get; set; }
        }
        public class TokenResult
        {
            public string refresh { get; set; }
            public string access { get; set; }

        }
        public class User
        {
            public string email { get; set; }
            public string password { get; set; }



        }
    }

 
}
