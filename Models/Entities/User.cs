using System;
using TrainStationManagementApp.Models.Enums;

namespace TrainStationManagementApp.Models.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password{ get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public double Wallet { get; set; }
        public RoleName Role { get; set; }


        public User(int id, string firstName, string lastName, string email, String password, string phoneNumber, Gender gender,string address, double wallet, RoleName role, bool isDeleted, DateTime dateCreated, DateTime dateUpdated) : base (id, isDeleted, dateCreated, dateUpdated)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            Gender = gender;
            Address = address;
            Wallet = wallet;
            Role = role;
            IsDeleted = isDeleted;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
        }

        public override string ToString()
        {
            return $"{Id};{FirstName};{LastName};{Email};{Password};{PhoneNumber};{Gender};{Address};{Wallet};{Role};{IsDeleted};{DateCreated};{DateUpdated}";
        }

        public static User ToUser(string user)
        {
            var result = user.Split(';');
            int id = int.Parse(result[0]);
            string firstName = result[1];
            string lastName = result[2];
            string email = result[3];
            string password = result[4];
            string phoneNumber = result[5];
            Gender gender = (Gender)Enum.Parse(typeof(Gender),result[6]);
            string address = result[7];
            double wallet = double.Parse(result[8]);
            RoleName role = (RoleName)Enum.Parse(typeof(RoleName), result[9]);
            bool isDeleted = bool.Parse(result[10]);
            DateTime dateCreated = DateTime.Parse(result[11]);
            DateTime dateUpdated = DateTime.Parse(result[12]);
            User user1 = new User(id, firstName, lastName, email, password, phoneNumber, gender, address, wallet, role, isDeleted, dateCreated, dateUpdated);
            return user1;
        }
    }
}