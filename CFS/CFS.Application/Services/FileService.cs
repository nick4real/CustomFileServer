using CFS.Application.Common.Result;
using CFS.Application.DTOs;
using CFS.Application.Interfaces.Repositories;
using CFS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace CFS.Application.Services
{
    public class FileService(IMetadataRepository metadataRepository) : IFileService
    {
        public async Task<Result<List<MetadataDto>>> GetFilesAsync(CancellationToken ct)
        {
            var metadataList = await metadataRepository.GetAllMetadataAsync(ct);
            if (metadataList is null)
                return Result<List<MetadataDto>>.Failure(new Error(ErrorCode.NotFound, "Not found."));

            return Result<List<MetadataDto>>
                .Success(metadataList
                .Select(f => new MetadataDto(f.Id, f.FileName, f.ContentType, f.SizeBytes, f.UploadedAtUtc))
                    .ToList());
        }

        public async Task<Result<MetadataDto>> UploadFileAsync(IFormFile file, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<MetadataDto>> DownloadFileAsync(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
