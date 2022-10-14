using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Business;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Hypermedia.Filters;
using RestWithASPNET.Model;

namespace RestWithASPNET.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        public readonly IBookBusiness _bookBusiness;

        public BookController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Get([FromQuery]string author, string sortDirection, int pageSize, int page)
        {
            return Ok(_bookBusiness.FindWithPagedSearch(author, sortDirection, pageSize, page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Get(int id)
        {
            var book = _bookBusiness.FindById(id);

            if(book is null)
                return NotFound();
            
            return Ok(book);
        }

        [HttpGet("findByAuthorOrTitle")]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Get([FromQuery]string author, [FromQuery]string title)
        {
            return Ok(_bookBusiness.FindByAuthorOrTitle(author, title));
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Post([FromBody] BookVO book)
        {
            if(book is null)
                return BadRequest();

            return Ok(_bookBusiness.Create(book));
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Put([FromBody] BookVO book)
        {
            if(book is null)
                return BadRequest();

            return Ok(_bookBusiness.Update(book));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Patch(long id)
        {
            return Ok(_bookBusiness.Disabled(id));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        public ActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}