using Web.Data;

namespace Web.UnitTests.Data;

public class PaginatedListTests
{
    [Fact]
    public void Constructor_InvalidPageIndex_ThrowsArgumentOutOfRangeException()
        => Assert.Throws<ArgumentOutOfRangeException>(() => new PaginatedList<int>([1, 2, 3], 3, 0, 2));

    [Fact]
    public void Constructor_InvalidPageSize_ThrowsArgumentOutOfRangeException()
        => Assert.Throws<ArgumentOutOfRangeException>(() => new PaginatedList<int>([1, 2, 3], 3, 1, 0));

    [Fact]
    public void Constructor_ValidParameters_PropertiesSetCorrectly()
    {
        // Arrange
        int[] items = [1, 2, 3];
        var totalItems = 10;
        var pageIndex = 2;
        var pageSize = 3;

        // Act
        var paginatedList = new PaginatedList<int>(items, totalItems, pageIndex, pageSize);

        // Assert
        Assert.Equal(pageIndex, paginatedList.PageIndex);
        Assert.Equal(4, paginatedList.TotalPages); // 10 total items with a page size of 3 should result in 4 pages
        Assert.Equal(items, paginatedList);
    }

    [Fact]
    public void HasNextPage_PageIndexLessThanTotalPages_ReturnsTrue()
    {
        // Arrange
        var paginatedList = new PaginatedList<int>([1, 2, 3], 10, 2, 3);

        // Act
        var hasNextPage = paginatedList.HasNextPage;

        // Assert
        Assert.True(hasNextPage);
    }

    [Fact]
    public void HasNextPage_PageIndexEqualToTotalPages_ReturnsFalse()
    {
        // Arrange
        var paginatedList = new PaginatedList<int>([1, 2, 3], 9, 3, 3);

        // Act
        var hasNextPage = paginatedList.HasNextPage;

        // Assert
        Assert.False(hasNextPage);
    }

    [Fact]
    public void HasPreviousPage_PageIndexGreaterThan1_ReturnsTrue()
    {
        // Arrange
        var paginatedList = new PaginatedList<int>([1, 2, 3], 10, 2, 3);

        // Act
        var hasPreviousPage = paginatedList.HasPreviousPage;

        // Assert
        Assert.True(hasPreviousPage);
    }

    [Fact]
    public void HasPreviousPage_PageIndexEquals1_ReturnsFalse()
    {
        // Arrange
        var paginatedList = new PaginatedList<int>([1, 2, 3], 9, 1, 3);

        // Act
        var hasPreviousPage = paginatedList.HasPreviousPage;

        // Assert
        Assert.False(hasPreviousPage);
    }
}
