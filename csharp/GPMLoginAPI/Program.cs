using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GPMSeleniumSample
{
    class Program
    {
        static void Main(string[] args)
        {
            /* ------------------------------------------------------
             * Code theo hướng dẫn tại file: 
             *              <THƯ_MỤC_GPM>/docs/api.md
             *              
             * (Mở bằng trình đọc file .md để tường minh)
            * ------------------------------------------------------ */


            // Bước 1:
            // Gọi commandline vào GPMLogin
            // Bạn có thể mở Profile có sẵn hoặc tạo Profile mới (cấu hình random) qua việc theo tác commandline với GPMLogin.exe

            string gpmPath = @"C:\Users\frien\Desktop\Test public tools\GPMLogin_full_132"; // Đường dẫn tới thư mục tool GPMLogin
            int remotePort = findFreePort();
            string profileId = "lhoioqxa96dvgmge3gc3ig0z47chzkc7cgnvdlgfk8dj7tyqsa"; // Copy ID trên App GPMLogin

            // Gọi profile có sẵn
            /*
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                WorkingDirectory = gpmPath, // Bắt buộc set WorkingDirectory vào GpmPath để gọi commandline
                FileName = gpmPath + "\\GPMLogin.exe",
                Arguments = $"--mode=open --profile_id={profileId} --remote_port={remotePort}"
            };
            */

            // hoặc tạo profile mới
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                WorkingDirectory = gpmPath, // Bắt buộc set WorkingDirectory vào GpmPath để gọi commandline
                FileName = gpmPath + "\\GPMLogin.exe",
                Arguments = $"--mode=new --profile_name=\"Test Simple API\" --remote_port={remotePort}"
            };
            

            Process.Start(startInfo);
            Thread.Sleep(3000);

            // Khởi tạo selenium AntiDetect kết nối vào port đã khởi tạo
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(gpmPath, "gpmdriver.exe");

            ChromeOptions options = new ChromeOptions();
            options.DebuggerAddress = "127.0.0.1:" + remotePort;

            UndetectChromeDriver driver = new UndetectChromeDriver(service, options);

            // LOGIN GMAIL
            driver.Get("https://nowsecure.nl/");

            return;
            driver.Get("https://accounts.google.com/signin/v2/identifier?continue=https%3A%2F%2Fmail.google.com%2Fmail%2F&service=mail&sacu=1&rip=1&flowName=GlifWebSignIn&flowEntry=ServiceLogin");
            Thread.Sleep(3000);

            var input = driver.FindElement(By.Name("identifier"));
            input.SendKeys("buiducduyit111");
            Thread.Sleep(500);
            input.SendKeys(Keys.Enter);

            Thread.Sleep(3000);

            input = driver.FindElement(By.Name("password"));
            input.SendKeys("abc");
            Thread.Sleep(500);
            input.SendKeys(Keys.Enter);
        }

        // Tìm port hợp lệ
        static int findFreePort()
        {
            int port = 0;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 0);
                socket.Bind(localEP);
                localEP = (IPEndPoint)socket.LocalEndPoint;
                port = localEP.Port;
            }
            finally
            {
                socket.Close();
            }
            return port;
        }
    }
}
