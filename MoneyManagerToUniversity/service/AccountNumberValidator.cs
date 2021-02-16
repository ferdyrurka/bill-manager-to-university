using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoneyManagerToUniversity.service
{
    public class AccountNumberValidator
    {
        /// <summary>
        /// check that the account number corresponds to the Polish format account number
        /// </summary>
        /// <returns>
        /// returns if the number is correct
        /// </returns>
        /// <param name="number">An string data with account number.</param>
        public bool Validate(string number)
        {
            Match match = Regex.Match(
                Regex.Replace(number, @"\s+", ""),
                "^[0-9]{26}$",
                RegexOptions.IgnoreCase
            );

            return match.Success;
        }
    }
}
