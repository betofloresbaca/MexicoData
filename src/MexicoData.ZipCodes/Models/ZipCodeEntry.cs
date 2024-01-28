namespace MexicoData.ZipCodes.Models
{
    public class ZipCodeEntry
    {
        public string ZipCode { get; set; }

        public Settlement Settlement { get; set; }

        public Municipality Municipality { get; set; }

        public City City { get; set; }

        public State State { get; set; }

        public ZipCodeEntrySummary GetSummary()
        {
            return new ZipCodeEntrySummary
            {
                ZipCode = ZipCode,
                Settlement = Settlement.Name,
                SettlementType = Settlement.Type.Name,
                SettlementZone = Settlement.Zone,
                Municipality = Municipality.Name,
                City = City.Name,
                State = State.Name,
            };
        }
    }
}
