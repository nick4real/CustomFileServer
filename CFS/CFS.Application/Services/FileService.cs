using CFS.Application.Common.Result;
using CFS.Application.DTOs;
using CFS.Application.Interfaces.Repositories;
using CFS.Application.Interfaces.Services;
using CFS.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace CFS.Application.Services;

public class FileService(IMetadataRepository metadataRepository, IFileRepository fileRepository) : IFileService
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
        if (ct.IsCancellationRequested)
            return Result<MetadataDto>.Failure(new Error(ErrorCode.ValidationFailed, "Request was cancelled."));

        if (file is null || file.Length == 0)
            return Result<MetadataDto>.Failure(new Error(ErrorCode.ValidationFailed, "No file provided."));

        var gridFsId = await fileRepository.SaveFileAsync(file, ct);

        var metadata = new Metadata
        {
            Id = Guid.NewGuid(),
            FileName = file.FileName,
            ContentType = file.ContentType,
            SizeBytes = file.Length,
            UploadedAtUtc = DateTimeOffset.UtcNow,
            GridFsId = gridFsId
        };

        await metadataRepository.AddMetadataAsync(metadata, ct);

        return Result<MetadataDto>.Success(new MetadataDto(metadata.Id, metadata.FileName, metadata.ContentType, metadata.SizeBytes, metadata.UploadedAtUtc));
    }

    public async Task<Result<FileDownloadDto>> DownloadFileAsync(Guid id, CancellationToken ct)
    {
        if (ct.IsCancellationRequested)
            return Result<FileDownloadDto>.Failure(new Error(ErrorCode.ValidationFailed, "Request was cancelled."));

        var metadata = await metadataRepository.GetMetadataByIdAsync(id, ct);
        if (metadata is null)
            return Result<FileDownloadDto>.Failure(new Error(ErrorCode.NotFound, "Not found."));

        var stream = await fileRepository.LoadFileAsync(metadata.GridFsId, ct);
        if (stream is null)
            return Result<FileDownloadDto>.Failure(new Error(ErrorCode.NotFound, "File content not found."));

        return Result<FileDownloadDto>.Success(new FileDownloadDto(metadata.Id, metadata.FileName, metadata.ContentType, metadata.SizeBytes, metadata.UploadedAtUtc, stream));
    }
}
