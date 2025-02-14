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

            var stringParts = input.Split(_delimiters, StringSplitOptions.None);

            return stringParts;
        }
    }
}
