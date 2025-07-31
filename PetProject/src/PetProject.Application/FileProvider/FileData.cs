using PetProject.Application.Providers;

namespace PetProject.Application.FileProvider;

public record FileData(Stream Stream, FileInfo Info);

public record FileInfo(FilePath FilePath, string BucketName);

public record FileMetaData(string BucketName, Guid ObjectName);

public record FileContent(Stream Stream, string ObjectName, string BucketName);

