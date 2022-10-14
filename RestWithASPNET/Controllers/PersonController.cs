using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNET.Model;
using RestWithASPNET.Business;

namespace RestWithASPNET.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);
            if(person is null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Person person)
        {
            if(person is null)
                return BadRequest();

            return Ok(_personBusiness.Create(person));
        }

        [HttpPut]
        public ActionResult Put([FromBody] Person person)
        {
            if(person is null)
                return BadRequest();

            return Ok(_personBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            _personBusiness.Delete(id);          
            return NoContent();
        }
    }
}