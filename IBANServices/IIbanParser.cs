using System.Collections.Generic;

namespace IBANOps
{
    public interface IIbanParser
    {
        IEnumerable<string> GetBankNames(IEnumerable<string> ibans);
    }
}
