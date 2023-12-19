using System.Diagnostics;

namespace Adressbok.Services;

public interface IFileService
{
    bool SaveContactToFile(string contact);
    string GetContactFromFile();
}
internal class FileService(string filePath) : IFileService
{
    private readonly string _filePath = filePath;

    public bool SaveContactToFile(string contact)
    {
        try
        {
            using (var sw = new StreamWriter(_filePath)) 
            {
                sw.WriteLine(contact);
            }

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        return false;
    }

    public string GetContactFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using var sr = new StreamReader(_filePath);
                return sr.ReadToEnd();
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        return null!;
    }
}

