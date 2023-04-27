using System;
using TrainStationManagementApp.Managers.Implementations;
using TrainStationManagementApp.Menu;
using TrainStationManagementApp.Models.Enums;

namespace TrainStationManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var passenger = new PassengerManager();
            passenger.ReadPassengerFromFile();

            var manager = new ManagerManager();
            manager.ReadManagerFromFile();

            var superAdmin = new SuperAdminManager();
            superAdmin.ReadSuperAdminFromFile();

            var booking = new BookingManager();
            booking.ReadBookingFromFile();

            var train = new TrainManager();
            train.ReadTrainFromFile();

            var user = new UserManager();
            user.ReadUserFromFile();

            MainMenu mainMenu = new MainMenu();
            mainMenu.RealMainMenu();
            // ManagerManager managerManage = new ManagerManager();
             //managerManage.ReadManagerFromFile();
            // Console.Write("Enter first name: ");
            // string firstName = Console.ReadLine();
            // Console.Write("Enter last name: ");
            // string lastName = Console.ReadLine();
            // Console.Write("Enter your email: ");
            // string email = Console.ReadLine();
            // Console.Write("Enter your password: ");
            // string password = Console.ReadLine();
            // Console.Write("Enter your phone number: ");
            // string phoneNumber = Console.ReadLine();
            // Console.Write("Enter your gender(Enter 1 for Male, 2 for Female): ");
            // Gender gender = (Gender)Enum.Parse(typeof(Gender),(Console.ReadLine()));
            // Console.Write("Enter your address: ");
            // string address = Console.ReadLine();
            // Console.Write("Enter role(Enter 1 for SuperAdmin, 2 for Manager, 3 for Passenger): ");
            // RoleName role = (RoleName)Enum.Parse(typeof(RoleName),(Console.ReadLine()));
            // managerManage.CreateManager(firstName, lastName, email, password, phoneNumber, gender, address, 1);
            //managerManage.DeleteManager(1);
        }
    }
}
