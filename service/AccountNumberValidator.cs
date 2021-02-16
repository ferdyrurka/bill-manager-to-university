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
