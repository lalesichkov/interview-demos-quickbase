namespace Backend;

public class Country
{
    public string Name { get; set; }
    public long Population { get; set; }

    public Country(string Name, long Population)
    {
        this.Name = Name;
        this.Population = Population;
    }
}