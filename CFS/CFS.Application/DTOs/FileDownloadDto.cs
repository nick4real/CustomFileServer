namespace CFS.Application.DTOs;

public sealed record FileDownloadDto(Guid Id, string FileName, string ContentType, long SizeBytes, DateTimeOffset UploadedAtUtc, Stream Content);
