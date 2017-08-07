using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace CTP.RELATION.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public string GetDataBbySql(string sql)
        {
            using (MySqlConnection connection = new MySqlConnection(constr))
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


                return string.Empty;
            }
        }

    }
}
