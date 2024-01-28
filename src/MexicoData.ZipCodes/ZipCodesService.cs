using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using MexicoData.ZipCodes.Models;

namespace MexicoData.ZipCodes
{
    internal class ZipCodesService : IZipCodesService, IDisposable
    {
        private readonly ILiteDatabase database;

        private readonly Lazy<ILiteCollection<ZipCodeEntry>> entriesCollection;

        public ZipCodesService()
        {
            this.database = new LiteDatabase("Data/data.db");
            this.entriesCollection = new Lazy<ILiteCollection<ZipCodeEntry>>(
                () => database.GetCollection<ZipCodeEntry>("entries")
            );
        }

        public List<ZipCodeEntry> GetByZipCode(
            string zipCode,
            int skip = 0,
            int limit = int.MaxValue
        ) => this.entriesCollection.Value.Find(x => x.ZipCode == zipCode, skip, limit).ToList();

        public List<ZipCodeEntry> GetBySettlement(
            string settlement,
            int skip = 0,
            int limit = int.MaxValue
        ) =>
            this.entriesCollection.Value.Find(x => x.Settlement.Name == settlement, skip, limit)
                .ToList();

        public List<ZipCodeEntry> GetByMunicipality(
            string municipality,
            int skip = 0,
            int limit = int.MaxValue
        ) =>
            this.entriesCollection.Value.Find(x => x.Municipality.Name == municipality, skip, limit)
                .ToList();

        public List<ZipCodeEntry> GetByCity(string city, int skip = 0, int limit = int.MaxValue) =>
            this.entriesCollection.Value.Find(x => x.City.Name == city, skip, limit).ToList();

        public List<ZipCodeEntry> GetByState(
            string state,
            int skip = 0,
            int limit = int.MaxValue
        ) => this.entriesCollection.Value.Find(x => x.State.Name == state, skip, limit).ToList();

        public void Dispose()
        {
            this.database.Dispose();
        }
    }
}
