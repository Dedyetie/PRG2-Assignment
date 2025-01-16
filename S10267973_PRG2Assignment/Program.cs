using PRG2_ASST;
using System.Diagnostics.CodeAnalysis;


// Task 2: Load files (flights)
Dictionary<string, Flight> FlightDict = new Dictionary<string, Flight>();
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
            FlightDict.Add(spline[0], new LWTTFlight(spline[0], spline[1], spline[2], Convert.ToDateTime(spline[3]), "On time"));
        }
        // if flight has special request DDJB
        else if (spline[4] == "DDJB")
        {
            FlightDict.Add(spline[0], new DDJBFlight(spline[0], spline[1], spline[2], Convert.ToDateTime(spline[3]), "On time"));
        }
        // if flight has special request CFFT
        else if (spline[4] == "CFFT")
        {
            FlightDict.Add(spline[0], new CFFTFlight(spline[0], spline[1], spline[2], Convert.ToDateTime(spline[3]), "On time"));
        }
        // if it is a normal flight (no special request)
        else
        {
            FlightDict.Add(spline[0], new NORMFlight(spline[0], spline[1], spline[2], Convert.ToDateTime(spline[3]), "On time"));
        }
        counter++;
    }
    Console.WriteLine($"{counter} Flights Loaded!");
}

// Task 3: List all the flights
void DisplayFlight()
{
    Console.WriteLine("=============================================\nList of Flights for Changi Airport Terminal 5\n=============================================\n");
    Console.WriteLine($"{"Flight Number", -16}{"Airline Name", -21}{"Origin", -21}{"Destination", -21}{"Expected Departure/Arrival Time"}");
    foreach (Flight flight in FlightDict.Values)
    {
        Console.WriteLine($"{flight.FlightNumber, -16}{"aa", -21}{flight.Origin, -21}{flight.Destination, -21}{flight.ExpectedTime}");
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