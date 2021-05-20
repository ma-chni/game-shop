using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GameShop.Data;
using GameShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using static System.Console;

namespace GameShop
{
    class Program
    {
        static GameShopContext Context = new GameShopContext();
        static void Main()
        {
            bool shouldRun = true;
            while (shouldRun)
            {
                Clear();

                WriteLine("1. Registrera kund");
                WriteLine("2. Visa kundregistret");
                WriteLine("3. Skapa order");
                WriteLine("4. Lista ordrar för kund");
                WriteLine("5. Registrera artikel");
                WriteLine("6. Avsluta");

                ConsoleKeyInfo keyPressed = ReadKey(true);
                Clear();
                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                        RegisterCustomer();
                        break;
                    case ConsoleKey.D2:
                        DisplayCustomer();
                        break;
                    case ConsoleKey.D3:
                        RegisterOrder();
                        break;
                    case ConsoleKey.D4:
                        DisplayOrder();
                        break;
                    case ConsoleKey.D5:
                        RegisterArticle();
                        break;
                    case ConsoleKey.D6:
                        shouldRun = false;
                        break;

                }
               
            }
        }

        private static void RegisterArticle()
        {
            {
                bool shouldAbort = false;
                bool articleExits = true;
                do
                {
                    Clear();

                    WriteLine("Artikelnummer:");
                    string articleNumber = ReadLine();
                    WriteLine();

                    WriteLine("Namn:");
                    string name = ReadLine();
                    WriteLine();


                    WriteLine("Beskrivning:");
                    string description = ReadLine();
                    WriteLine();


                    WriteLine("Pris:");
                    int price = int.Parse(ReadLine());
                    WriteLine();

                    WriteLine("Är detta korrekt? (J)a eller (N)ej");

                    bool hasInvalidInput = true;

                    do
                    {
                        ConsoleKeyInfo keyPressed = ReadKey(true);
                        switch (keyPressed.Key)
                        {
                            case ConsoleKey.J:
                                hasInvalidInput = false;
                                shouldAbort = true;
                                Clear();
                                List<Article> articleList = Context.Article.ToList();
                                if (articleList.Count == 0)
                                {
                                    Article article = new Article(articleNumber,name,description,price);
                                    SaveArticle(article);
                                    WriteLine("Artikel registrerad");
                                    Thread.Sleep(3000);
                                    break;
                                }
                                for (int i = 0; i < articleList.Count; i++)
                                {
                                    if (articleList[i].ArticleNumber == articleNumber)
                                    {
                                        WriteLine("Artikel finns redan");
                                        Thread.Sleep(3000);
                                        break;
                                    }
                                    if (articleExits)
                                    {
                                        Article article = new Article(articleNumber, name, description, price);
                                        SaveArticle(article);
                                        WriteLine("Artikel registrerad");
                                        Thread.Sleep(3000);
                                        break;
                                    }
                                }
                                break;
                            case ConsoleKey.N:
                                hasInvalidInput = false;
                                break;
                        }
                    } while (hasInvalidInput);
                } while (shouldAbort == false);
            }
        }
        private static void DisplayOrder()
        {
            Write("Kund (personr.): ");

            string socialSecurityNumber = ReadLine();
            Customer customer = Context.Customer.FirstOrDefault(x => x.SocialSecurityNumber == socialSecurityNumber);
            List<ArticleOrder> articleOrderList = Context.ArticleOrder.Include(x => x.Article).ToList();
            List<Customer> customerList = Context.Customer.Include(x => x.Address).Include(x => x.Order).ToList();
            if (customer != null)
              {
                Clear();
         
                    WriteLine($"{customer.FirstName}, {customer.LastName}\n{customer.SocialSecurityNumber}");
                    WriteLine();
                    WriteLine($"{customer.Address.Street}, {customer.Address.Postcode} {customer.Address.City}");
                   
                WriteLine();
                WriteLine("Order Id\t\t\tDatum");
                WriteLine("---------------------------------------------------------------------------");
                foreach (var order in customer.Order)
                {
                    WriteLine($"{order.Id}\t\t\t{order.Date}");
                }
                WriteLine();
                WriteLine("Artiklar");
                WriteLine("---------------------------------------------------------------------------");
                foreach (ArticleOrder articleOrder in articleOrderList)
                {
                    WriteLine($"{articleOrder.Article.Name.PadRight(15, ' ')}{articleOrder.Article.Description.PadRight(25, ' ')}{articleOrder.Article.Price}KR");
                }

                //foreach (Article article in articleList)
                //{
                //    WriteLine($"{article.Name.PadRight(15, ' ')}{article.Description.PadRight(25, ' ')}{article.Price}KR");
                //}

                ConsoleKeyInfo keyPressed;

                bool escapePressed = false;

                do
                {
                    keyPressed = ReadKey(true);

                    if (keyPressed.Key == ConsoleKey.Escape)
                    {
                        escapePressed = true;
                    }

                } while (!escapePressed);
            }
 
        }

