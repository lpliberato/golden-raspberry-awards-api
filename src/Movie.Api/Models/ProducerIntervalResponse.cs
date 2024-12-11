using Api.Models;

public class ProducerIntervalResponse
{
    public ProducerInterval[] Min { get; set; }
    public ProducerInterval[] Max { get; set; }

    public ProducerIntervalResponse(ProducerInterval[] min, ProducerInterval[] max)
    {
        Min = min;
        Max = max;
    }
}