using System;
using TrainStationManagementApp.Managers.Implementations;
using TrainStationManagementApp.Managers.Interfaces;

namespace TrainStationManagementApp
{
    public class Validate
    {
        public static int NumberValidate(string input, string fieldName)
        {
            var check = true;
            int number;
             do
             {
                if (!int.TryParse(input, out number))
                {
                    check = false;
                    Console.WriteLine($"Invalid input! {fieldName} should be number");
                    return NumberValidate(Console.ReadLine(), fieldName);
                }
             } while (!check);

             return number;
        }
        public static int TrainIdValidate(string input, string fieldName)
        {

            int trainId = NumberValidate(input, fieldName);

            
            ITrainManager trainManager = new TrainManager();
            var train = trainManager.GetTrain(trainId);

             if (train == null)
             {
                Console.WriteLine($"Wrong input!");
                return TrainIdValidate(Console.ReadLine(), fieldName);
             }

             return trainId;
        }
        public static int GenderValidate(string input, string fieldName)
        {

            int trainId = NumberValidate(input, fieldName);


             if (trainId < 1 || trainId > 2)
             {
                Console.WriteLine($"Wrong input!");
                return TrainIdValidate(Console.ReadLine(), fieldName);
             }

             return trainId;
        }
    }
}