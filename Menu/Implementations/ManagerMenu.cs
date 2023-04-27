using System;
using System.Collections.Generic;
using TrainStationManagementApp.Managers.Implementations;
using TrainStationManagementApp.Managers.Interfaces;
using TrainStationManagementApp.Menu.Interfaces;
using TrainStationManagementApp.Models.Entities;
using TrainStationManagementApp.Models.Enums;

namespace TrainStationManagementApp.Menu.Implementations
{
    public class ManagerMenu : IManagerMenu
    {
        ITrainManager trainManager = new TrainManager();
        IUserManager userManager = new UserManager();
        IPassengerManager passengerManager = new PassengerManager();
        public void CreateTrainMenu()
        {
              Console.WriteLine();

            Console.WriteLine("Create Train");

            Console.Write("Enter Train Name: ");
            string trainName = Console.ReadLine();

            Console.Write("Enter Take Off Point: ");
            string takeOffPoint = Console.ReadLine();

            Console.Write("Enter Destination: ");
            string destination = Console.ReadLine();

            Console.Write("Enter Take Off Time: ");
            TimeSpan takeOffTime = TimeSpan.Parse(Console.ReadLine());

            Console.Write("Enter Landing Time: ");
            TimeSpan landingTime = TimeSpan.Parse(Console.ReadLine());

            Console.Write("Enter Price: ");
            double price = double.Parse(Console.ReadLine());
            
            Console.Write("Enter Capacity: ");
            int capacity = int.Parse(Console.ReadLine());

            Console.Write("Enter Avalable Space: ");
            int availableSpace = int.Parse(Console.ReadLine());
            
            var train = trainManager.CreateTrain(trainName, takeOffPoint, destination, takeOffTime , landingTime, price, capacity, availableSpace);
            if (train == null)
            {
                Console.WriteLine("Unable To Create Train!");
            }
            else
            {
                Console.WriteLine("Train Created!");
            }
        }

        public void DeleteTrainMenu()
        {
             Console.WriteLine();

            Console.WriteLine("Delete Train");

            Console.WriteLine("Enter Train Name");
            string trainName = Console.ReadLine();

            var train = trainManager.GetTrain(trainName);
            if (train != null)
            {
                var deleteTrain = trainManager.DeleteTrain(train.Id);
                if (deleteTrain == true)
                {
                    Console.WriteLine("Deleted Successfully");
                }
                else
                {
                    Console.WriteLine("Unable to Delete");
                }
            }
            else
            {
                Console.WriteLine("Train not found");
            }
        }

        public void RealManagerMenu(string email)
        {
            MainMenu mainMenu = new MainMenu();
            Console.WriteLine(" ");
            Console.WriteLine("Manager Dashboard");
            Console.Write("Enter 1 To Create Train\nEnter 2 To View Train\nEnter 3 To View All Train\nEnter 4 To Delete Train\nEnter 5 To Update Train\nEnter 6 To View Passenger\nEnter 7 To View All Passenger\nEnter 8 To Update Passenger\nEnter 9 To View Profile\nEnter 10 To Logout\nEnter 0 To Go Back: ");
            int option = Validate.NumberValidate(Console.ReadLine(), "Option");
            //int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 0:
                mainMenu.RealMainMenu();
                RealManagerMenu(email);
                break;
                case 1:
                CreateTrainMenu();
                RealManagerMenu(email);
                break;
                case 2:
                ViewTrainMenu();
                RealManagerMenu(email);
                break;
                case 3:
                ViewAllTrainMenu();
                RealManagerMenu(email);
                break;
                case 4:
                DeleteTrainMenu();
                RealManagerMenu(email);
                break;
                case 5:
                UpdateTrainMenu();
                RealManagerMenu(email);
                break;
                case 6:
                ViewPassengerrMenu();
                RealManagerMenu(email);
                break;
                case 7:
                ViewAllPassengerMenu();
                RealManagerMenu(email);
                break;
                case 8:
                UpdatePassengerMenu();
                RealManagerMenu(email);
                break;
                case 9:
                ViewProfileMenu(email);
                RealManagerMenu(email);
                break;
                case 10:
                Logout();
                break;
                default:
                Console.WriteLine("Invalid Inpute");
                RealManagerMenu(email);
                break;
            }
        }

        public void ViewProfileMenu(string userEmail)
        {
            Console.WriteLine();

            Console.WriteLine("Manager Profile Menu");

            var user = userManager.GetUser(userEmail);
            if (user == null)
            {
                Console.WriteLine("Manager not found");
            }
            else{
                Console.WriteLine($"Id: {user.Id}");
                Console.WriteLine($"FirstName: {user.FirstName}");
                Console.WriteLine($"LastName: {user.LastName}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Wallet: {user.Wallet}");
            }
        }

        public void UpdatePassengerMenu()
        {
             Console.WriteLine();

            Console.WriteLine("Update Passenger");

            Console.Write("Enter Your First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Your Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Your Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Your PhoneNumber: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter Your Gender(1 For Male and 2 For Female): ");
            Gender gender = (Gender)Enum.Parse(typeof(Gender),(Console.ReadLine()));

            Console.Write("Enter Your Address: ");
            string address = Console.ReadLine();

            var user = userManager.GetUser(email);
            if (user == null)
            {
                Console.WriteLine("User does not exist");
            }
            else
            {
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Email = email;
                user.PhoneNumber =phoneNumber;
                user.Gender = gender;
                user.Address = address;
                var userUpdate = userManager.UpdateUser(user);
                var messager = userUpdate == null ? "Passenger not updated!" : "updated successful";
                Console.WriteLine(messager);
            }
        }

