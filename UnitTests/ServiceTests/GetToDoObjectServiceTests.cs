
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ToDoListWebAPI.Models.EntityModels;
using ToDoListWebAPI.Services;
using ToDoListWebAPI.Services.ToDo;

namespace ToDoListWeb.UnitTests.ServiceTests
{
  [TestFixture]
  public class GetToDoObjectServiceTests
  {
    [Test]
    public async Task WithValidObject_GetToDoObject_ReturnsObject()
    {
      var fakeDb = A.Fake<IDBInterface>();
      var fakeConfig = A.Fake<IConfiguration>();
      var fakeLogger = A.Fake<ILogger<GetToDoObjectService>>();
      var toDoEntity = new ToDoEntity();

      //A.CallTo(() => fakeDb.Read<ToDoEntity>(A<string>._, A<string>._, A<string>._, A<string>._))
      //.Returns(new ToDoEntity("userId", "title") { Description = "My First To Do Object" });

      //var sut = new GetToDoObjectService(fakeDb, fakeConfig, fakeLogger);

      //var toDoObject = await sut.GetToDoObject(toDoEntity);
      //var description = toDoObject.Description;

      //Assert.That(toDoObject, Is.Not.Null);
      //Assert.That(description, Is.EqualTo("My First To Do Object"));
      //Assert.That(toDoObject, Is.TypeOf(typeof(ToDoEntity)));
    }

    [Test]
    public void WithExceptionThrown_GetToDoObject_ThrowsException()
    {
      var fakeDb = A.Fake<IDBInterface>();
      var fakeConfig = A.Fake<IConfiguration>();
      var fakeLogger = A.Fake<ILogger<GetToDoObjectService>>();
      var toDoEntity = new ToDoEntity();

      //A.CallTo(() => fakeDb.Read<ToDoEntity>(A<string>._, A<string>._, A<string>._, A<string>._)).Throws<Exception>();

      var sut = new GetToDoObjectService(fakeDb, fakeConfig, fakeLogger);

      //Assert.That(() => sut.GetToDoObject(toDoEntity).Result, Throws.Exception);
    }

    // todo: Added this new test. Check with Ryan how it's structured, is it correct?
    [Test]
    public async Task WithValidObject_GetAllToDoObjects_ReturnsListOfObjects()
    {
      var fakeDb = A.Fake<IDBInterface>();
      var fakeConfig = A.Fake<IConfiguration>();
      var fakeLogger = A.Fake<ILogger<GetToDoObjectService>>();

      var sut = new GetToDoObjectService(fakeDb, fakeConfig, fakeLogger);

      var toDoEntity1 = new ToDoEntity() { Description = "My First To Do Object" };
      //var toDoEntity2 = new ToDoEntity("userId", "title") { Description = "My Second To Do Object" };

      //A.CallTo(() => fakeDb.ReadAll<ToDoEntity>(A<string>._, A<string>._)).Returns<ToDoEntity>(
      //  new List<ToDoEntity>
      //  {
      //    toDoEntity1,
      //    toDoEntity2
      //  });

      //var toDoObjects = await sut.GetAllToDoObjects();
      //var count = toDoObjects.Count;

      //var toDoDesc1 = toDoObjects[0].Description;
      //var toDoDesc2 = toDoObjects[1].Description;

      //Assert.That(toDoObjects, Is.Not.Null);
      //Assert.That(count, Is.EqualTo(2));
      //Assert.That(toDoDesc1, Is.EqualTo("My First To Do Object"));
      //Assert.That(toDoDesc2, Is.EqualTo("My Second To Do Object"));
    }

    //// todo: Added this new test. Check with Ryan how it's structured, is it correct?
    // [Test]
    // public void WithExceptionThrown_GetAllToDoObjects_ThrowsException()
    // {
    // var fakeDb = A.Fake<IDBInterface>();
    // var toDoObjectService = new GetToDoObjectService(fakeDb);

    // A.CallTo(() => fakeDb.ReadAll<ToDoEntity>()).Throws<Exception>();

    // Assert.That(() => toDoObjectService.GetAllToDoObjects().Result, Throws.Exception);
    // }
  }
}