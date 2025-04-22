using DK_Project.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DK_Project
{
    internal class server
    {
        private ArrayList m_aryClients = new ArrayList();
        QuaMienDiSanEntities _entity = new QuaMienDiSanEntities();
        public string ip;
        string server_ip = ConfigurationManager.AppSettings["IP"];
        public server()
        {
            // server app = new server(); ;
            Console.WriteLine("*** Chat Server Started {0} *** ", DateTime.Now.ToString("G"));
            truncateLoginUser();


            const int nPortListen = 399;

            IPAddress[] aryLocalAddr = null;
            String strHostName = "";
            try
            {

                strHostName = Dns.GetHostName();
                IPHostEntry ipEntry = Dns.GetHostByName(strHostName);
                aryLocalAddr = ipEntry.AddressList;
                ip = aryLocalAddr[0].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error trying to get local address {0} ", ex.Message);
            }
            
            if (aryLocalAddr == null || aryLocalAddr.Length < 1)
            {
                Console.WriteLine("Unable to get local address");
                return;
            }
            //Console.WriteLine("Listening on : [{0}] {1}:{2}", strHostName, aryLocalAddr[5], nPortListen);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //listener.Bind(new IPEndPoint(aryLocalAddr[0], 399));
            //listener.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 399));
            listener.Bind(new IPEndPoint(IPAddress.Parse(server_ip), 399));
            listener.Listen(10);

            listener.BeginAccept(new AsyncCallback(OnConnectRequest), listener);

            // Console.WriteLine("Press Enter to exit");
            //Console.ReadLine();     server app = new server();
            //Console.WriteLine("OK that does it! Screw you guys I'm going home...");

            // listener.Close();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void truncateLoginUser()
        {
            _entity.Database.ExecuteSqlCommand("TRUNCATE TABLE [ds_userlogin]");
        }

        public void OnConnectRequest(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;
            NewConnection(listener.EndAccept(ar));
            listener.BeginAccept(new AsyncCallback(OnConnectRequest), listener);
        }


        public void NewConnection(Socket sockClient)
        {

            SocketChatClient client = new SocketChatClient(sockClient);
            m_aryClients.Add(client);
            Console.WriteLine("Client {0}, joined", client.Sock.RemoteEndPoint);

            addOrRemoveUserByEndPoint(client.Sock.RemoteEndPoint.ToString());

            // Get current date and time.
            /*DateTime now = DateTime.Now;
            String strDateLine = "Welcome " + now.ToString("G") + "\n\r";

            // Convert to byte array and send.
            Byte[] byteDateLine = System.Text.Encoding.ASCII.GetBytes(strDateLine.ToCharArray());
            client.Sock.Send(byteDateLine, byteDateLine.Length, 0);*/

            client.SetupRecieveCallback(this);
        }


        public void OnRecievedData(IAsyncResult ar)
        {
            SocketChatClient client = (SocketChatClient)ar.AsyncState;
            byte[] aryRet = client.GetRecievedData(ar);
            if (aryRet.Length < 1)
            {
                Console.WriteLine("Client {0}, disconnected", client.Sock.RemoteEndPoint);
                addOrRemoveUserByEndPoint(client.Sock.RemoteEndPoint.ToString(), true);
                int doi_disconnet = getIdFromIpAddress(client.Sock.RemoteEndPoint.ToString());
                string message = doi_disconnet + ",cli,connected,off";
                byte[] n_message = Encoding.ASCII.GetBytes(message);
                client.Sock.Close();
                m_aryClients.Remove(client);
                foreach (SocketChatClient clientSend in m_aryClients)
                {
                    try
                    {
                        clientSend.Sock.Send(n_message);
                    }
                    catch
                    {
                        //Console.WriteLine("Send to client {0} failed", client.Sock.RemoteEndPoint);
                        clientSend.Sock.Close();
                        m_aryClients.Remove(client);
                        return;
                    }
                }
                return;
            }
            foreach (SocketChatClient clientSend in m_aryClients)
            {
                try
                {
                    clientSend.Sock.Send(aryRet);
                }
                catch
                {
                    //Console.WriteLine("Send to client {0} failed", client.Sock.RemoteEndPoint);
                    clientSend.Sock.Close();
                    m_aryClients.Remove(client);
                    return;
                }
            }
            client.SetupRecieveCallback(this);
        }

        public void addOrRemoveUserByEndPoint(string endPoint, bool isDelete = false)
        {
            if (endPoint.Length > 0)
            {
                string[] spl = endPoint.Split(':');

                if (spl[0].Contains(server_ip))
                {
                    ds_userlogin _user = new ds_userlogin
                    {
                        tendoi = "Máy điều khiển",
                        tennguoichoi = "Máy điều khiển",
                        role = "DK",
                        doiid = 0,
                        trangthai = 1
                    };
                    _entity.ds_userlogin.Add(_user);
                }
                else
                {
                    string ip_client = spl[0];
                    var doiInfo = _entity.ds_doi.FirstOrDefault();
                    if (!isDelete)
                    {
                        if (doiInfo != null)
                        {
                            var _existUser = _entity.ds_userlogin.FirstOrDefault(x => x.doiid == doiInfo.doiid);
                            if (_existUser != null)
                            {
                                _existUser.trangthai = 1;
                                _existUser.isreconnect = true;
                            }
                            else
                            {
                                ds_userlogin _user = new ds_userlogin
                                {
                                    tendoi = doiInfo.tendoi,
                                    tennguoichoi = doiInfo.tennguoichoi,
                                    role = doiInfo.vaitro,
                                    doiid = doiInfo.doiid,
                                    trangthai = 1
                                };
                                _entity.ds_userlogin.Add(_user);
                            }
                        }
                    }
                    else
                    {
                        if (doiInfo != null)
                        {
                            var _existUser = _entity.ds_userlogin.FirstOrDefault(x => x.doiid == doiInfo.doiid);
                            if (_existUser != null)
                            {
                                _existUser.trangthai = 0;
                            }
                        }
                    }
                }
                _entity.SaveChanges();
            }
        }
        public int getIdFromIpAddress(string ipAddress)
        {
            try
            {
                string[] spl = ipAddress.Split(':');
                string ip = spl[0];
                var doiInfo = _entity.ds_doi.FirstOrDefault();
                if (doiInfo != null)
                {
                    return doiInfo.doiid;
                }
            }
            catch (Exception)
            {

                return -1;
            }
            return 0;
        }
    }
    internal class SocketChatClient
    {
        private Socket m_sock;						// Connection to the client
        private byte[] m_byBuff = new byte[50];		// Receive data buffer
        public SocketChatClient(Socket sock)
        {
            m_sock = sock;
        }

        public Socket Sock
        {
            get { return m_sock; }
        }
        public void SetupRecieveCallback(server app)
        {
            try
            {
                AsyncCallback recieveData = new AsyncCallback(app.OnRecievedData);
                m_sock.BeginReceive(m_byBuff, 0, m_byBuff.Length, SocketFlags.None, recieveData, this);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Recieve callback setup failed! {0}", ex.Message);
            }
        }


        public byte[] GetRecievedData(IAsyncResult ar)
        {
            int nBytesRec = 0;
            try
            {
                nBytesRec = m_sock.EndReceive(ar);
            }
            catch { }
            byte[] byReturn = new byte[nBytesRec];
            Array.Copy(m_byBuff, byReturn, nBytesRec);


            return byReturn;
        }
    }
}
