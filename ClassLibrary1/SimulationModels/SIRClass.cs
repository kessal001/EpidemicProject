using System;
using System.Collections.Generic;

namespace DAL
{
    public class SIRClass
    {
        #region variabili
        /// <summary>
        /// Valore per calcoli
        /// </summary>
        public double Scale { get; private set; } = 0.1;
        /// <summary>
        /// Numero totale della popolazione
        /// </summary>
        private double totalPopulation;
        public double TotalPopulation
        {
#warning Implementare controllo sui valori
            get { return totalPopulation; }
            set { totalPopulation = value; }
        }
        /// <summary>
        /// Numero totale suscettibili
        /// </summary>
        private double susceptiblePopulation;
        public double SusceptiblePopulation
        {
#warning Implementare controllo sui valori
            get { return susceptiblePopulation; }
            set { susceptiblePopulation = value; }
        }
        /// <summary>
        /// Numero totale della infetti
        /// </summary>
        private double infectedPopulation;
        public double InfectedPopulation
        {
#warning Implementare controllo sui valori
            get { return infectedPopulation; }
            set { infectedPopulation = value; }
        }
        /// <summary>
        /// Numero totale della rimossi
        /// </summary>
        private double removedPopulation;
        public double RemovedPopulation
        {
#warning Implementare controllo sui valori
            get { return removedPopulation; }
            set { removedPopulation = value; }
        }
        /// <summary>
        /// Properties per settare il tempo
        /// </summary>
        private double time = 0.0;
        public double Time
        {
            get { return time; }
            set { time = value; }
        }
        /// <summary>
        /// Valore di diffusione della malattia
        /// </summary>
        private double beta = 2.0;
        public double Beta
        {
            get { return beta; }
            set { beta = value; }
        }
        private double alpha = 0.5;

        public double Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }


        List<double> dsList = new List<double>();
        List<double> diList = new List<double>();
        List<double> drList = new List<double>();
        #endregion      
        public SIRClass(double totalPopulation, double susceptiblePopulation, double infectedPopulation, double removedPopulation, double beta, double alpha)
        {
            TotalPopulation = totalPopulation;
            SusceptiblePopulation = susceptiblePopulation;
            InfectedPopulation = infectedPopulation;
            RemovedPopulation = removedPopulation;
            Beta = beta;
            Alpha = alpha;
        }
        public SIRClass()
        {

        }

        public ReturnList AvviaSimulazione()
        {
            for (int i = 0; i < 100; ++i)
            {
                dsList.Add(susceptiblePopulation);
                diList.Add(infectedPopulation);
                drList.Add(removedPopulation);
                double dS = (-Beta * SusceptiblePopulation * InfectedPopulation) / TotalPopulation;
                double dI = ((Beta * SusceptiblePopulation * InfectedPopulation) / TotalPopulation) - Alpha * InfectedPopulation;
                double dR = Alpha * InfectedPopulation;
                double dt = 1;

                SusceptiblePopulation = SusceptiblePopulation + dS * Scale;
                InfectedPopulation = InfectedPopulation + dI * Scale;
                RemovedPopulation = RemovedPopulation + dR * Scale;
                Time = Time + dt * Scale;
            }
            return new ReturnList() { DsList = dsList, DiList = diList, DrList = drList };
        }
    }
}
