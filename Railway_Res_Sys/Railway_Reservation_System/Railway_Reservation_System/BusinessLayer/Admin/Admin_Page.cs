using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Railway_Reservation_System;
using Railway_Reservation_System.BusinessLayer.User;

namespace Railway_Reservation_System.BusinessLayer.Admin
{
    public class Admin_Page
    {
        static Train_ReservationEntities tr1 = new Train_ReservationEntities();
        public static void Admin_LogIn()
        {
            Admin_Page al = new Admin_Page();
            Console.WriteLine("====================================================================================================================\n");
            Console.WriteLine();



            
            Console.WriteLine("Enter ADMIN Id:");
            int Ad_id = int.Parse(Console.ReadLine());
                

            Console.WriteLine("Enter ADMIN Password:");
            string Ad_password = Console.ReadLine();

            // Check if the user_ID and entered password exists in the database
            var Admin = tr1.Admins.FirstOrDefault(u => u.Admin_Id == Ad_id);
            var Password = tr1.Admins.FirstOrDefault(a => a.Admin_Password == Ad_password);
           // var Name = tr1.Admins.FirstOrDefault(x => x.Admin_Name == Admin.Admin_Name);


            if (Admin != null && Password != null)
            {
                // Proceed with other options
                Console.WriteLine("\t\t\t\tExisting ADMIN logged in successfully.....");
                Console.WriteLine("=================================================================================================================");
                Console.WriteLine();
                Console.WriteLine($"******************************************** Welcome {Admin.Admin_Name} *****************************************************");
                Console.WriteLine();
            }
           
            else
            {
                Console.WriteLine("\t\t\t\t Invalid Input.Please Try Again !!!");
                Admin_LogIn();
            }



            bool flag = true;
            while (flag)
            {
                Console.WriteLine("==================================================================================================================");
                Console.WriteLine("1. Add Trains \n2. Modify Trains \n3. Activate_Deactivate Trains \n4. Delete Trains \n5. Exit\n");
                Console.WriteLine("=> Select the respective options\n");
                int Admin_Input = int.Parse(Console.ReadLine());

                if (Admin_Input == 1)
                {
                    Add_Trains();
                }
                else if (Admin_Input == 2)
                {
                    Modify_Trains();
                }
                else if (Admin_Input == 3)
                {
                    Activate_Deactivate();
                }
                else if (Admin_Input == 4)
                {
                    Delete_Trains();
                }
                else if (Admin_Input == 5)
                {
                    flag = false;
                    Exit();
                }
                else
                {
                    Console.WriteLine("\t\t\t\t\t Wrong Input !!! \nPlease Choose Respective options");
                    //Admin_LogIn();

                }
            }
           
        }

        public static void Add_Trains()
        {
            Train tn = new Train();
            Console.WriteLine("Enter the train number:");
            int tn_no = int.Parse(Console.ReadLine());
            tn.Train_No = tn_no;
            var Tr_Valid = tr1.Trains.FirstOrDefault(tp => tp.Train_No == tn_no);

            if (Tr_Valid == null)
            {
                Console.WriteLine("Enter train name:");
                tn.Train_Name = Console.ReadLine();

                Console.WriteLine("Enter Source of the train:");
                tn.Source_Station = Console.ReadLine();

                Console.WriteLine("Enter Destination of the train:");
                tn.Destination_Station = Console.ReadLine();

                Console.WriteLine("Enter Departure Time in (HH:MM):");
                string arrivalTimeString = Console.ReadLine();
                tn.Arrival_Time = TimeSpan.ParseExact(arrivalTimeString, "hh\\:mm", null);

                Console.WriteLine("Enter Arrival Time in (HH:MM):");
                string departureTimeString = Console.ReadLine();
                tn.Departure_Time = TimeSpan.ParseExact(departureTimeString, "hh\\:mm", null);

                // Assuming 'isActiven' is a property indicating whether the train is active or not & Newly added train is active by default.
                tn.isActiven = true;

                //Add the new train in database.
                tr1.Trains.Add(tn);


                // @Train_no, @F_Ac, @S_Ac,@Sl,@Tot_Birth
                //Console.WriteLine("Enter the Train_no");
                //int Train_no = Convert.ToInt32(Console.ReadLine());




                tr1.SaveChanges();
                Console.WriteLine("Enter the no. of seats you want to add in [1AC] Class: ");
                int AC1_Seats = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the no. of seats you want to add in [2AC] Class: ");
                int AC2_Seats = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the no. of seats you want to add in [SL] Class: ");
                int SL_Seats = Convert.ToInt32(Console.ReadLine());
                int Tot_Seats = AC1_Seats + AC2_Seats + SL_Seats;

                tr1.Add_Seat_In_Berth_Availability(tn_no, AC1_Seats, AC2_Seats, SL_Seats, Tot_Seats);


                Console.WriteLine("Enter the price of Ticket for [1AC]: ");
                int F_Af = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the price of Ticket for [2AC]: ");
                int S_Af = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the price of Ticket for [SL]: ");
                int Slf = Convert.ToInt32(Console.ReadLine());

                tr1.Add_Fair(tn_no, F_Af, S_Af, Slf);

                Console.WriteLine("Train added successfully.....");
            }

            else
            {
                
                Console.WriteLine($"Train number {tn_no} is already exist so you can not add this train.........To add again press (a) and enter or press (e) for exit\n");
                Console.Write("=>");
                string sel = Console.ReadLine();    
                switch(sel) {
                    case "a":
                        Add_Trains();
                        break;

                    case "e":
                        Console.WriteLine("You have been Logged Out....");
                        break;
                    
                    default:
                         Console.WriteLine("Wrong Input");
                         break;

                }

            }
        }

        
        public static void Modify_Trains()
        {
            Console.WriteLine("\t\t\t\t*** Welcome to Train modification Page ***");
            Console.WriteLine("===============================================================================================================");

            Console.WriteLine("Enter train number which you want to modify:");
            int tr_no = int.Parse(Console.ReadLine());
            var Train_D = tr1.Trains.FirstOrDefault(Tid => Tid.Train_No == tr_no);
            if (Train_D != null )
            {
                Console.Write("Enter modified Train Name: ");
                Train_D.Train_Name = Convert.ToString(Console.ReadLine());

                Console.Write("Enter modified source: ");
                Train_D.Source_Station = Convert.ToString(Console.ReadLine());

                Console.Write("Enter modified Destination: ");
                Train_D.Destination_Station= Convert.ToString(Console.ReadLine());

                tr1.SaveChanges();
                Console.WriteLine("Your Information updated succesfully......");

            } 
            else
            {
                Console.WriteLine($"Train no. {tr_no} doesn't exist in Database so You can't modify...\n");
                Console.WriteLine("\n\n\n |||Please Enter a valid Train Number.||| ");
                Console.WriteLine();
                Modify_Trains();
            }
        }

