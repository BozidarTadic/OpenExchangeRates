namespace OpenExchangeRates.Api.Models
{
    public class ResultDto
    {
        public CompareAverageDto EUR { get; set; }=new CompareAverageDto();
        public CompareAverageDto GBR { get; set; }= new CompareAverageDto();


        public ResultDto() { }
    }
}
