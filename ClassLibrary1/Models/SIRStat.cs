namespace DAL;

public sealed record SIRStat(double Susceptible, double Infected, double Removed, double Day)
{
    public double Ds => Susceptible;
    public double Di => Infected;
    public double Dr => Removed;
    public double Dt => Day;
}
