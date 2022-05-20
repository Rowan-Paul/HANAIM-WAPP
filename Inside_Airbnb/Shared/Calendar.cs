using System;
using System.Collections.Generic;

namespace Inside_Airbnb.Server
{
    public partial class Calendar
    {
        public long? ListingId { get; set; }
        public DateTime? Date { get; set; }
        public string? Available { get; set; }
        public int? Price { get; set; }
        public int? AdjustedPrice { get; set; }
        public int? MinimumNights { get; set; }
        public int? MaximumNights { get; set; }
    }
}
