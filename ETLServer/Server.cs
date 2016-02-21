/*
 * File         : Server.cs
 * Project      : ETL
 * Author       : SAMEER SAPRA
 * Date         : 3/27/2015
 * Description  : This is the server application which receives the queries from client and runs it in MSSQL.
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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;

using System.Threading;

namespace ETLServer
{
    public partial class Server : Form
    {
        //constants
        private const int kZERO = 0;
        private const int kNEGATIVE_ONE = -1;
        private const int kFIVE = 5;
        private const int kLISTEN = 100;
        private const int kBYTES = 1024;

        //events to manage the threads.
        private ManualResetEvent connectDone = new ManualResetEvent(false);
        private ManualResetEvent receiveDone = new ManualResetEvent(false);

        //delegate used to point a function.
        delegate void MyCallback(Object obj);

        //condition variable to stop the different threads.
        static volatile bool Run = true;

        IPHostEntry ipHostInfo;
        IPAddress ipAddress;
        IPEndPoint localEndPoint;
        Socket listener;

        List<object> allClients = new List<object>();
        public Server()
        {
            InitializeComponent();
        }      



        /*
         * Function name    : AcceptCallback(IAsyncResult ar)
         * 
         * Parameters       : IAsyncResult ar : status of an asynchronous operation.
         *                  
         * Return           : void
         * 
         * Description      : This function signals the main thread to continue, and creates a client's state object. It also accepts receives messages fromt the client end under a condtion.
         * 
         */

        public void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                if (Run)
                {
                    // Signal the main thread to continue.
                    connectDone.Set();

                    // Get the socket that handles the client request.
                    Socket listener = (Socket)ar.AsyncState;
                    Socket handler = listener.EndAccept(ar);

                    // Create the state object.
                    StateObject state = new StateObject();
                    state.workSocket = handler;
                    allClients.Add(state);

                    //receive message
                    while (true)
                    {
                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);

                        //wait for a signal to continue.
                        receiveDone.WaitOne();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /*
        * Function name    : ReadCallback(IAsyncResult ar)
        * 
        * Parameters       : IAsyncResult ar : status of an asynchronous operation.
        *                  
        * Return           : void
        * 
        * Description      : This function signals the main thread to continue, and recieve a client message.Later it creates a thread to print a message in textbox.
        * 
        */
        public void ReadCallback(IAsyncResult ar)
        {
            if (Run)
            {
                String tempContent = String.Empty;
                String content = String.Empty;

                // Retrieve the state object and the handler socket
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;
             
                // Read data from the client socket. 
                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > kZERO)
                {
                    // There might be more data, so store the data received so far.
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    content = state.sb.ToString();

                    // Check for end-of-file tag. 
                    if (content.IndexOf("<EOF>") > kNEGATIVE_ONE)
                    {
                    
                        tempContent = content.Remove(content.Length - kFIVE);
                        //signal the main thread to continue.
                        
                        //send the content to MSSQL 
                        sendQueryToMSSQL(tempContent);

                        //clear the data.
                        state.sb.Clear();
                        Array.Clear(state.buffer, 0, state.buffer.Length);
                        receiveDone.Set();
                        Thread t1 = new Thread(new ParameterizedThreadStart(manageServerMessages));
                        t1.Start("Read" + content.Length.ToString() + "bytes from socket. Data : " + tempContent.ToString() + "\n\n");
                    
                        
                    }
                    else
                    {
                        // Not all data received. Get more.
                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                    }
                 
                }
            }
        }



        /*
         * Function name    : Send((Socket handler, String data)
         * 
         * Parameters       : Socket handler : client's handle to which message has to send.
         *                  : String data    : message to send to the client 
         *                  
         * Return           : void
         * 
         * Description      : This function sends the message to the client.
         * 
         */
        private void Send(Socket handler, String data)
        {
            if (Run)
            {
                // Convert the string data to byte data using ASCII encoding.
                byte[] byteData = Encoding.ASCII.GetBytes(data);


                // Begin sending the data to the remote device.
                handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
            }

        }




        /*
         * Function name    : SendCallback((Socket handler, String data)
         * 
         * Parameters       : IAsyncResult ar : state of the asynchronous operation.
         *                  
         * Return           : void
         * 
         * Description      : This function sends the message to the clienta dn diplays the sent message on the textbox.
         * 
         */
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                if (Run)
                {
                    // Retrieve the socket from the state object.
                    Socket handler = (Socket)ar.AsyncState;

                    // Complete sending the data to the remote device.
                    int bytesSent = handler.EndSend(ar);

                    //create a thread to display the message on the textbox.
                    Thread t2 = new Thread(new ParameterizedThreadStart(manageServerMessages));
                    t2.Start("Sent" + bytesSent.ToString() + " bytes to client.\n");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        /*
         * Function name    : manageServerMessages(Object str)
         * 
         * Parameters       : Object str : store the messge to print in the textbox.
         *                  
         * Return           : void
         * 
         * Description      : This function is used to access the textbox as thread safe to print all the messages. 
         * 
         */
        private void manageServerMessages(Object str)
        {
            if (Run)
            {
                // InvokeRequired property is true if child thread
                if (serverDataTB.InvokeRequired)
                {
                    // Callback is instance of delegate
                    MyCallback callback = new MyCallback(manageServerMessages);
                    Invoke(callback, new object[] { str });
                }
                else
                {
                    // Direct access to Control if parent thread              
                    serverDataTB.Text += (String)str + "\n";
                }
            }
        }


        void sendQueryToMSSQL(string query)
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;

            connetionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", destServer.Text.ToString(), destinationDbName.Text.ToString(), destinationUname.Text.ToString(), destinationPassword.Text.ToString());
            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AcceptClientConnections()
        {
            while (Run)
            {
                // Set the event to nonsignaled state.
                connectDone.Reset();

                // Start an asynchronous socket to listen for connections.
                listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                // Wait until a connection is made before continuing.
                connectDone.WaitOne();
            }
        }

        private void startServer_Click(object sender, EventArgs e)
        {
            if ((portToConnect.Text != ""))
            {
                Run = true;

                // Establish the local endpoint for the socket.
                // The DNS name of the computer        
                ipHostInfo = Dns.Resolve(Dns.GetHostName());
                ipAddress = ipHostInfo.AddressList[0];
                int port;
                Int32.TryParse(portToConnect.Text, out port);
                localEndPoint = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.
                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Bind the socket to the local endpoint and listen for incoming connections.
                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(kLISTEN);

                    //create a thread to accept the connections.
                    ThreadStart tStart = new ThreadStart(AcceptClientConnections);
                    Thread t1 = new Thread(tStart);
                    t1.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("ERROR...enter the port to start the server.");
            }
        }


        private void checkDestination_Click(object sender, EventArgs e)
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
    }
}
