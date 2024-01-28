using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using MexicoData.ZipCodes.Models;

namespace MexicoData.ZipCodes
{
    public class ZipCodesService : IDisposable
    {
        private readonly ILiteDatabase database;

        private readonly Lazy<ILiteCollection<Entry>> entriesCollection;

        public ZipCodesService()
        {
            this.database = new LiteDatabase("Data/data.db");
            this.entriesCollection = new Lazy<ILiteCollection<Entry>>(
                () => database.GetCollection<Entry>("entries")
            );
        }

        public List<Entry> GetByZipCode(string zipCode, int skip = 0, int limit = int.MaxValue) =>
            this.entriesCollection.Value.Find(x => x.ZipCode == zipCode, skip, limit).ToList();

        public List<Entry> GetBySettlement(
            string settlement,
            int skip = 0,
            int limit = int.MaxValue
        ) =>
            this.entriesCollection.Value.Find(x => x.Settlement.Name == settlement, skip, limit)
                .ToList();

        public List<Entry> GetByMunicipality(
            string municipality,
            int skip = 0,
            int limit = int.MaxValue
        ) =>
            this.entriesCollection.Value.Find(x => x.Municipality.Name == municipality, skip, limit)
                .ToList();

        public List<Entry> GetByCity(string city, int skip = 0, int limit = int.MaxValue) =>
            this.entriesCollection.Value.Find(x => x.City.Name == city, skip, limit).ToList();

        public List<Entry> GetByState(string state, int skip = 0, int limit = int.MaxValue) =>
            this.entriesCollection.Value.Find(x => x.State.Name == state, skip, limit).ToList();

        public void Dispose()
        {
            this.database.Dispose();
        }
    }
}
