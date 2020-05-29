using System;

namespace DALP
{
    public class Class1
    {
        //Numero totale della popolazione
        private double totalPopulation;
        public double TotalPopulation
        {
#warning Implementare controllo sui valori
            get { return totalPopulation; }
            set { totalPopulation = value; }
        }
        //Numero totale suscettibili
        private double susceptiblePopulation;
        public double SusceptiblePopulation
        {
#warning Implementare controllo sui valori
            get { return susceptiblePopulation; }
            set { susceptiblePopulation = value; }
        }
        //Numero totale infetti
        private double infectedPopulation;
        public double InfectedPopulation
        {
#warning Implementare controllo sui valori
            get { return infectedPopulation; }
            set { infectedPopulation = value; }
        }
        //Numero totale rimossi
        private double removedPopulation;
        public double RemovedPopulation
        {
#warning Implementare controllo sui valori
            get { return removedPopulation; }
            set { removedPopulation = value; }
        }


        public void AvviaSimulazione()
        {
            TotalPopulation = 10000.0;
            SusceptiblePopulation = 9000.0;
            InfectedPopulation = 1000.0;
            RemovedPopulation = 0.0;
            double time = 0.0;
            double beta = 2.0; //valore di diffusione
            double alpha = 0.5; //valore di rimossione
            double scale = 0.1; //valore per calcoli
            for (int i = 0; i < 100 /*giorni*/; ++i)
            {
                double dS = (-beta * SusceptiblePopulation * InfectedPopulation) / TotalPopulation;
                double dI = ((beta * SusceptiblePopulation * InfectedPopulation) / TotalPopulation) - alpha * InfectedPopulation;
                double dR = alpha * InfectedPopulation;
                double dt = 1;

                SusceptiblePopulation = SusceptiblePopulation + dS * scale;
                InfectedPopulation = InfectedPopulation + dI * scale;
                RemovedPopulation = RemovedPopulation + dR * scale;
                time = time + dt * scale;
            }
        }
    }
}
