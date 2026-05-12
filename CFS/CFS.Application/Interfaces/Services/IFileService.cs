using CFS.Application.Common.Result;
using CFS.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace CFS.Application.Interfaces.Services
{
    public interface IFileService
    {
        Task<Result<List<MetadataDto>>> GetFilesAsync(CancellationToken ct);
        Task<Result<MetadataDto>> UploadFileAsync(IFormFile file, CancellationToken ct);
        Task<Result<FileDownloadDto>> DownloadFileAsync(Guid id, CancellationToken ct);
    }
}