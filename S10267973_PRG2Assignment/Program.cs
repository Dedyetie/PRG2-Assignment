using S10267973_PRG2Assignment;

Terminal terminal = new Terminal("Terminal 5");

//1) Load files (airlines and boarding gates)
using (StreamReader sr = new StreamReader("airlines.csv"))
{
    string? s = sr.ReadLine();

    while ((s = sr.ReadLine()) != null)
    {
        string[] info = s.Split(",");
        Airline airline = new Airline(info[0], info[1]);
        terminal.AddAirline(airline);
    }
}

using (StreamReader sr = new StreamReader("boardinggates.csv"))
{
    string? s = sr.ReadLine();

    while ((s = sr.ReadLine()) != null)
    {
        string[] info = s.Split(",");
        BoardingGate boardingGate = new BoardingGate(info[0], Convert.ToBoolean(info[1]), Convert.ToBoolean(info[2]), Convert.ToBoolean(info[3]));
    }
}

//2) Load files (flights)
using (StreamReader sr = new StreamReader("flights.csv"))
{
    int counter = 0;
    sr.ReadLine();
    string line = "";
    Console.WriteLine("Loading Flights...");
    while ((line = sr.ReadLine()) != null)
    {
        string[] spline = line.Split(',');
        // if flight has special request LWTT
        if (spline[4] == "LWTT")
        {
            terminal.Flights.Add(spline[0], new LWTTFlight(spline[0], spline[1], spline[2], Convert.ToDateTime(spline[3]), "On time"));
        }
        // if flight has special request DDJB
        else if (spline[4] == "DDJB")
        {
            terminal.Flights.Add(spline[0], new DDJBFlight(spline[0], spline[1], spline[2], Convert.ToDateTime(spline[3]), "On time"));
        }
        // if flight has special request CFFT
        else if (spline[4] == "CFFT")
        {
            terminal.Flights.Add(spline[0], new CFFTFlight(spline[0], spline[1], spline[2], Convert.ToDateTime(spline[3]), "On time"));
        }
        // if it is a normal flight (no special request)
        else
        {
            terminal.Flights.Add(spline[0], new NORMFlight(spline[0], spline[1], spline[2], Convert.ToDateTime(spline[3]), "On time"));
        }
        counter++;
    }
    Console.WriteLine($"{counter} Flights Loaded!");
}

// Task 3: List all the flights
void DisplayFlight()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-21}{"Origin",-21}{"Destination",-21}{"Expected Departure/Arrival Time"}");
    foreach (Flight flight in FlightDict.Values)
    {
        Console.WriteLine($"{flight.FlightNumber,-16}{"aa",-21}{flight.Origin,-21}{flight.Destination,-21}{flight.ExpectedTime}");
    }
}
DisplayFlight();

// Task 5: Assign a boarding gate to a flight
string flightNumber = "";
try
{
    while (true)
    {
        Console.WriteLine("=============================================\nAssign a Boarding Gate to a Flight\n=============================================\n");
        Console.WriteLine("Enter Flight Number: ");
        flightNumber = Console.ReadLine();
        Console.WriteLine("Enter Boarding Gate Name: ");

        break;
    }
}
catch (Exception ex)
{

}