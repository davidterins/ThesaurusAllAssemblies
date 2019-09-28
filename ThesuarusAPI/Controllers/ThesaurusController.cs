using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Thesaurus;
using System.Diagnostics;

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
      return thesaurusService.GetWords();
    }

    // GET: api/Thesaurus/synonym
    [HttpGet("{synonym}", Name = "Get")]
    public IEnumerable<string> Get(string synonym)
    {
      return thesaurusService.GetSynonyms(synonym);
    }

    // POST: api/Thesaurus
    [HttpPost]
    public void Post(IEnumerable<string> value)
    {
      Debug.WriteLine("post Value " + value);

      thesaurusService.AddSynonyms(value);
    }
  }
}
