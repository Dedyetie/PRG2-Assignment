﻿//==========================================================
// Student Number: S10267973, S10268020
// Student Name	 : Eng Zhe Xuan, Gerel & Ong Jun Shu, Camellia
//==========================================================

namespace S10267973_PRG2Assignment
{
    class LWTTFlight : Flight
    {
        public double RequestFee { get; set; } = 500;
        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status) : base(flightNumber, origin, destination, expectedTime, status) { }
        public override double CalculateFees()
        {
            double boardArrivalFee = (Destination == "Singapore (SIN)") ? 500 : 800;
            //300 is for boarding gate
            return boardArrivalFee + RequestFee + 300;
        }
        public override string ToString()
        {
            return base.ToString() + "RequestFee" + RequestFee;
        }
    }
}
