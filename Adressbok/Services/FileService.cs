using System.Diagnostics;

namespace Adressbok.Services;

/// <summary>
/// Interface för hantering av kontaktinformation.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Sparar kontaktinformation till fil.
    /// </summary>
    bool SaveContactToFile(string contact);

    /// <summary>
    /// Hämtar kontaktinformation från fil.
    /// </summary>
    string GetContactFromFile();
}

/// <summary>
/// Implementering av IFileService.
/// </summary>
public class FileService(string filePath) : IFileService
{
    private readonly string _filePath = filePath;

    /// <summary>
    /// Sparar kontaktinformation till fil.
    /// </summary>
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

    /// <summary>
    /// Hämtar kontaktinformation från fil.
    /// </summary>
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

