using S10267973_PRG2Assignment;

Terminal terminal = new Terminal("Terminal 5");

//1) Load files (airlines and boarding gates)
using (StreamReader sr = new StreamReader("airlines.csv"))
{
    int counter = 0;
    Console.WriteLine("Loading Airlines...");

    string? s = sr.ReadLine();

    while ((s = sr.ReadLine()) != null)
    {
        string[] info = s.Split(",");
        Airline airline = new Airline(info[0], info[1]);
        terminal.AddAirline(airline);
        counter++;
    }
    Console.WriteLine($"{counter} Airlines Loaded!");
}

using (StreamReader sr = new StreamReader("boardinggates.csv"))
{
    int counter = 0;
    Console.WriteLine("Loading Boarding Gates...");

    string? s = sr.ReadLine();

    while ((s = sr.ReadLine()) != null)
    {
        string[] info = s.Split(",");
        BoardingGate boardingGate = new BoardingGate(info[0], Convert.ToBoolean(info[1]), Convert.ToBoolean(info[2]), Convert.ToBoolean(info[3]));
        terminal.AddBoardingGate(boardingGate);
        counter++;
    }
    Console.WriteLine($"{counter} Boarding Gates Loaded!");
}


// 2) Load files (flights)
using (StreamReader sr = new StreamReader("flights.csv"))
{
    int counter = 0;
    Console.WriteLine("Loading Flights...");

    string line = "";
    sr.ReadLine();
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

Console.WriteLine("");
Console.WriteLine("");

// Main Program
while (true)
{
    int opt = Menu();

    if (opt == 0)
    {
        Console.WriteLine("Goodbye!");
        break;
    }
    else if (opt == 1)
    {
        DisplayFlight();
        Console.WriteLine("");
    }

    else if (opt == 2)
    {

    }
    else if (opt == 3)
    {
        BoardGateAssignment();
        Console.WriteLine("");
    }
    else if (opt == 4)
    {
        CreateFlight();
        Console.WriteLine("");
    }
    else if (opt == 5)
    {
    }
    else if (opt == 6)
    {
    }
    else if (opt == 7)
    {

    }
    else
    {
        Console.WriteLine("Invalid option! Please try again.");
        Console.WriteLine("");
    }
}


// Menu 
int Menu()
{
    int opt;
    try
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Welcome to Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("1. List All Flights");
        Console.WriteLine("2. List Boarding Gates");
        Console.WriteLine("3. Assign a Boarding Gate to a Flight");
        Console.WriteLine("4. Create Flight");
        Console.WriteLine("5. Display Airline Flights");
        Console.WriteLine("6. Modify Flight Details");
        Console.WriteLine("7. Display Flight Schedule");
        Console.WriteLine("0. Exit");
        Console.WriteLine("");
        Console.WriteLine("Please select your option:");
        opt = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("");
    }
    catch (FormatException)
    {
        opt = -1;
    }
    return opt;
}

// 3) List all flights with basic information
void DisplayFlight() //option 1
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-21}{"Origin",-21}{"Destination",-21}{"Expected Departure/Arrival Time"}");
    foreach (Flight flight in terminal.Flights.Values)
    {
        Console.WriteLine($"{flight.FlightNumber,-16}{"aa",-21}{flight.Origin,-21}{flight.Destination,-21}{flight.ExpectedTime}");
    }
}


