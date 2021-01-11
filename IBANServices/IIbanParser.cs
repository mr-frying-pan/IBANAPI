using System.Collections.Generic;

namespace IBANServices
{
    public interface IIbanParser
    {
        IEnumerable<string> GetBankNames(IEnumerable<string> ibans);
    }
}
