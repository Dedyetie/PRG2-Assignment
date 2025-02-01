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
            double baseTotal = 0;
            double discounts = 0;
            double finalTotal = 0;

            Console.WriteLine("=============================================");
            Console.WriteLine("Fees For Each Airline");
            Console.WriteLine("=============================================\n");

            Console.WriteLine($"{"Airline",-21}{"Original Subtotal ($)",-23}{"Total Discount ($)",-20}{"Final Fee ($)"}");

            foreach (Airline airline in Airlines.Values)
            {
                double nineToEleven = 0;
                double dubaiBangkokTokyo = 0;
                double noSpecialRequest = 0;
                double percentageDiscount = 1;

                double baseFee = airline.CalculateFees();
                double flightx3 = Math.Floor(Convert.ToDouble(airline.Flights.Count / 3)) * 350;

                foreach (Flight flight in airline.Flights.Values)
                {
                    if (flight.ExpectedTime.TimeOfDay >= new TimeSpan(21, 0, 0) || flight.ExpectedTime.TimeOfDay < new TimeSpan(11, 0, 0))
                    {
                        nineToEleven += 110;
                    }
                    if ((flight.Origin == "Dubai (DXB)") || (flight.Origin == "Bangkok (BKK)") || (flight.Origin == "Tokyo (NRT)"))
                    {
                        dubaiBangkokTokyo += 25;
                    }
                    if (flight is NORMFlight)
                    {
                        noSpecialRequest += 50;
                    }
                }

                if (airline.Flights.Count > 5)
                {
                    percentageDiscount = 0.97;
                }

                double netFee = (baseFee * percentageDiscount) - flightx3 - nineToEleven - dubaiBangkokTokyo - noSpecialRequest;

                baseTotal += baseFee;
                discounts += flightx3 + nineToEleven + dubaiBangkokTokyo + noSpecialRequest;
                finalTotal += netFee;

                Console.WriteLine($"{airline.Name,-21}{baseFee,-23:F2}{(baseFee - netFee),-20:F2}{netFee:F2}");
            }

            Console.WriteLine("\n=============================================");
            Console.WriteLine("Totals For All Airlines");
            Console.WriteLine("=============================================\n");

            Console.WriteLine($"Subtotal of all Airline fees: ${baseTotal:F2}");
            Console.WriteLine($"Subtotal of all Airline discounts to be Deducted: ${discounts:F2}");
            Console.WriteLine($"Airline fees to be collected: ${finalTotal:F2}");
            Console.WriteLine($"Percentage of discounts over Subtotal of all Airline fees: {(discounts / baseTotal * 100):F2}%");
            
        }

        public override string ToString()
        {
            return "TerminalName" + TerminalName + "Airlines" + Airlines + "Flights" + Flights + "BoardingGates" + BoardingGates + "GateFees" + GateFees;
        }

    }
}
