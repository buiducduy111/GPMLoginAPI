import subprocess
import time
# python.exe -m pip install --upgrade pip
# pip install selenium
from selenium.webdriver.chrome import service
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.common.keys import Keys

from UndetectChromeDriver import UndetectChromeDriver
#               ------------------------------------------------------
#              * Code theo hướng dẫn tại file: 
#              *              <THƯ_MỤC_GPM>/docs/api.md
#              *              
#              * (Mở bằng trình đọc file .md để tường minh)
#              * ------------------------------------------------------ */


#             // Bước 1:
#             // Gọi commandline vào GPMLogin
#             // Bạn có thể mở Profile có sẵn hoặc tạo Profile mới (cấu hình random) qua việc theo tác commandline với GPMLogin.exe
gpmPath = "C:\\Users\\frien\\Desktop\\Test public tools\\GPMLogin_full_132"; # Đường dẫn tới thư mục tool GPMLogin
remotePort = 11269 # random port
profileId = "isfboozwefoxiwg5srfdhtbohjgsd1tm8xw8mxvsg3jo4g0yz6"; # Copy ID trên App GPMLogin

# Gọi profile có sẵn
args = [gpmPath + '\\GPMLogin.exe', '--mode=open', f'--profile_id={profileId}', f'--remote_port={remotePort}', '--turn-off-whats-new']

# hoặc tạo profile mới
args = [gpmPath + '\\GPMLogin.exe', '--mode=new', f'--profile_name=ngochoaitn-gpm-test', f'--remote_port={remotePort}', '--turn-off-whats-new']

subprocess.call(args, cwd=gpmPath) # phải có cwd nha các bác <3
time.sleep(3)

# Khởi tạo selenium AntiDetect kết nối vào port đã khởi tạo
options = Options()
options.debugger_address = f'127.0.0.1:{remotePort}'
myService  = service.Service("C:\\Users\\frien\\Desktop\\Test public tools\\GPMLogin_full_132\\gpmdriver.exe")
driver = UndetectChromeDriver(service = myService, options=options)

driver.GetByGpm('https://nowsecure.nl/')

"""
driver.GetByGpm('https://accounts.google.com/signin/v2/identifier?continue=https%3A%2F%2Fmail.google.com%2Fmail%2F&service=mail&sacu=1&rip=1&flowName=GlifWebSignIn&flowEntry=ServiceLogin')
time.sleep(3)

driver.find_element_by_name("identifier").send_keys('ngochoaitn3')
time.sleep(1)

driver.find_element_by_name("identifier").send_keys(Keys.RETURN)
time.sleep(3)

driver.find_element_by_name("password").send_keys('test1')
time.sleep(1)

driver.find_element_by_name("password").send_keys(Keys.RETURN)
time.sleep(0.5)
"""

input()