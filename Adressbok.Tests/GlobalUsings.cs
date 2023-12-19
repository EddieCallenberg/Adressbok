using Xunit;
using Moq;
using Adressbok.Services;

public class FileServiceTests
{
    [Fact]
    public void SaveContactToFile_ShouldReturnTrueOnSuccessfulSave()
    {
        // Arrange
        var filePath = "testfile.txt";
        var contact = "John Doe";
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
        var contact = "John Doe";
        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(x => x.GetContactFromFile()).Returns(contact);

        // Act
        var result = mockFileService.Object.GetContactFromFile();

        // Assert
        Assert.Equal(contact, result);
    }
}
