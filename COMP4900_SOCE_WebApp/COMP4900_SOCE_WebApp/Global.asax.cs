using LumenWorks.Framework.IO.Csv;
using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace COMP4900_SOCE_WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public System.Threading.Timer timer;

        protected void Application_Start(object sender, EventArgs e)
        {
            //When the web application is opened, csv importing starts immediately and when there are new files, importing starts again.
            timer = new System.Threading.Timer(FindFileNames, null, 0, Timeout.Infinite);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }

        public void FindFileNames(Object stateInfo)
        {
            string[] files = Directory.GetFiles("D:/computer/csv file", "*.csv");
            string projectName;

            for (int i = 0; i < files.Length; i++)
            {
                Match match_filename_hpws = Regex.Match(files[i], @"^C:(.*)hpws_file(.*).csv$");
                Match match_filename_hpws_rp = Regex.Match(files[i], @"^C:(.*)hpws_rp(.*).csv$");
                Match match_filename_mh_north = Regex.Match(files[i], @"^C:(.*)mh_north(.*).csv$");
                Match match_filename_mh_south = Regex.Match(files[i], @"^C:(.*)mh_south(.*).csv$");
                Match match_filename_th_int = Regex.Match(files[i], @"^C:(.*)th_int(.*).csv$");
                Match match_filename_th_ext = Regex.Match(files[i], @"^C:(.*)th_ext(.*).csv$");
                Match match_filename_th_ps = Regex.Match(files[i], @"^C:(.*)th_ps(.*).csv$");
                Match match_filename_gvs_south = Regex.Match(files[i], @"^C:(.*)gvs_south(.*).csv$");
                Match match_filename_gvs_north = Regex.Match(files[i], @"^D:(.*)gvs_north(.*).csv$");

                if (match_filename_hpws.Success)
                {
                    projectName = "hpws";
                    bool insertIntoDbStatus = InsertIntoDb(match_filename_hpws.Value, projectName);
                    DeleteFile(files[i], insertIntoDbStatus);
                }
                else if (match_filename_hpws_rp.Success)
                {
                    projectName = "hpws_rp";
                    bool insertIntoDbStatus = InsertIntoDb(match_filename_hpws_rp.Value, projectName);
                    DeleteFile(files[i], insertIntoDbStatus);
                }
                else if (match_filename_mh_north.Success)
                {
                    projectName = "mh_north";
                    bool insertIntoDbStatus = InsertIntoDb(match_filename_mh_north.Value, projectName);
                    DeleteFile(files[i], insertIntoDbStatus);
                }
                else if (match_filename_mh_south.Success)
                {
                    projectName = "mh_south";
                    bool insertIntoDbStatus = InsertIntoDb(match_filename_mh_south.Value, projectName);
                    DeleteFile(files[i], insertIntoDbStatus);
                }
                else if (match_filename_th_int.Success)
                {
                    projectName = "th_int";
                    bool insertIntoDbStatus = InsertIntoDb(match_filename_th_int.Value, projectName);
                    DeleteFile(files[i], insertIntoDbStatus);
                }
                else if (match_filename_th_ext.Success)
                {
                    projectName = "th_ext";
                    bool insertIntoDbStatus = InsertIntoDb(match_filename_th_ext.Value, projectName);
                    DeleteFile(files[i], insertIntoDbStatus);
                }
                else if (match_filename_th_ps.Success)
                {
                    projectName = "th_ps";
                    bool insertIntoDbStatus = InsertIntoDb(match_filename_th_ps.Value, projectName);
                    DeleteFile(files[i], insertIntoDbStatus);
                }
                else if (match_filename_gvs_south.Success)
                {
                    projectName = "gvs_south";
                    bool insertIntoDbStatus = InsertIntoDb(match_filename_gvs_south.Value, projectName);
                    DeleteFile(files[i], insertIntoDbStatus);
                }
                else if (match_filename_gvs_north.Success)
                {
                    projectName = "gvs_north";
                    bool insertIntoDbStatus = InsertIntoDb(match_filename_gvs_north.Value, projectName);
                    DeleteFile(files[i], insertIntoDbStatus);
                }
            }
            //restarts the timer immediately, thus FindFileNames() starts again.
            timer.Change(0, Timeout.Infinite);
        }

        public void DeleteFile(string fileName, bool insertionStatus)
        {
            DirectoryInfo di = new DirectoryInfo("D:/computer/csv files/test2");

            if (insertionStatus)
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    if (di + "\\" + file.Name == fileName)
                    {
                        File.Delete(di + "\\" + file.Name);
                        return;
                    }
                }
            }
            else
                return;
        }

        public bool InsertIntoDb(string csvFile, string projectFileName)
        {
            string connectionString = "server=127.0.0.1;database=soceweb;UserId=root;password=thisismysql;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            using (MySqlCommand insertCommand = connection.CreateCommand())
            {
                try
                {
                    insertCommand.CommandText = @"INSERT INTO Sensors (SensorName, SensorValue, DateTime, ProjectName) VALUES (@SensorName, @SensorValue, @DateTime, @ProjectName)";
                    insertCommand.Parameters.Add(new MySqlParameter("@SensorName", MySqlDbType.String));
                    insertCommand.Parameters.Add(new MySqlParameter("@SensorValue", MySqlDbType.Decimal));
                    insertCommand.Parameters.Add(new MySqlParameter("@DateTime", MySqlDbType.DateTime));
                    insertCommand.Parameters.Add(new MySqlParameter("@ProjectName", MySqlDbType.String));

                    using (var csv = new CsvReader(new StreamReader(csvFile), true))
                    {
                        string[] sensorNames = csv.GetFieldHeaders().Skip(1).ToArray();

                        while (csv.ReadNextRecord())
                        {
                            for (int i = 0; i < sensorNames.Count(); i++)
                            {
                                DateTime parsedDateTime;

                                MySqlParameter sensorName = insertCommand.Parameters[0];
                                sensorName.Value = sensorNames[i];

                                MySqlParameter sensorValue = insertCommand.Parameters[1];
                                sensorValue.Value = csv[i + 1];

                                if (DateTime.TryParseExact(csv[0], "dd/MMM/yyyy H:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime))
                                {
                                    parsedDateTime = DateTime.ParseExact(csv[0], "dd/MMM/yyyy H:mm:ss", CultureInfo.InvariantCulture);
                                    MySqlParameter dateTime = insertCommand.Parameters[2];
                                    dateTime.Value = parsedDateTime;
                                }
                                else if (DateTime.TryParseExact(csv[0], "MM/dd/yy H:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime))
                                {
                                    parsedDateTime = DateTime.ParseExact(csv[0], "MM/dd/yy H:mm", CultureInfo.InvariantCulture);
                                    MySqlParameter dateTime = insertCommand.Parameters[2];
                                    dateTime.Value = parsedDateTime;
                                }
                                else if (DateTime.TryParseExact(csv[0], "M/dd/yyyy H:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime))
                                {
                                    parsedDateTime = DateTime.ParseExact(csv[0], "M/dd/yyyy H:mm", CultureInfo.InvariantCulture);
                                    MySqlParameter dateTime = insertCommand.Parameters[2];
                                    dateTime.Value = parsedDateTime;
                                }

                                MySqlParameter projectName = insertCommand.Parameters[3];
                                projectName.Value = projectFileName;

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write(ex);
                    return false;
                }
                connection.Close();
            }
            return true;
        }
    }
}