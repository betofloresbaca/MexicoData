namespace MexicoData.ZipCodes.Models
{
    /// <summary>
    /// A database entry for a zip code.
    /// </summary>
    public class ZipCodeEntry
    {
        /// <summary>
        /// Gets or sets the zip code (es_MX: código postal).
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the settlement (es_MX: asentameinto).
        /// </summary>
        public Settlement Settlement { get; set; }

        /// <summary>
        /// Gets or sets the municipality (es_MX: municipio).
        /// </summary>
        public Municipality Municipality { get; set; }

        /// <summary>
        /// Gets or sets the city (es_MX: ciudad).
        /// </summary>
        public City City { get; set; }

        /// <summary>
        /// Gets or sets the state (es_MX: estado).
        /// </summary>
        public State State { get; set; }

        /// <summary>
        /// Gets the human entry summary containing only the names.
        /// </summary>
        /// <returns>A <see cref="ZipCodeEntrySummary"/> build from this entry.</returns>
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
