using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;


namespace Util
{
    public class Data
    {

        private static object islock = new object();
        /// <summary>
        /// mysql by sql to data
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="constr"></param>
        /// <returns></returns>
        public static List<string> GetMySqlDataBySql(string sql, string constr)
        {
            var strdata = new List<string>();
            using (SqlConnection connection = new SqlConnection(constr))
            {
                lock (islock)
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 6000;
                    var r = cmd.ExecuteReader();


                    while (r.Read())
                    {
                        var objstr = new List<string>();
                        for (int i = 0; i < r.FieldCount; i++)
                        {
                            string fildName = r.GetName(i).Trim();
                            string value = Convert.ToString(r[fildName]);
                            string valueString = string.Empty;
                            var limit = 30000;
                            var num = (value.Length / limit);
                            if (value.Length > 32766)
                            {
                                for (var k = 0; k <= num; k++)
                                {
                                    if (k == num)
                                    {
                                        valueString += Uri.EscapeUriString(value.ToString().Substring(limit * k, value.Length - limit * k));
                                    }
                                    else
                                    {
                                        valueString += Uri.EscapeUriString(value.ToString().Substring(limit * k, limit));
                                    }
                                }
                            }
                            else
                            {

                                valueString = Uri.EscapeUriString(value);
                            }
                            objstr.Add(String.Format("\"{0}\":\"{1}\"", Uri.EscapeUriString(fildName), valueString));

                        }

                        string str = string.Format("{0}{1}{2}", "{", string.Join(",", objstr), "}");
                        strdata.Add(str);
                    }
                }
                return strdata;
            }
        }
    }
}
