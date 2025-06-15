using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Application.FileProvider;

public record FileData(Stream Stream, string BucketName, Guid ObjectName);
public record FileMetaData(string BucketName, Guid ObjectName);

