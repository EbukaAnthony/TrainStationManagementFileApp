using System;
using System.Collections.Generic;
using System.IO;
using TrainStationManagementApp.Managers.Interfaces;
using TrainStationManagementApp.Models.Entities;

namespace TrainStationManagementApp.Managers.Implementations
{
    public class BookingManager : IBookingManager
    {
        static List<Booking> listOfBooking = new List<Booking>();
        ITrainManager trainManager = new TrainManager();
        IUserManager userManager = new UserManager();
        IManagerManager managerManager = new ManagerManager();
        IPassengerManager passengerManager = new PassengerManager();
        string filePath = AppFilePath.FilePathRoot("bookingManager.txt");
        
        public BookingManager()
        {
            //ReadBookingFromFile();  // The Constructor Will Help Me To Read The File To The List.
        }

        public void ReadBookingFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                        var bookings = File.ReadAllLines(filePath);
                        foreach (var booking in bookings)
                        {
                            listOfBooking.Add(Booking.ToBooking(booking));
                        }
                }
                else
                {
                    string path = AppFilePath.FilePathRoot();
                    Directory.CreateDirectory(path);
                    string fileName = "bookingManager.txt";
                    string fullPath = Path.Combine(path, fileName);
                    File.Create(fullPath);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

          public void WriteBookingToFile(Booking booking)
        {
           try
           {
                using (var streamWriter = new StreamWriter(filePath, true))
                {
                    streamWriter.WriteLine(booking.ToString());
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
                using (var streamWriter = new StreamWriter(filePath))
                {
                    foreach (var item in listOfBooking)
                    {
                        streamWriter.WriteLine(item.ToString());
                    }
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Booking CreateBooking1(string passengerEmail, int trainId)
        {
            var passenger = passengerManager.Get(passengerEmail);
            var train = trainManager.GetTrain(trainId);
            if(train != null)
            {
                if(passenger != null)
                {
                    var user = userManager.GetUser(passengerEmail);
                    if(train.Price < user.Wallet)
                    {
                        int id = listOfBooking.Count + 1;
                        string referenceNumber = GenerateReferenceNumber();
                        //int seatNumber = (train.Capacity - train.AvailableSpace) + 1;
                        Booking booking = new Booking(id, passenger.Id,  passengerEmail, train.Id, "clh", 7, false, DateTime.Now, DateTime.Now);
                        listOfBooking.Add(booking);
                        WriteBookingToFile(booking);
                        return booking;
                    }
                    else{
                         System.Console.WriteLine("insufficient fund");
                         userManager.FundUserWallet(passengerEmail, train.Price);
                    }
                   
                }
                System.Console.WriteLine("wrong email");
                return null;
            }
            System.Console.WriteLine("Train not available");
            return  null;
        }
        public Booking CreateBooking(Passenger passenger, Train train)
        {
            
            var train1 = trainManager.GetTrain(train.Id);
            if (train1 == null)
            {
                Console.WriteLine("Train not found");
                return null;
            }
            else if (train1.AvailableSpace < 1)
            {
                Console.WriteLine("No available space");
                return null;
            }
            else
            { 
                var user = userManager.GetUser(passenger.UserId);
                double bookingPrice = train.Price;
                if (user.Wallet < bookingPrice)
                {
                    Console.WriteLine("Oops! Insufficient fund");
                    return null;
                }

                var messager = train.TakeOffTime < DateTime.Now.TimeOfDay ? "Sorry, this train is closed for booking. Kindly check for next available train": "Congrats! Train booked successfully";
                Console.WriteLine(messager);
                
                var passengerEmail = userManager.GetUser(passenger.UserId).Email;
                userManager.DebitUserWallet(passengerEmail,bookingPrice);
                var manager2 = new ManagerManager();
                manager2.RefreshFile();
                var id2 = manager2.GetManagerUserId();
                Console.WriteLine(id2);
                var manager = userManager.GetUser(id2);
                userManager.FundManagerWallet(manager.Email, bookingPrice);

                int seatNumber = (train.Capacity - train.AvailableSpace) + 1;
                int id = listOfBooking.Count + 1;
                string referenceNumber = GenerateReferenceNumber();
                Booking booking = new Booking(id, passenger.Id, user.Email, train.Id, referenceNumber, seatNumber, false, DateTime.Now, DateTime.Now);

                train.AvailableSpace -= 1;
                trainManager.RefreshFile();
                WriteBookingToFile(booking);
                listOfBooking.Add(booking);
                return booking;
            }
        }

        public bool DeleteBooking(int id)
        {
            var booking = TryGet(id);
            if (booking != null && booking.IsDeleted == false)
            {
                booking.IsDeleted = true;
                File.WriteAllText(filePath, string.Empty);
                RefreshFile();
                return true;
            }
            
            return false;
        }

        public List<Booking> GetAllBooking()
        {
            return listOfBooking;
        }

        public Booking GetBooking(int id)
        {
            return TryGet(id);
        }

        public Booking GetBooking(string referenceNumber)
        {
            foreach (var booking in listOfBooking)
            {
                if (booking.ReferenceNumber == referenceNumber)
                {
                    return booking;
                }
            }
            return null;
        }

         public string GenerateReferenceNumber()
        {
            return $"TSM/{DateTime.Now.Month}/{DateTime.Now.Day}/{listOfBooking.Count + 1}";
        }

        public Booking TryGet(int id)
        {
            foreach (var booking in listOfBooking)
            {
                if (booking.Id == id && booking.IsDeleted == false)
                {
                    return booking;
                }
            }
            return null;
        }

       
    }
}