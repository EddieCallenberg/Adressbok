using Xunit;
using Moq;
using Adressbok.Services;
using Adressbok.Models;
using Newtonsoft.Json;
using Adressbok.Interfaces;
using System.Reflection;

public class FileServiceTests
{
    [Fact]
    public void SaveContactToFile_ShouldReturnTrueOnSuccessfulSave()
    {
        // Arrange
        var filePath = "testfile.txt";
        var contact = "Test Person";
        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(x => x.SaveContactToFile(contact)).Returns(true);

        // Act
        var result = mockFileService.Object.SaveContactToFile(contact);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetContactFromFile_ShouldReturnContactString()
    {
        // Arrange
        var filePath = "testfile.txt";
        var contact = "Test Person";
        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(x => x.GetContactFromFile()).Returns(contact);

        // Act
        var result = mockFileService.Object.GetContactFromFile();

        // Assert
        Assert.Equal(contact, result);
    }
}

public class ContactServiceTests
{
    [Fact]
    public void SaveContactToContactlist_ShouldNotAddContact_WhenAlreadyExists()
    {
        // Arrange
        var fileServiceMock = new Mock<IFileService>();
        var contactService = new ContactService(new FileService(@"C:\CSharp-projects-school\test-contacts.json"));

        fileServiceMock.Setup(x => x.SaveContactToFile(It.IsAny<string>())).Returns(true);
        fileServiceMock.Setup(x => x.GetContactFromFile()).Returns("{\"Email\":\"minnie@domain.com\"}");

        // Act
        contactService.SaveContactToContactlist(new Contact("Minnie", "Lind", "0726044647", "minnie@domain.com", "Adressgatan 11"));

        // Assert
        fileServiceMock.Verify(x => x.SaveContactToFile(It.IsAny<string>()), Times.Never);
    }
}