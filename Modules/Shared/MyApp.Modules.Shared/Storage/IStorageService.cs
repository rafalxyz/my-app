using Microsoft.AspNetCore.Http;

namespace MyApp.Modules.Shared.Storage;

public interface IStorageService
{
    byte[] Download(string directoryName, string fileName);

    void Upload(string directoryName, IFormFile file);
}