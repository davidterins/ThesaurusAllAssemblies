using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thesaurus;

namespace ThesuarusAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ThesaurusController : ControllerBase
  {
    public readonly IThesaurus thesaurusService;

    public ThesaurusController(IThesaurus thesaurusService)
    {
      this.thesaurusService = thesaurusService;
    }

    // GET: api/Thesaurus
    [HttpGet]
    public IEnumerable<string> Get()
    {
      //var result = thesaurusService.GetWords();
      return new string[] { "value1", "value2" };
    }

    // GET: api/Thesaurus/5
    [HttpGet("{synonym}", Name = "Get")]
    public IEnumerable<string> Get(string synonym)
    {
      //var result = thesaurusService.GetSynonyms(synonym);
      return new string[] { "value1", "value2" };
    }

    // POST: api/Thesaurus
    [HttpPost]
    public void Post([FromBody] string value)
    {
      //thesaurusService.AddSynonyms(value);
    }
  }
}
