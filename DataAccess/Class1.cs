using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Entities;

namespace DataAccess
{
    public static class LocalDBContext
    {
        public static Root alldata;
        public static PorductList top5data;

        public static void CreateDatabaseAndTable()
        {
            SQLiteConnection con;
            SQLiteCommand cmd;
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
            var con = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            var cmd = new SQLiteCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "insert into Products(name,productCode,quantity) values ('" + name + "','" + productCode + "','" + quantity + "')";
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void DeleteData(int id)
        {
            var con = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            var cmd = new SQLiteCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM products WHERE id = {id};";
            cmd.ExecuteNonQuery();
            con.Close();
            Console.WriteLine($"Product Deleted! -->ID :{id}");

            Console.WriteLine("New Product List -->");


            SelectData();

        }
        public static void SelectData()
        {
            int counter = 0;
            var con = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            var cmd = new SQLiteCommand("Select *From Products", con);
            con.Open();
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                counter++;
                Console.WriteLine(dr[0] + " : " + dr[1] + " " + dr[2]);

            }
            con.Close();

        }

        public static void SearchData(string searchText)
        {
            int counter = 0;
            var con = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            var cmd = new SQLiteCommand($"Select *From Products where name like '%{searchText}%'", con);
            con.Open();
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                counter++;
                Console.WriteLine(dr[0] + " : " + dr[1] + " " + dr[2]);

            }
            con.Close();

        }
        public static void TruncateDb()
        {
            int counter = 0;
            var con = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            var cmd = new SQLiteCommand("DELETE FROM products;", con);
            con.Open();
            var dr = cmd.ExecuteReader();
            con.Close();

        }
        public static void UpdateProduct(int id)
        {
            var product = GetTop5Products().FirstOrDefault(p => p.id == Convert.ToString(id));
            int counter = 0;
            var con = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            var cmd = new SQLiteCommand($"UPDATE Products\r\nSET name =  {product.name}, productCode = {product.productCode}, quantity = {product.quantity} WHERE id={product.id} ;", con);
            con.Open();
            var dr = cmd.ExecuteReader();

            con.Close();
            Console.WriteLine("Product Deleted!");

        }
        public static List<PorductList> GetTop5Products()
        {

            return alldata.porductList.Take(5).ToList();



        }



    }


}
