using Widely.Models;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Configuration;

namespace Widely
{
    
    public static class Global_Functions
    {
       
        
        private static string AddEmail = "wb.cctv@amrelisteels.com";
        private static string FromEmail = "system.notify@amrelisteels.com";
        private static string NotifyEmail = "system.notify@amrelisteels.com";
        private static string PassEmail = "ASL@1234566";
        private static string HostEmail = "mail.amrelisteels.com";
        private static int PortEmail = 25;

        static string macAddresses = Global_Functions.GetMac();
        public static System.Timers.Timer WeightTimer;

        

        

        public static string GetMac()
        {
            string macAddresses = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }
        public static string GetIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        
        

        public static void Sendemail(string subject, string body)
        {
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(AddEmail);
                mail.From = new MailAddress(FromEmail, "", System.Text.Encoding.UTF8);
                mail.Subject = subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = body;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential(NotifyEmail, PassEmail);
                client.Port = PortEmail;
                client.Host = HostEmail;
                client.EnableSsl = false;
                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    Exception ex2 = ex;
                    string errorMessage = string.Empty;
                    while (ex2 != null)
                    {
                        errorMessage += ex2.ToString();
                        ex2 = ex2.InnerException;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                
            }
        }
       


        
       

      
    }
}
