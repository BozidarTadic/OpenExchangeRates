namespace OpenExchangeRates.Api.Models
{
    
        public class Rates
        {
           
            public double BAM { get; set; }
            public double GBP { get; set; }
            public double EUR { get; set; }
            public double USD { get; set; }
           
        }

        public class CurrencyDto
    {
            public string disclaimer { get; set; }
            public string license { get; set; }
            public int timestamp { get; set; }
            public string @base { get; set; }
            public Rates rates { get; set; }
        }
    
}
