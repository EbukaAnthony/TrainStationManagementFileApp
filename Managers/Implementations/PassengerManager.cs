using System;
using System.Collections.Generic;
using System.IO;
using TrainStationManagementApp.Managers.Interfaces;
using TrainStationManagementApp.Models.Entities;

namespace TrainStationManagementApp.Managers.Implementations
{
    public class PassengerManager : IPassengerManager
    {
        static List<Passenger> listOfPassenger = new List<Passenger>();
        IUserManager userManager = new UserManager();
        string filePath = AppFilePath.FilePathRoot("passenger.txt");

        public PassengerManager()
        {
            //ReadPassengerFromFile();   // The Constructor Will Help Me To Read The File To The List.
        }

        public void ReadPassengerFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    
                        var passengers = File.ReadAllLines(filePath);
                        foreach (var passenger in passengers)
                        {
                            listOfPassenger.Add(Passenger.ToPassenger(passenger));
                        }
                }
                else
                {
                    string path = AppFilePath.FilePathRoot();
                    Directory.CreateDirectory(path);
                    string fileName = "passenger.txt";
                    string fullPath = Path.Combine(path, fileName);
                    File.Create(fullPath);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void WritePassengerToFile(Passenger passenger)
        {
            try
            {
                using(var write = new StreamWriter(filePath, true))
                {
                    write.WriteLine(passenger.ToString());
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RefreshFile()
        {
            try
            {
                File.WriteAllText(filePath , string.Empty);
                using(StreamWriter stream = new StreamWriter(filePath))
                {
                    foreach (var item in listOfPassenger)
                    {
                        stream.WriteLine(item.ToString());
                    }
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool Delete(int id)
        {
           var passender = TryGet(id);
            if (passender != null && passender.IsDeleted == false)
            {
                passender.IsDeleted = true;
                return true;
            }
            File.WriteAllText(filePath, string.Empty);
            RefreshFile();
            return false;
        }

        public Passenger Get(int id)
        {
            return TryGet(id);
        }

        public Passenger Get(string email)
        {
            return TryGet(email);
        }

        public List<Passenger> GetAllPassenger()
        {
            return listOfPassenger;
        }

        public Passenger Register(User user)
        {
             int id = listOfPassenger.Count + 1;
                Passenger passenger = new Passenger(id, user.Id, false, DateTime.Now, DateTime.Now);
                WritePassengerToFile(passenger);
                listOfPassenger.Add(passenger);
                return passenger;
        }

        private Passenger TryGet(int id)
        {
            foreach (var passenger in listOfPassenger)
            {
                if (passenger.UserId == id && passenger.IsDeleted == false)
                {
                    return passenger;
                }
            }
            return null;
        }
        private Passenger TryGet(string email)
        {
            var user = userManager.GetUser(email);
            if (user != null)
            {
                return TryGet(user.Id);
            }
            return null;
        }
    }
}