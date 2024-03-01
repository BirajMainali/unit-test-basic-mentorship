using AutoFixture;
using lorem_app.Models;
using lorem_app.Services;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Shouldly;

namespace lorem_test;

public class LoremAppTestService
{
    private readonly DbContext _dbContext;
    private readonly LoremAppService _loremAppService;

    public LoremAppTestService()
    {
        // mocking _dbcontext using NSubstitute
        _dbContext = Substitute.For<DbContext>();

        _loremAppService = new LoremAppService(_dbContext);
    }

    [Fact]
    public async Task test_lorem_create_creates_lorem()
    {
        // Arrange
        var fixture = new Fixture(); // Fixture -> Fake filler
        var lorem = fixture.Create<Lorem>();

        // Act
        await _loremAppService.CreateAsync(lorem);

        // Assert
        await _dbContext.Set<Lorem>().Received()
            .AddAsync(Arg.Is<Lorem>(x =>
                x.FirstName == lorem.FirstName &&
                x.LastName == lorem.LastName &&
                x.Email == lorem.Email &&
                x.Phone == lorem.Phone &&
                x.Zip == lorem.Zip)
            );

        await _dbContext.Received().SaveChangesAsync();
    }

    [Theory]
    [InlineData(1, "first_name_updated", "last_name_updated")]
    public async Task test_lorem_update_updates_lorem(int id, string firstName, string lastName)
    {
        // Arrange
        var existingLorem = new Lorem { Id = 1, FirstName = "OldFirstName", LastName = "OldLastName" }; // assume this row present in database

        var dbSet = Substitute.For<DbSet<Lorem>, IQueryable<Lorem>>();

        _dbContext.Set<Lorem>().Returns(dbSet);

        dbSet.FindAsync(id).Returns(existingLorem);

        var entity = await _loremAppService.UpdateAsync(id, firstName, lastName);

        entity.FirstName.ShouldBe(firstName);
        entity.LastName.ShouldBe(lastName);

        await _dbContext.Received().SaveChangesAsync();
    }


    [Theory]
    [InlineData(1)]
    public async Task test_invalid_id_throws_on_lorem_update(int id)
    {
        // Arrange
        var dbSet = Substitute.For<DbSet<Lorem>, IQueryable<Lorem>>();
        _dbContext.Set<Lorem>().Returns(dbSet);
        dbSet.FindAsync(id).Returns((Lorem?)null);

        // Act
        var exception = await Record.ExceptionAsync(async () =>
            await _loremAppService.UpdateAsync(id, "first_name_updated", "last_name_updated"));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<Exception>();
        exception.Message.ShouldBe("lorem not found");
    }

    [Theory]
    [InlineData(2)]
    public async Task test_lorem_delete_deletes_lorem(int id)
    {
        // Arrange
        var existingLorem = new Lorem { Id = id, FirstName = "OldFirstName", LastName = "OldLastName" };
        var dbSet = Substitute.For<DbSet<Lorem>, IQueryable<Lorem>>();
        _dbContext.Set<Lorem>().Returns(dbSet);
        dbSet.FindAsync(id).Returns(existingLorem);

        // Act
        await _loremAppService.DeleteAsync(existingLorem);

        // Assert
        _dbContext.Received().Set<Lorem>().Remove(existingLorem);
        await _dbContext.Received().SaveChangesAsync();
    }

    [Theory]
    [InlineData(1)]
    public async Task test_lorem_get_by_id_returns_lorem(int id)
    {
        // Arrange
        var existingLorem = new Lorem { Id = id, FirstName = "OldFirstName", LastName = "OldLastName" };
        var dbSet = Substitute.For<DbSet<Lorem>, IQueryable<Lorem>>();
        _dbContext.Set<Lorem>().Returns(dbSet);
        dbSet.FindAsync(id).Returns(existingLorem);

        // Act
        var result = await _loremAppService.GetById(id);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(id);
        result.FirstName.ShouldBe(existingLorem.FirstName);
        result.LastName.ShouldBe(existingLorem.LastName);
    }
}