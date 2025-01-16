//==========================================================
// Student Number: 
// Student Name	 : Eng Zhe Xuan, Gerel
// Student Number: S10268020
// Partner Name	 : Ong Jun Shu Camellia
//==========================================================
namespace PRG2_ASST
{
    abstract class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = status;
        }
        public abstract double CalculateFees();
        public override string ToString()
        {
            return "FlightNumber" + FlightNumber + "Origin" + Origin + "Destination" + Destination + "ExpectedTime" + ExpectedTime + "Status" + Status;
        }
    }
}
