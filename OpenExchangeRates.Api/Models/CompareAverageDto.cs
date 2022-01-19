namespace OpenExchangeRates.Api.Models
{
    public class CompareAverageDto
    {
        public AverageDto OpenAvg { get; set; } = new AverageDto();
        public AverageDto NBSAvg { get; set; } = new AverageDto();
        public AverageDto Diference { get; set; } = new AverageDto();

        public CompareAverageDto() { }
    }
}
