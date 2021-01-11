using System.Collections.Generic;
using System.Linq;

namespace IBANServices
{
    public class IbanValidator : IIbanValidator
    {
        // if country code is not one of these, then probably incorrect. These are the only ones defined according to wikipedia
        private static readonly HashSet<string> countryCodes = new HashSet<string>
        {
            "AD", "AE", "AF", "AG", "AI", "AL", "AM", "AO", "AQ", "AR", "AS", "AT", "AU", "AW", "AX", "AZ",
            "BA", "BB", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BL", "BM", "BN", "BO", "BQ", "BR", "BS", "BT", "BV", "BW", "BY", "BZ",
            "CA", "CC", "CD", "CF", "CG", "CH", "CI", "CK", "CL", "CM", "CN", "CO", "CR", "CU", "CV", "CW", "CX", "CY", "CZ",
            "DE", "DJ", "DK", "DM", "DO", "DZ",
            "EC", "EE", "EG", "EH", "ER", "ES", "ET", "FI",
            "FJ", "FK", "FM", "FO", "FR",
            "GA", "GB", "GD", "GE", "GF", "GG", "GH", "GI", "GL", "GM", "GN", "GP", "GQ", "GR", "GS", "GT", "GU", "GW", "GY",
            "HK", "HM", "HN", "HR", "HT", "HU",
            "ID", "IE", "IL", "IM", "IN", "IO", "IQ", "IR", "IS", "IT",
            "JE", "JM", "JO", "JP",
            "KE", "KG", "KH", "KI", "KM", "KN", "KP", "KR", "KW", "KY", "KZ",
            "LA", "LB", "LC", "LI", "LK", "LR", "LS", "LT", "LU", "LV", "LY",
            "MA", "MC", "MD", "ME", "MF", "MG", "MH", "MK", "ML", "MM", "MN", "MO", "MP", "MQ", "MR", "MS", "MT", "MU", "MV", "MW", "MX", "MY", "MZ",
            "NA", "NC", "NE", "NF", "NG", "NI", "NL", "NO", "NP", "NR", "NU", "NZ",
            "OM",
            "PA", "PE", "PF", "PG", "PH", "PK", "PL", "PM", "PN", "PR", "PS", "PT", "PW", "PY",
            "QA",
            "RE", "RO", "RS", "RU", "RW",
            "SA", "SB", "SC", "SD", "SE", "SG", "SH", "SI", "SJ", "SK", "SL", "SM", "SN", "SO", "SR", "SS", "ST", "SV", "SX", "SY", "SZ",
            "TC", "TD", "TF", "TG", "TH", "TJ", "TK", "TL", "TM", "TN", "TO", "TR", "TT", "TV", "TW", "TZ",
            "UA", "UG", "UM", "US", "UY", "UZ",
            "VA", "VC", "VE", "VG", "VI", "VN", "VU",
            "WF", "WS",
            "YE", "YT",
            "ZA", "ZM", "ZW",
        };

        public bool Validate(string iban)
        {
            // 2 chars country, 2 chars required checksum. 
            // Afterwards there is BBAN which is of different lengths, but definitely should be at least 1 char
            if (iban.Length < 5)
                return false;
            string upperIban = iban.ToUpper();
            string country = upperIban.Substring(0, 2);
            if (!countryCodes.Contains(country))
                return false;
            if (country == "LT" && upperIban.Length != 20)
                return false;

            string firstFour = upperIban.Substring(0, 4);
            string tail = upperIban.Substring(4);
            var D = string.Concat((tail + firstFour).Select(
                    c => char.IsDigit(c)
                    ? c.ToString()
                    : (c - 'A' + 10).ToString()
                ));
            var mod97 = Mod97(D);
            return mod97 == 1;
        }

        private int Mod97(string D)
        {
            int res = 0;
            foreach (var c in D)
            {
                res = (res * 10 + c - '0') % 97;
            }
            return res;
        }
    }
}
