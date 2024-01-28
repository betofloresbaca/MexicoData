namespace MexicoData.ZipCodes.Models
{
    /// <summary>
    /// Human readable (only names) summary for an entry.
    /// </summary>
    public class ZipCodeEntrySummary
    {
        /// <summary>
        /// Gets or sets the zip code (es_MX: código postal).
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the settlement (es_MX: nombre del asentamiento).
        /// </summary>
        public string Settlement { get; set; }

        /// <summary>
        /// Gets or sets the type of the settlement (es_MX: tipo de asentamiento).
        /// </summary>
        public string SettlementType { get; set; }

        /// <summary>
        /// Gets or sets the zone of the settlement (es_MX: tipo de zona del asentamiento).
        /// </summary>
        public string SettlementZone { get; set; }

        /// <summary>
        /// Gets or sets the name of the municipality (es_MX: nombre del municipio).
        /// </summary>
        public string Municipality { get; set; }

        /// <summary>
        /// Gets or sets the name of the city (es_MX: nombre de la ciudad).
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the name of the state (es_MX: nombre del estado).
        /// </summary>
        public string State { get; set; }
    }
}
