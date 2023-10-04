namespace ABPTestTask.Models;

public class Experiments
{
    public IEnumerable<ColorExperiment> ColorExperiments { get; set; }
    public IEnumerable<PriceExperiment> PriceExperiments { get; set; }
}