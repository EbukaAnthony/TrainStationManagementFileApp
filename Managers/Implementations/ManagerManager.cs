using System;
using System.Collections.Generic;
using System.IO;
using TrainStationManagementApp.Managers.Interfaces;
using TrainStationManagementApp.Models.Entities;
using TrainStationManagementApp.Models.Enums;

namespace TrainStationManagementApp.Managers.Implementations
{
    public class ManagerManager : IManagerManager
    {
        IUserManager userManager = new UserManager();
        static List<Manager> listOfManager = new List<Manager>();
        
        string filePath = AppFilePath.FilePathRoot("managerManager.txt");

        public ManagerManager()
        {
            //ReadManagerFromFile();  // The Constructor Will Help Me To Read The File To The List.
        }


        public void ReadManagerFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                        var managers = File.ReadAllLines(filePath);
                        foreach (var manager in managers)
                        {
                            listOfManager.Add(Manager.ToManager(manager));
                        } 
                }
                else
                {
                    string path = AppFilePath.FilePathRoot();
                    Directory.CreateDirectory(path);
                    string fileName = "managerManager.txt";
                    string fullPath = Path.Combine(path, fileName);
                    File.Create(fullPath);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void WriteManagerToFile(Manager manager)
        {
            try
            {
                using(var streamWriter = new StreamWriter(filePath, true))
                {
                    streamWriter.WriteLine(manager.ToString());
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
                    foreach (var item in listOfManager)
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



        public Manager CreateManager(string firstName, string lastName, string email, string password, string phoneNumber, Gender gender, string address, int userId)
        {
            if(true)
            {
                 User user = new User(UserManager.listOfUser.Count +1,firstName,lastName,email,password,phoneNumber,gender,address,0,RoleName.Manager,false,DateTime.Now,DateTime.Now);
                if(userId != 0)
                {
                    int id = listOfManager.Count + 1;
                    string staffNumber = GenerateStaffNumber();
                    Manager manager = new Manager(id, userId, staffNumber, false, DateTime.Now, DateTime.Now);
                    WriteManagerToFile(manager);
                    listOfManager.Add(manager); 
                    return manager;
                }
            }
            return null;
        }

        public bool DeleteManager(int id)
        {
            var manager = TryGet(id);
            if (manager != null && manager.IsDeleted == false)
            {
                manager.IsDeleted = true;
                return true;
            }
            File.WriteAllText(filePath, string.Empty);
            RefreshFile();
            return false;
        }

        public List<Manager> GetAllManager()
        {
            return listOfManager;
        }

        public Manager GetManager(int id)
        {
            return TryGet(id);
        }

        public Manager GetManager(string staffNumber)
        {
            foreach (var manager in listOfManager)
            {
                if (manager.StaffNumber == staffNumber && manager.IsDeleted == false)
                {
                    return manager;
                }
            }
            return null;
        }

        public User UpdateManager(string firstName, string lastName, User user, string phoneNumber, Gender gender, string address)
        {
            var managerExist = userManager.GetUser(user.Email);
            if (managerExist == null)
            {
                return null;
            }
            managerExist.FirstName = firstName;
            managerExist.LastName = lastName;
            managerExist.Email = user.Email;
            managerExist.Password = user.Password;
            managerExist.PhoneNumber = phoneNumber;
            managerExist.Gender = gender;
            managerExist.Address = address;
            //File.WriteAllText(filePath, string.Empty);//
            RefreshFile();
            return managerExist;
        }

        private string GenerateStaffNumber()
        {
            return $"STF/000{listOfManager.Count + 1}";
        }

        private Manager TryGet(int id)
        {
            foreach (var manager in listOfManager)
            {
                if (manager.Id == id && manager.IsDeleted == false)
                {
                    return manager;
                }
            }
            return null;
        }

        public  int GetManagerUserId()
        {
            return listOfManager[listOfManager.Count - 1].UserId;
        }
    }
}