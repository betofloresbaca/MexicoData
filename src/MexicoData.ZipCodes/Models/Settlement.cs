namespace MexicoData.ZipCodes.Models
{
    /// <summary>
    /// The settlement information.
    /// </summary>
    public class Settlement
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the settlement.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the settlement.
        /// </summary>
        public SettlementType Type { get; set; }

        /// <summary>
        /// Gets or sets the zone of the settlement.
        /// </summary>
        public string Zone { get; set; }
    }
}
