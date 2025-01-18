//==========================================================
// Student Number: S10267973, S10268020
// Student Name	 : Eng Zhe Xuan, Gerel & Ong Jun Shu, Camellia
//==========================================================

namespace S10267973_PRG2Assignment
{
    class Terminal
    {
        public string TerminalName {  get; set; }
        public Dictionary<string, Airline> Airlines { get; set; } 
            = new Dictionary<string, Airline>();
        public Dictionary<string, Flight> Flights { get; set; } 
            = new Dictionary<string, Flight>();
        public Dictionary<string, BoardingGate> BoardingGates { get; set; } 
            = new Dictionary<string, BoardingGate>();
        public Dictionary<string, double> GateFees { get; set; }
            = new Dictionary<string, double>();

        public Terminal() { }
        public Terminal(string tN)
        {
            TerminalName = tN;
        }

        public bool AddAirline(Airline A)
        {
            Airlines[A.Code] = A;
            if (Airlines.ContainsKey(A.Code) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddBoardingGate(BoardingGate BG)
        {
            BoardingGates[BG.GateName] = BG;
            if (BoardingGates.ContainsKey(BG.GateName) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Airline GetAirlineFromFlight(Flight f)
        {
            foreach (Airline airline in Airlines.Values)
            {
                if (f.FlightNumber.Contains(airline.Code) == true)
                {
                    return airline;
                }
            }

            return null;
        }
        public void PrintAirlineFees()
        {
            foreach (KeyValuePair<string, Airline> kvp in Airlines)
            {
                Console.WriteLine($"{kvp.Value.Name} Fee: {kvp.Value.CalculateFees()}");
            }
        }

        public override string ToString()
        {
            return "";
        }

    }
}
