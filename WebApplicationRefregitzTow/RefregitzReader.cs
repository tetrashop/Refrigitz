using System;
using System.Collections.Generic;
namespace RefregitzReader
{
    [Serializable]
    internal class RefregitzReader
    {
        private readonly List<string> Item;
        //public List<SearchGoogle> ItemSearch;
        public bool OKResult = false;
        public RefregitzReader(string Path)
        {

        }
        public DateTime ConvertRefregitzStringToDateTime(string RefregitzTime)
        {

            DateTime A = new DateTime();
            A = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(System.Convert.ToDouble(RefregitzTime) / 1000).ToLocalTime();
            return A;

        }

    }
}

