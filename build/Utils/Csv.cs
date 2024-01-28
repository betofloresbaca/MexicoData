namespace _build.Utils
{
    internal class Csv
    {
        private readonly Dictionary<string, int> headersIndex;
        private readonly string[] headers;
        private readonly string[][] data;

        public Csv(string filename, string sep, int skipLines = 0)
        {
            var lines = File.ReadAllLines(filename).Skip(skipLines).Select(x => x.Split(sep));
            this.headers = lines.First().ToArray();
            this.data = lines.Skip(1).ToArray();
            this.headersIndex = BuildHeadersIndex();
        }

        private Dictionary<string, int> BuildHeadersIndex()
        {
            int index = 0;
            return this.headers.Select(x => (x, index++)).ToDictionary(x => x.Item1, x => x.Item2);
        }

        public Dictionary<string, string> Row(int index)
        {
            return this.headersIndex.ToDictionary(x => x.Key, x => this.data[index][x.Value]);
        }

        public string[] RawRow(int index)
        {
            return this.data[index];
        }

        public string[] Col(int index)
        {
            return this.data.Select(x => x[index]).ToArray();
        }

        public string[] Col(string header)
        {
            return this.Col(headersIndex[header]);
        }

        public IEnumerable<Dictionary<string, string>> Rows()
        {
            for (int i = 0; i < this.data.Length; i++)
            {
                yield return this.Row(i);
            }
        }
    }
}
