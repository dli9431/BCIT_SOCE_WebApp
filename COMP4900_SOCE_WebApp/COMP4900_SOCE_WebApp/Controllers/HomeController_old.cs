using LumenWorks.Framework.IO.Csv;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace COMP4900_SOCE_WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult csv()
        {
            sqlTest();
            return View();
        }

    
        MySqlConnection connection;
        string[] files = Directory.GetFiles("c:/sensor_data", "*.csv");
        string[] sensorNames;
        string[] eachRow;

        public void MySQLConnection()
        {
            string connectionString = "server=127.0.0.1;database=soceweb;UserId=root;password=thisismysql;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        public void sqlTest()
        {
            MySQLConnection();
            string projectName;

            //"hpws", "hpws_rp", "mh_north", "mh_south", 
            //"th_int", "th_ext", "th_ps", "gvs_south", "gvs_north"

            for (int i = 0; i < files.Length; i++)
            {
                projectName = "hpws";
                string path = "C:\\sensor_data\\hpws.csv";
                process(path, projectName);

                //Match match_filename_hpws = Regex.Match(files[i], @"^C:\\sensor_data\\hpws.csv$");
                //Match match_filename_hpws_rp = Regex.Match(files[i], @"^D:(.*)hpws_rp(.*).csv$");
                //Match match_filename_th_ext = Regex.Match(files[i], @"^D:(.*)th_ext(.*).csv$");

                //if (match_filename_hpws.Success)
                //{
                //    projectName = "hpws";
                //    process(match_filename_hpws.Value, projectName);
                //}
                //else if (match_filename_hpws_rp.Success)
                //{
                //    projectName = "hpws_rp";
                //    process(match_filename_hpws_rp.Value, projectName);
                //}
                //else if (match_filename_th_ext.Success)
                //{
                //    projectName = "th_ext";
                //    process(match_filename_th_ext.Value, projectName);
                //}
            }
        }

        public void process(string csvFile, string projectName)
        {
            using (MySqlCommand insertCommand = connection.CreateCommand())
            {
                insertCommand.CommandText =
                    @"INSERT INTO " + projectName + "(SensorName) VALUES (@SensorName)";
                //insertCommand.Parameters.AddWithValue("@Part_Number", partNumber);
                insertCommand.Parameters.Add(new MySqlParameter("@SensorName", "String"));

                using (CsvReader csv =
           new CsvReader(new StreamReader(csvFile), true))
                {
                    while (csv.ReadNextRecord())
                    {
                        MySqlParameter param = insertCommand.Parameters[0];
                        param.value = ""
                        insertCommand.Parameters.Add(new MySqlParameter("@SensorValue", "DECIMAL"));
                        //param.Value = "dddd";
                        insertCommand.ExecuteNonQuery();
                    }
                }



                //string[] lines = System.IO.File.ReadAllLines(csvFile);


                //foreach (string line in lines)
                //{
                //    string trimedLine = line.Trim();

                //    if (line.StartsWith("Sensor"))
                //    {
                //        sensorNames = trimedLine.Split(new[] { ',' }).Skip(1).ToArray();

                //        continue;
                //    }

                //    eachRow = trimedLine.Split(new[] { ',' }).Skip(1).ToArray();

                //    foreach (MySqlParameter parameter in insertCommand.Parameters)
                //    {
                //        parameter.Value = null;
                //    }

                //    for (int i = 0; i < sensorNames.Length; i++)
                //    {
                //        MySqlParameter param = insertCommand.Parameters[0];
                //        param.Value = sensorNames[i];
                //        insertCommand.ExecuteNonQuery();
                //    }

                //    for (int i = 0; i < eachRow.Length; i++)
                //    {
                //        MySqlParameter param = insertCommand.Parameters[1];
                //        param.Value = eachRow[i];
                //        insertCommand.ExecuteNonQuery();
                //    }
                //    int[] datetime = { 1, 2, 3, 4, 5, 6, 7, 8 };
                //    for (int i = 0; i < datetime.Length; i++)
                //    {
                //        MySqlParameter param = insertCommand.Parameters[2];
                //        param.Value = datetime[i];
                //        insertCommand.ExecuteNonQuery();
                //    }
                //    insertCommand.ExecuteNonQuery();

                //}
                //insertCommand.CommandText =
                //   @"INSERT INTO " + projectName + "(SensorValue) VALUES (@SensorValue)";
                //insertCommand.Parameters.Add(new MySqlParameter("@SensorValue", "DECIMAL"));

                //insertCommand.CommandText =
                //   @"INSERT INTO " + projectName + "(DateTimeId) VALUES (@DateTimeId)";
                //insertCommand.Parameters.Add(new MySqlParameter("@DateTimeId", "DECIMAL"));



            }
        }
    }
}