// 5) Assign a boarding gate to a flight
void BoardGateAssignment() //option 3
{
    string flightNumber = "";
    string boardingGateNumber = "";
    try
    {
        while (true)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Assign a Boarding Gate to a Flight");
            Console.WriteLine("=============================================");
            Console.WriteLine("Enter Flight Number: ");
            flightNumber = Console.ReadLine().ToUpper();
            bool flightNumberFound = false;
            foreach (Flight flight in terminal.Flights.Values)
            {
                if ((flightNumber == flight.FlightNumber) && (terminal.BoardingGates.ContainsKey(boardingGateNumber)))
                {
                    //Checks if the flight number has any special request
                    string specialRequest = "";
                    if (terminal.Flights[flightNumber] is NORMFlight)
                    {
                        specialRequest = "None";
                    }
                    else if (terminal.Flights[flightNumber] is DDJBFlight)
                    {
                        specialRequest = "DDJB";
                    }
                    else if (terminal.Flights[flightNumber] is CFFTFlight)
                    {
                        specialRequest = "CFFT";
                    }
                    else
                    {
                        specialRequest = "LWTT";
                    }

                    // Displays the information of the flight number
                    Console.WriteLine("Flight Number: " + flight.FlightNumber);
                    Console.WriteLine("Origin: " + flight.Origin);
                    Console.WriteLine("Destination: " + flight.Destination);
                    Console.WriteLine("Expected Time: " + flight.ExpectedTime);
                    Console.WriteLine("Special Request Code: " + specialRequest);
                    Console.WriteLine("Supports DDJB: " + terminal.BoardingGates[boardingGateNumber].SupportsDDJB);
                    Console.WriteLine("Supports CFFT: " + terminal.BoardingGates[boardingGateNumber].SupportsCFFT);
                    Console.WriteLine("Supports LWTT: " + terminal.BoardingGates[boardingGateNumber].SupportsLWTT);
                    flightNumberFound = true;
                    break;
                }
            }
            Console.WriteLine("Enter Boarding Gate Name: ");
            boardingGateNumber = Console.ReadLine().ToUpper();
            if (flightNumberFound)
            {
                break;
            }
            else
            {
                throw new Exception();
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Invalid input or flight details. Please try again.");
    }

    try
    {
        while (true)
        {
            Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
            string update = Console.ReadLine().ToLower();
            if (update == "y")
            {
                Console.WriteLine("1. Delayed \n2. Boarding\n3. On Time");
                Console.WriteLine("Please select the new status of the flight:");
                string integer = Console.ReadLine();

                // Updates the status of the flight to any of the above 3 mentioned
                if (integer == "1")
                {
                    terminal.Flights[flightNumber].Status = "Delayed";
                }
                else if (integer == "2")
                {
                    terminal.Flights[flightNumber].Status = "Boarding";
                }
                else if (integer == "3")
                {
                    terminal.Flights[flightNumber].Status = "Delayed";
                }

                Console.WriteLine($"Boarding gate successfully assigned to gate {boardingGateNumber}!");
                break;
            }
            else if (update == "n")
            {
                Console.WriteLine($"Boarding gate successfully assigned to gate {boardingGateNumber}!");
                break;
            }
            else
            {
                throw new Exception();
            }
        }
    }
    catch
    {
        Console.WriteLine("Invalid input. Please try again.");
    }
}


// 6) Create a new flight
void CreateFlight() //option 4
{
    string flightNumber = "";
    string origin = "";
    string destination = "";
    DateTime expectedTime = DateTime.Now;
    List<string> flightsAdded = new List<string>();

    while (true)
    {
        try
        {
            Console.WriteLine("Enter Flight Number: ");
            flightNumber = Console.ReadLine().ToUpper();
            Console.WriteLine("Enter Flight Origin: ");
            origin = Console.ReadLine();
            Console.WriteLine("Enter Flight Destination: ");
            destination = Console.ReadLine();
            Console.WriteLine("Enter Flight Expected Departure/Arrival Time: ");
            expectedTime = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Would You Like To Enter Any Additional Information? (Y/N)");
            string option = Console.ReadLine().ToLower();
            if (option == "y")
            {
                Console.WriteLine("Enter Special Request Code: ");
                string requestCode = Console.ReadLine();
                if (requestCode == "CFFT")
                {
                    terminal.Flights.Add(flightNumber, new CFFTFlight(flightNumber, origin, destination, expectedTime, "On time"));
                    
                }
                else if (requestCode == "DDJB")
                {
                    terminal.Flights.Add(flightNumber, new DDJBFlight(flightNumber, origin, destination, expectedTime, "On time"));
                }
                else if (requestCode == "LWTT")
                {
                    terminal.Flights.Add(flightNumber, new LWTTFlight(flightNumber, origin, destination, expectedTime, "On time"));
                }
                else
                {
                    throw new Exception();
                }
            }
            else if (option == "n")
            {
                terminal.Flights.Add(flightNumber, new NORMFlight(flightNumber, origin, destination, expectedTime, "On time"));
            }
            else
            {
                throw new Exception();
            }
            flightsAdded.Add(flightNumber);

            Console.WriteLine("Would You Like To Add Another Flight? (Y/N)");
            option = Console.ReadLine().ToLower();
            if (option == "y")
            {
                continue;
            }
            else if (option == "n")
            { }
            else
            {
                throw new Exception();
            }
            Console.WriteLine("The Following Flights: ");
            foreach (string flight in flightsAdded)
            {
                Console.WriteLine(flight);
            }
            Console.WriteLine("Have Been Successfully Added!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Invalid input. Please try again.");
        }
    }
}