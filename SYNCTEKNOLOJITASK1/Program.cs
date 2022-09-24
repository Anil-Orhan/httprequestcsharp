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
    public class Program
    {
        private static string APIUrl = "https://apiv2.entegrabilisim.com/api/user/token/obtain/";
        private static string APIUrlProducts = "https://apiv2.entegrabilisim.com/product/page=1/";
        private static string testApi = "https://aoprojectslive.xyz/api/users/getall";


        private static string username = "apiis@entegrabilisim.com";
        private static string password = "test123.";
        public static string token = "";
        public static Root alldata;
        static void Main(string[] args)
        {
            Console.BufferHeight = short.MaxValue-1;
            
            Console.ForegroundColor= ConsoleColor.DarkCyan;
         
            Console.Title="SYNCTEKNOLOJI";
            GetToken().Wait();
            GetProducts().Wait();
          




        }


        public static void WrittenProducts()
        {
            
            
            foreach (var item in alldata.porductList)
            {
                Console.WriteLine(item.name);
                
            }
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
            
               alldata = JsonConvert.DeserializeObject<Root>(jsonstring);




               WrittenProducts();





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

        public class Picture
        {
            public string picture { get; set; }
        }

        public class PorductList
        {
            public string id { get; set; }
            public string productCode { get; set; }
            public string status { get; set; }
            public string description { get; set; }
            public string product_type { get; set; }
            public string barcode { get; set; }
            public string gtin { get; set; }
            public string send_api { get; set; }
            public string name { get; set; }
            public string brand { get; set; }
            public string group { get; set; }
            public string quantity { get; set; }
            public string currencyType { get; set; }
            public string kdv_id { get; set; }
            public string price1 { get; set; }
            public string price2 { get; set; }
            public string price3 { get; set; }
            public string price4 { get; set; }
            public string price5 { get; set; }
            public string price6 { get; set; }
            public string price7 { get; set; }
            public string price8 { get; set; }
            public string n11_price { get; set; }
            public string n11_discountValue { get; set; }
            public string hb_price { get; set; }
            public string trendyol_listPrice { get; set; }
            public string trendyol_salePrice { get; set; }
            public string eptt_price { get; set; }
            public string eptt_iskonto { get; set; }
            public string n11pro_price { get; set; }
            public string n11pro_discountValue { get; set; }
            public string amazon_price { get; set; }
            public string amazon_salePrice { get; set; }
            public string mizu_price1 { get; set; }
            public string mizu_price2 { get; set; }
            public string zebramo_listPrice { get; set; }
            public string zebramo_salePrice { get; set; }
            public string farmazon_price { get; set; }
            public string farmazon_market_price { get; set; }
            public string farmaBorsaPrice { get; set; }
            public string farmaborsa_psPrice { get; set; }
            public string morhipo_listPrice { get; set; }
            public string morhipo_salePrice { get; set; }
            public string lidyana_listPrice { get; set; }
            public string lidyana_salePrice { get; set; }
            public string pazarama_listPrice { get; set; }
            public string pazarama_salePrice { get; set; }
            public string vfmall_listPrice { get; set; }
            public string vfmall_salePrice { get; set; }
            public string aliniyor_listPrice { get; set; }
            public string aliniyor_salePrice { get; set; }
            public string aliexpress_price { get; set; }
            public string aliexpress_salePrice { get; set; }
            public string modanisa_listPrice { get; set; }
            public string modanisa_salePrice { get; set; }
            public string bpazar_price1 { get; set; }
            public string bpazar_price2 { get; set; }
            public string flo_listPrice { get; set; }
            public string flo_salePrice { get; set; }
            public string novadan_price { get; set; }
            public string needion_listPrice { get; set; }
            public string needion_salePrice { get; set; }
            public string bisifirat_listPrice { get; set; }
            public string bisifirat_salePrice { get; set; }
            public string iyifiyat_listPrice { get; set; }
            public string iyifiyat_salePrice { get; set; }
            public string turkcellpasaj_listPrice { get; set; }
            public string turkcellpasaj_salePrice { get; set; }
            public string narwoo_price { get; set; }
            public string narwoo_salePrice { get; set; }
            public string joom_price { get; set; }
            public string joom_msrPrice { get; set; }
            public string nevade_listPrice { get; set; }
            public string nevade_salePrice { get; set; }
            public string yapisepeti_listPrice { get; set; }
            public string yapisepeti_salePrice { get; set; }
            public string op1001_listPrice { get; set; }
            public string op1001_salePrice { get; set; }
            public string etsy_price { get; set; }
            public string wish_price { get; set; }
            public string omnitron_price1 { get; set; }
            public string omnitron_price2 { get; set; }
            public string zoodmall_price { get; set; }
            public string buying_price { get; set; }
            public string supplier { get; set; }
            public string date_change { get; set; }
            public string date_add { get; set; }
            public string hb_sku { get; set; }
            public string mpn { get; set; }
            public string alan1 { get; set; }
            public string alan2 { get; set; }
            public string alan3 { get; set; }
            public string alan4 { get; set; }
            public string alan5 { get; set; }
            public List<Variatio> variatios { get; set; }
            public List<Picture> pictures { get; set; }
        }

        public class Root
        {
            public List<PorductList> porductList { get; set; }
        }

        public class Variatio
        {
            public string id { get; set; }
            public string productCode { get; set; }
            public string barcode { get; set; }
            public string gtin { get; set; }
            public string quantity { get; set; }
            public string price { get; set; }
            public string hb_st_sku { get; set; }
            public string hb_sku { get; set; }
            public string price1 { get; set; }
            public string price2 { get; set; }
            public string price3 { get; set; }
            public string price4 { get; set; }
            public string price5 { get; set; }
            public string price6 { get; set; }
            public string price7 { get; set; }
            public string price8 { get; set; }
            public string n11_price { get; set; }
            public string n11_discountValue { get; set; }
            public string hb_price { get; set; }
            public string trendyol_listPrice { get; set; }
            public string trendy_price { get; set; }
            public string eptt_price { get; set; }
            public string eptt_iskonto { get; set; }
            public string n11pro_price { get; set; }
            public string n11pro_discountValue { get; set; }
            public string amazon_price { get; set; }
            public string amazon_salePrice { get; set; }
            public string mizu_price1 { get; set; }
            public string mizu_price2 { get; set; }
            public string zebramo_listPrice { get; set; }
            public string zebramo_salePrice { get; set; }
            public string farmazon_price { get; set; }
            public string farmazon_market_price { get; set; }
            public string farmaBorsaPrice { get; set; }
            public string farmaborsa_psPrice { get; set; }
            public string morhipo_listPrice { get; set; }
            public string morhipo_price { get; set; }
            public string lidyana_listPrice { get; set; }
            public string lidyana_salePrice { get; set; }
            public string pazarama_listPrice { get; set; }
            public string pazarama_salePrice { get; set; }
            public string vfmall_listPrice { get; set; }
            public string vfmall_salePrice { get; set; }
            public string aliniyor_listPrice { get; set; }
            public string aliniyor_salePrice { get; set; }
            public string aliexpress_price { get; set; }
            public string aliexpress_salePrice { get; set; }
            public string modanisa_listPrice { get; set; }
            public string modanisa_price { get; set; }
            public string bpazar_price1 { get; set; }
            public string bpazar_price2 { get; set; }
            public string flo_listPrice { get; set; }
            public string flo_salePrice { get; set; }
            public string novadan_price { get; set; }
            public string needion_listPrice { get; set; }
            public string needion_salePrice { get; set; }
            public string bisifirat_listPrice { get; set; }
            public string bisifirat_salePrice { get; set; }
            public string iyifiyat_listPrice { get; set; }
            public string iyifiyat_salePrice { get; set; }
            public string turkcellpasaj_listPrice { get; set; }
            public string turkcellpasaj_salePrice { get; set; }
            public string narwoo_price { get; set; }
            public string narwoo_salePrice { get; set; }
            public string joom_price { get; set; }
            public string joom_msrPrice { get; set; }
            public string nevade_listPrice { get; set; }
            public string nevade_salePrice { get; set; }
            public string yapisepeti_listPrice { get; set; }
            public string yapisepeti_salePrice { get; set; }
            public string op1001_listPrice { get; set; }
            public string op1001_salePrice { get; set; }
            public string etsy_price { get; set; }
            public string wish_price { get; set; }
            public string omnitron_price1 { get; set; }
            public string omnitron_price2 { get; set; }
            public string zoodmall_price { get; set; }
            public string buying_price { get; set; }
            public string itemdim1code { get; set; }
            public string itemdim2code { get; set; }
            public List<VariationSpec> variationSpec { get; set; }
            public List<VariationPicture> variation_pictures { get; set; }
        }

        public class VariationPicture
        {
            public string url { get; set; }
        }

        public class VariationSpec
        {
            public string name { get; set; }
            public string value { get; set; }
        }

    }

} 
