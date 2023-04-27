namespace TrainStationManagementApp.Menu.Interfaces
{
    public interface IPassengerMenu
    {
        public void FundWalletMenu(string userEmail);
        void RealPassengerMenu(string userEmail);
        void CreateBookingMenu(string userEmail);
        void ViewBookingMenu(string userEmail);
        void ViewAllBookingMenu(string userEmail);
        public void ViewAllTrainMenu(string userEmail);
        public void Logout();
    }
}