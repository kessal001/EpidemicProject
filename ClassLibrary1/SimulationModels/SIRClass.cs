namespace DAL;

/// <summary>Modello epidemiologico deterministico SIR integrato con Runge-Kutta del quarto ordine.</summary>
public sealed class SIRClass
{
    public double TotalPopulation => SusceptiblePopulation + InfectedPopulation + RemovedPopulation;
    public double SusceptiblePopulation { get; set; } = 999_000;
    public double InfectedPopulation { get; set; } = 1_000;
    public double RemovedPopulation { get; set; }
    public double Beta { get; set; } = 0.30;
    public double Alpha { get; set; } = 0.10;
    public int Days { get; set; } = 160;
    public double Step { get; set; } = 0.25;
    public double BasicReproductionNumber => Alpha > 0 ? Beta / Alpha : double.PositiveInfinity;

    public SIRClass() { }

    public SIRClass(double totalPopulation, double susceptiblePopulation, double infectedPopulation,
        double removedPopulation, double beta, double alpha)
    {
        SusceptiblePopulation = susceptiblePopulation;
        InfectedPopulation = infectedPopulation;
        RemovedPopulation = removedPopulation;
        Beta = beta;
        Alpha = alpha;
    }

    public IReadOnlyList<SIRStat> Simulate()
    {
        Validate();
        var result = new List<SIRStat>(Days + 1);
        var s = SusceptiblePopulation;
        var i = InfectedPopulation;
        var r = RemovedPopulation;
        var population = TotalPopulation;
        var steps = (int)Math.Ceiling(Days / Step);
        result.Add(new(s, i, r, 0));
        var nextRecordedDay = 1;

        for (var stepIndex = 1; stepIndex <= steps; stepIndex++)
        {
            var h = Math.Min(Step, Days - (stepIndex - 1) * Step);
            (s, i, r) = Rk4(s, i, r, population, h);
            var time = Math.Min(stepIndex * Step, Days);
            if (time + 1e-9 >= nextRecordedDay || stepIndex == steps)
            {
                result.Add(new(Math.Max(0, s), Math.Max(0, i), Math.Max(0, r), time));
                nextRecordedDay++;
            }
        }
        return result;
    }

    public ReturnList AvviaSimulazione()
    {
        var points = Simulate();
        return new ReturnList
        {
            DsList = points.Select(x => x.Susceptible).ToList(),
            DiList = points.Select(x => x.Infected).ToList(),
            DrList = points.Select(x => x.Removed).ToList()
        };
    }

    private (double S, double I, double R) Rk4(double s, double i, double r, double n, double h)
    {
        static (double S, double I, double R) D(double s, double i, double n, double beta, double gamma)
        {
            var infections = beta * s * i / n;
            return (-infections, infections - gamma * i, gamma * i);
        }
        var k1 = D(s, i, n, Beta, Alpha);
        var k2 = D(s + h * k1.S / 2, i + h * k1.I / 2, n, Beta, Alpha);
        var k3 = D(s + h * k2.S / 2, i + h * k2.I / 2, n, Beta, Alpha);
        var k4 = D(s + h * k3.S, i + h * k3.I, n, Beta, Alpha);
        return (
            s + h * (k1.S + 2 * k2.S + 2 * k3.S + k4.S) / 6,
            i + h * (k1.I + 2 * k2.I + 2 * k3.I + k4.I) / 6,
            r + h * (k1.R + 2 * k2.R + 2 * k3.R + k4.R) / 6);
    }

    private void Validate()
    {
        if (SusceptiblePopulation < 0 || InfectedPopulation < 0 || RemovedPopulation < 0)
            throw new ArgumentOutOfRangeException(nameof(SusceptiblePopulation), "Le popolazioni non possono essere negative.");
        if (TotalPopulation <= 0)
            throw new InvalidOperationException("La popolazione totale deve essere maggiore di zero.");
        if (Beta < 0 || Alpha <= 0)
            throw new ArgumentOutOfRangeException(nameof(Alpha), "Beta deve essere non negativo e gamma maggiore di zero.");
        if (Days is < 1 or > 2_000)
            throw new ArgumentOutOfRangeException(nameof(Days), "La durata deve essere compresa tra 1 e 2000 giorni.");
        if (Step is <= 0 or > 1)
            throw new ArgumentOutOfRangeException(nameof(Step), "Il passo deve essere maggiore di zero e non superiore a un giorno.");
    }
}
