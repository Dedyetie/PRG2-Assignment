﻿using S10267973_PRG2Assignment;
using System.Linq.Expressions;

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
        BoardingGate boardingGate = new BoardingGate(info[0], Convert.ToBoolean(info[2]), Convert.ToBoolean(info[1]), Convert.ToBoolean(info[3]));
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

    foreach (Flight flight in terminal.Flights.Values)
    {
        Airline airline = terminal.GetAirlineFromFlight(flight);
        airline.AddFlight(flight);
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
        ListBoardingGates();
        Console.WriteLine("");
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
        DisplayAirlineFlights();
        Console.WriteLine("");
    }
    else if (opt == 6)
    {
        ModifyFlightDetails();
        Console.WriteLine("");
    }
    else if (opt == 7)
    {
        DisplayFlightSchedule();
        Console.WriteLine("");
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


// 3) List all flights with basic information (option 1)
void DisplayFlight()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");

    Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-21}{"Origin",-21}{"Destination",-21}{"Expected Departure/Arrival Time"}");
    foreach (Flight flight in terminal.Flights.Values)
    {
        foreach (Airline airline in terminal.Airlines.Values)
        {
            if (airline.Flights.ContainsKey(flight.FlightNumber) == true)
            {
                Console.WriteLine($"{flight.FlightNumber,-16}{airline.Name,-21}{flight.Origin,-21}{flight.Destination,-21}{flight.ExpectedTime}");
            }
        }
    }
}


// 4) List all boarding gates (option 2)
void ListBoardingGates()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");

    Console.WriteLine($"{"Gate Name",-15}{"DDJB",-20}{"CFFT",-20}{"LWTT"}");
    foreach (BoardingGate boardingGate in terminal.BoardingGates.Values)
    {
        Console.WriteLine($"{boardingGate.GateName,-15}{boardingGate.SupportsDDJB,-20}{boardingGate.SupportsCFFT,-20}{boardingGate.SupportsLWTT}");
    }
}


