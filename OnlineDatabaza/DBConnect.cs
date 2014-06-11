using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WebBrowser.PomocneTriedy;

namespace WebBrowser.OnlineDatabaza
{
    public class DBConnect
    {

        private MySqlConnection _connection;
        private string _server;
        private string _database;
        private string _uid;
        private string _password;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            _server = "sql5.freemysqlhosting.net";
            _database = "sql542898";
            _uid = "sql542898";
            _password = "sC4%zC4!";
            string connectionString = "SERVER=" + _server + ";" + "DATABASE=" + _database + ";" + "UID=" + _uid + ";" +
                                      "PASSWORD=" + _password + ";";

            _connection = new MySqlConnection(connectionString);
        }


        public bool CheckConnectionState()
        {
            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show(@"Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show(@"Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Insert statement
        public int Insert(List<Planeta> planety)
        {
            int i = 0;
            foreach (var planeta in planety)
            {
                string query =
                    "INSERT INTO `sql542898`.`planety` (`idPlanety`, `Nazov`, `Pozicia`, `Majitel`, `Typ`, `Sektor`, `FlagAktualny`, `Vlozil`, `DatumVlozenia`) VALUES ('" +
                    planeta.Id + "','" + planeta.Meno + "','" + planeta.Pozicia + "','" + planeta.Majitel + "','" +
                    planeta.Typ + "','" + planeta.Sektor + "','" + planeta.FlagAktualny + "','" + planeta.Vlozil +
                    "',SYSDATE());";

                //open connection
                if (this.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, _connection);

                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    this.CloseConnection();
                    i++;
                }
            }
            return i;
        }

        //Update statement
        public void Update()
        {
            string query = "UPDATE emp SET e_name='Peachy', age='22' WHERE name='Pooja R'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = _connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        /*     //Delete statement
             public void Delete()
             {
                 string query = "DELETE FROM tableinfo WHERE name='John Smith'";

                 if (this.OpenConnection() == true)
                 {
                     MySqlCommand cmd = new MySqlCommand(query, connection);
                     cmd.ExecuteNonQuery();
                     this.CloseConnection();
                 }
             }
     */


        //Select statement
        public List<Planeta> Select(string sql)
        {
            string query = sql;

            //Create a list to store the result
            var list = new List<Planeta>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    var datum = dataReader.GetDateTime(9);
                    list.Add(new Planeta(dataReader["nazov"].ToString(), dataReader["pozicia"].ToString(),
                        dataReader["idPlanety"].ToString(), dataReader["majitel"].ToString(),
                        dataReader["typ"].ToString(), dataReader["sektor"].ToString(),
                        bool.Parse(dataReader["flagAktualny"].ToString()), dataReader["vlozil"].ToString(), datum));

                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }
    }
}