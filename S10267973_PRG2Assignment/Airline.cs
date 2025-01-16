namespace PRG2_ASST
{
    class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();

        public Airline() { }
        public Airline(string n, string c)
        {
            Name = n;
            Code = c;
        }

        public bool AddFlight(Flight flight)
        { 
            Flights[flight.FlightNumber] = flight;
            if (Flights.ContainsKey(flight.FlightNumber) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public double CalculateFees()
        {
            double fees = 0;
            foreach (KeyValuePair<string, Flight> kvp in Flights)
            {
                fees += kvp.Value.CalculateFees();
            }
            return fees;
        }
        public bool RemoveFlight(Flight flight)
        {
            Flights.Remove(flight.FlightNumber);
            if (Flights.ContainsKey(flight.FlightNumber) == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            return "";
          
        }

    }
}
