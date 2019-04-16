using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSCP;

namespace SFTP
{
    public class SFTP
    {
        //localpath = @"d:\toupload\*"
        //remotepath = "/home/user/"
        public static String PutFiles(string hostname, string username, string password, int portnumber, string localpath, string remotepath, string VarSshHostKeyFingerprint)
        {
            try
            {
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    HostName = hostname,//"b2btest.te.com",
                    UserName = username,//"xftp_ils",
                    Password = password,//"ILS@110789",
                    PortNumber = portnumber,//6084,
                    SshHostKeyFingerprint = VarSshHostKeyFingerprint
                    //TimeoutInMilliseconds = 99999999//300000,
                };
                /*
                // Setup session options
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Ftp,
                    HostName = "172.16.12.88",
                    UserName = "jpalma",
                    Password = "sonora1234",
                    SshHostKeyFingerprint = "ssh-rsa 2048 xx:xx:xx:xx:xx:xx:xx:xx:..."
                };
                */
                using (Session session = new Session())
                {
                    //sessionOptions.AddRawSettings("Utf", "0"); 

                    // Connect
                    session.Open(sessionOptions);

                    // Upload files
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;
                    transferOptions.PreserveTimestamp = false;
                    TransferOperationResult transferResult;
                    transferResult =
                        session.PutFiles(localpath, remotepath, false, transferOptions);

                    // Throw on any error
                    transferResult.Check();

                    // Print results
                    foreach (TransferEventArgs transfer in transferResult.Transfers)
                    {
                        //Console.WriteLine("Upload of {0} succeeded", transfer.FileName);
                    }
                }

                Console.WriteLine("Exito");
                return "Exito";
                
            }
            catch (Exception e)
            {

                Console.WriteLine("Error: {0}", e.Message.ToString());
                return "Error:"+e.Message.ToString();
            }
        }

        //localpath = @"d:\toupload\*"
        //remotepath = "/home/user/"

        public static string GetFiles(string hostname, string username, string password, int portnumber, string localpath, string remotepath, string VarSshHostKeyFingerprint)
        {
            try
            {
                // Setup session options
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    HostName = hostname,//"b2btest.te.com",
                    UserName = username,//"xftp_ils",
                    Password = password,//"ILS@110789",
                    PortNumber = portnumber,//6084,
                    //TimeoutInMilliseconds = 99999999 //,
                    SshHostKeyFingerprint = VarSshHostKeyFingerprint
                    //SshHostKeyFingerprint = "ssh-rsa 2048 xx:xx:xx:xx:xx:xx:xx:xx..."
                };

                using (Session session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);

                    //Console.WriteLine("session.ToString()" + session.ToString());
                    
                    // Download files
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;

                    TransferOperationResult transferResult;
                    transferResult =
                        session.GetFiles(remotepath, localpath, false, transferOptions);

                    // Throw on any error
                    transferResult.Check();

                    // Print results
                    foreach (TransferEventArgs transfer in transferResult.Transfers)
                    {
                        //Console.WriteLine("Download of {0} succeeded", transfer.FileName);
                    }
                     
                }
                Console.WriteLine("Exito");
                return "Exito";

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                return "Error:"+e.Message.ToString();
            }
        }


    }
}
