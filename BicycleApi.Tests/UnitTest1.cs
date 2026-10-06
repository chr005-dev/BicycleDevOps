using BicycleApi.Services;
using Xunit;

namespace BicycleApi.Tests;

public class BicycleServiceTests
{
    [Fact]
    public void GetById_ExistingId_ReturnsCorrectBicycle()
    {
        var service = new BicycleService();

        var bicycle = service.GetById(1);

        Assert.NotNull(bicycle);
        Assert.Equal("Trek", bicycle.Brand);
    }

    [Fact]
    public void GetById_UnknownId_ReturnsNull()
    {
        var service = new BicycleService();

        Assert.Null(service.GetById(999));
    }

    [Fact]
    public void Add_ValidBicycles_CanBeRetrievedWithUniqueIds()
    {
        var service = new BicycleService();

        var first = service.Add("  Specialized  ", 5999m);
        var second = service.Add("Scott", 7000m);

        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal("Specialized", first.Brand);
        Assert.Equal(5999m, first.Price);
        Assert.Equal(first, service.GetById(first.Id));
        Assert.Equal(second, service.GetById(second.Id));
        Assert.Equal(4, service.GetAll().Count);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    [InlineData(1000001)]
    public void Add_InvalidPrice_ThrowsAndDoesNotSave(int price)
    {
        var service = new BicycleService();

        Assert.Throws<ArgumentOutOfRangeException>(
            () => service.Add("Trek", price));

        Assert.Equal(2, service.GetAll().Count);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Add_EmptyBrand_ThrowsAndDoesNotSave(string brand)
    {
        var service = new BicycleService();

        Assert.Throws<ArgumentException>(
            () => service.Add(brand, 1000m));

        Assert.Equal(2, service.GetAll().Count);
    }
}
