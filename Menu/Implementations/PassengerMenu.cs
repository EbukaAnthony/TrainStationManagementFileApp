using System;
using System.Collections.Generic;
using TrainStationManagementApp.Managers.Implementations;
using TrainStationManagementApp.Managers.Interfaces;
using TrainStationManagementApp.Menu.Interfaces;
using TrainStationManagementApp.Models.Entities;

namespace TrainStationManagementApp.Menu.Implementations
{
    public class PassengerMenu : IPassengerMenu
    {
        IBookingManager bookingManager = new BookingManager();
        IPassengerManager passengerManager = new PassengerManager();
        ITrainManager trainManager = new TrainManager();
        IUserManager userManager = new UserManager();

        
        public void CreateBookingMenu(string userEmail)
        {
            TrainManager trainManager = new TrainManager();
            Console.WriteLine();
            Console.WriteLine("Create Booking Menu");
            Console.Write($"Enter Train Name ({trainManager.TrainToOption()}): ");
            int trainId = Validate.TrainIdValidate(Console.ReadLine(), "Name");
            var train = trainManager.GetTrain(trainId);
            
            //train.TakeOffTime < DateTime.Now.TimeOfDay
            Passenger passenger = passengerManager.Get(userEmail);

            if(passenger == null)
            Console.WriteLine("Not Found");
            var booking = bookingManager.CreateBooking(passenger, train);
            if(booking != null)
            {
                Console.WriteLine("Booking Succesful");
                Console.WriteLine();
                Console.WriteLine("Booking Details");
                Console.WriteLine($"Booking Id: {booking.Id}");
                Console.WriteLine($"ReferenceNumber: {booking.ReferenceNumber}");
                Console.WriteLine($"PassengerEmail: {booking.PassengerEmail}");
                Console.WriteLine($"SeatNumber: {booking.SeatNumber}");
            }
        }

        public void FundWalletMenu(string userEmail)
        {
            Console.WriteLine();

            Console.WriteLine("Fund Wallet Menu");

            Console.Write($"Enter amount: ");
            double amount = double.Parse(Console.ReadLine());

            var fundWallet = userManager.FundUserWallet(userEmail, amount);
        }

        public void RealPassengerMenu(string userEmail)
        {
            MainMenu mainMenu = new MainMenu();

            Console.WriteLine();

            Console.WriteLine("Passenger Dashboard");

            Console.WriteLine("Enter 1 to View Profile \nEnter 2 to Fund Wallet \nEnter 3 to View all Train \nEnter 4 to Create Booking \nEnter 5 to View Booking  \nEnter 6 to View All Booking\nEnter 7 to Logout \nEnter 0 to Go Back");
            int option = Validate.NumberValidate(Console.ReadLine(), "Option");
            //int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 0:
                mainMenu.RealMainMenu();
                RealPassengerMenu(userEmail);
                break;
                case 1:
                ViewProfileMenu(userEmail);
                RealPassengerMenu(userEmail);
                break;
                case 2:
                FundWalletMenu(userEmail);
                RealPassengerMenu(userEmail);
                break;
                case 3:
                ViewAllTrainMenu(userEmail);
                RealPassengerMenu(userEmail);
                break;
                case 4:
                CreateBookingMenu(userEmail);
                RealPassengerMenu(userEmail);
                break;
                case 5:
                ViewBookingMenu(userEmail);
                RealPassengerMenu(userEmail);
                break;
                case 6:
                ViewAllBookingMenu(userEmail);
                RealPassengerMenu(userEmail);
                break;
                case 7:
                Logout();
                break;
                default:
                Console.WriteLine("Invalid input");
                RealPassengerMenu(userEmail);
                break;
            }
        }

        public void ViewProfileMenu(string userEmail)
        {
            Console.WriteLine();
            
            Console.WriteLine("Passenger Profile Menu");

            var user = userManager.GetUser(userEmail);
            if (user == null)
            {
                Console.WriteLine("Passenger not found");
            }
            else{
                Console.WriteLine($"Id: {user.Id}");
                Console.WriteLine($"FirstName: {user.FirstName}");
                Console.WriteLine($"LastName: {user.LastName}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Wallet: {user.Wallet}");
            }
        }

        public void ViewAllBookingMenu(string userEmail)
        {
             Console.WriteLine();

            Console.WriteLine("View All Booking Menu");

            var bookings = bookingManager.GetAllBooking();
            if (bookings.Count > 0)
            {
                var bookLists = new List<Booking>();
                foreach (var booking in bookings)
                {
                    if ( booking.PassengerEmail == userEmail)
                    {
                        bookLists.Add(booking);
                    }
                }

                if (bookLists.Count > 0)
                {
                    Console.WriteLine($"Id \tReferenceNumber \tName \tSeatNumber \tCustomerEmail");
                    foreach (var bookList in bookLists)
                    {
                        var train = trainManager.GetTrain(bookList.TrainId);
                        Console.WriteLine($"{bookList.Id}\t{bookList.ReferenceNumber}\t{train.Name}\t{bookList.SeatNumber}\t{bookList.PassengerEmail}");
            
                    }
                }
                else
                {
                    Console.WriteLine("Booking not found");
                }
            }
            else{
                
                System.Console.WriteLine("No Booking found!");
            }
        }

        public void ViewAllTrainMenu(string userEmail)
        {
             Console.WriteLine();
            Console.WriteLine("View all Train Menu");
            var trains = trainManager.GetTrainByDeleteStatus(false);
            if (trains.Count > 0 )
            {  
                Console.WriteLine($"Id \tName \tDestination \tTakeOffPoint \tTakeOffTime \tLandingTime \tPrice \tCapacity \t AvailableSpace");   
                foreach (var train in trains)
                {
                    if (DateTime.Now.TimeOfDay >= train.TakeOffTime)
                    {
                        Console.WriteLine($"{train.Id} \t{train.Name} \t{train.Destination} \t{train.TakeOffPoint} \t{train.TakeOffTime} \t{train.LandingTime} \t{train.Price} \t{train.Capacity} \t{train.AvailableSpace}");
                    }
                    
                }   
            }
            else
            {
                Console.Write("Train not found");
            }
        }

        public void ViewBookingMenu(string userEmail)
        {
             Console.WriteLine();

            Console.WriteLine("View Booking Menu");

            Console.Write($"Enter Reference Number: ");
            string referenceNumber = Console.ReadLine();

            var booking = bookingManager.GetBooking(referenceNumber);
            if (booking == null)
            {
                Console.WriteLine("Booking not found");
            }
            else{
                var train = trainManager.GetTrain(booking.TrainId);
                Console.WriteLine($"Id \tReferenceNumber \tName \tSeatNumber \tCustomerEmail");
                Console.WriteLine($"{booking.Id}\t{booking.ReferenceNumber}\t{train.Name}\t{booking.SeatNumber}\t{booking.PassengerEmail}");
            }
        }

        public void Logout()
        {
            Console.WriteLine("Logout Successful");
        }
    }
}