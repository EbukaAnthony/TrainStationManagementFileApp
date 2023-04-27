using System;
using TrainStationManagementApp.Managers.Implementations;
using TrainStationManagementApp.Managers.Interfaces;
using TrainStationManagementApp.Menu.Implementations;
using TrainStationManagementApp.Menu.Interfaces;
using TrainStationManagementApp.Models.Enums;

namespace TrainStationManagementApp.Menu
{
    public class MainMenu
    {
        IUserManager userManager = new UserManager();
        IPassengerManager passengerManager = new PassengerManager();
        public void RealMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to my App");
            Console.WriteLine("Enter 1 to Signup\nEnter 2 to Login");
            int option = Validate.NumberValidate(Console.ReadLine(), "Option");

             switch (option)
            {
                case 1:
                SignupMenu();
                break;
                case 2:
                LoginMenu();
                break;
                default:
                Console.WriteLine("Invalid input");
                break;
            }
            
            RealMainMenu();
        }

        
        public void SignupMenu()
        {
            Console.WriteLine();

            Console.WriteLine("SignUp");

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
            Gender gender = (Gender)Validate.GenderValidate(Console.ReadLine(), "gender");
            
            Console.Write("Enter your address: ");
            string address = Console.ReadLine();

            RoleName role = RoleName.Passenger;
            var user = userManager.RegisterUser(firstName, lastName, email, password, phoneNumber, gender, address, role);
            var passenger = passengerManager.Register(user);
            if (user == null)
            {
                Console.WriteLine("User already exist!");
            }
            else
            {
                Console.WriteLine("Register successful");
            }
        }


         public void LoginMenu()
        {
            Console.WriteLine();
            Console.WriteLine("LogIn");

            Console.Write("Enter your email: ");
            string email = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            
            var user = userManager.LoginUser(email, password);
            if (user != null)
            {
               Console.WriteLine("Login Successful");
                var role = user.Role;
                if (role == RoleName.SuperAdmin)
                {
                    ISuperAdminMenu superAdminMenu = new SuperAdminMenu();
                    superAdminMenu.RealSuperAdminMenu(user.Email);
                }
                else if (role == RoleName.Manager)
                {
                    IManagerMenu managerMenu = new ManagerMenu();
                    managerMenu.RealManagerMenu(user.Email);
                }
                else
                {
                    IPassengerMenu passengerMenu = new PassengerMenu();
                    passengerMenu.RealPassengerMenu(user.Email);
                }
            }
            else
            {
                Console.WriteLine("Wrong Email or Password!");
            }
        }

    }
}