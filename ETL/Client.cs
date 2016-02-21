/*
 * File         : Client.cs
 * Project      : ETL
 * Author       : SAMEER SAPRA
 * Date         : 3/27/2015
 * Description  : This is the client application which is used to send the queries to the MSSQL database.
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace ETL
{
   
    public partial class Client : Form
    {

        IPHostEntry ipHostInfo;
        IPAddress ipAddress;
        IPEndPoint remoteEP;
        Socket client;

        bool srcChecked = false;
        bool destChecked = false;

        // The port number for the remote device.
        int port = 0;

        // ManualResetEvent instances signal completion.
        private ManualResetEvent connectDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(false);
        private ManualResetEvent receiveDone = new ManualResetEvent(false);
        private ManualResetEvent startReceive = new ManualResetEvent(false);

        delegate void MyCallback(Object obj);
        delegate void MyCallback1();

        List<String> sourceTablenames = new List<String>();
        List<String> destinationTablenames = new List<String>();    
        List<String> tablesToTransfer = new List<string>();
        List<String> commonTables = new List<string>();
        List<String> nonExistedTableDataTypes = new List<string>();
        List<String> nonExistedTablesColNames = new List<string>();
        List<String> nonExistedTableCharLength = new List<string>();
        List<String> srcDataTypes = new List<string>();
        List<String> srcColumnNames = new List<string>();
        List<String> srcCharLength = new List<string>();
        List<String> destDataTypes = new List<string>();
        List<String> destColumnNames = new List<string>();
        List<String> destCharLength = new List<string>();

        // The response from the remote device.
        private static String response = String.Empty;

        //condtional variable to stop the threads.
        static volatile bool Run = true;


        public Client()
        {
            InitializeComponent();
        }


        /*
        * Function name : ConnectCallback(IAsyncResult ar)
        *  
        * Parameters    : IAsyncResult ar : status of an asynchronous operation.
        *  
        * Return        : void
        *  
        * Description   : This function completes the client connection and signal the main thread to proceed.
        * 
        */
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                // Signal that the connection has been made.
                connectDone.Set();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }




        /*
       * Function name : Receive(Socket client)
       *  
       * Parameters    : Socket client :store the connected client's info.
       *  
       * Return        : void
       *  
       * Description   : This function receives the messages in while loop until chat session is closed by either of the client or server.
       * 
       */
        private void Receive(Socket client)
        {
            try
            {
                //print the connection message.
                Thread t2 = new Thread(new ParameterizedThreadStart(ManageReceiveMessages));
                t2.Start("You are connected to the Server.\n");

                // Create the state object.

                StateObject state = new StateObject();
                state.workSocket = client;

                while (Run)
                {
                    receiveDone.Reset();

                    // Begin receiving the data from the remote device.
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);

                    //wait for a signal to continue further.
                    receiveDone.WaitOne();

                    //create a thread to show messages in a texbox.
                    Thread t1 = new Thread(new ParameterizedThreadStart(ManageReceiveMessages));
                    t1.Start("Server : " + response.ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }




        /*
         * Function name : ReceiveCallback(IAsyncResult ar)
         *  
         * Parameters    : IAsyncResult ar : status of an asynchronous operation
         *  
         * Return        : void
         *  
         * Description   : This function processes the received messages from the server.
         * 
         */
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                if (Run)
                {
                    // Retrieve the state object and the client socket 
                    // from the asynchronous state object.

                    StateObject state = (StateObject)ar.AsyncState;
                    Socket client = state.workSocket;

                    // Read data from the remote device.
                    int bytesRead = client.EndReceive(ar);

                    if (bytesRead > 0)
                    {
                        // There might be more data, so store the data received so far.
                        state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                        //if the received message is to close the client.
                        if (state.sb.ToString() == "*-*-*-*<EOF>")
                        {
                            //stop all the threads.
                            Run = false;

                            //signal the main thread to continue.
                            receiveDone.Set();
                            state.sb.Clear();

                            //clear the socket.
                            client.Shutdown(SocketShutdown.Both);
                            client.Close();

                            //close the form.
                            ThreadStart tStart = new ThreadStart(CloseForm);
                            Thread t1 = new Thread(tStart);
                            t1.Start();
                        }
                        else
                        {
                            //save the message.
                            response = state.sb.ToString();

                            //signal the main thread to continue.
                            receiveDone.Set();
                            state.sb.Clear();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }





        /*
        * Function name : Send(Socket client, String data)
        *  
        * Parameters    : Socket client : contains the info of the connected client.
        *               : String data   : data to send the server.
        *  
        * Return        : void
        *  
        * Description   : This function calls the asynchronous method to send the mesage.
        * 
        */

        private void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }


        /*
         * Function name : SendCallback(IAsyncResult ar)
         *  
         * Parameters    : IAsyncResult ar : status of an asynchronous operation
         *  
         * Return        : void
         *  
         * Description   : This function sends the message to the server.
         * 
         */
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);

                // Signal that all bytes have been sent.
                sendDone.Set();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }



        /*
        * Function name : ManageSendMessages(Object str)
        *  
        * Parameters    : Object str : data to be print in the textbox.
        *  
        * Return        : void
        *  
        * Description   : This function prints the messages sent to the server in blue color into the textbox using thread safe method.
        * 
        */
        private void ManageReceiveMessages(Object str)
        {
            if (Run)
            {
                // InvokeRequired property is true if child thread
                if (dataTB.InvokeRequired)
                {
                    // Callback is instance of delegate
                    MyCallback callback = new MyCallback(ManageReceiveMessages);
                    Invoke(callback, new object[] { str });
                }

                // Direct access to Control if parent thread
                else
                {
                    dataTB.Text += (String)str + "\n";
                }
            }
        }

        /*
        * Function name : CloseForm()
        *  
        * Parameters    : 
        *               
        * Return        : void
        *  
        * Description   : This function closes the form.
        * 
        */
        private void CloseForm()
        {
            try
            {
                // InvokeRequired property is true if child thread
                if (this.InvokeRequired)
                {
                    // Callback is instance of delegate
                    MyCallback1 callback = new MyCallback1(CloseForm);
                    Invoke(callback);
                }

                     // Direct access to Control if parent thread
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

   


        void processETL()
        {
            bool errorInCommonTable = false;        
            try
            {
                getSrcDestTableNames();
                getTableNamesFromSrcNonExistedInDest();
                errorInCommonTable = compareCommonTables();
                if (!errorInCommonTable)
                {
                    //send the query to create the tables which does not exist in destination.
                    if (tablesToTransfer.Count > 0)
                    {
                        const string message = "Are you sure you want to tranfer data?";
                        const string caption = "Transfer Data";
                        var result = MessageBox.Show(message, caption,
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);

                       
                        if (result == DialogResult.Yes)
                        {
                            const string message1 = "Are you sure you want to create tables?";
                            const string caption1 = "Create Table";
                            var result1 = MessageBox.Show(message1, caption1,
                                                         MessageBoxButtons.YesNo,
                                                         MessageBoxIcon.Question);
                            if (result1 == DialogResult.Yes)
                            {
                                sendCreateNonExistedTablesQueryToDest();
                                transferQuery(tablesToTransfer);
                            }                                                                         
                        }                      
                    }
                    else
                    {
                        MessageBox.Show("ERROR...There is no table to transfer from source to destination.");
                    }
                    transferQuery(commonTables);
                }
             
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }    
        }



        void getSrcDestTableNames()
        {
            MySqlConnection connection;
            MySqlCommand cmd;
            MySqlDataReader rdr = null;

            SqlConnection sqlconnection;
            SqlCommand command;
            SqlDataReader dataReader;

            string query = string.Empty;
            string sql = null;
            try
            {
                //get the names of the tables from source database  
                dataTB.Text += "Getting table names from source database..." + "\n";
                connection = openMySQLConnection();
                connection.Open();
                cmd = connection.CreateCommand();
                query = ("show tables");
                cmd.CommandText = query;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    //get the table names.
                    sourceTablenames.Add(rdr.GetString(0));
                }
                if (sourceTablenames.Count == 0)
                {
                    MessageBox.Show("Source database is empty.");
                }
                rdr.Close();
                cmd.Dispose();
                connection.Close();

                //check if the destination database is empty or not, if not then get the table names in a list                                           
                dataTB.Text += "Getting table names from destination database..." + "\n";
                sqlconnection = openMSSQLConnection();
                sql = "select TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'";
                sqlconnection.Open();
                command = new SqlCommand(sql, sqlconnection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    //get the table names of destination database
                    destinationTablenames.Add(dataReader.GetString(0));
                }
                if (destinationTablenames.Count == 0)
                {
                    MessageBox.Show("Destination database is empty.");
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        void getTableNamesFromSrcNonExistedInDest()
        {
            try
            {
                //get the table name that does not exist in destination but exists in source. 
                foreach (string srcTable in sourceTablenames)
                {
                    //these tables will be transferred to destination database.
                    if (!destinationTablenames.Contains(srcTable, StringComparer.OrdinalIgnoreCase))
                    {
                        tablesToTransfer.Add(srcTable);
                        dataTB.Text += "Table '" + srcTable + "' will be added to the destination \n";
                    }
                    else
                    {
                        commonTables.Add(srcTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
           



        void sendCreateNonExistedTablesQueryToDest()
        {
            int j = 0;
            string sql = string.Empty;
            getColNameDataTypeForNonExistedTables(tablesToTransfer[0].ToString());
            //if table does not exist in destination,then creata a table in destination
            for (int i = 0; i < tablesToTransfer.Count; i++)
            {
                if (Run)
                {
                    MessageBox.Show(String.Format("Table ' {0} ' is about to create in destination database.", tablesToTransfer[i]));
                    try
                    {
                        //send the CREATE table query
                        string createTableQuery = string.Empty;
                        for (j = 0; j < nonExistedTablesColNames.Count; j++)
                        {

                            if (nonExistedTableCharLength[j].ToString() == " ")
                            {
                                if (j == nonExistedTablesColNames.Count - 1)
                                {
                                    createTableQuery += nonExistedTablesColNames[j].ToString() + " " + nonExistedTableDataTypes[j].ToString();
                                }
                                else
                                {
                                    createTableQuery += nonExistedTablesColNames[j].ToString() + " " + nonExistedTableDataTypes[j].ToString() + ",";
                                }

                            }
                            else
                            {
                                if (j == nonExistedTablesColNames.Count - 1)
                                {
                                    createTableQuery += nonExistedTablesColNames[j].ToString() + " " + nonExistedTableDataTypes[j].ToString() + "(" + nonExistedTableCharLength[j] + ")";
                                }
                                else
                                {
                                    createTableQuery += nonExistedTablesColNames[j].ToString() + " " + nonExistedTableDataTypes[j].ToString() + "(" + nonExistedTableCharLength[j] + ")" + ",";
                                }
                            }
                        }
                        sql = string.Format("CREATE TABLE {0} ({1})<EOF>", tablesToTransfer[i], createTableQuery);
                        Send(client, sql);
                        sendDone.WaitOne();

                        Thread.Sleep(50);
                        Thread t1 = new Thread(new ParameterizedThreadStart(ManageReceiveMessages));
                        t1.Start(sql);

                        nonExistedTableCharLength.Clear();
                        nonExistedTableDataTypes.Clear();
                        nonExistedTablesColNames.Clear();

                        if (i < tablesToTransfer.Count - 1)
                        {
                            getColNameDataTypeForNonExistedTables(tablesToTransfer[i + 1].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }


        void getColNameDataTypeForNonExistedTables(string tableName)
        {
            MySqlConnection connection;
            MySqlCommand cmd;
            MySqlDataReader rdr = null;
            string query = string.Empty;
            int i = 0;
            try
            {
                //first check the source database                    
                dataTB.Text += "Getting the data types and name of columns of NON-EXISTED TABLES in destination db..." + "\n";
                connection = openMySQLConnection();
                connection.Open();
                cmd = connection.CreateCommand();
                query = string.Format("select COLUMN_NAME,data_type,character_maximum_length from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='{0}'",tableName);
                cmd.CommandText = query;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    //get the column names.                            
                    nonExistedTablesColNames.Add(rdr.GetString(0));
                    //get the datatypes
                    nonExistedTableDataTypes.Add(rdr.GetString(1));
                    //get the maximum lenght 
                    if (rdr.GetValue(2).ToString() == "")
                    {
                        nonExistedTableCharLength.Add(" ");
                    }
                    else
                    {
                        if (string.Equals(nonExistedTableDataTypes[i].ToString(), "text", StringComparison.OrdinalIgnoreCase))
                        {
                            nonExistedTableDataTypes[i] = nonExistedTableDataTypes[i].Replace("text", "varchar");
                            nonExistedTableCharLength.Add("8000");
                        }
                        else
                        {
                            nonExistedTableCharLength.Add(rdr.GetString(2));
                           
                        }
                    }
                    i++;
                }
                rdr.Close();
                cmd.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }



        void transferQuery(List<String> list)
        {
            //insert data into destination db
            MySqlConnection connection;
            MySqlCommand cmd;
            MySqlDataReader rdr = null;     
            string query = string.Empty;

            foreach (string tableName in list)
            {
                if (Run)
                {
                    string cols = string.Empty;

                    connection = openMySQLConnection();
                    connection.Open();
                    cmd = connection.CreateCommand();
                    query = string.Format("select *from {0}", tableName);
                    cmd.CommandText = query;
                    rdr = cmd.ExecuteReader();

                    getColNameDataTypeForNonExistedTables(tableName);
                    for (int i = 0; i < nonExistedTablesColNames.Count; i++)
                    {

                        if (i == nonExistedTablesColNames.Count - 1)
                        {
                            cols += nonExistedTablesColNames[i];
                        }
                        else
                        {
                            cols += nonExistedTablesColNames[i] + ",";
                        }
                    }
                    while (rdr.Read())
                    {
                        string values = string.Empty;
                        for (int i = 0; i < nonExistedTablesColNames.Count; i++)
                        {
                            if (i == nonExistedTablesColNames.Count - 1)
                            {
                                if( String.Equals(nonExistedTableDataTypes[i].ToString(),"tinyint",StringComparison.OrdinalIgnoreCase))
                                {
                                    if(rdr.GetBoolean(i))
                                    {
                                        values += 1;
                                    }
                                    else
                                    {
                                        values += 0;
                                    }
                                }
                                else
                                {
                                    if ((String.Equals(nonExistedTableDataTypes[i].ToString(), "varchar", StringComparison.OrdinalIgnoreCase)) || (String.Equals(nonExistedTableDataTypes[i].ToString(), "char", StringComparison.OrdinalIgnoreCase))
                                        || (String.Equals(nonExistedTableDataTypes[i].ToString(), "text", StringComparison.OrdinalIgnoreCase)) || (String.Equals(nonExistedTableDataTypes[i].ToString(), "date", StringComparison.OrdinalIgnoreCase)))
                                    {
                                        if (rdr.GetValue(i).ToString() == "")
                                        {
                                            values += "'" + rdr.GetValue(i).ToString() + "'";
                                        }
                                        else
                                        {

                                            values += "'" + rdr.GetString(i) + "'";
                                        }
                                    }
                                    else
                                    {
                                        if (rdr.GetValue(i).ToString() == "")
                                        {
                                            values += rdr.GetValue(i).ToString();
                                        }
                                        else
                                        {
                                            values += rdr.GetString(i);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (String.Equals(nonExistedTableDataTypes[i].ToString(), "tinyint", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (rdr.GetBoolean(i))
                                    {
                                        values += 1+",";
                                    }
                                    else
                                    {
                                        values += 0+",";
                                    }
                                }
                                else
                                {
                                    if ((String.Equals(nonExistedTableDataTypes[i].ToString(), "varchar", StringComparison.OrdinalIgnoreCase)) || (String.Equals(nonExistedTableDataTypes[i].ToString(), "char", StringComparison.OrdinalIgnoreCase))
                                        || (String.Equals(nonExistedTableDataTypes[i].ToString(), "text", StringComparison.OrdinalIgnoreCase)) || (String.Equals(nonExistedTableDataTypes[i].ToString(), "date", StringComparison.OrdinalIgnoreCase)))
                                    {
                                        if (rdr.GetValue(i).ToString() == "")
                                        {
                                            values += "'" + rdr.GetValue(i).ToString() + "'" + ",";
                                        }
                                        else
                                        {
                                            values += "'" + rdr.GetString(i) + "'" + ",";
                                        }
                                    }
                                    else
                                    {
                                        values += rdr.GetString(i) + ",";
                                    }
                                }
                            }
                        }
                        string insertQuery = string.Format("insert into {0}({1})values({2})<EOF>", tableName, cols, values);
                        Send(client, insertQuery);
                        sendDone.WaitOne();
                        Thread.Sleep(50);
                    }
                    rdr.Close();
                    cmd.Dispose();
                    connection.Close();
                    nonExistedTablesColNames.Clear();
                    nonExistedTableDataTypes.Clear();
                    nonExistedTableCharLength.Clear();
                }
            }
        }

   


        bool compareCommonTables()
        {
            MySqlConnection connection;
            MySqlCommand cmd;
            MySqlDataReader rdr = null;

            SqlConnection sqlconnection;
            SqlCommand command;
            SqlDataReader dataReader;

            string query = string.Empty;
            string sql = null;
            try
            {
                //get the column name, datatype and maximum character length
                 foreach (string tableName in commonTables)
                {
                    //first check the source database                    
                    dataTB.Text += "Getting the data types and name of columns from source db..." + "\n";
                    connection = openMySQLConnection();
                    connection.Open();
                    cmd = connection.CreateCommand();
                    query = string.Format("select COLUMN_NAME,data_type,character_maximum_length from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='{0}'", tableName);
                    cmd.CommandText = query;
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        //get the column names.                            
                        srcColumnNames.Add(rdr.GetString(0));
                        //get the datatypes
                        srcDataTypes.Add(rdr.GetString(1));
                        //get the maximum lenght 
                        if (rdr.GetValue(2).ToString() == "")
                        {
                            srcCharLength.Add(" ");
                        }
                        else
                        {
                            srcCharLength.Add(rdr.GetValue(2).ToString());
                        }
                    }
                    rdr.Close();
                    cmd.Dispose();
                    connection.Close();

                    //check the destination database
                    dataTB.Text += "Getting the data types and name of columns from destination db..." + "\n";
                    sqlconnection = openMSSQLConnection();
                    sql = string.Format("select COLUMN_NAME,data_type,character_maximum_length from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='{0}'", tableName);
                    sqlconnection.Open();
                    command = new SqlCommand(sql, sqlconnection);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        //getting the column names
                        destColumnNames.Add(dataReader.GetString(0));
                        //getting the data types
                        destDataTypes.Add(dataReader.GetString(1));
                        //get the maximum lenght
                        if (dataReader.GetValue(2).ToString() == "")
                        {
                            destCharLength.Add(" ");
                        }
                        else
                        {
                            destCharLength.Add(dataReader.GetValue(2).ToString());
                        }
                    }
                    dataReader.Close();
                    command.Dispose();
                    sqlconnection.Close();

                    //compare the number of datatypes & columns of common table
                    if ((srcColumnNames.Count == destColumnNames.Count) && (srcDataTypes.Count == destDataTypes.Count))
                    {
                        int i = 0;
                        //compare the column names,data types and maximum character length
                        for (i = 0; i < srcColumnNames.Count; i++)
                        {
                            //the order of table differs, which will create problem in inserting data. Stop here
                            if (!(srcColumnNames[i] == destColumnNames[i]) && (srcDataTypes[i] == destDataTypes[i]) && (srcCharLength[i] == destCharLength[i]))
                            {
                                //notify the user and abort the operation.
                                MessageBox.Show(String.Format("ERROR...problem in column names, data types or maximum charater length for common table '{0}'.", tableName));
                                return true;

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Format("ERROR...number of columns differ in common table '{0}'.", tableName));
                    }

                    //clear the lists  
                    srcColumnNames.Clear();
                    srcDataTypes.Clear();
                    destDataTypes.Clear();
                    destColumnNames.Clear();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }



        MySqlConnection openMySQLConnection()
        {
            string MyConnectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", sourceServer.Text.ToString(), sourceDbName.Text.ToString(), sourceUsername.Text.ToString(), sourcePassword.Text.ToString());
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            return connection;
        }



        SqlConnection openMSSQLConnection()
        {
            string connetionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", destServer.Text.ToString(), destinationDbName.Text.ToString(), destinationUname.Text.ToString(), destinationPassword.Text.ToString());
            SqlConnection connection = new SqlConnection(connetionString);
            return connection;
        }


        private void testSource_Click(object sender, EventArgs e)
        {

            //check the source DB textfields
            if (sourceDbName.Text.Length > 0)
            {
                if (sourcePassword.Text.Length > 0)
                {
                    if (sourceUsername.Text.Length > 0)
                    {
                        //test the database.
                        string MyConnectionString ;
                        MyConnectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};",sourceServer.Text.ToString(),sourceDbName.Text.ToString(),sourceUsername.Text.ToString(),sourcePassword.Text.ToString());
                        MySqlConnection connection = new MySqlConnection(MyConnectionString);
                        MySqlCommand cmd;                    
                        try
                        {                  
                            connection.Open();
                            cmd = connection.CreateCommand();
                            string query = string.Format("USE {0}",sourceDbName.Text.ToString());
                            cmd.CommandText = query;                                      
                            cmd.ExecuteNonQuery();
                            connection.Close();
                            MessageBox.Show("Success...you can access the database.");
                            srcChecked = true;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Can not open connection ! ");
                        }
                    }
                    else
                    {
                        MessageBox.Show("ERROR...Username cannot be blank.");
                    }
                }
                else
                {
                    MessageBox.Show("ERROR...Password cannot be blank.");
                }
            }
            else
            {
                MessageBox.Show("ERROR...Database name cannot be blank.");
            }
        }


        private void transferData_Click(object sender, EventArgs e)
        {
            if (srcChecked)
            {
                if (destChecked)
                {

                    if (startClientConnection())
                    {
                        Run = true;
                        processETL();
                    }
                }
                else
                {
                    MessageBox.Show("ERROR...first check the existence of destination database in MSSQL.");
                }
            }
            else
            {
                MessageBox.Show("ERROR...first check the existence of source database in MySQL.");
            }

        }


        private bool startClientConnection()
        {
            try
            {
                //check if textbox is empty
                if ((IPaddressTB.Text == "") || (portTB.Text == ""))
                {
                    MessageBox.Show("Enter both an IP address and port to connect. ");
                    return false;
                }
                else
                {
                    // Establish the remote endpoint for the socket.   
                    ipHostInfo = Dns.Resolve(IPaddressTB.Text.ToString());
                    ipAddress = ipHostInfo.AddressList[0];
                    Int32.TryParse(portTB.Text, out port);
                    remoteEP = new IPEndPoint(ipAddress, port);

                    // Create a TCP/IP socket.
                    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // Connect to the remote endpoint.
                    client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                    connectDone.WaitOne();

                    //create a thread to start receiving the messages.
                    ThreadStart tStart = new ThreadStart(ReceiveMessage);
                    Thread t1 = new Thread(tStart);
                    t1.Start();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        private void ReceiveMessage()
        {
            Receive(client);

        }
      

        private void testDestination_Click(object sender, EventArgs e)
        {
               //check the source DB textfields
            if (destinationDbName.Text != "")
            {
                if (destinationPassword.Text != "")
                {
                    if (destinationUname.Text !="")
                    {
                        if(destServer.Text != "")
                        {
                            //test the database.
                            string connetionString = null;
                            SqlConnection connection;
                            SqlCommand command;
                            string sql = null;
                            SqlDataReader dataReader;
                            connetionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", destServer.Text.ToString(), destinationDbName.Text.ToString(), destinationUname.Text.ToString(), destinationPassword.Text.ToString());

                            sql = string.Format("USE {0}", destinationDbName.Text.ToString());

                            connection = new SqlConnection(connetionString);
                            try
                            {
                                connection.Open();
                                command = new SqlCommand(sql, connection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    MessageBox.Show(dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2));
                                }
                                dataReader.Close();
                                command.Dispose();
                                connection.Close();
                                MessageBox.Show("Success...you can access the database.");
                                destChecked = true;
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Can not open connection ! ");
                            }
                        }
                        else
                        {
                            MessageBox.Show("ERROR...Server name cannot be blank.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("ERROR...Username cannot be blank.");
                    }
                }
                else
                {
                    MessageBox.Show("ERROR...Password cannot be blank.");
                }
            }
            else
            {
                MessageBox.Show("ERROR...Database name cannot be blank.");
            }
        }


        private void Client_FormClosed(object sender, FormClosedEventArgs e)
        {
            Run = false;

        }
    }
}
