using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestWithASPNET.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public ActionResult GetSum(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Invalid input.");
        }

        [HttpGet("sub/{firstNumber}/{secondNumber}")]
        public ActionResult GetSub(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sub = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
                return Ok(sub.ToString());
            }
            return BadRequest("Invalid input.");
        }

        [HttpGet("mult/{firstNumber}/{secondNumber}")]
        public ActionResult GetMult(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var mult = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
                return Ok(mult.ToString());
            }

            return BadRequest("Invalid input.");
        }

        [HttpGet("div/{firstNumber}/{secondNumber}")]
        public ActionResult GetDiv(string firstNumber, string secondNumber)
        {
            if(IsNumeric(secondNumber) && ConvertToDecimal(secondNumber) == 0)
            {
                return BadRequest("Invalid input");
            }

            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var div = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
                return Ok(div.ToString());
            }

            return BadRequest("Invalid input.");
        }

        [HttpGet("med/{firstNumber}/{secondNumber}")]
        public ActionResult GetMed(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var media = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2;
                return Ok(media.ToString());
            }

            return BadRequest("Invalid input.");
        }

        [HttpGet("sqrt/{firstNumber}")]
        public ActionResult GetSqrt(string firstNumber)
        {
            if(IsNumeric(firstNumber))
            {
                var sqrt = Math.Sqrt(ConvertToDouble(firstNumber));

                return Ok(sqrt.ToString("0.00"));
                   
            }
            return BadRequest("Invalid input.");
        }

        private double ConvertToDouble(string stringNumber)
        {   
            double doubleValue;
            if(double.TryParse(stringNumber, out doubleValue))
            {
                return doubleValue;
            }
            return 0;
        }

        private decimal ConvertToDecimal(string stringNumber)
        {
            decimal decimalValue;
            if(decimal.TryParse(stringNumber, out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }

        private bool IsNumeric(string stringNumber)
        {
            double number;
            bool isNumber = double.TryParse(
                stringNumber, 
                System.Globalization.NumberStyles.Any, 
                System.Globalization.NumberFormatInfo.InvariantInfo, 
                out number);
            return isNumber;
        }
    }
}