using IBANServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IBANValidator.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IbanController : ControllerBase
    {
        private readonly IIbanValidator _validator = null;

        public IbanController(IIbanValidator validator)
        {
            _validator = validator;
        }

        [HttpGet("{iban}")]
        public IDictionary<string, bool> Get(string iban)
        {
            return new Dictionary<string, bool>() { { iban, _validator.Validate(iban) } };
            //return (iban, _validator.Validate(iban));
        }
    }
}
