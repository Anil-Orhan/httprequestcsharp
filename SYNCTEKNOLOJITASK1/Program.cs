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
        public static List<Product> _productsAll;
        static void Main(string[] args)
        {
            Console.ForegroundColor= ConsoleColor.DarkCyan;
         
            Console.Title="SYNCTEKNOLOJI";
            GetToken().Wait();
            GetProducts().Wait();





        }


        public static void WrittenProducts(List<Product> products)
        {
            
            Console.WriteLine(products);
           /* foreach (var item in _productsAll)
            {
                Console.WriteLine(item.name);
            }*/
        }

        static async Task GetProducts()
        {
            using (var client = new HttpClient())
            {


                // var personJSON = JsonSerializer.Serialize();
                // var buffer = System.Text.Encoding.UTF8.GetBytes(personJSON);
                // var byteContent = new ByteArrayContent(buffer);

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "JWT eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ0b2tlbl90eXBlIjoiYWNjZXNzIiwiZXhwIjoxNjcxNzM0NDUyLCJqdGkiOiJlYzIwZjgyNzE2ZmE0NDIxODllMGYwMzI1YmYyOWExYiIsInVzZXJfaWQiOjEzNH0.obE5fg1I37faDnRr-AdoQXw36Gpqy0RIBDIfN57B3SE");
                client.DefaultRequestHeaders.Add("User-Agent", "MSIE 6.0");

                var response = await client.GetAsync(APIUrlProducts);
                var jsonstring = await response.Content.ReadAsStringAsync();


                  var products = JsonConvert.DeserializeObject<AllProducts>(jsonstring);
                  Console.WriteLine(products.AllProduct.Count);

                 // WrittenProducts(products);








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
                
                string info = await response.Content.ReadAsStringAsync();
              

              

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

        public class AllProducts
        {

            public List<Product> AllProduct{ get; set; }
        }
        public class Product
        {
            public string name { get; set; }
            public string productCode { get; set; }
            public long quantity { get; set; }

        }
    }

} 
