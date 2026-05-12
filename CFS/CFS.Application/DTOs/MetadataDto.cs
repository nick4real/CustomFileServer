namespace CFS.Application.DTOs;

public sealed record MetadataDto(Guid Id, string FileName, string ContentType, long SizeBytes, DateTimeOffset UploadedAtUtc);
