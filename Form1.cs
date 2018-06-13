using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.IO;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace SeleniumFlix
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private IWebDriver driver;

        public IWebDriver Driver
        {
            get
            {
                if(driver == null)
                {
                    ChromeDriverService cs = ChromeDriverService.CreateDefaultService();
                    // The Chromedriver cmd window can be hidden with: cs.HideCommandPromptWindow = true;
                    ChromeOptions co = new ChromeOptions();
                    // Insecure Certificates can be accepted with: co.AcceptInsecureCertificates = true;
                    driver = new ChromeDriver(cs, co); // The driver doesn't HAVE to be called with the service or options. new ChromeDriver(); works too.
                    return driver;
                }

                return driver;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //Specify a URL to navigate to
            Driver.Url = "https://www.netflix.com/Login";

            try
            {
                //Wait 5 seconds = Thread.Sleep(5000);

                //Find a web element. Element search is CASE SENSITIVE
                Driver.FindElement(By.Id("email")).SendKeys(File.ReadAllText(@"C:\Users\mape\Documents\un.txt"));
                Driver.FindElement(By.Id("password")).SendKeys(File.ReadAllText(@"C:\Users\mape\Documents\pwd.txt"));
                
                Driver.FindElement(By.CssSelector("button.btn.login-button.btn-submit.btn-small")).Click();

                Driver.FindElement(By.TagName("div[data-reactid=19]")).Click();
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred:\r\n" + ex.Message + "\r\nThis WebBrowser will close.", 
                    "Error!", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);

                driver.Quit();
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //Closes all active browsers and the driver. Same as driver.dispose();
            //driver.close() only closes the browser. 
            driver.Quit();
        }
    }
}
