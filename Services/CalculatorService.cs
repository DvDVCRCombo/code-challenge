using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
    public class CalculatorService
    {
        private string[] _delimiters = { ",", "\n", Environment.NewLine };

        public int CalculcateAddForString(string input)
        {
            var stringParts = GetStringParts(input);

            var numbers = stringParts.Select(x =>
            {
                var result = 0;
                int.TryParse(x, out result);
                return result;
            });

            ValidateNoNegativeNumbers(numbers);

            return numbers.Sum();
        }

        internal IList<string> GetStringParts(string input)
        {
            if (input == null || string.IsNullOrEmpty(input))
            {
                return new List<string>();
            }

            var stringParts = input.Split(_delimiters, StringSplitOptions.None);

            return stringParts;
        }

        internal void ValidateNoNegativeNumbers(IEnumerable<int> values)
        {
            var negativeNumbers = values.Where(x => x < 0).Select(x => x).ToList();

            if (negativeNumbers.Any())
            {
                throw new Exception($"Input string contained the following negative numbers: {string.Join(",", negativeNumbers)}");
            }
        }
    }
}
