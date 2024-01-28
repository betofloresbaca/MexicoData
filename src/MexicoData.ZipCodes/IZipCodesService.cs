using System.Collections.Generic;
using MexicoData.ZipCodes.Models;

namespace MexicoData.ZipCodes
{
    public interface IZipCodesService
    {
        List<ZipCodeEntry> GetByZipCode(string zipCode, int skip, int limit);

        List<ZipCodeEntry> GetBySettlement(string settlement, int skip, int limit);

        List<ZipCodeEntry> GetByMunicipality(string municipality, int skip, int limit);

        List<ZipCodeEntry> GetByCity(string city, int skip, int limit);

        List<ZipCodeEntry> GetByState(string state, int skip, int limit);
    }
}
