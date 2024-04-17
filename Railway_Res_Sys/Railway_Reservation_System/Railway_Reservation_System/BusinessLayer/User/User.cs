using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Railway_Reservation_System;


namespace Railway_Reservation_System.BusinessLayer.User
{
    public class User
    {
        static Train_ReservationEntities tr = new Train_ReservationEntities();

        //Declaring uid as global variable.........
        static int uid;  
        public static void User_LogIn_Page()
        {
            User_LogIn ul = new User_LogIn();

            Console.WriteLine("====================================================================================================================\n");
            Console.WriteLine("1. New User \n2. Existing User\n");
            Console.WriteLine("=> Select the respective options:\n");
            int user = int.Parse(Console.ReadLine());
            Console.WriteLine();



            if (user == 1)
            {
                Console.WriteLine("\t\t\t\t Welcome to New User Registration Page....");
                Console.WriteLine("Enter User Id:");
                ul.User_id = int.Parse(Console.ReadLine());
                uid = (int)ul.User_id;

                Console.WriteLine("Enter User Name:");
                ul.User_Name = Console.ReadLine();

                Console.WriteLine("Enter Your Age:");
                ul.User_Age = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Your Password:");
                ul.User_Password = Console.ReadLine();

                tr.User_LogIn.Add(ul);
                tr.SaveChanges();

                Console.WriteLine("\t\t\t\t New User Registration Successful.....!");
                Console.WriteLine("==================================================================================================================");
                Console.WriteLine();
            }
            else if (user == 2)
            {
                Console.WriteLine("Enter User Id:");
                int userId = int.Parse(Console.ReadLine());
                uid = (int)userId;

                Console.WriteLine("Enter User Password:");
                string password = Console.ReadLine();

                // Check if the user_ID and entered password exists in the database
                var existingUser = tr.User_LogIn.FirstOrDefault(u => u.User_id == userId);
                var existingPassword = tr.User_LogIn.FirstOrDefault(a => a.User_Password == password);


                if (existingUser != null && existingPassword != null)
                {
                    // Proceed with other options
                    Console.WriteLine("\t\t\t\tExisting user logged in successfully.....");
                    Console.WriteLine("==============================================================================================================");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\t\t\tUser not Found!!! Please try again or register as a new user.");
                    User_LogIn_Page();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("\t\t\t\tInvalid Input.....Try Again !!!");
                User_LogIn_Page();
            }


            bool flag = true;
            while(flag)
            {
                Console.WriteLine("1. Show All Trains \n2. Book Tickets \n3. Cancel Tickets \n4. View Bookings/Cancellations \n5. Exit");
                Console.WriteLine();

                Console.WriteLine("=> Select the respective options:\n");
                int User_Input = int.Parse(Console.ReadLine());


                switch (User_Input)
                {
                    case 1:
                        Show_All_Trains();
                        break;

                    case 2:
                        Book_Ticket(uid);
                        break;

                    case 3:
                        Cancel_Ticket();
                        break;

                    case 4:
                        Booking_Cancellation();
                        break;

                    case 5:
                        flag = false;
                        Exit();
                        break;

                    default:
                        Console.WriteLine("Wrong Input !!");
                        break;
                }
            }
            
        }



        public static void Show_All_Trains()
        {
            Console.WriteLine("----------------------------------------------All trains with Detail-------------------------------------------------\n");
            var t = tr.Trains.ToList();
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("Train no.\tTrain_Name\tSource\t\tDestination\tDepart_Time\tArrival_Time\tTrain_Status");
            Console.WriteLine("=======================================================================================================================");
            foreach (var x in t)
            {
                if (x.isActiven == true)
                {
                    Console.WriteLine($"{x.Train_No}\t\t{x.Train_Name}\t{x.Source_Station}\t{x.Destination_Station}\t{x.Departure_Time}\t{x.Arrival_Time}\t{x.isActiven}");
                }
            }
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine();
        }


        public static void Book_Ticket(int uid)
        {
            Show_All_Trains();
            Console.WriteLine();
            to:
            Console.WriteLine("\nEnter train number for which you want to book a ticket:\n");
            int trainNumber = int.Parse(Console.ReadLine());
            

            // Check if the train exists
            var train = tr.Trains.FirstOrDefault(t => t.Train_No == trainNumber);
            if (train == null)
            {
                Console.WriteLine($"\t\t\t|||Train number {trainNumber} does not exist.|||");
                Console.WriteLine();
                goto to;
            }



            // Get berth availability and seat fare for the selected train.
            var trBrth = tr.Berth_Availability.FirstOrDefault(b => b.Train_No == trainNumber);
            var stFair = tr.Seat_Fare.SingleOrDefault(f => f.Train_No == trainNumber);

            var tr_active = tr.Trains.Where(t => trainNumber == t.Train_No && t.isActiven == true).FirstOrDefault();

            if (tr_active == null)
            {
                Console.WriteLine("\t\t\t\t ||| This train is not Active.|||");
                goto to;
                
            }
            else
            {

                Console.WriteLine($"\t\t\tSeats availability of berths and fare of seats of Train NO {trainNumber} are:");
                Console.WriteLine();

                Console.WriteLine("=======================================================================================================================");
                Console.WriteLine($"Berth availability for [1-AC]:-- {trBrth.C1_AC}\t\t" + "Fare of Seat :--" + stFair.C1_AC_Fare);
                Console.WriteLine($"Berth availability for [2-AC]:-- {trBrth.C2_AC}\t\t" + "Fare of Seat :--" + stFair.C2_AC_Fare);
                Console.WriteLine($"Berth availability for [SL-Class]:-- {trBrth.SL_Class}\t" + "Fare of Seat :--" + stFair.SL_Class_Fare);
                Console.WriteLine("=======================================================================================================================");
                Console.WriteLine("");

                Console.WriteLine($"Select your berth class choice: \n\n1. [1-AC] \n2. [2-AC] \n3. [SL-Class]\n");
                int berthChoice = int.Parse(Console.ReadLine());


                Console.WriteLine("Enter the number of tickets you want to book:\n");
                int numberOfTickets = int.Parse(Console.ReadLine());
                Console.WriteLine();

                // Validating number of tickets
                if (numberOfTickets <= 0 || numberOfTickets > 5)
                {
                    Console.WriteLine("\t\t *Invalid number of tickets. Maximum 5 tickets can be booked at a time.*");
                    Console.WriteLine();
                    Book_Ticket(uid);
                    return;
                }


                User_LogIn ul = new User_LogIn();

                //Because we are taking integer as input from user when he's going to select the berth but berths are in string.
                string berth = "";

                //Because we need the fair of particulare person when i'm going to create ticket / update the data in Ticket_Confirmation Table.
                int fare = 0;

                //We have to calculate total fair according to number of tickets selected by user.
                int totalFare = 0;
                switch (berthChoice)
                {
                    case 1:
                        totalFare = (int)(numberOfTickets * stFair.C1_AC_Fare);
                        fare = (int)(stFair.C1_AC_Fare);
                        berth = "1-AC";
                        break;

                    case 2:
                        totalFare = (int)(numberOfTickets * stFair.C2_AC_Fare);
                        fare = (int)(stFair.C2_AC_Fare);
                        berth = "2-AC";
                        break;

                    case 3:
                        totalFare = (int)(numberOfTickets * stFair.SL_Class_Fare);
                        fare = (int)(stFair.SL_Class_Fare);
                        berth = "SL-Class";
                        break;

                    default:
                        Console.WriteLine("Invalid berth choice.");
                        return;
                }

                // Asking user to Confirm booking.
                Console.WriteLine($"Total amount to be paid for {numberOfTickets} tickets is: {totalFare}");
                Console.WriteLine("=======================================================================================================================");
                Console.WriteLine("Are you sure to Confirm booking:\n 1. Yes\n 2. No \n");
                int confirmation = int.Parse(Console.ReadLine());


                var st = tr.Trains.FirstOrDefault(t => t.Source_Station == t.Source_Station);
                var dt = tr.Trains.FirstOrDefault(t => t.Destination_Station == t.Destination_Station);
                var us = tr.User_LogIn.FirstOrDefault(t => t.User_id == t.User_id);



                if (confirmation == 1)
                {
                    // Create tickets and save to database
                    for (int i = 0; i < numberOfTickets; i++)
                    {
                        Console.WriteLine($"Enter passenger name for ticket {i + 1}:");
                        string passengerName = Console.ReadLine();

                        Console.WriteLine($"Enter passenger age for ticket {i + 1}:");
                        int passengerAge = int.Parse(Console.ReadLine());

                        Console.WriteLine($"Enter the Date on which you want to Travel in (YYYY-MM-DD) format:");
                        string date_input = Console.ReadLine();

                        Console.WriteLine();

                        // Generating PNR Number using random numbers
                        int pnr = new Random().Next(100000, 999999);


                        //Create new Ticket object
                        Ticket_Confirmation ticket = new Ticket_Confirmation();

                        ticket.Train_No = trainNumber;
                        ticket.Passenger_Name = passengerName;
                        ticket.Age = passengerAge;
                        ticket.User_Id = uid;
                        ticket.Source_Station = train.Source_Station;
                        ticket.Destination_Station = train.Destination_Station;
                        ticket.Berth = berth;
                        ticket.Paid_Amount = fare;
                        ticket.Booking_Date = DateTime.Now; // Assuming current date as booking date
                        ticket.PNR_No = pnr;


                        tr.Ticket_Confirmation.Add(ticket);
                        //tr.Berth_Availability.Add(trBrth);

                        //Console.WriteLine("Congratulations your ticket is Booked and your Ticket details are:");
                        Console.WriteLine();


                        Console.WriteLine("\t\t\t\t\t***Your Ticket Details***");
                        Console.WriteLine("===================================================================================================================");
                        //Console.WriteLine($"Ticket of {i +1} passenger:");
                        Console.WriteLine($"Train Number: {ticket.Train_No}");
                        Console.WriteLine($"Passenger Name: {ticket.Passenger_Name}");
                        Console.WriteLine($"Age: {ticket.Age}");
                        Console.WriteLine($"User ID: {ticket.User_Id}");
                        Console.WriteLine($"Source Station: {ticket.Source_Station}");
                        Console.WriteLine($"Destination Station: {ticket.Destination_Station}");
                        Console.WriteLine($"Berth: {berth}");
                        Console.WriteLine($"Paid Amount: {ticket.Paid_Amount}");
                        Console.WriteLine($"Booking Date: {ticket.Booking_Date}");
                        Console.WriteLine($"Travelling Date: {date_input}");
                        Console.WriteLine($"PNR Number: {ticket.PNR_No}");
                        Console.WriteLine("==================================================================================================================");
                        Console.WriteLine();
                    }
                    Console.WriteLine("\t\t\t\tTickets booked successfully!\n");
                    tr.SaveChanges();

                    //calling the procedure which subtracts the no. of booked seats from berth_Availability table and update it.
                    tr.UpdateBookedTicket(trainNumber, berth, numberOfTickets);
                }
                else
                {
                    Console.WriteLine("Booking cancelled.");
                }
            }

        }




        public static void Cancel_Ticket()
        {
            Show_All_Trains();
            Console.WriteLine("\t\t\t\t***Welcome to Ticket Cancellation Page***");
            Console.WriteLine("==================================================================================================================\n");
            Console.WriteLine("Enter train number of train which you want to cancel:");
            int tc = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter PNR number");
            int pnrNo = int.Parse(Console.ReadLine());

            // Check if the ticket exists
            var ticket = tr.Ticket_Confirmation.FirstOrDefault(t => t.Train_No == tc && t.PNR_No == pnrNo);

            if (ticket == null)
            {
                Console.WriteLine($"Ticket with train number {tc} and PNR number {pnrNo} does not exist.");
                return;
            }


            // Removing the ticket from the database
            //tr.Ticket_Confirmation.Remove(ticket);

            // Generating a cancel ID using random numbers
            int C_Id = new Random().Next(100000, 999999);


            // Now we are going to add cancellation information to Ticket_Cancellation table
            Ticket_Cancellation cancellation = new Ticket_Cancellation
            {
                Cancel_Id = C_Id,
                Train_No = tc,
                Passenger_Name = ticket.Passenger_Name,
                Age = ticket.Age,
                User_Id = ticket.User_Id, // Assuming User_Id is available in the ticket table
                Berth = ticket.Berth,
                Paid_Amount = ticket.Paid_Amount,
                PNR_No = pnrNo,
                Cancel_Date = DateTime.Now, // Assuming current date as cancellation date
                Cancel_Status = "Cancelled", // Assuming cancellation status
            };


            Console.WriteLine("===================================================================================================================");
            Console.WriteLine();
            Console.WriteLine($"Cancel ID: {cancellation.Cancel_Id}");
            Console.WriteLine($"Train Number: {cancellation.Train_No}");
            Console.WriteLine($"Passenger Name: {cancellation.Passenger_Name}");
            Console.WriteLine($"Age: {cancellation.Age}");
            Console.WriteLine($"User ID: {cancellation.User_Id}");
            Console.WriteLine($"Berth: {cancellation.Berth}");
            Console.WriteLine($"Paid Amount: {cancellation.Paid_Amount}");
            Console.WriteLine($"Cancel Date: {cancellation.Cancel_Date}");
            Console.WriteLine($"Cancel Status: {cancellation.Cancel_Status}");
            Console.WriteLine($"PNR Number: {cancellation.PNR_No}");
            Console.WriteLine("==================================================================================================================");

            tr.Ticket_Cancellation.Add(cancellation);
            tr.SaveChanges();
            Console.WriteLine("\t\t\t\tTicket cancelled successfully!");

            //Calling function.....
            Remove_Tkt(pnrNo); 

            //calling the procedure which adds the no. of seats to berth_Availability table and update it.
            tr.UpdateCancelTickets(tc, ticket.Berth, 1);
        }
 
        //Creating a function which removes ticket from Ticket_Confirmation Table after cancellation....
        public static void Remove_Tkt(int pnrNo)
        {
            Ticket_Confirmation tr1 = new Ticket_Confirmation();
            var tkt_Cnm = tr.Ticket_Confirmation.FirstOrDefault(tc => tc.PNR_No == pnrNo);
            if(tkt_Cnm != null ) 
            {
                tr.Ticket_Confirmation.Remove(tkt_Cnm);
                tr.SaveChanges();
            }
        }



    public static void Booking_Cancellation()
        {
            Console.WriteLine("Select an option: \n1. View Bookings \n2. View Cancellations");
            Console.WriteLine();
            int option = int.Parse(Console.ReadLine());

            if (option == 1)
            {
                ViewBookings();
            }
            else if (option == 2)
            {
                ViewCancellations();
            }
            else
            {
                Console.WriteLine("\t\t\t***Invalid option selected.***");
                Console.WriteLine("Please Select Correct Options............");
                Booking_Cancellation();
            }
        }

        public static void ViewBookings()
        {
            Console.WriteLine("Enter your user ID:");
            int userId = int.Parse(Console.ReadLine());

            // Retrieve all bookings for the user from the database
            var bookings = tr.Ticket_Confirmation.Where(t => t.User_Id == userId).ToList();

            if (bookings.Count > 0)
            {
                Console.WriteLine("Your booking tickets are:=>");
                Console.WriteLine("===========================================================================================================================");
                foreach (var b in bookings)
                {
                    Console.WriteLine($"Train Number: {b.Train_No}, PNR Number: {b.PNR_No}, Passenger Name: {b.Passenger_Name}, Age: {b.Age}, Booking Date: {b.Booking_Date}, Travelling Date: {b.Travelling_Date}");
                }
            }
            else
            {
                Console.WriteLine("No bookings found for the user.");
            }
        }

        public static void ViewCancellations()
        {
            Console.WriteLine("Enter your user ID:");
            int userId = int.Parse(Console.ReadLine());

            // Retrieve all cancellations for the user from the database
            var cancellations = tr.Ticket_Cancellation.Where(c => c.User_Id == userId).ToList();

            if (cancellations.Count > 0)
            {

                Console.WriteLine("Your cancellation tickets are:=>");
                Console.WriteLine("==================================================================================================================");
                foreach (var c in cancellations)
                {
                    Console.WriteLine($"Train Number: {c.Train_No}, PNR Number: {c.PNR_No}, Cancellation Date: {c.Cancel_Date}, Cancel Status: {c.Cancel_Status}");
                }
            }
            else
            {
                Console.WriteLine("No cancellations found for the user.");
            }
        }


        static void Exit()
        {
            Console.WriteLine("Thanks for using Railway Reservation System \n");
            //Console.WriteLine("\t\t\t\t*****Please visit Again.......*****");
        }
    }
}
