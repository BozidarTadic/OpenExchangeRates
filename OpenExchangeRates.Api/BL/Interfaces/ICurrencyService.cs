using OpenExchangeRates.Api.Models;

namespace OpenExchangeRates.Api.BL.Interfaces
{
    public interface ICurrencyService
    {
        public ResultDto GetAvg ();    
    }
}
