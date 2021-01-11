using IBANServices;
using Microsoft.AspNetCore.Mvc;
using Parser.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace IBANParser.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IbanController : ControllerBase
    {
        private readonly IIbanParser _ibanParser;

        public IbanController(IIbanParser ibanParser)
        {
            _ibanParser = ibanParser;
        }

        [HttpGet]
        public IDictionary<string, string> GetBankNames([FromBody] IEnumerable<string> ibans)
        {
            var banks = _ibanParser.GetBankNames(ibans);
            return ibans.Zip(banks, (k, v) => new { k, v }).ToDictionary(p => p.k, p => p.v);
        }
    }
}
