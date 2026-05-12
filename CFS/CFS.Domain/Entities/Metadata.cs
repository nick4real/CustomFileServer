using System.ComponentModel.DataAnnotations;

namespace CFS.Domain.Entities;

public class Metadata
{
    [Key]
    public Guid Id { get; set; }
    public string FileName { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public long SizeBytes { get; set; }
    public DateTimeOffset UploadedAtUtc { get; set; }

    [Required]
    public string GridFsId { get; set; } = default!;
}
