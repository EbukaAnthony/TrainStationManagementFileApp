using System;

namespace TrainStationManagementApp.Models.Entities
{
    public class Train : BaseEntity
    {
        public string Name { get; set; }
        public string TakeOffPoint { get; set; }
        public string Destination { get; set; }
        public TimeSpan TakeOffTime { get; set; }
        public TimeSpan LandingTime { get; set; }
        public double Price { get; set; }
        public int Capacity { get; set; }
        public int AvailableSpace { get; set; }

        public Train(int id, String name, string takeOffPoint, string destination, TimeSpan takeOffTime, TimeSpan landingTime, double price, int capacity, int availableSpace, bool isDeleted, DateTime dateCreated, DateTime dateUpdated) : base(id, isDeleted, dateCreated, dateUpdated)
        {
            Id = id;
            Name = name;
            TakeOffPoint = takeOffPoint;
            Destination = destination;
            TakeOffTime = takeOffTime;
            LandingTime = landingTime;
            Price = price;
            Capacity = capacity;
            AvailableSpace = availableSpace;
            IsDeleted = isDeleted;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
        }

        public override string ToString()
        {
            return $"{Id};{Name};{TakeOffPoint};{Destination};{TakeOffTime};{LandingTime};{Price};{Capacity};{AvailableSpace};{IsDeleted};{DateCreated};{DateUpdated}";
        }

        public static Train ToTrain(string train)
        {
            var result = train.Split(';');
            int id = int.Parse(result[0]);
            string name = result[1];
            string takeOffPoint = result[2];
            String destination = result[3];
            TimeSpan takeOffTime = TimeSpan.Parse(result[4]);
            TimeSpan landingTime = TimeSpan.Parse(result[5]);
            double price = double.Parse(result[6]);
            int  capacity = int.Parse(result[7]);
            int  availableSpace = int.Parse(result[8]);
            bool isDeleted = bool.Parse(result[9]);
            DateTime dateCreated = DateTime.Parse(result[10]);
            DateTime dateUpdated = DateTime.Parse(result[11]);
            Train train1 = new Train(id, name, takeOffPoint, destination, takeOffTime, landingTime, price, capacity, availableSpace, isDeleted, dateCreated, dateUpdated);
            return train1;
        }

    }
}