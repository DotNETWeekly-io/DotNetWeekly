using LanguageExt.Common;

using Microsoft.AspNetCore.Mvc;

namespace ResultSample.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {

        [HttpPost]
        public IActionResult Create([FromBody]Customer customer)
        {
            /*            var validate = IsValid(customer);
                        if (!validate)
                        {
                            throw new InvalidOperationException("Error customer");
                        }

                        return Ok();*/

            var result = IsValidResult(customer);
            return result.Match<IActionResult>(b =>
            {
                return Ok();
            }, exception =>
            {
                if (exception is InvalidOperationException)
                {
                    return BadRequest();
                }

                return StatusCode(500);
            });
        }


        private static bool IsValid(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.Email))
            {
                return false;
            }

            return true;
        }

        private static Result<bool> IsValidResult(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name) || string.IsNullOrWhiteSpace(customer.Email))
            {
                var expcetion = new InvalidOperationException("Error customer");

                return new Result<bool>(expcetion);
            }

            return true;
        }
    }
}