        public static void Activate_Deactivate()
        {
            Console.WriteLine("\t\t\t *** Welcome to Admin Activation/Deactivation of Trains ***\n");
            Console.WriteLine("=============================================================================================================");
            Console.WriteLine();

            Console.WriteLine("1. Deactivate Train \n2. Activate Train");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine();

            if (x == 1)
            {
                Console.WriteLine("Enter Train_no(Id) you want to Deactivate: ");
                int tn_no = Convert.ToInt32(Console.ReadLine());

                var DA_Train = tr1.Trains.FirstOrDefault(t => t.Train_No == tn_no);
                if (DA_Train != null)
                {
                    if (DA_Train.isActiven == true)
                    {
                        DA_Train.isActiven = false;
                        tr1.SaveChanges();

                        Console.WriteLine($"\n\t\t\tTrain_no {tn_no} has been Deactivated for Passengers.....");
                    }
                    else
                    {
                        Console.WriteLine($"\nTrain_no {tn_no} is already Deactivated...");
                    }
                }
                else
                {
                    Console.WriteLine($"\nTrain_no {tn_no} does not exist. So you can't Deactivate it...");
                }
            }
            else if( x == 2)
            {
                Console.WriteLine("Enter Train_no(Id) you want to Activate: ");
                int tr_no = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                var A_train = tr1.Trains.FirstOrDefault(t => t.Train_No == tr_no);
                if (A_train != null)
                {
                    if (A_train.isActiven == false)
                    {
                        A_train.isActiven = true;
                        tr1.SaveChanges();
                        Console.WriteLine($"\n\t\t\tTrain_no {tr_no} has been Activated for Passengers.");
                    }
                    else
                    {
                        Console.WriteLine($"\nTrain_no {tr_no} is already Activated...");
                    }
                }
                else
                {
                    Console.WriteLine($"\nTrain_no {tr_no} does not exist. So you can't Activate...");
                }
            }
            else
            {
                Console.WriteLine("Wrong User Input !!!! >>>>> Please Choose respective options");
                Activate_Deactivate();
            }
        }


        public static void Delete_Trains()
        {
            Console.WriteLine("Enter train number which you want to Delete: \n");
            int trNumber = int.Parse(Console.ReadLine());

            var trainToDelete = tr1.Trains.FirstOrDefault(t => t.Train_No == trNumber);

            if (trainToDelete != null)
            {
                // Perform soft deletion by marking the train as inactive
                trainToDelete.isActiven = false; // Assuming 'isActiven' is the column indicating whether the train is active or not
                tr1.SaveChanges();
                Console.WriteLine($"\n\t\t\t Train number {trNumber} has been deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Train number {trNumber} does not exist.");
            }
        }


        public static void Exit()
        {
            Console.WriteLine("\t\t\t Thanks for visiting Railway Reservation System.....");
            Console.WriteLine("Please Visit Again !!!");
        }
    }
}

