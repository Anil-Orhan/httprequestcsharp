using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DataAccess;
using Entities;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SYNCTEKNOLOJITASK1
{
    public class Program
    {
        private static string APIUrl = "https://apiv2.entegrabilisim.com/api/user/token/obtain/";
        private static string APIUrlProducts = "https://apiv2.entegrabilisim.com/product/page=1/";
        private static string testApi = "https://aoprojectslive.xyz/api/users/getall";


        private static string username = "apiis@entegrabilisim.com";
        private static string password = "test123.";
        public static string token = "";

  
        static void Main(string[] args)
        {
            Console.BufferHeight = short.MaxValue-1;
            
            Console.ForegroundColor= ConsoleColor.DarkCyan;
         
            Console.Title="SYNCTEKNOLOJI";
            GetToken().Wait();
            GetProducts().Wait();


            //Db Oluşturma ve API'den ilk 5 datayı çekme
           // LocalDbOperations.CreateDatabaseAndTable();


            //Tüm Ürünler
            // LocalDbOperations.SelectData();

            //ID ile Ürün Getirme
            // LocalDbOperations.GetProductByID(15);




            //Ürün Ekleme
            //3 Parametre ile name - productCode - quantity
            // LocalDbOperations.AddData(name:"Colgate 360 White Diş Macunu",productCode:"101223695",quantity:950);


            //Ürün Silme

            // LocalDbOperations.DeleteData();


            //Ürün Arama
            // LocalDbOperations.SearchData();


            //ÜRÜN Güncelle
            //    LocalDbOperations.UpdateProduct();

        }


       

      public static  async Task GetProducts()
        {
            using (var client = new HttpClient())
            {


                // var personJSON = JsonSerializer.Serialize();
                // var buffer = System.Text.Encoding.UTF8.GetBytes(personJSON);
                // var byteContent = new ByteArrayContent(buffer);

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "JWT "+token);
                client.DefaultRequestHeaders.Add("User-Agent", "MSIE 6.0");
                var response = await client.GetAsync(APIUrlProducts);
                var jsonstring = await response.Content.ReadAsStringAsync();
            
               LocalDbOperations.Alldata = JsonConvert.DeserializeObject<Root>(jsonstring);
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
                 

              



            }
        }


 
       

    }

} 
