
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Threading.Tasks;
using ToDoListWebAPI.Models.EntityModels;
using ToDoListWebAPI.Services;
using ToDoListWebAPI.Services.ToDo;

namespace ToDoListWeb.UnitTests.ServiceTests
{
  [TestFixture]
  public class DeleteToDoObjectServiceTests
  {
    [Test]
    public async Task WithValidObject_DeleteToDoObject_DeletesObjectAsync()
    {
      var fakeDb = A.Fake<IDBInterface>();
      var fakeConfig = A.Fake<IConfiguration>();
      var fakeLogger = A.Fake<ILogger<DeleteToDoObjectService>>();
      var toDoEntity = new ToDoEntity() { Description = "My First To Do Object" };

      //A.CallTo(() => fakeDb.Delete<ToDoEntity>(A<ToDoEntity>._, A<string>._, A<string>._))
      //    .Returns(new TableResult() { Result = toDoEntity });

      //var sut = new DeleteToDoObjectService(fakeDb, fakeConfig, fakeLogger);

      //var result = await sut.DeleteToDoObject(toDoEntity);

      //Assert.That(result, Is.Not.Null);
      //A.CallTo(() => fakeDb.Delete<ToDoEntity>(toDoEntity, A<string>._, A<string>._)).MustHaveHappened();
      //fakeLogger.VerifyLogHappened(LogLevel.Information, "Successfully deleted ToDoObject");
      //fakeLogger.VerifyLogMustNotHaveHappened(LogLevel.Error, "Failed to delete the ToDoObject");
    }

    [Test]
    public async Task ExceptionThrown_AddToDoObject_ReturnsNull()
    {
      var fakeDb = A.Fake<IDBInterface>();
      var fakeConfig = A.Fake<IConfiguration>();
      var fakeLogger = A.Fake<ILogger<DeleteToDoObjectService>>();
      var toDoEntity = new ToDoEntity();

      //A.CallTo(() => fakeDb.Delete<ToDoEntity>(A<ToDoEntity>._, A<string>._, A<string>._))
      //    .Throws<Exception>();

      //var sut = new DeleteToDoObjectService(fakeDb, fakeConfig, fakeLogger);

      //var result = await sut.DeleteToDoObject(toDoEntity);

      //Assert.That(result, Is.Null);
      //A.CallTo(() => fakeDb.Delete<ToDoEntity>(toDoEntity, A<string>._, A<string>._)).MustHaveHappened();
      //fakeLogger.VerifyLogHappened(LogLevel.Error, "Failed to delete the ToDoObject");
      //fakeLogger.VerifyLogMustNotHaveHappened(LogLevel.Information, "Successfully deleted ToDoObject");
    }
  }
}
