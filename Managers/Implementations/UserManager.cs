using System;
using System.Collections.Generic;
using System.IO;
using TrainStationManagementApp.Managers.Interfaces;
using TrainStationManagementApp.Models.Entities;
using TrainStationManagementApp.Models.Enums;

namespace TrainStationManagementApp.Managers.Implementations
{
    public class UserManager : IUserManager
    {
        public static List<User> listOfUser = new List<User>();
        string filePath =  AppFilePath.FilePathRoot("userManager.txt");

        public UserManager()
        {
            // ReadUserFromFile(); 
        }

        public void ReadUserFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                   
                        var users = File.ReadAllLines(filePath);
                        foreach (var user in users)
                        {
                            listOfUser.Add(User.ToUser(user));
                        }
                }
                else
                {
                    string path = AppFilePath.FilePathRoot();
                    Directory.CreateDirectory(path);
                    string fileName = "userManager.txt";
                    string fullPath = Path.Combine(path, fileName);
                    File.Create(fullPath);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void WriteUserToFile(User user)
        {
            try
            {
                using (var write = new StreamWriter(filePath, true))
                {
                    write.WriteLine(user.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void RefreshFile()
        {
            try
            {
                File.WriteAllText(filePath , string.Empty);
                using(StreamWriter str = new StreamWriter(filePath))
                {
                    foreach (var item in listOfUser)
                    {
                        str.WriteLine(item.ToString());
                    }
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
            
        public bool DebitUserWallet(string email, double amount)
        {
            var user = TryGet(email);
            if (user == null)
            {
                Console.WriteLine("Oops! User not found");
                return false;
            }
            //Console.WriteLine(amount);
            user.Wallet -= amount;
            File.WriteAllText(filePath, string.Empty);
            RefreshFile();
            Console.WriteLine($"Wallet Fund Successfully");
            return true;
        }

        public bool DeleteUser(string email)
        {
           var user = TryGet(email);
           if (user != null && user.IsDeleted == false)
           {
                user.IsDeleted = true;
                return true;
           }
           File.WriteAllText(filePath , String.Empty);
           RefreshFile();
           return false;
        }

        public bool FundManagerWallet(string managerEmail, double amount)
        {
             var user = TryGet(managerEmail);
            if (user == null)
            {
                Console.WriteLine("Oops! User not found");
                return false;
            }
            //Console.WriteLine(amount);
            user.Wallet += amount;
            File.WriteAllText(filePath, string.Empty);
            RefreshFile();
            return true;
        }

        public bool FundUserWallet(string email, double amount)
        {
            var user = TryGet(email);
            if (user == null)
            {
                Console.WriteLine("Oops! User not found");
                return false;
            }
            user.Wallet = user.Wallet += amount;
            File.WriteAllText(filePath, string.Empty);
            RefreshFile();
            Console.WriteLine($"Wallet Fund Successfully");
            return true;;
        }

        public List<User> GetAllUser()
        {
            //RefreshFile();
            return listOfUser;
        }

        public User GetUser(int id)
        {
            var users = TryGet(id);
            foreach (var user in listOfUser)
            {
                if (users.Id == user.Id && users.IsDeleted == false)
                {
                    return user;
                }
            }
            return null;
        }

        public User GetUser(string email)
        {
             var users = TryGet(email);
            foreach (var user in listOfUser)
            {
                if (user.Email == email && user.IsDeleted == false)
                {
                    return user;
                }
            }
            return null;
        }

        public User LoginUser(string email, string password)
        {
            foreach (var user in listOfUser)
            {
                if (user.Email == email && user.Password == password && user.IsDeleted == false)
                {
                    RefreshFile();
                    return user;
                }
            }
            return null;
        }

        public User RegisterUser(string firstName, string lastName, string email, string password, string phoneNumber, Gender gender, string address, RoleName role)
        {
            var user1 = TryGet(email);
            if (user1 == null)
            {
                int id = listOfUser.Count + 1;
                var user = new User(id, firstName, lastName, email, password, phoneNumber, gender, address, 0, role, false, DateTime.Now, DateTime.Now);
                WriteUserToFile(user);
                listOfUser.Add(user);
                return user;
            }
            return null;
        }

        public User UpdateUser(User user)
        {
            var userExist = TryGet(user.Id);
            if(userExist == null)
            {
                return null;
            }
            userExist.FirstName = user.FirstName;
            userExist.LastName = user.LastName;
            userExist.PhoneNumber = user.PhoneNumber;
            userExist.Gender = user.Gender;
            userExist.Address = user.Address;
            userExist.DateCreated = user.DateCreated;
            userExist.DateUpdated = user.DateUpdated;
            //File.WriteAllText(filePath, string.Empty);
            RefreshFile();
            return userExist;
        }


        public User TryGet(int id)
        {
            foreach (var user in listOfUser)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }

        public User TryGet(string email)
        {
            foreach (var user in listOfUser)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }
            return null;
        }
    }
}