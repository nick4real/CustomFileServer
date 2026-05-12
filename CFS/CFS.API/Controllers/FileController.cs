using CFS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CFS.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController(IFileService fileService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetFiles(CancellationToken ct)
    {
        var result = await fileService.GetFilesAsync(ct);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken ct)
    {
        if (file is null || file.Length == 0)
            return BadRequest("No file provided.");

        var result = await fileService.UploadFileAsync(file, ct);
        return HandleResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> DownloadFile([FromRoute] Guid id, CancellationToken ct)
    {
        var result = await fileService.DownloadFileAsync(id, ct);
        if (!result.IsSuccess)
            return HandleResult(result);

        var file = result.Value!;
        return File(file.Content, file.ContentType, file.FileName);
    }
}