namespace PRG2_ASST
{
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        public BoardingGate() { }
        public BoardingGate(string gN, bool CFFT, bool DDJB, bool LWTT, Flight f)
        {
            GateName = gN;
            SupportsCFFT = CFFT;
            SupportsDDJB = DDJB;
            SupportsLWTT = LWTT;
            Flight = f;
        }

        public double CalculateFees()
        {
            double fees = 0;

            if (SupportsCFFT == true)
            {
                fees += 150;
            }
            else if (SupportsDDJB == true)
            {
                fees += 300;
            }
            else if (SupportsLWTT == true)
            {
                fees += 500;
            }

            return fees;
        }

        public override string ToString()
        {
            return $"{GateName, -15}{SupportsDDJB, -10}{SupportsCFFT, -10}{SupportsLWTT}";
        }
    }
}
