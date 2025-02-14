using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
    public class CalculatorService
    {
        public int CalculcateAddForString(string input)
        {
            var stringParts = GetStringParts(input);
            if(stringParts.Count() > 2)
            {
                throw new ArgumentException($"Expected 2 numbers, got {stringParts}");
            }

            var numbers = stringParts.Select(x => {
                var result = 0;
                int.TryParse(x, out result);
                return result;
            });

            return numbers.Sum();
        }

        internal IList<string> GetStringParts(string input)
        {
            if(input == null || string.IsNullOrEmpty(input))
            {
                return new List<string>();
            }

            var stringParts = input.Split(',').Select(x => x).ToList();

            return stringParts;
        }
    }
}
