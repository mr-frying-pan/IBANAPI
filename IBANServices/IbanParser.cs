using System.Collections.Generic;
using System.Linq;

namespace IBANServices
{
    public class IbanParser : IIbanParser
    {
        private readonly IIbanValidator _validator = null;

        private static readonly Dictionary<string, string> bankCodes = new Dictionary<string, string>
        {
            { "70440", "SEB" },
            { "40100", "Luminor-DNB" },
            { "71800", "Šiaulių bankas" }
        };

        public IbanParser(IIbanValidator validator)
        {
            _validator = validator;
        }

        public IEnumerable<string> GetBankNames(IEnumerable<string> ibans)
        {
            return ibans.Select(iban =>
            {
                if (_validator.Validate(iban)
                    && iban.ToUpper().Substring(0, 2) == "LT"
                    && bankCodes.TryGetValue(iban.Substring(4, 5), out string bankName))
                    return bankName;
                return "";
            });
        }
    }
}
