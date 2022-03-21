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

            string gpmPath = @"D:\out_source\2021_projects\GPMLogin\GPMLogin\GPMLogin\bin\Debug"; // Đường dẫn tới thư mục tool GPMLogin
            int remotePort = findFreePort();
            string profileId = "c0savast2qp1f2mhx6kd0ez8k52eihuq3nbzbazl1bc7rzblk9"; // Copy ID trên App GPMLogin

            // Gọi profile có sẵn
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                WorkingDirectory = gpmPath, // Bắt buộc set WorkingDirectory vào GpmPath để gọi commandline
                FileName = gpmPath + "\\GPMLogin.exe",
                Arguments = $"--mode=open --profile_id={profileId} --remote_port={remotePort}"
            };

            // hoặc tạo profile mới
            startInfo = new ProcessStartInfo()
            {
                WorkingDirectory = gpmPath, // Bắt buộc set WorkingDirectory vào GpmPath để gọi commandline
                FileName = gpmPath + "\\GPMLogin.exe",
                Arguments = $"--mode=new --profile_name=\"Duy dep zai khoai to\" --remote_port={remotePort}"
            };

            Process.Start(startInfo);
            Thread.Sleep(3000);

            // Khởi tạo selenium AntiDetect kết nối vào port đã khởi tạo
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(gpmPath, "gpmdriver.exe");

            ChromeOptions options = new ChromeOptions();
            options.DebuggerAddress = "127.0.0.1:" + remotePort;
            options.AddArguments(new string[]
            {
                "--no-default-browser-check",
                "--no-first-run"
            });

            UndetectChromeDriver driver = new UndetectChromeDriver(service, options);

            // Đối với tạo profile mới, sẽ có tab Welcome, cần switch tới tab thứ 2 để thực hiện auto (bỏ qua tab Welcome)
            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);


            // LOGIN GMAIL
            driver.Get("https://accounts.google.com/signin/v2/identifier?continue=https%3A%2F%2Fmail.google.com%2Fmail%2F&service=mail&sacu=1&rip=1&flowName=GlifWebSignIn&flowEntry=ServiceLogin");
            Thread.Sleep(3000);

            var input = driver.FindElement(By.Name("identifier"));
            input.SendKeys("test1");
            Thread.Sleep(500);
            input.SendKeys(Keys.Enter);

            Thread.Sleep(3000);

            input = driver.FindElement(By.Name("password"));
            input.SendKeys("test1");
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
