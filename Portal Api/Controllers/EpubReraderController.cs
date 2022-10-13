using Microsoft.AspNetCore.Mvc;

namespace Portal_Api.Controllers;

[ApiController]
[Route("epubreader")]
public class EpubReaderController : ControllerBase
{
    [HttpPost]
    public void OpenEBook() {}

    [HttpGet]
    [Route("title")]
    public ActionResult<string> GetTitle() => "";

    [HttpGet]
    [Route("authors")]
    public ActionResult<string> GetAuthors() => "";

    [HttpGet]
    [Route("tableofcontents")]
    public ActionResult<string> GetTableOfContents() => "";

    [HttpGet]
    [Route("sections/{sectionName}")]
    public ActionResult<string> GetPage(string sectionName) => "";
}