﻿//==========================================================
// Student Number: S10267973, S10268020
// Student Name	 : Eng Zhe Xuan, Gerel & Ong Jun Shu, Camellia
//==========================================================

namespace S10267973_PRG2Assignment
{
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight? Flight { get; set; } = null;

        public BoardingGate() { }
        public BoardingGate(string gN, bool CFFT, bool DDJB, bool LWTT)
        {
            GateName = gN;
            SupportsCFFT = CFFT;
            SupportsDDJB = DDJB;
            SupportsLWTT = LWTT;
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
            return "GateName" + GateName + "SupportsCFFT" + SupportsCFFT + "SupportsDDJB" + SupportsDDJB + "SupportsLWTT" + SupportsLWTT;
        }
    }
}

            //return $"{GateName, -15}{SupportsDDJB, -10}{SupportsCFFT, -10}{SupportsLWTT}";