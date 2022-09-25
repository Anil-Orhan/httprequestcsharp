using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Entities;

namespace DataAccess
{
    public static class LocalDbOperations
    {
        public static Root Alldata;
        public static PorductList Top5data;
        public static SQLiteConnection con = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
        public static SQLiteCommand cmd = new SQLiteCommand();
        public static void CreateDatabaseAndTable()
        {
            
            SQLiteDataReader dr;
            if (!File.Exists("MyDatabase.sqlite"))
            {
                SQLiteConnection.CreateFile("MyDatabase.sqlite");

                string sql = @"CREATE TABLE Products(
                               ID INTEGER PRIMARY KEY AUTOINCREMENT ,
                               name            TEXT      NOT NULL,
                               productCode             TEXT       NOT NULL,
                                quantity    LONG       NOT NULL
                            );";
                con = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
                con.Open();
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();

            }
            else
            {
                con = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            }




        }
        public static void AddData(string name, string productCode, long quantity)
        {
            
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "insert into Products(name,productCode,quantity) values ('" + name + "','" + productCode + "','" + quantity + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            Console.WriteLine("Product Added!");
            Console.WriteLine("*---------------INFO---------------*");
            Console.Write($"Product Name -> {name}, Product Code -> {productCode}, Quantity -> {quantity}");
        }
        public static void DeleteData()
        {
            SelectData();

            Console.WriteLine("Please enter the ID of the product you want to delete.");
           var productId =Console.ReadLine();
           var selectedProduct = GetProductByID(Convert.ToInt32(productId));
           if (selectedProduct!=null)
           {
               var con = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
               var cmd = new SQLiteCommand();
               con.Open();
               cmd.Connection = con;
               cmd.CommandText = $"DELETE FROM products WHERE id = {productId};";
               cmd.ExecuteNonQuery();
               con.Close();
               Console.WriteLine($"Product Deleted! -->ID :{productId}");

               Console.WriteLine("New Product List -->");


               SelectData();
            }
           else
           {
               Console.WriteLine($"No product with ID {productId}");
           }
          

        }
        public static void SelectData()
        {
            int counter = 0;
       
             cmd = new SQLiteCommand("Select *From Products", con);
            con.Open();
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Console.WriteLine("\n Product -> " + counter);
                counter++;

                Console.WriteLine("\n ID :" + dr[0] + "\n Product Name : " + dr[1] + "\n Product Code : " + dr[2]+ "\n Quantity :  " + dr[3]);
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            }
            con.Close();

        }
        public static PorductList GetProductByID(int id)
        {
            int counter = 0;

            cmd = new SQLiteCommand($"Select *From Products where id= {id}", con);
            con.Open();
            var dr = cmd.ExecuteReader();
            PorductList selectedProduct = null;
            while (dr.Read())
            {
                Console.WriteLine(counter);
                counter++;
               
                selectedProduct = new PorductList() {id = dr[0].ToString(),name = dr[1].ToString(),productCode = dr[2].ToString(),quantity = dr[3].ToString()};

            }
            con.Close();
            return selectedProduct;

        }
        public static void SearchData()
        {
            var check = true;
            while (check==true)
            {
                

                Console.WriteLine("*---------------Product Search---------------*");
                Console.Write("Search : ");
                var searchT = Console.ReadLine();
                int counter = 0;

                cmd = new SQLiteCommand($"Select *From Products where name like '%{searchT}%'", con);
                con.Open();
                var dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    counter++;
                    Console.WriteLine("\n ID :" + dr[0] + "\n Product Name : " + dr[1] + "\n Product Code : " + dr[2] + "\n Quantity :  " + dr[3]);
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------");

                }
                Console.WriteLine(counter + " products found!");
                con.Close();
                
               var  checkNewSearch = true;
                while (checkNewSearch)
                {
                    Console.WriteLine("Do you want to make a new search? [y/n]");
                    var newSearch = Console.ReadLine();
                    if (newSearch == "y")
                    {
                        check = true;
                        break;
                    }
                    else if (newSearch == "n")
                    {
                        check = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You Made the Wrong Choice!");
                    }
                }
                


            }

            

        }
        public static void TruncateDb()
        {
            int counter = 0;
          
             cmd = new SQLiteCommand("DELETE FROM products;", con);
            con.Open();
            var dr = cmd.ExecuteReader();
            con.Close();

        }
        public static void UpdateProduct()
        {
           
            SelectData();
           
           

            while (true)
            {
                Console.WriteLine("Enter the Product ID For Update -->");
                var id = Console.ReadLine();
                var selectedProduct = GetProductByID(Convert.ToInt32(id));


                if (selectedProduct != null)
                {
                    Console.WriteLine("\n Enter the number of the value to be changed -> \n [0] Name \n [1] Product Code \n [2] Quantity");
                    var selectedProperty = Console.ReadLine();
                    Console.WriteLine("Please enter new value -->");
                    var newval = Console.ReadLine();

                    if (selectedProperty == "0")
                    {
                        cmd = new SQLiteCommand($"UPDATE Products SET name =  '{newval}' WHERE id={selectedProduct.id} ;", con);
                        con.Open();
                        var dr = cmd.ExecuteReader();
                    }
                    else if (selectedProperty == "1")
                    {
                        cmd = new SQLiteCommand($"UPDATE Products SET  productCode = '{newval}' WHERE id={selectedProduct.id} ;", con);
                        con.Open();
                        var dr = cmd.ExecuteReader();
                    }
                    else if (selectedProperty == "2")
                    {
                        cmd = new SQLiteCommand($"UPDATE Products SET  quantity = '{newval}' WHERE id={selectedProduct.id} ;", con);
                        con.Open();
                        var dr = cmd.ExecuteReader();
                    }
                    else
                    {
                        Console.WriteLine("You have entered incorrectly!");
                    }




                    con.Close();
                    Console.WriteLine("Product Updated!");
                    break;
                }
                else
                {
                    Console.WriteLine("Product ID Incorrect ! Please enter the correct product ID ");
                }

            }



        }
        public static List<PorductList> GetTop5Products()
        {

            return Alldata.porductList.Take(5).ToList();



        }



    }


}
