using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Railway_Reservation_System.BusinessLayer.Admin;
using Railway_Reservation_System.BusinessLayer.User;

namespace Railway_Reservation_System
{
    public class Program
    {
        //Program p = new Program();
        
       // Admin ad = new Admin();
        public static void Main(string[] args)
        {
            Console.WriteLine("\t\t***************Welcome to Indian Railway Reservation System***************\n");
            Console.WriteLine("1. Admin LogIn \n2. User LogIn \n3. Exit \n");
            Console.WriteLine("=> Select the respective options:");
            int a = int.Parse(Console.ReadLine());

            if (a == 1)
            {
                Console.WriteLine("\t\t\t\t*** Welcome to Admin LogIn ***");
                // Admin ad = new Admin();
                Admin_Page.Admin_LogIn();
            }
            else if (a == 2)
            {
                Console.WriteLine("\t\t\t\t*** Welcome to User LogIn ***");
                //User us = new User();
                User.User_LogIn_Page();
                //User.Show_All_Trains();
            }
            else if (a == 3)
            {
                Console.WriteLine("Thanks for using Railway Reservation System.........\nPlease visit Again!!!");
            }
            else
            {
                Console.WriteLine("Wrong Input....");
            }
            Console.ReadLine();
        }
        
    }
}