// 5) Assign a boarding gate to a flight (option 3)
void BoardGateAssignment()
{
    string flightNumber;
    string boardingGateNumber;
    Console.WriteLine("=============================================");
    Console.WriteLine("Assign a Boarding Gate to a Flight");
    Console.WriteLine("=============================================");

    // Collects flight flight number and boarding gate number
    while (true)
    {
        try
        {
            Console.WriteLine("Enter Flight Number: ");
            flightNumber = Console.ReadLine().ToUpper();
            if (terminal.Flights.ContainsKey(flightNumber))
            {
                break;
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Invalid input or flight details. Please try again.");
        }
    }

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
    Console.WriteLine("Flight Number: " + flightNumber);
    Console.WriteLine("Origin: " + terminal.Flights[flightNumber].Origin);
    Console.WriteLine("Destination: " + terminal.Flights[flightNumber].Destination);
    Console.WriteLine("Expected Time: " + terminal.Flights[flightNumber].ExpectedTime);
    Console.WriteLine("Special Request Code: " + specialRequest);
    Console.WriteLine();

    while (true)
    {
        try
        {
            Console.WriteLine("Enter Boarding Gate Name: ");
            boardingGateNumber = Console.ReadLine().ToUpper();
            if (terminal.BoardingGates.ContainsKey(boardingGateNumber))
            {
                // checks if the boarding gate is not assigned to any flight OR if it is assigned to its ownself
                if ((terminal.BoardingGates[boardingGateNumber].Flight == null) || (terminal.BoardingGates[boardingGateNumber].Flight != null) && (terminal.BoardingGates[boardingGateNumber].Flight.FlightNumber == flightNumber))
                {
                    // help to double check this please fiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjw
                    // checks if the gate does supports the special request
                    if ((terminal.BoardingGates[boardingGateNumber].SupportsCFFT = false) && (terminal.Flights[flightNumber] is CFFTFlight))
                    {
                        Console.WriteLine($"The Gate {boardingGateNumber} does not support Special Request Code CFFT. Please choose another Boarding Gate.");
                        continue;
                    }
                    else if ((terminal.BoardingGates[boardingGateNumber].SupportsDDJB = false) && (terminal.Flights[flightNumber] is DDJBFlight))
                    {
                        Console.WriteLine($"The Gate {boardingGateNumber} does not support Special Request Code DDJB. Please choose another Boarding Gate.");
                        continue;
                    }
                    else if ((terminal.BoardingGates[boardingGateNumber].SupportsLWTT = false) && (terminal.Flights[flightNumber] is LWTTFlight))
                    {
                        Console.WriteLine($"The Gate {boardingGateNumber} does not support Special Request Code LWTT. Please choose another Boarding Gate.");
                        continue;
                    }
                    else
                    {
                        foreach (BoardingGate gate in terminal.BoardingGates.Values)
                        {
                            if (gate.Flight.FlightNumber == flightNumber)
                            {
                                gate.Flight = null;
                                break;
                            }
                        }
                        terminal.BoardingGates[boardingGateNumber].Flight = terminal.Flights[flightNumber];
                        break;
                    }
                }
                // checks if the boarding gate is already assigned to another flight
                else if ((terminal.BoardingGates[boardingGateNumber] != null) && (terminal.BoardingGates[boardingGateNumber].Flight.FlightNumber != flightNumber))
                {
                    Console.WriteLine($"Boarding Gate {boardingGateNumber} is already assigned to Flight {terminal.BoardingGates[boardingGateNumber].Flight.FlightNumber}. Please choose another Boarding Gate.\n");
                }
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Invalid input or gate details. Please try again.");
        }
    }

    // Displays the information of the flight number
    Console.WriteLine("Flight Number: " + flightNumber);
    Console.WriteLine("Origin: " + terminal.Flights[flightNumber].Origin);
    Console.WriteLine("Destination: " + terminal.Flights[flightNumber].Destination);
    Console.WriteLine("Expected Time: " + terminal.Flights[flightNumber].ExpectedTime);
    Console.WriteLine("Special Request Code: " + specialRequest);
    Console.WriteLine("Supports DDJB: " + terminal.BoardingGates[boardingGateNumber].SupportsDDJB);
    Console.WriteLine("Supports CFFT: " + terminal.BoardingGates[boardingGateNumber].SupportsCFFT);
    Console.WriteLine("Supports LWTT: " + terminal.BoardingGates[boardingGateNumber].SupportsLWTT);

    while (true)
    {
        try
        {
            Console.WriteLine("\nWould you like to update the status of the flight? (Y/N)");
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
                    terminal.Flights[flightNumber].Status = "On Time";
                }
                else
                {
                    throw new Exception();
                }
            }
            else if (update == "n")
            { }
            else
            {
                throw new Exception();
            }

            Console.WriteLine($"Flight {flightNumber} has been assigned to Boarding Gate {boardingGateNumber}!");
            break;
        }
        catch
        {
            Console.WriteLine("Invalid input. Please try again.");
        }
    }
}


// 6) Create a new flight (option 4)
void CreateFlight()
{
    string flightNumber = "";
    string origin = "";
    string destination = "";
    DateTime expectedTime = DateTime.Now;

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

            Console.WriteLine("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
            string requestCode = Console.ReadLine().ToUpper();
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
            else if (requestCode == "NONE")
            {
            terminal.Flights.Add(flightNumber, new NORMFlight(flightNumber, origin, destination, expectedTime, "On time"));
            }
            Console.WriteLine($"Flight {flightNumber} has been added!");

            Console.WriteLine("Would You Like To Add Another Flight? (Y/N)");
            string option = Console.ReadLine().ToLower();
            if (option == "y")
            {
                continue;
            }
            else if (option == "n")
            {
                break;
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Invalid input. Please try again.");
        }
    }
}


// 7) Display full flight details from an airline (option 5)
void DisplayAirlineFlights()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");

    Console.WriteLine($"{"Airline Code",-15}{"Airline Name"}");
    foreach (Airline airline in terminal.Airlines.Values)
    {
        Console.WriteLine($"{airline.Code,-15}{airline.Name}");
    }

    string code;
    while (true)
    {
        try
        {
            Console.Write("Enter Airline Code: ");
            code = Console.ReadLine().ToUpper();
            if (terminal.Airlines.ContainsKey(code) == true)
            {
                break;
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Invalid input. Please try again.");
            continue;
        }
    }

    foreach (KeyValuePair<string, Airline> kvp in terminal.Airlines)
    {
        if (code == kvp.Key)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine($"List of Flights for {kvp.Value.Name}");
            Console.WriteLine("=============================================");

            Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-20}{"Origin",-20}{"Destination",-20}{"Expected Departure/Arrival Time"}");
            foreach (Flight flight in kvp.Value.Flights.Values)
            {
                Console.WriteLine($"{flight.FlightNumber,-16}{kvp.Value.Name,-20}{flight.Origin,-20}{flight.Destination,-20}{flight.ExpectedTime}");
            }
        }
    }
}


