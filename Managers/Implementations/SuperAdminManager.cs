using System;
using System.Collections.Generic;
using System.IO;
using TrainStationManagementApp.Managers.Interfaces;
using TrainStationManagementApp.Models.Entities;
using TrainStationManagementApp.Models.Enums;

namespace TrainStationManagementApp.Managers.Implementations
{
    public class SuperAdminManager : ISuperAdminManager
    {
        static List<SuperAdmin> listOfSuperAdmin = new List<SuperAdmin>();
        IUserManager userManager = new UserManager();
        string filePath = AppFilePath.FilePathRoot("SuperAdminManager.txt");

        public SuperAdminManager()
        {
           // ReadSuperAdminFromFile();  // The Constructor Will Help Me To Read The File To The List.
        }

        public void ReadSuperAdminFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                        var superAdmins = File.ReadAllLines(filePath);
                        foreach (var superAdmin in superAdmins)
                        {
                           listOfSuperAdmin.Add(SuperAdmin.ToSuperAdmin(superAdmin));
                        }
                }
                else 
                {
                    string path = AppFilePath.FilePathRoot();
                    Directory.CreateDirectory(path);
                    string fileName = "superAdminManager.txt";
                    string fullPath = Path.Combine(path, fileName);
                    File.Create(fullPath);
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
                    foreach (var item in listOfSuperAdmin)
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
             var superAdmin = TryGet(id);
            if (superAdmin != null && superAdmin.IsDeleted == false)
            {
                superAdmin.IsDeleted = true;
                return true;
            }
            File.WriteAllText(filePath, string.Empty);
            RefreshFile();
            return false;
        }

        public SuperAdmin Get(int id)
        {
            return TryGet(id);;
        }

        public SuperAdmin Get(string email)
        {
            return TryGet(email);
        }

        public List<SuperAdmin> GetAll()
        {
            return listOfSuperAdmin;
        }

        public User Update(string firstName, string lastName, User user, string phoneNumber, Gender gender, string address)
        {
            var admin = userManager.GetUser(user.Email);
            if (admin  == null)
            {
                return null;
            }
            admin.FirstName = firstName;
            admin.LastName = lastName;
            admin.Email = user.Email;
            admin.Password = user.Password;
            admin.PhoneNumber = phoneNumber;
            admin.Gender = gender;
            admin.Address = address;
            RefreshFile();
            return admin;
        }


         public SuperAdmin TryGet(string email)
        {
            var user = userManager.GetUser(email);
            if (user != null)
            {
                return TryGet(user.Email);
            }File.WriteAllText(filePath, string.Empty);
            RefreshFile();
            return null;
        }
        public SuperAdmin TryGet(int id)
        {
            foreach (var superAdmin in listOfSuperAdmin)
            {
                if (superAdmin.Id == id && superAdmin.IsDeleted == false)
                {
                    return superAdmin;
                }
            }
            return null;
        }
    }
}