using Web.Data;

namespace Web.UnitTests.Data;

public class PaginatedListTests
{
    [Fact]
    public void HasNextPage_ReturnsFalseIfPageIndexIsGreaterOrEqualThanTotalPages()
    {
        // Arrange
        var paginatedList = new PaginatedList<int>([], 5, 1, 5);

        // Act
        var result = paginatedList.HasNextPage;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasNextPage_ReturnsTrueIfPageIndexIsLessThanTotalPages()
    {
        // Arrange
        var paginatedList = new PaginatedList<int>([], 10, 1, 5);

        // Act
        var result = paginatedList.HasNextPage;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasPreviousPage_ReturnsFalseIfItIsTheFirstPage()
    {
        // Arrange
        var paginatedList = new PaginatedList<int>([], 10, 1, 5);

        // Act
        var result = paginatedList.HasPreviousPage;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasPreviousPage_ReturnsTrueIfNotTheFirstPage()
    {
        // Arrange
        var paginatedList = new PaginatedList<int>([], 10, 2, 5);

        // Act
        var result = paginatedList.HasPreviousPage;

        // Assert
        Assert.True(result);
    }
}
