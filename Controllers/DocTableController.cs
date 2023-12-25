using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sharable.Models;

namespace Sharable.Controllers;

[ApiController]
[Route("DocTable")]
public class DocTableController : ControllerBase
{
    private readonly ILogger<DocTableController> _logger;
    private readonly IDataTableProcessor _dataTableProcessor;

    public DocTableController(ILogger<DocTableController> logger, IDataTableProcessor dataTableProcessor)
    {
        _logger = logger;
        _dataTableProcessor = dataTableProcessor;
    }

    [HttpGet]
    [Route("GetXGPTable")]
    public XGPGame[] GetXGPTable()
    {
        return _dataTableProcessor.ParseXGPMasterListFromGoogleDrive();
    }
}
