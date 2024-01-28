using System.Collections.Generic;
using MexicoData.ZipCodes.Models;

namespace MexicoData.ZipCodes
{
    /// <summary>
    /// ZipCodes service.
    /// </summary>
    public interface IZipCodesService
    {
        /// <summary>
        /// Gets a list of zip codes entries (settlements) by zip code.
        /// </summary>
        /// <param name="zipCode">the zip code.</param>
        /// <param name="skip">The entries to skip (used for pagination).</param>
        /// <param name="limit">The limit entries to take (used for pagination).</param>
        /// <returns></returns>
        List<ZipCodeEntry> GetByZipCode(string zipCode, int skip = 0, int limit = int.MaxValue);

        /// <summary>
        /// Gets a list of zip codes entries (settlements) by settlement name.
        /// </summary>
        /// <param name="settlement"></param>
        /// <param name="skip">The entries to skip (used for pagination).</param>
        /// <param name="limit">The limit entries to take (used for pagination).</param>
        /// <returns></returns>
        List<ZipCodeEntry> GetBySettlement(
            string settlement,
            int skip = 0,
            int limit = int.MaxValue
        );

        /// <summary>
        /// Gets a list of zip codes entries (settlements) by municipality name.
        /// </summary>
        /// <param name="municipality">The municipality name.</param>
        /// <param name="skip">The entries to skip (used for pagination).</param>
        /// <param name="limit">The limit entries to take (used for pagination).</param>
        /// <returns></returns>
        List<ZipCodeEntry> GetByMunicipality(
            string municipality,
            int skip = 0,
            int limit = int.MaxValue
        );

        /// <summary>
        /// Gets a list of zip codes entries (settlements) by city name.
        /// </summary>
        /// <param name="city"></param>
        /// <param name="skip">The entries to skip (used for pagination).</param>
        /// <param name="limit">The limit entries to take (used for pagination).</param>
        /// <returns></returns>
        List<ZipCodeEntry> GetByCity(string city, int skip = 0, int limit = int.MaxValue);

        /// <summary>
        /// Gets a list of zip codes entries (settlements) by state name.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="skip">The entries to skip (used for pagination).</param>
        /// <param name="limit">The limit entries to take (used for pagination).</param>
        /// <returns></returns>
        List<ZipCodeEntry> GetByState(string state, int skip = 0, int limit = int.MaxValue);
    }
}
