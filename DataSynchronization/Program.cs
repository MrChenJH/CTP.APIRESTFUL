using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CTP.Redis;
using CTP.Redis.Client;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;


namespace DataSynchronization
{
    class Program
    {
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var configurationBuilder = new ConfigurationBuilder();
            var providerjson = configurationBuilder.AddJsonFile("profile.json");
            var buildjson = providerjson.Build();
            Profile.con = buildjson.GetValue<string>("profile:con");
            Profile.redisIp = buildjson.GetValue<string>("profile:ip");
            Profile.redisIp = buildjson.GetValue<string>("profile:ip");
            var configurationBuilderxml = new ConfigurationBuilder();
            var providerxml = configurationBuilderxml.AddXmlFile("DelEmenu.xml");
            var buildxml = providerxml.Build();
            var Client = RedisClient.GetInstance();
            //string str = Data.GetMySqlDataBySql("select * from sys_menu_top", constr);

            using (MySqlConnection connection = new MySqlConnection(Profile.con))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "select * from sys_menu_top";
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 6000;
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    for (int i = 0; i < r.FieldCount; i++)
                    {
                        string a = r.GetName(i).Trim();
                    }
                }
                var id = connection.QueryFirst<int>("insert into user values(null, 'linezero', 'http://www.cnblogs.com/linezero/', 18);select last_insert_id();");
                //Client.Command(RedisCommand.brpop,)
                Console.ReadKey();
            }
        }
    }
}