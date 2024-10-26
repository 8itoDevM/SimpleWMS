﻿using System;
using System.Collections.Generic;
//using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SimpleWMS
{
    internal class Program
    {
        static int Menu()
        {
            int i;
            Console.WriteLine("Count items - 1");
            Console.WriteLine("Show users - 2");
            Console.WriteLine("Register item - 3");
            Console.WriteLine("Add items - 4");
            Console.WriteLine("Ship items - 5");
            Console.WriteLine("Remove items - 6");
            Console.WriteLine("Exit - 0");

            try
            {
                i = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                return -1;
            }

            Console.Clear();
            return i;
        }

        static void CountItems(DatabaseManager dbManager)
        {
            dbManager.CountItems();
            //Console.WriteLine($"There are {qty} items in stock");
        }

        static void ShowUsers(DatabaseManager dbManager)
        {
            dbManager.ShowUsers();
        }

        static void RegisterItem(DatabaseManager dbManager)
        {
            string itmName = "default";
            int itmInit = 0;

            Console.WriteLine("Registering an item - Type 'C' to cancel");
            Console.WriteLine(" ");

            Console.Write("Item name: ");
            itmName = Console.ReadLine();

            if (itmName.ToLower() == "c")
            {
                Console.Clear();
                Console.WriteLine("Cancelled");
                return;
            }
                

            Console.Write("Item initial quantity: ");
            itmInit = Convert.ToInt32(Console.ReadLine());

            dbManager.RegisterItems(itmName, itmInit);
        }

        static void AddItem(DatabaseManager dbManager)
        {
            string itmName = "default";
            int itmQty = 0;

            Console.WriteLine("Adding an Item - Type 'C' to cancel");
            Console.WriteLine(" ");

            Console.Write("Item name: ");
            itmName = Console.ReadLine();

            if (itmName.ToLower() == "c")
            {
                Console.Clear();
                Console.WriteLine("Cancelled");
                return;
            }

            Console.Write("Quantity to be added: ");
            itmQty = Convert.ToInt32(Console.ReadLine());

            dbManager.AddItem(itmName, itmQty);
        }

        static void ShipItems(DatabaseManager dbManager)
        {
            string itmName = "default";
            int itmQty = 0;

            Console.WriteLine("Shipping an Item - Type 'C' to cancel");
            Console.WriteLine(" ");

            Console.Write("Item name: ");
            itmName = Console.ReadLine();

            if (itmName.ToLower() == "c")
            {
                Console.Clear();
                Console.WriteLine("Cancelled");
                return;
            }

            Console.Write("Quantity to be shipped: ");
            itmQty = Convert.ToInt32(Console.ReadLine());

            dbManager.ShipItem(itmName, itmQty);
        }

        static void RemoveItems(DatabaseManager dbManager)
        {
            string itmName = "default";
            bool confirmed = false;
            string confirmation = "default";

            Console.WriteLine("Removing an Item - Type 'C' to cancel");
            Console.WriteLine(" ");

            Console.Write("Item name: ");
            itmName = Console.ReadLine();

            if (itmName.ToLower() == "c")
            {
                Console.Clear();
                Console.WriteLine("Cancelled");
                return;
            }

            while (!confirmed)
            {
                Console.Write($"Are you sure you want to delete {itmName}? - Y/N");
                confirmation = Console.ReadLine();

                if(confirmation.ToLower() == "y")
                {
                    dbManager.RemoveItems(itmName);
                    confirmed = true;
                }
                else if(confirmation.ToLower() == "n")
                {
                    confirmed = false;
                    Console.Clear();
                    Console.WriteLine("Cancelled");
                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid option");
                }
            }
        }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("MySqlDatabase");

            DatabaseManager dbManager = new DatabaseManager(connectionString);

            bool running = true;

            while (running)
            {
                switch (Menu())
                {
                    case 0:
                        Console.Clear();
                        running = false;

                        break;
                    case 1:
                        Console.Clear();

                        CountItems(dbManager);

                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Clear();

                        ShowUsers(dbManager);

                        Console.ReadLine();
                        break;
                    case 3:
                        Console.Clear();

                        RegisterItem(dbManager);

                        Console.ReadLine();
                        break;
                    case 4:
                        Console.Clear();

                        AddItem(dbManager);

                        Console.ReadLine();
                        break;
                    case 5:
                        Console.Clear();

                        ShipItems(dbManager);

                        Console.ReadLine();
                        break;
                    case 6:
                        Console.Clear();

                        RemoveItems(dbManager);

                        Console.ReadLine();
                        break;
                    default:
                        Console.Clear();

                        Console.WriteLine("Invalid choice. Please try again.");

                        Console.ReadLine();
                        break;
                }

                Console.Clear();
            }

            Console.Write("Press any Key to exit ");
            Console.ReadLine();
        }
    }
}