        public void UpdateTrainMenu()
        {
            Console.WriteLine();

            Console.WriteLine("Update Train");

            Console.Write("Enter Train Name: ");
            string trainName = Console.ReadLine();

            Console.Write("Enter Take Off Point: ");
            string takeOffPoint = Console.ReadLine();

            Console.Write("Enter Destination: ");
            string destination = Console.ReadLine();

            Console.Write("Enter Take Off Time: ");
            TimeSpan takeOffTime = TimeSpan.Parse(Console.ReadLine());

            Console.Write("Enter Landing Time: ");
            TimeSpan landingTime= TimeSpan.Parse(Console.ReadLine());

            Console.Write("Enter Price: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Enter Capacity: ");
            int capacity = int.Parse(Console.ReadLine());

            Console.Write("Enter Available Space: ");
            int availableSpace = int.Parse(Console.ReadLine());
            
            var train = trainManager.GetTrain(trainName);
            if (train == null)
            {
                Console.WriteLine("Train not exist");
            }
            else
            {
                var trainUpdate = trainManager.UpdateTrain(trainName, takeOffPoint, destination, takeOffTime, landingTime, price, capacity, availableSpace);
                var messager = trainUpdate == null ? "Train not updated!" : "Train Updated Successful";
                Console.WriteLine(messager);
            }
        }

        public void ViewPassengerrMenu()
        {
            Console.WriteLine();

            Console.WriteLine("View Passenger Menu");

            Console.Write("Enter Passenger Email: ");
            string email = Console.ReadLine();
            var passenger = userManager.GetUser(email);
            if (passenger != null)
            {
                Console.WriteLine($"Id \tFirstName \tLastName \tEmail \tWallet");   
                Console.WriteLine($"{passenger.Id} \t{passenger.FirstName} \t{passenger.LastName} \t{passenger.Email} \t{passenger.Wallet} ");   
            }
            else
            {
                Console.Write("Passenger not found");
            }
        }

        public void ViewAllPassengerMenu()
        {
            Console.WriteLine();
            Console.WriteLine("View All Customer Menu");
            var users = userManager.GetAllUser();
            if (users.Count > 0)
            {    
                var passengerList = new List<User>();   
                foreach (var user in users)
                {
                    if (user.Role == RoleName.Passenger)
                    {   
                        passengerList.Add(user);
                    }
                } 

                if (passengerList.Count > 0)
                {
                    Console.WriteLine($"Id \tFirstName \tLastName \tEmail \tWallet");
                    foreach (var passenger in passengerList)
                    {
                        Console.WriteLine($"{passenger.Id} \t{passenger.FirstName} \t{passenger.LastName} \t{passenger.Email} \t{passenger.Wallet}");
                    } 
                }
                else
                {
                    Console.WriteLine("Customer not found");
                }

            }
            else
            {
                Console.Write("Customer not found");
            }
        }

        public void ViewAllTrainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("View all train Menu");
            var trains = GetTrainByDeleteStatus(false);
            if (trains.Count > 0)
            {  
        
                Console.WriteLine($"Id \tName \tDestination \tTakeOffPoint \tTakeOffTime \tLandingTime \tPrice \tCapacity \t AvailableSpace");   
                foreach (var train in trains)
                {
                    Console.WriteLine($"{train.Id} \t{train.Name} \t{train.Destination} \t{train.TakeOffPoint} \t{train.TakeOffTime} \t{train.LandingTime} \t{train.Price} \t{train.Capacity} \t{train.AvailableSpace}");
                }   
            }
            else
            {
                Console.Write("Train not found");
            }
        }

        public void ViewTrainMenu()
        {            
            Console.WriteLine();

            Console.WriteLine("View a Train Menu");

            Console.Write("Enter Train Name: ");
            string name = Console.ReadLine();

            var train = trainManager.GetTrain(name);
            if (train != null)
            {  
                Console.WriteLine($"Id \tName \tDestination \tTakeOffPoint \tTakeOffTime \tLandingTime \tPrice \tCapacity \tAvailableSpace");   
                Console.WriteLine($"{train.Id} \t{train.Name} \t{train.Destination} \t{train.TakeOffPoint} \t{train.TakeOffTime} \t{train.LandingTime} \t{train.Price} \t{train.Capacity} \t{train.AvailableSpace}");   
            }
            else
            {
                Console.Write("Train not found");
            }
        }

        public List<Train> GetTrainByDeleteStatus(bool isDeleted)
        {
            var trains = trainManager.GetAllTrain();
            var newList = new List<Train>();
            foreach (var train in trains)
            {
                if (train.IsDeleted == isDeleted)
                {
                    newList.Add(train);
                }
            } 

            return newList;
        }

        public void Logout()
        {
            Console.WriteLine("Logout Successful");
        }
    }
}