//==========================================================
// Student Number: S10267973
// Student Name	 : Eng Zhe Xuan, Gerel
// Partner Name	 : Ong Jun Shu Camellia
//==========================================================

namespace PRG2_ASST
{
    class CFFTFlight : Flight
    {
        public double RequestFee { get; set; } = 150;
        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status) : base(flightNumber, origin, destination, expectedTime, status) { }
        public override double CalculateFees()
        {
            double boardArrivalFee = (Destination == "Singapore (Sin)") ? 500 : 800;
            //300 is for boarding gate
            return boardArrivalFee + RequestFee + 300;
        }
        public override string ToString()
        {
            return base.ToString() + "RequestFee" + RequestFee;
        }
    }
}
