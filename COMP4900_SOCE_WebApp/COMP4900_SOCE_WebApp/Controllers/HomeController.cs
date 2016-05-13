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

        /*
        public void sqlTest()
        {
            string connectionString = "server=127.0.0.1;database=soceweb;UserId=root;password=thisismysql;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"DROP TABLE test IF EXISTS";
                    cmd.CommandText = @"CREATE TABLE test ( 
                                            Id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
                                            W2PlwMcD1MiSu DECIMAL(5,2),
                                            W2PlwMcD2MiSu DECIMAL(5,2),
                                            W2PlwMcD3MiSu DECIMAL(5,2),
                                            W2PlwMcM1R1Su DECIMAL(5,2)
                                        )";
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
                connection.Open();

                using (MySqlCommand insertCommand = connection.CreateCommand())
                {
                    insertCommand.CommandText =
                        @"INSERT INTO test (W2PlwMcD1MiSu, W2PlwMcD2MiSu, W2PlwMcD3MiSu, W2PlwMcM1R1Su)
                         
                       VALUES (@W2PlwMcD1MiSu, @W2PlwMcD2MiSu, @W2PlwMcD3MiSu, @W2PlwMcM1R1Su)";

                    insertCommand.Parameters.Add(new MySqlParameter("@W2PlwMcD1MiSu", "DECIMAL"));
                    insertCommand.Parameters.Add(new MySqlParameter("@W2PlwMcD2MiSu", "DECIMAL"));
                    insertCommand.Parameters.Add(new MySqlParameter("@W2PlwMcD3MiSu", "DECIMAL"));
                    insertCommand.Parameters.Add(new MySqlParameter("@W2PlwMcM1R1Su", "DECIMAL"));

                    string[] files = Directory.GetFiles("D:/computer/csv files", "*.csv");

                    foreach (string file in files)
                    {
                        string[] lines = System.IO.File.ReadAllLines(file);
                        bool parse = false;

                        foreach (string tmpLine in lines)
                        {
                            string line = tmpLine.Trim();
                            if (!parse && line.StartsWith("W2PlwMcD1MiSu"))
                            {
                                parse = true;
                                continue;
                            }
                            if (!parse || string.IsNullOrEmpty(line))
                            {
                                continue;
                            }

                            foreach (MySqlParameter parameter in insertCommand.Parameters)
                            {
                                parameter.Value = null;
                            }

                            string[] values = line.Split(new[] { ',' });

                            for (int i = 0; i < values.Length; i++)
                            {
                                MySqlParameter param = insertCommand.Parameters[i];

                                decimal value;
                                param.Value = decimal.TryParse(values[i], out value) ? value : 0;
                                                         
                            }
                            insertCommand.ExecuteNonQuery();
                        }
                        
                    }
                }
                connection.Close();
            }            
        }   
        */


        //public void sqlTest()
        //{
        //    string connectionString = "server=127.0.0.1;database=soceweb;UserId=root;password=thisismysql;";
        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        string query = @"
        //                CREATE TABLE test (                        
        //                test1 varchar(255),
        //                test2 varchar(255),
        //                test3 varchar(255),
        //                test4 varchar(255) 
        //                )";

        //        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        //        {
        //            cmd.ExecuteNonQuery();
        //            string[] files = Directory.GetFiles("D:/computer/csv files", "*.csv");

        //            foreach (string file in files)
        //            {
        //                string tableName = "test";
        //                query = "LOAD DATA INFILE 'D:/computer/csv files/test.csv' INTO TABLE " + tableName + " FIELDS TERMINATED BY ',' LINES TERMINATED BY '\r\n' IGNORE 1 LINES";

        //                using (MySqlCommand cmd1 = new MySqlCommand(query, connection))
        //                {
        //                    cmd1.ExecuteNonQuery();
        //                }

        //            }
        //        }

        //    }
        //}
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
                Match match_filename_hpws = Regex.Match(files[i], @"^D:(.*)hpws_file(.*).csv$");
                Match match_filename_hpws_rp = Regex.Match(files[i], @"^D:(.*)hpws_rp(.*).csv$");
                Match match_filename_th_ext = Regex.Match(files[i], @"^D:(.*)th_ext(.*).csv$");

                if (match_filename_hpws.Success)
                {
                    projectName = "hpws";
                    process(match_filename_hpws.Value, projectName);
                }
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

                string[] lines = System.IO.File.ReadAllLines(csvFile);


                foreach (string line in lines)
                {
                    string trimedLine = line.Trim();

                    if (line.StartsWith("Sensor"))
                    {
                        sensorNames = trimedLine.Split(new[] { ',' }).Skip(1).ToArray();

                        continue;
                    }

                    eachRow = trimedLine.Split(new[] { ',' }).Skip(1).ToArray();

                    //foreach (MySqlParameter parameter in insertCommand.Parameters)
                    //{
                    //    parameter.Value = null;
                    //}

                    for (int i = 0; i < sensorNames.Length; i++)
                    {
                        MySqlParameter param = insertCommand.Parameters[0];
                        param.Value = sensorNames[i];
                        insertCommand.ExecuteNonQuery();
                    }
                    /*
                    for (int i = 0; i < eachRow.Length; i++)
                    {
                        MySqlParameter param = insertCommand.Parameters[1];
                        param.Value = eachRow[i];
                       // insertCommand.ExecuteNonQuery();
                    }
                    int[] datetime = { 1, 2, 3, 4, 5, 6, 7, 8 };
                    for (int i = 0; i < datetime.Length; i++)
                    {
                        MySqlParameter param = insertCommand.Parameters[2];
                        param.Value = datetime[i];
                      //  insertCommand.ExecuteNonQuery();
                    }
                    //  insertCommand.ExecuteNonQuery();
                    */
                }
                insertCommand.CommandText =
                   @"INSERT INTO " + projectName + "(SensorValue) VALUES (@SensorValue)";
                insertCommand.Parameters.Add(new MySqlParameter("@SensorValue", "DECIMAL"));

                insertCommand.CommandText =
                   @"INSERT INTO " + projectName + "(DateTimeId) VALUES (@DateTimeId)";
                insertCommand.Parameters.Add(new MySqlParameter("@DateTimeId", "DECIMAL"));



            }
        }
    }
}

