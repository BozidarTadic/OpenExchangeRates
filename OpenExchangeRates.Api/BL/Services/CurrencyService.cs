using OpenExchangeRates.Api.BL.Interfaces;
using OpenExchangeRates.Api.Data;
using OpenExchangeRates.Api.Models;

namespace OpenExchangeRates.Api.BL.Services
{
    public class CurrencyService : ICurrencyService
    {
        private string AdppId = "769ecacefa2141d69a2212a60b60a395";
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;
        
        public CurrencyService(HttpClient httpClient,ApplicationDbContext context)
        {

            _httpClient = httpClient;
            _context = context;
        }
        public ResultDto GetAvg()
        {
            ResultDto result = new ResultDto();
            CompareAverageDto eur = new CompareAverageDto();
            CompareAverageDto gbr = new CompareAverageDto();


            List<double> nbsEurList = GetListRate("NbsEur").ToList();
            List<double> nbsGbrList = GetListRate("NbsGbr");
            List<double> openEurList = GetListRate("OpenEur");
            List<double> openGbrList = GetListRate("OpenGbr");

            result.EUR.NBSAvg.MovingAvg = GetMoveAvg(nbsEurList);
            result.EUR.OpenAvg.MovingAvg = GetMoveAvg(openEurList);
            result.EUR.Diference.MovingAvg = result.EUR.NBSAvg.MovingAvg - result.EUR.OpenAvg.MovingAvg;


            result.GBR.NBSAvg.MovingAvg = GetMoveAvg(nbsGbrList);
            result.GBR.OpenAvg.MovingAvg =  GetMoveAvg(openGbrList);
            result.GBR.Diference.MovingAvg = result.GBR.NBSAvg.MovingAvg - result.GBR.OpenAvg.MovingAvg;

            result.GBR.NBSAvg.ExpMovingAvg = 0;
            result.GBR.OpenAvg.ExpMovingAvg = 0;
            result.GBR.Diference.ExpMovingAvg = result.GBR.NBSAvg.ExpMovingAvg - result.GBR.OpenAvg.ExpMovingAvg;

            result.EUR.NBSAvg.ExpMovingAvg = 0;
            result.EUR.OpenAvg.ExpMovingAvg = 0;
            result.EUR.Diference.ExpMovingAvg = result.EUR.NBSAvg.ExpMovingAvg - result.EUR.OpenAvg.ExpMovingAvg;


            return result;
        }
        public double GetNbsRate(string code, string date)
        {
            var urlUsd = "https://kurs.resenje.org/api/v1/currencies/Usd/rates/"+date;
            var url = "https://kurs.resenje.org/api/v1/currencies/"+code+"/rates/"+date;

            var usd = _httpClient.GetAsync(urlUsd).Result.Content.ReadFromJsonAsync<NBSDto>();
            var eur = _httpClient.GetAsync(url).Result.Content.ReadFromJsonAsync<NBSDto>();
            if(eur.Result.exchange_middle == 0) { return 0; }

            var result = usd.Result.exchange_middle / eur.Result.exchange_middle;


            return result;
        }
        public double GetOpenRate(string code, string date)
        {
            var url = _httpClient.BaseAddress + "historical/"+date+".json?app_id="+AdppId;

            var open = _httpClient.GetAsync(url).Result.Content.ReadFromJsonAsync<CurrencyDto>();

            if(code.ToUpper() == "EUR")
            {
                return open.Result.rates.EUR;
            }
            else 
            {
                return open.Result.rates.GBP;
            }

        }
        public double getRateFromDb(string code,DateTime date)
        {
            double result = new double();
            

            if (code == "NbsEur")
            {
                try
                {
                    result = _context.NbsEurs.Where(n => n.Date == date).First().Value;
                }
                catch (Exception)
                {

                    result = GetNbsRate("eur", date.ToString("yyyy-MM-dd"));
                    NbsEur nbsEur = new NbsEur();
                    nbsEur.Value = result;
                    nbsEur.Date = date;
                    _context.Add(nbsEur);
                    _context.SaveChanges();
                }
                
                
            }
            else if(code == "NbsGbr")
            {
                try
                {
                    result = _context.BbsGbrs.Where(n => n.Date == date).First().Value;
                }
                catch (Exception)
                {

                    result = GetNbsRate("gbr", date.ToString("yyyy-MM-dd"));
                    BbsGbr nbsGrb = new BbsGbr();
                    nbsGrb.Value = result;
                    nbsGrb.Date = date;
                    _context.Add(nbsGrb);
                    _context.SaveChanges();
                }
               
            }
            else if(code == "OpenEur")
            {
                try
                {
                    result = _context.OpenEurs.Where(n => n.Date == date).First().Value;
                }
                catch (Exception)
                {

                    result = GetOpenRate("eur", date.ToString("yyyy-MM-dd"));
                    OpenEur openEur= new OpenEur();
                    openEur.Value = result;
                    openEur.Date = date;
                    _context.Add(openEur);
                    _context.SaveChanges();
                }
                
            }
            else
            {
                try
                {
                    result = _context.OpenGbrs.Where(n => n.Date == date).First().Value;
                }
                catch (Exception)
                {

                    result = GetOpenRate("gbr", date.ToString("yyyy-MM-dd"));
                    OpenGbr openGbr = new OpenGbr();
                    openGbr.Value = result;
                    openGbr.Date = date;
                    _context.Add(openGbr);
                    _context.SaveChanges();
                }
                
            }
            
            
            return result;
        }
        public List<double> GetListRate(string code)
        {
            List<double> result = new List<double>();
            DateTime date = DateTime.Today;

            for (int i=1; i<=7; i++)
            {
                date = date.AddDays(-1);
                result.Add(getRateFromDb(code,date));
            }

            return result;
        }
        public double GetMoveAvg(List<double> vs)
        {
            double avg = new double();
            foreach(double v in vs)
            {
                avg += v;
            }
            avg /= vs.Count;
            return avg;
        }
        public double GetExpAvg(List<double> vs)
        {
            double result = new double();

            foreach(var v in vs)
            {

            }
            

            return result;
        }
    }
}
