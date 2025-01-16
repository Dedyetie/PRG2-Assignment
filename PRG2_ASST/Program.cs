using PRG2_ASST;

Dictionary<string, Flight> FlightDict = new Dictionary<string, Flight>();
using (StreamReader sr = new StreamReader("flights.csv"))
{
    sr.ReadLine();
    string line = "";
    while ((line = sr.ReadLine()) != null)
    {
        
        string[] spline = line.Split(',');
        FlightDict.Add(spline[0], new );
    }
}