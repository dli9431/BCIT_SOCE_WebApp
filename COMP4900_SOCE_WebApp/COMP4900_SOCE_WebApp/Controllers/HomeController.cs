using LumenWorks.Framework.IO.Csv;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Collections.Generic;


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
        string[] sensorNames;
        string[] sensorValues;
        string[] dateTimeId;

        public void MySQLConnection()
        {
            //string connectionString = "server=127.0.0.1;database=soceweb;UserId=root;password=thisismysql;";
            //connection = new MySqlConnection(connectionString);
            //connection.Open();
        }

        public void sqlTest()
        {
            //MySQLConnection();
            //string[] files = Directory.GetFiles("D:/computer/csv file", "*.csv");
            string[] files = Directory.GetFiles("C:/sensor_data", "*.csv");
            string projectName;

            for (int i = 0; i < files.Length; i++)
            {
                //Match match_filename_hpws = Regex.Match(files[i], @"^D:(.*)hpws_file(.*).csv$");
                //Match match_filename_hpws_rp = Regex.Match(files[i], @"^D:(.*)hpws_rp(.*).csv$");
                //Match match_filename_mh_north = Regex.Match(files[i], @"^D:(.*)mh_north(.*).csv$");
                //Match match_filename_mh_south = Regex.Match(files[i], @"^D:(.*)mh_south(.*).csv$");
                //Match match_filename_th_int = Regex.Match(files[i], @"^D:(.*)th_int(.*).csv$");
                //Match match_filename_th_ext = Regex.Match(files[i], @"^D:(.*)th_ext(.*).csv$");
                //Match match_filename_th_ps = Regex.Match(files[i], @"^D:(.*)th_ps(.*).csv$");
                Match match_filename_gvs_south = Regex.Match(files[i], @"^C:\/sensor_data\/(.*)gvs_south(.*).csv$");
                //Match match_filename_gvs_north = Regex.Match(files[i], @"^D:(.*)gvs_north(.*).csv$");

                //if (match_filename_hpws.success)
                //{
                //    projectname = "hpws";
                //    process(match_filename_hpws.value, projectname);
                //}
                //else if (match_filename_hpws_rp.Success)
                //{
                //    projectName = "hpws_rp";
                //    process(match_filename_hpws_rp.Value, projectName);
                //}
                //else if (match_filename_mh_north.Success)
                //{
                //    projectName = "mh_north";
                //    process(match_filename_mh_north.Value, projectName);
                //}
                //else if (match_filename_mh_south.Success)
                //{
                //    projectName = "mh_south";
                //    process(match_filename_mh_south.Value, projectName);
                //}
                //else if (match_filename_th_int.Success)
                //{
                //    projectName = "th_int";
                //    process(match_filename_th_int.Value, projectName);
                //}
                //else if (match_filename_th_ext.Success)
                //{
                //    projectName = "th_ext";
                //    process(match_filename_th_ext.Value, projectName);
                //}
                //else if (match_filename_th_ps.Success)
                //{
                //    projectName = "th_ps";
                //    process(match_filename_th_ps.Value, projectName);
                //}
                if (match_filename_gvs_south.Success)
                {
                    projectName = "gvs_south";
                    process(match_filename_gvs_south.Value, projectName);
                }
                //else if (match_filename_gvs_north.Success)
                //{
                //    projectName = "gvs_north";
                //    process(match_filename_gvs_north.Value, projectName);
                //}
            }    
        }

        public void process(string csvFile, string projectName)
        {
            string connectionString = "server=127.0.0.1;database=soceweb;UserId=root;password=thisismysql;";
            connection = new MySqlConnection(connectionString);
            connection.Open();

            using (MySqlCommand insertCommand = connection.CreateCommand())
            {
                insertCommand.CommandText = @"INSERT INTO " + projectName + "(SensorName, SensorValue, DateTime) VALUES (@SensorName, @SensorValue, @DateTime)";
                insertCommand.Parameters.Add(new MySqlParameter("@SensorName", MySqlDbType.String));
                insertCommand.Parameters.Add(new MySqlParameter("@SensorValue", MySqlDbType.Decimal));
                insertCommand.Parameters.Add(new MySqlParameter("@DateTime", MySqlDbType.DateTime));
            

                //List<string> sensors = new List<string>();
                using (var csv = new CsvReader(new StreamReader(csvFile), true))
            {
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders().Skip(1).ToArray();

                    while (csv.ReadNextRecord())
                    {
                        for (int i = 1; i < fieldCount-1; i++)
                        {
                            DateTime parsedDateTime = DateTime.ParseExact(csv[0], "dd/MMM/yyyy H:mm:ss", CultureInfo.InvariantCulture);

                            MySqlParameter sensorName = insertCommand.Parameters[0];
                            sensorName.Value = headers[i];

                            MySqlParameter sensorValue = insertCommand.Parameters[1];
                            sensorValue.Value = csv[i];

                            MySqlParameter dateTime = insertCommand.Parameters[2];
                            dateTime.Value = parsedDateTime;

                            insertCommand.ExecuteNonQuery();

                        }
                    }
                }
            }

            connection.Close();



            //using (MySqlCommand insertCommand = connection.CreateCommand())
            //{
            //    insertCommand.CommandText = @"INSERT INTO " + projectName + "(SensorName, SensorValue, DateTime) VALUES (@SensorName, @SensorValue, @DateTime)";
            //    insertCommand.Parameters.Add(new MySqlParameter("@SensorName", MySqlDbType.String));
            //    insertCommand.Parameters.Add(new MySqlParameter("@SensorValue", MySqlDbType.Decimal));
            //    insertCommand.Parameters.Add(new MySqlParameter("@DateTime", MySqlDbType.DateTime));

            //    //string[] lines = System.IO.File.ReadAllLines(csvFile);

            //    foreach (string line in System.IO.File.ReadLines(csvFile))
            //    {
            //        string trimedLine = line.Trim();

            //        if (line.StartsWith("Sensor"))
            //        {
            //            sensorNames = trimedLine.Split(',').Skip(1).ToArray();
            //            continue;
            //        }

            //        sensorValues = trimedLine.Split(new[] { ',' }).Skip(1).ToArray();
            //        dateTimeId = trimedLine.Split(',').Take(1).ToArray();
            //        string convertToString = string.Join("", dateTimeId);
            //        DateTime parsedDateTime = DateTime.ParseExact(convertToString, "dd/MMM/yyyy H:mm:ss", CultureInfo.InvariantCulture);
            //        //"dd/MMM/yyyy H:mm:ss"
            //        // "MM/dd/yy H:mm"

            //        for (int i = 0; i < sensorNames.Length; i++)
            //        {
            //            MySqlParameter sensorName = insertCommand.Parameters[0];
            //            sensorName.Value = sensorNames[i];

            //            MySqlParameter sensorValue = insertCommand.Parameters[1];
            //            sensorValue.Value = sensorValues[i];

            //            MySqlParameter dateTime = insertCommand.Parameters[2];
            //            dateTime.Value = parsedDateTime;

            //            insertCommand.ExecuteNonQuery();
            //        }
            //    }
            //}
        }        
    }
}