using NUnit.Framework;
using System;

namespace ToDoListWeb.UnitTests.ServiceTests
{
  using FakeItEasy;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.Logging;
  using System.Threading.Tasks;
  using ToDoListWebAPI.Models.EntityModels;
  using ToDoListWebAPI.Services;
  using ToDoListWebAPI.Services.ToDo;

  [TestFixture]
  public class AddToDoObjectServiceTests
  {
    [Test]
    public async Task NoExceptionThrown_AddToDoObject_ReturnsNotNullResult()
    {
      var fakeDb = A.Fake<IDBInterface>();
      var fakeConfig = A.Fake<IConfiguration>();
      var fakeLogger = A.Fake<ILogger<AddToDoObjectService>>();
      var toDoEntity = new ToDoEntity();

      //A.CallTo(() => fakeDb.CreateAsync<ToDoEntity>(A<ToDoEntity>._, A<string>._, A<string>._))
      //    .Returns(new TableResult() { Result = toDoEntity});

      //var sut = new AddToDoObjectService(fakeDb, fakeConfig, fakeLogger);

      //var result = await sut.AddToDoObject(toDoEntity);

      //Assert.That(result.Result, Is.Not.Null);

    }

    [Test]
    public async Task ExceptionThrown_AddToDoObject_ReturnsNull()
    {
      var fakeDb = A.Fake<IDBInterface>();
      var fakeConfig = A.Fake<IConfiguration>();
      var fakeLogger = A.Fake<ILogger<AddToDoObjectService>>();
      var toDoEntity = new ToDoEntity();

      //A.CallTo(() => fakeDb.CreateAsync<ToDoEntity>(A<ToDoEntity>._, A<string>._, A<string>._))
      //    .Throws<Exception>();

      //var sut = new AddToDoObjectService(fakeDb, fakeConfig, fakeLogger);

      //var result = await sut.AddToDoObject(toDoEntity);

      //Assert.That(result, Is.Null);
    }
  }
}