        private static void RegisterOrder()
        {
            bool showMenu = true;

            WriteLine("Kund (personr.):");
            string socialSecurityNumber = ReadLine();
            Order order = new Order();
            Customer customer = Context.Customer.FirstOrDefault(customer => customer.SocialSecurityNumber == socialSecurityNumber);
  
            while (showMenu)
            {
                if (customer != null)
                {
                    Clear();

                    WriteLine($"{customer.FirstName} {customer.LastName}, {customer.SocialSecurityNumber}");
                    WriteLine();
                    WriteLine("Artikel");
                    WriteLine("-------------------------------------------");
                    foreach (ArticleOrder articleOrder in order.Articles)
                    {
                        WriteLine(articleOrder.Article.Name);
                    }
                    WriteLine();
                    WriteLine("(L)ägg till (S)kapa order");
                    ConsoleKeyInfo keyPressed = ReadKey(true);
                    switch (keyPressed.Key)
                    {
                        case ConsoleKey.L:
                
                            Clear();
                            WriteLine("Art.nmr");
                            string articleNumber = ReadLine();

                            Article article = Context.Article.FirstOrDefault(x => x.ArticleNumber == articleNumber);
                            ArticleOrder articleOrder = new ArticleOrder(article);

                            order.Articles.Add(articleOrder);
                            showMenu = true;
                            break;
                        case ConsoleKey.S:
                            Clear();
                            customer.Order.Add(order);
                            Context.SaveChanges();
                            WriteLine("Order created");
                            Thread.Sleep(2000);
                            showMenu = false;
                            break;
                    }
                }
                else
                {
                    Clear();
                    WriteLine("Kund finns ej");
                    Thread.Sleep(2000);
                    showMenu = false;
                }
            }
        }

        private static void DisplayCustomer()
        {
            List<Customer> customerList = Context.Customer.Include(customer => customer.Address).ToList();
            WriteLine("Namn\t\t\t\t\tAdress");
            WriteLine("------------------------------------------------------------");
            foreach (var customer in customerList)
            {
                WriteLine($"{customer.FirstName} {customer.LastName}, {customer.SocialSecurityNumber}              {customer.Address.Street}, {customer.Address.Postcode} {customer.Address.City}");
            }

            ConsoleKeyInfo keyPressed;

            bool escapePressed = false;

            do
            {
                keyPressed = ReadKey(true);

                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    escapePressed = true;
                }

            } while (!escapePressed);
        }

        private static void RegisterCustomer()
        {
            {
                bool CustomerExists = true;
                bool shouldAbort = false;
                do
                {
                    Clear();

                    WriteLine("Firstname:");
                    string firstName = ReadLine();
                    WriteLine();

                    WriteLine("Lastname:");
                    string lastName = ReadLine();
                    WriteLine();

                    WriteLine("Socialsecuritynumber:");
                    string socialSecurityNumber = ReadLine();
                    WriteLine();

                    WriteLine("Gata:");
                    string street = ReadLine();
                    WriteLine();

                    WriteLine("Ort:");
                    string city = ReadLine();
                    WriteLine();

                    WriteLine("Postnummer:");
                    string postcode = ReadLine();
                    WriteLine();

                    WriteLine("Är detta korrekt? (J)a eller (N)ej");

                    bool hasInvalidInput = true;

                    do
                    {
                        ConsoleKeyInfo keyPressed = ReadKey(true);
                        switch (keyPressed.Key)
                        {
                            case ConsoleKey.J:
                                hasInvalidInput = false;
                                shouldAbort = true;
                                Clear();
                                List<Customer> customerList = Context.Customer.ToList();
                                if (customerList.Count == 0)
                                {
                                    Address address = new Address(street, city, postcode);
                                    Customer customer = new Customer(firstName, lastName, socialSecurityNumber, address);
                                    SaveCustomer(customer);
                                    WriteLine("Kund registrerad");
                                    Thread.Sleep(3000);
                                    break;
                                }
                                for (int i = 0; i < customerList.Count; i++)
                                {
                                    if (customerList[i].SocialSecurityNumber == socialSecurityNumber)
                                    {
                                        WriteLine("Kund finns redan");
                                        Thread.Sleep(3000);
                                        break;
                                    }
                                    if (CustomerExists)
                                    {
                                        Address address = new Address(street, city, postcode);
                                        Customer customer = new Customer(firstName, lastName, socialSecurityNumber, address);
                                        SaveCustomer(customer);
                                        WriteLine("Kund registrerad");
                                        Thread.Sleep(3000);
                                        break;
                                    }
                                }
                                break;
                                
                            case ConsoleKey.N:
                                hasInvalidInput = false;
                                break;
                        }
                    } while (hasInvalidInput);
                } while (shouldAbort == false);
            }
        }

        private static void SaveCustomer(Customer customer)
        {
            Context.Customer.Add(customer);
            Context.SaveChanges();
        }

        private static void SaveArticle(Article article)
        {
            Context.Article.Add(article);
            Context.SaveChanges();
        }
    }
}
