namespace TrainStationManagementApp
{
    public class AppFilePath
    {
        public static string FilePathRoot(string fileName = "")
        {
            if (fileName == "")
            {
                return @"C:\Users\hp\Desktop\TrainStationManagementApp\File";
            }
            return @"C:\Users\hp\Desktop\TrainStationManagementApp\File\"+ fileName;
        }
    }
}