using System;
using System.Linq;

namespace BookLibrary.Api
{
    class Generator
    {
        private Random _random;
        public Generator()
        {
            _random = new Random();
        }
        public string GenerateString(string array)
        {
            var stringLength = _random.Next(6, 10);
            return new string(Enumerable.Repeat(array, stringLength)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public DateTime GenerateDate(DateTime start)
        {
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }
    }
}
