using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        private static string testApi = "https://aoprojectslive.xyz/api/users/getall";


        private static string username = "apitestv2@entegrabilisim.com";
        private static string password = "apitestv2";
        public static string token = "";
        static void Main(string[] args)
        {
            Console.ForegroundColor= ConsoleColor.DarkCyan;
         
            Console.Title="SYNCTEKNOLOJI";
            GetToken().Wait();
       




        }
        static async Task GetProducts()
        {
            using (var client = new HttpClient())
            {

               
               // var personJSON = JsonSerializer.Serialize();
               // var buffer = System.Text.Encoding.UTF8.GetBytes(personJSON);
               // var byteContent = new ByteArrayContent(buffer);
                
                client.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue(
                    "Bearer", Convert.ToBase64String(
                        System.Text.Encoding.UTF8.GetBytes(
                            $"{"JWT "+token}")));

                var response = await client.GetAsync(APIUrlProducts);
                var jsonstring = await response.Content.ReadAsStringAsync();
                


                
                Console.WriteLine( jsonstring);





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
                var jsonObject=JsonConvert.DeserializeObject<dynamic>(jsonstring);
                TokenResult result = JsonSerializer.Deserialize<TokenResult>(jsonstring);
                Console.WriteLine("ACCESS TOKEN : " + result.access);
               // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "JWT "+jsonObject.access.ToString());
               Console.WriteLine("------------------------------");
               Console.WriteLine("Access: "+result.access);
               Console.WriteLine("------------------------------");
               Console.WriteLine(result.refresh);
              
                client.DefaultRequestHeaders.Add("Authorization", "JWT " + result.access);
                 response.Headers.Add("Authorization", "JWT " + result.access);
                response= await client.GetAsync("http://apiv2.entegrabilisim.com/product/page=1/");
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Bearer", "JWT "+result.access));
              

               // response.EnsureSuccessStatusCode();
                string info = await response.Content.ReadAsStringAsync();
                Console.WriteLine("\r\n\r\n");
                Console.WriteLine(info);

              

                token = result.access;
                 

              



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