// 8) Modify flight details (option 6)

void ModifyFlightDetails()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");

    Console.WriteLine($"{"Airline Code",-15}{"Airline Name"}");
    foreach (Airline airline in terminal.Airlines.Values)
    {
        Console.WriteLine($"{airline.Code,-15}{airline.Name}");
    }

    string code;
    Airline airline1;
    while (true)
    {
        try
        {
            Console.WriteLine("Enter Airline Code: ");
            code = Console.ReadLine().ToUpper();
            if (terminal.Airlines.ContainsKey(code) == true)
            {
                airline1 = terminal.Airlines[code];
                break;
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid input. Please try again.");
            continue;
        }
    }

    Console.WriteLine("");
    Console.WriteLine($"List of Flights for {airline1.Name}");
    Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-20}{"Origin",-20}{"Destination",-20}{"Expected Departure/Arrival Time"}");
    foreach (Flight flight in airline1.Flights.Values)
    {
        Console.WriteLine($"{flight.FlightNumber,-16}{airline1.Name,-20}{flight.Origin,-20}{flight.Destination,-20}{flight.ExpectedTime}");
    }

    string fNo;
    Flight flight1;
    while (true)
    {
        try
        {
            Console.WriteLine("Choose an existing Flight to modify or delete:");
            fNo = Console.ReadLine().ToUpper();

            if (airline1.Flights.ContainsKey(fNo) == true)
            {
                flight1 = airline1.Flights[fNo];
                break;
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid input. Please try again.");
            continue;
        }
    }

    Console.WriteLine("");
    Console.WriteLine("1. Modify Flight");
    Console.WriteLine("2. Delete Flight");
    int opt;

    while (true)
    {
        try
        {
            Console.WriteLine("Choose an option:");
            opt = Convert.ToInt32(Console.ReadLine());

            if (opt == 1 || opt == 2)
            {
                break;
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid input. Please try again.");
            continue;
        }
    }

    if (opt == 1)
    {
        Console.WriteLine("");
        Console.WriteLine("1. Modify Basic Information");
        Console.WriteLine("2. Modify Status");
        Console.WriteLine("3. Modify Special Request Code");
        Console.WriteLine("4. Modify Boarding Gate");
        int opt2;

        while (true)
        {
            try
            {
                Console.WriteLine("Choose an option:");
                opt2 = Convert.ToInt32(Console.ReadLine());

                if (1 >= opt2 || 4 >= opt2)
                {
                    break;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input. Please try again.");
                continue;
            }
        }

        string newStatus;
        string newOrigin;
        string newBoardingGate;
        if (opt2 == 1)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter new Origin: ");
                    newOrigin = Console.ReadLine();
                    flight1.Origin = newOrigin;

                    Console.Write("Enter new Destination: ");
                    string newDest = Console.ReadLine();
                    flight1.Destination = newDest;

                    Console.Write("Enter new Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
                    string newExpected = Console.ReadLine();
                    flight1.ExpectedTime = Convert.ToDateTime(newExpected);

                    Console.WriteLine("Flight updated!");
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }
            }
        }
        else if (opt2 == 2)
        {
            //feel free to change it back idk how she wants it to be but based on one of my earlier tasks they asked it dis way sooooooooo now to get the attemtion fiwnifhwu9gfhwigfbuiwehgowebnjigwigbhiwgjiwnjgwjgbnjiwrngfjiwbngjiwngjijnggjnwkfewnjifnewjofnewjfnewjofowebwrigbifnjiwenfjiewnjifnewjifbniwefbwifbweifbiewbfhebhfjebhefwbfweijbnfjiewfjghrighrhghhirghiwrhgihighwrighijwrghjiewgjijwhigbhiwgbhiewbfhibewrhfbewhjifnjiwefnjiewebjiewbngjibheibnijefbjiwebfwhuinecgrhuiorethunioecsgtrnhuoecgrhunioecwtnhuiocewtnihuecwthuniecwgrhuincetwhuicrewghuirecwg

            //while (true)
            //{
            //    try
            //    {
            //        Console.Write("Enter new Status:");
            //        newStatus = Console.ReadLine();
            //        flight1.Status = newStatus;

            //        Console.WriteLine("Flight updated!");
            //        break;
            //    }
            //    catch (Exception)
            //    {
            //        Console.WriteLine("Invalid input. Please try again.");
            //        continue;
            //    }
            //}
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter New Status:");
                    Console.WriteLine("1. Delayed \n2. Boarding\n3. On Time\n4. Scheduled");
                    Console.WriteLine("Please select the new status of the flight:");
                    opt = Convert.ToInt32(Console.ReadLine());

                    // Updates the status of the flight to any of the above 3 mentioned
                    if (opt == 1)
                    {
                        flight1.Status = "Delayed";
                        Console.WriteLine("Flight status has been updated to Delayed!");
                        break;
                    }
                    else if (opt == 2)
                    {
                        flight1.Status = "Boarding";
                        Console.WriteLine("Flight status has been updated to Boarding!");
                        break;
                    }
                    else if (opt == 3)
                    {
                        flight1.Status = "On Time";
                        Console.WriteLine("Flight status has been updated to On Time!");
                        break;
                    }
                    else if (opt == 4)
                    {
                        flight1.Status = "Scheduled";
                        Console.WriteLine("Flight status has been updated to Scheduled!");
                        break;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

        else if (opt2 == 3)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter new Special Request Code: (CFFT/DDJB/LWTT/None)");
                    string newSpecialRequestCode = Console.ReadLine().ToUpper();

                    if (newSpecialRequestCode == "LWTT")
                    {
                        terminal.Flights[flight1.FlightNumber] = new LWTTFlight(flight1.FlightNumber, flight1.Origin, flight1.Destination, flight1.ExpectedTime, flight1.Status);
                    }
                    else if (newSpecialRequestCode == "CFFT")
                    {
                        terminal.Flights[flight1.FlightNumber] = new CFFTFlight(flight1.FlightNumber, flight1.Origin, flight1.Destination, flight1.ExpectedTime, flight1.Status);
                    }
                    else if (newSpecialRequestCode == "DDJB")
                    {
                        terminal.Flights[flight1.FlightNumber] = new DDJBFlight(flight1.FlightNumber, flight1.Origin, flight1.Destination, flight1.ExpectedTime, flight1.Status);
                    }
                    else if (newSpecialRequestCode == "NONE")
                    {
                        terminal.Flights[flight1.FlightNumber] = new NORMFlight(flight1.FlightNumber, flight1.Origin, flight1.Destination, flight1.ExpectedTime, flight1.Status);
                    }
                    else
                    {
                        throw new Exception();
                    }
                    Console.WriteLine("Flight updated!");
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }
            }
        }

        else if (opt2 == 4)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter new Boarding Gate: ");
                    newBoardingGate = Console.ReadLine().ToUpper();

                    if (terminal.BoardingGates.ContainsKey(newBoardingGate))
                    {
                        // checks if the boarding gate is not assigned to any flight
                        if (terminal.BoardingGates[newBoardingGate].Flight == null)
                        {
                            // help to double check this please fiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjwfiwefnuiwebfuiwnfbwenifbnewuifbiefbweijefjw
                            // checks if the gate does supports the special request
                            if ((terminal.BoardingGates[newBoardingGate].SupportsCFFT == false) && (flight1 is CFFTFlight))
                            {
                                Console.WriteLine($"The Gate {newBoardingGate} does not support Special Request Code CFFT. Please choose another Boarding Gate.");
                                continue;
                            }
                            else if ((terminal.BoardingGates[newBoardingGate].SupportsDDJB == false) && (flight1 is DDJBFlight))
                            {
                                Console.WriteLine($"The Gate {newBoardingGate} does not support Special Request Code DDJB. Please choose another Boarding Gate.");
                                continue;
                            }
                            else if ((terminal.BoardingGates[newBoardingGate].SupportsLWTT == false) && (flight1 is LWTTFlight))
                            {
                                Console.WriteLine($"The Gate {newBoardingGate} does not support Special Request Code LWTT. Please choose another Boarding Gate.");
                                continue;
                            }
                            else
                            {
                                // removes the boarding gate currently assigned to the flight
                                foreach (BoardingGate gate in terminal.BoardingGates.Values)
                                {
                                    if (gate.Flight == flight1)
                                    {
                                        gate.Flight = null;
                                        break;
                                    }
                                }
                                terminal.BoardingGates[newBoardingGate].Flight = terminal.Flights[flight1.FlightNumber];
                            }

                        }
                        // checks if the boarding gate is already assigned to another flight
                        else if ((terminal.BoardingGates[newBoardingGate] != null) && (terminal.BoardingGates[newBoardingGate].Flight.FlightNumber != flight1.FlightNumber))
                        {
                            Console.WriteLine($"Boarding Gate {newBoardingGate} is already assigned to Flight {terminal.BoardingGates[newBoardingGate].Flight.FlightNumber}. Please choose another Boarding Gate.\n");
                            continue;
                        }

                        else
                        {
                            throw new Exception();
                        }
                        Console.WriteLine("Flight updated!");
                        break;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }
            }
        }

        Console.WriteLine("");
        Console.WriteLine($"Flight Number: {fNo}");
        Console.WriteLine($"Airline Name: {airline1.Name}");
        Console.WriteLine($"Origin: {flight1.Origin}");
        Console.WriteLine($"Destination: {flight1.Destination}");
        Console.WriteLine($"Expected Departure/Arrival Time: {flight1.ExpectedTime}");
        Console.WriteLine($"Status: {flight1.Status}");

        if (terminal.Flights[flight1.FlightNumber] is CFFTFlight)
        {
            Console.WriteLine($"Special Request Code: CFFT");
        }
        else if (terminal.Flights[flight1.FlightNumber] is DDJBFlight)
        {
            Console.WriteLine($"Special Request Code: DDJB");
        }
        else if (terminal.Flights[flight1.FlightNumber] is LWTTFlight)
        {
            Console.WriteLine($"Special Request Code: LWTT");
        }
        else
        {
            Console.WriteLine($"Special Request Code: NORM");
        }

        string boardingGate = "Unassigned";
        foreach (BoardingGate Gate in terminal.BoardingGates.Values)
        {
            if ((Gate.Flight != null) && (Gate.Flight.FlightNumber == fNo))
            {
                boardingGate = Gate.GateName;
            }
        }
        Console.WriteLine($"Boarding Gate: {boardingGate}");
    }

    else if (opt == 2)
    {
        string opt2;
        while (true)
        {
            try
            {
                Console.WriteLine("Are you sure you want to delete this flight? (Y/N)");
                opt2 = Console.ReadLine().ToLower();
                if (opt2 == "y")
                {
                    terminal.Flights.Remove(fNo);
                    Console.WriteLine($"Flight {fNo} was successfully deleted.");
                    break;
                }
                else if (opt2 == "n")
                {
                    Console.WriteLine($"Flight {fNo} was not deleted.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }
}


// 9) Display scheduled flights in chronological order, with boarding gates assignments where applicable (option 7)
void DisplayFlightSchedule()
{
    List<Flight> flightList = new List<Flight>();
    // adds all flights to a list to sort them out
    foreach (KeyValuePair<string, Flight> kvp in terminal.Flights)
    {
        flightList.Add(kvp.Value);
    }
    flightList.Sort();

    Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-21}{"Origin",-21}{"Destination",-21}{"Expected Departure/Arrival Time",-34}{"Status",-11}{"Special Request Code", -23}{"Boarding Gate"}");
    foreach (Flight flight in flightList)
    {
        string boardingGate = "Unassigned";
        string specialRequest = "None";
        
        foreach (BoardingGate Gate in terminal.BoardingGates.Values)
        {
            if ((Gate.Flight != null) && (Gate.Flight.FlightNumber == flight.FlightNumber))
            {
                boardingGate = Gate.GateName;
            }
        }

        if (flight is CFFTFlight)
        {
            specialRequest = "CFFT";
        }
        else if (flight is DDJBFlight)
        {
            specialRequest = "DDJB";
        }
        else if (flight is LWTTFlight)
        {
            specialRequest = "LWTT";
        }

        foreach (Airline airline in terminal.Airlines.Values)
        {
            if (airline.Flights.ContainsKey(flight.FlightNumber) == true)
            {
                Console.WriteLine($"{flight.FlightNumber,-16}{airline.Name,-21}{flight.Origin,-21}{flight.Destination,-21}{flight.ExpectedTime,-34}{flight.Status,-11}{specialRequest, -23}{boardingGate}");
            }
        }
    }
}

// a) Process all unassigned flights to boarding gates in bulk
void ProcessUnassignedFlights()
{
    Queue<Flight> flights = new Queue<Flight>();
    List<BoardingGate> boardingGate = new List<BoardingGate>();

    int numberOfFlights1 = 0;
    bool foundflight = false;
    foreach (Flight flight in terminal.Flights.Values)
    {
        foreach (BoardingGate gate in terminal.BoardingGates.Values)
        {
            if (flight == gate.Flight)
            {
                foundflight = true;
                numberOfFlights1++;
                break;
            }
            else
            {
                continue;
            }
        }
        if (foundflight == false)
        {
            flights.Enqueue(flight);
        }
    }
    int numberOfFlights = flights.Count;
    Console.WriteLine($"Total number of Flights that do not have any Boarding Gate assigned yet: {numberOfFlights}");

    int numberOfBoardingGate1 = 0;
    foreach (BoardingGate gate in terminal.BoardingGates.Values)
    {
        if (gate.Flight == null)
        {
            boardingGate.Add(gate);
        }
        else
        {
            numberOfBoardingGate1++;
            continue;
        }
    }
    int numberOfBoardingGate = boardingGate.Count;
    Console.WriteLine($"Total number of Boarding Gates that do not have a Flight Number assigned yet: {numberOfBoardingGate}");

    int total = 0;
    foreach (Flight f in flights)
    {
        if (f is LWTTFlight)
        {
            foreach (BoardingGate gate in boardingGate)
            {
                if (gate.SupportsLWTT == true)
                {
                    gate.Flight = f;
                    total++;
                    boardingGate.Remove(gate);
                }
            }
        }
        else if (f is CFFTFlight)
        {
            foreach (BoardingGate gate in boardingGate)
            {
                if (gate.SupportsCFFT == true)
                {
                    gate.Flight = f;
                    total++;
                    boardingGate.Remove(gate);
                }
            }
        }
        else if (f is DDJBFlight)
        {
            foreach (BoardingGate gate in boardingGate)
            {
                if (gate.SupportsDDJB == true)
                {
                    gate.Flight = f;
                    total++;
                    boardingGate.Remove(gate);
                }
            }
        }
        flights.Dequeue();
    }

    Console.WriteLine($"Total number of Flights and Boarding Gates processed and assigned: {total * 2} ({((total * 2) / (numberOfFlights1 + numberOfBoardingGate1)) / 100}%)");
}


// Bonus task b) Display the total fee per airline for the day
void AirLineFee()
{
    List<Flight> flights = new List<Flight>();
    foreach (Flight flight in terminal.Flights.Values)
    {
        flights.Add(flight);
    }

    // Checks if each flight has been assigned a boarding gate
    foreach (BoardingGate gate in terminal.BoardingGates.Values)
    {
        if (flights.Contains(gate.Flight))
        {
            flights.Remove(gate.Flight);
        }
    }

    if (flights != null)
    {
        Console.WriteLine("Please ensure that these Flights are assigned a Boarding Gate before running this feature again:");
        foreach (Flight flight in flights)
        {
            Console.WriteLine(flight.FlightNumber);
        }
    }

    else
    {
        double baseTotal = 0;
        double discounts = 0;
        double finalTotal = 0;

        Console.WriteLine("=============================================");
        Console.WriteLine("Fees For Each Airline");
        Console.WriteLine("=============================================\n");

        Console.WriteLine($"{"Airline",-21}{"Original Subtotal ($)",-23}{"Total Discount ($)",-20}{"Final Fee ($)"}");

        foreach (Airline airline in terminal.Airlines.Values)
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

            double netFee = (baseFee * percentageDiscount) + flightx3 + nineToEleven + dubaiBangkokTokyo + noSpecialRequest;

            baseTotal += baseFee;
            discounts += flightx3 + nineToEleven + dubaiBangkokTokyo + noSpecialRequest;
            finalTotal += netFee;

            Console.WriteLine($"{airline.Name,-21}{baseFee,-23:F2}{(netFee - baseFee),-20:F2}{netFee:F2}");
        }

        Console.WriteLine("\n=============================================");
        Console.WriteLine("Totals For All Airlines");
        Console.WriteLine("=============================================\n");

        Console.WriteLine($"Subtotal of all Airline fees: ${baseTotal:F2}");
        Console.WriteLine($"Subtotal of all Airline discounts to be Deducted: ${discounts:F2}");
        Console.WriteLine($"Airline fees to be collected: ${finalTotal:F2}");
        Console.WriteLine($"Percentage of discounts over Subtotal of all Airline fees: {(discounts / baseTotal * 100):F2}%");
    }
}

