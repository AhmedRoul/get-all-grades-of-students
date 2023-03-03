using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Text;
using System.IO;


namespace getAllNategaBella
{

    public class Program
    {

        static void Main(string[] args)
        {
            int MaxDrivers = 1;
            IWebDriver[] drive = new IWebDriver[MaxDrivers];
            string URL = "https://c8bkr414.caspio.com/dp/2082c0005a05eaf752fa4bbbb452?fbclid=IwAR0jcN-zqRDNus3uM6pgp-6CbBdgNPb7Dg1pTzRDezObqMYKhkEejaV3Tl4&";

            RUN run = new RUN(drive, URL);
            run.ThreadsRun();
            List<User> users = run.users;

            run.SaveData();
            //IWebDriver drivers;
            //Edge = new EdgeDriver();
            //// navigate to URL  
            //drivers.Manage().Window.Maximize();
            //drivers.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //drivers.Navigate().GoToUrl("https://www.facebook.com/");

            //drivers.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(40);




        }
    }
    public class RUN
    {
        private IWebDriver[] drivers;
        private string URL;
        private int number = 1;
        private static readonly object Identity_student1 = new object();
        private static readonly object Identity_student2 = new object();
        private static readonly object List_User = new object();
        private int maxID = 10;
        public  List<User > users;


        public RUN(IWebDriver[] drivers, string URL)
        {
            users = new List<User>();
            this.drivers = drivers;
            this.URL = URL;
            this.drivers[0] = new ChromeDriver();
            //this.drivers[1] = new ChromeDriver();
            //this.drivers[2] = new ChromeDriver();


            for (int i = 0; i < drivers.Length; i++)
            {
                this.drivers[i].Manage().Window.Maximize();
                //this.drivers[i].Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(10);
                this.drivers[i].Navigate().GoToUrl(this.URL);
                //this.drivers[i].Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(20);
                

            }

        }
        public void SaveData()
        {
            //before your loop
            var csv = new StringBuilder();
            foreach (var user in users)
            {

                List<string> subject = new List<string>(){ "Arabic","English","Fr","Math","phy","chi","bio","app","Reg","computer","watania" };
                //in your loop
                var first = "Id";
                var second = user.Id.ToString();
                //Suggestion made by KyleMit
                var newLine = string.Format(",{0}", second );
                csv.Append(newLine);

                first = "Name";
                second = user.Name.ToString();
                //Suggestion made by KyleMit
                newLine = string.Format("{0}", second);
                csv.Append(newLine);

                for(int i = 0; i < subject.Count-1; i++)
                {
                    first = subject[i].ToString();
                    second = user.subject[i].ToString();
                    //Suggestion made by KyleMit
                    newLine = string.Format("{0}", second);
                    csv.Append(newLine);
                }

                csv.Append("\n");

            }

            //after your loop
            File.WriteAllText("Data.csv", csv.ToString());

        }

        public void ThreadsRun()
        {
            //Parallel.For(0, drivers.Length, ThreadsCondition);


            ThreadsCondition(0);
            //ThreadsCondition(1);
           // ThreadsCondition(2);
        }

        private void ThreadsCondition(int IDDrive)
        {
            //object oj;
            
            IWebDriver driver = drivers[IDDrive];
            while (number <= maxID)
            {
                int ID = 0;
                lock (Identity_student1)
                {
                    //Monitor.Wait(oj); // Wait for a signal from the boss

                    // Monitor monitor = new Monitor;

                    ID = number;
                    number++;
                    //Monitor.Pulse(oj); // Signal boss we are done

                }

                driver.FindElement(By.Id("Value1_1")).Clear();
                driver.FindElement(By.Id("Value1_1")).SendKeys(number.ToString());
                driver.FindElement(By.ClassName("cbSearchButton")).Click();

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                int numberInTable = 4;

                string path = "/html/body/article/div/div/div/article/form/div/section/div[";
                string completepath = "]";


                int ID_user = int.Parse(driver.FindElement(By.XPath(path + numberInTable + completepath)).Text);
                numberInTable += 2;
                string Name_user = (driver.FindElement(By.XPath(path + numberInTable + completepath)).Text);
                numberInTable += 1;

                string Total_user = "";
                List<string> Values_user = new List<string>();
                

                for (int i = numberInTable; i < 35; i++)
                {
                    string value;
                    //#var key = (driver.FindElement(By.XPath(path + i + completepath)).Text);

                    i++;
                    value = (driver.FindElement(By.XPath((path + i + completepath).ToString())).Text);

                    if (i != 24)
                    {
                        Values_user.Add( value);
                    }
                    else
                    {
                        Total_user = value;

                    }

                }
                User user = new User(ID_user, Name_user, Values_user, Total_user);


                lock (List_User)
                {
                    users.Add(user);
                }
                driver.FindElement(By.XPath("/html/body/article/div/div/div/article/form/div/nav/ul/li/a[2]")).Click();
               


            }
        }

    }


}
