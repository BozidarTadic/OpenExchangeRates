using System;
using System.Collections.Generic;

namespace OpenExchangeRates.Api.Data
{
    public partial class OpenGbr
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }
}
