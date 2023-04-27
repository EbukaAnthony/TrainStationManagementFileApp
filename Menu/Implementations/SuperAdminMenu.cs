using System;
using System.Collections.Generic;
using TrainStationManagementApp.Managers.Implementations;
using TrainStationManagementApp.Managers.Interfaces;
using TrainStationManagementApp.Menu.Interfaces;
using TrainStationManagementApp.Models.Entities;
using TrainStationManagementApp.Models.Enums;

namespace TrainStationManagementApp.Menu.Implementations
{
    public class SuperAdminMenu : ISuperAdminMenu
    {
        IUserManager userManager = new UserManager();
        IManagerManager managerManager = new ManagerManager();
        ITrainManager trainManager = new TrainManager();
        public void CreateManagerMenu()
        {
             Console.WriteLine();

            Console.WriteLine("Create Manager");

            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter your email: ");
            string email = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            Console.Write("Enter your phone number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter your gender(1 for male and 2 for female): ");
            Gender gender = (Gender)int.Parse(Console.ReadLine());

            Console.Write("Enter your address: ");
            string address = Console.ReadLine();

            RoleName role = RoleName.Manager;
            var user = userManager.RegisterUser(firstName, lastName, email, password, phoneNumber, gender, address, role);
            if (user == null)
            {
                Console.WriteLine("User already exist!");
            }
            else
            {
                var manager = managerManager.CreateManager(firstName, lastName, email, password, phoneNumber, gender, address , user.Id);
                var messager = manager == null ? "Manager already exist!" : "Register successful";
                Console.WriteLine(messager);
            }
        }

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
            
            var train = trainManager.CreateTrain(trainName, takeOffPoint, destination, takeOffTime, landingTime, price, capacity, availableSpace);
            if (train == null)
            {
                Console.WriteLine("Unable To Create Train!");
            }
            else
            {
                Console.WriteLine("Train Created!");
            }
        }

        public void DeleteManagerMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Delete Manager");

            Console.Write("Enter manager email: ");
            string email = Console.ReadLine();
            var user = userManager.GetUser(email);
            if (user != null && user.Role == RoleName.Manager)
            {
                var deleteUser = userManager.DeleteUser(email);
                if (deleteUser == true)
                {
                    // var manager = managerManager.GetManager(user.Id);
                    // Console.WriteLine($"{manager.Id} {manager.UserId}");
                    Console.WriteLine($"{user.Id} {user.Role}");
                    // var delete = managerManager.DeleteManager(manager.Id);
                    // var messager = delete == false ? "Manager not deleted!" : "Deleted successful";
                    // Console.WriteLine(messager);
                }
            }
            else
            {
                Console.WriteLine("Manager not found!");
            }
        }

        public void RealSuperAdminMenu(string email)
        {
            Console.WriteLine();
            Console.WriteLine("Super Admin Dashboard");
            Console.WriteLine("Enter 1 to Create Manager \nEnter 2 to Delete Manager \nEnter 3 to Update Manager \nEnter 4 to Create Train \nEnter 5 to View All Train \nEnter 6 to Viewe All User\nEnter 7 to Logout \nEnter 0 to Go Back");
            int option = Validate.NumberValidate(Console.ReadLine(), "Option");
            //int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 0:
                break;
                case 1:
                CreateManagerMenu();
                RealSuperAdminMenu(email);
                break;
                case 2:
                DeleteManagerMenu();
                RealSuperAdminMenu(email);
                break;
                case 3:
                UpdateManagerMenu();
                RealSuperAdminMenu(email);
                break;
                case 4:
                CreateTrainMenu();
                RealSuperAdminMenu(email);
                break;
                case 5:
                ViewAllTrainMenu();
                RealSuperAdminMenu(email);
                break;
                case 6:
                VieweAllUserMenu();
                RealSuperAdminMenu(email);
                break;
                case 7:
                Logout();
                break;
                default:
                Console.WriteLine("Invalid input");
                RealSuperAdminMenu(email);
                break;
            }
        }

        public void UpdateManagerMenu()
        {
              Console.WriteLine();

            Console.WriteLine("Update Manager");

            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter your phone number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter your gender(1 for male and 2 for female): ");
            Gender gender = (Gender)int.Parse(Console.ReadLine());

            Console.Write("Enter your address: ");
            string address = Console.ReadLine();

            var user = userManager.GetUser(email);

            if (user == null)
            {
                Console.WriteLine("User already exist!");
            }
            else
            {
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Email = email;
                user.PhoneNumber = phoneNumber;
                user.Gender = gender; 
                user.Address = address;
                var updateUser = userManager.UpdateUser(user);
                var messager = updateUser == null ? "Manager not updated!" : "updated successful";
                Console.WriteLine(messager);
            };
        }

        public void ViewAllTrainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("View all Train Menu");
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
            };
        }

        public void VieweAllUserMenu()
        {
            Console.WriteLine();
            Console.WriteLine("View All User");
            var users = userManager.GetAllUser();
            Console.WriteLine("Id\t FirstName\t LastName\t Email");
            foreach (var user in users)
            {
                Console.WriteLine($"{user.Id}\t {user.FirstName}\t {user.LastName}\t {user.Email}");
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