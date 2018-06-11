using System;
using System.Collections.Generic;
using SpringHeroBanking.controller;
using SpringHeroBanking.entity;
using SpringHeroBanking.model;

namespace SpringHeroBanking
{
    class Program
    {
        private static YYController _acc = new YYController();
        private static YYAccountModel _model = new YYAccountModel();

        static void Main(string[] args)
        {
            GenerateMenu();
        }

        private static void GenerateMenu()
        {
            while (true)
            {
                Console.WriteLine("----------SpringHeroBanking--------");
                Console.WriteLine("1, Log In.");
                Console.WriteLine("2, Register.");
                Console.WriteLine("3, Exit.");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Please enter choice: ");
                int choice = Int32.Parse(Console.ReadLine());
                ;
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("You choice Log In");
                        _acc.Login();
                        break;
                    case 2:
                        Console.WriteLine("You choice Register");
                        _model.Save(_acc.Account());
                        break;
                    case 3:
                        Console.WriteLine("Exit. Bye Bye");
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("You enter fails. Please enter again!");
                        break;
                }
            }
        }

    }
}