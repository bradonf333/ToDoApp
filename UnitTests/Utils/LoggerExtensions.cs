using System;
using FakeItEasy;
using FakeItEasy.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;

namespace ToDoListWeb.UnitTests.Utils
{
  public static class LoggerExtensions
  {

    public static void VerifyLogHappened<T>(this ILogger<T> logger, LogLevel level, string message)
    {
      try
      {
        logger.VerifyLog(level, message)
            .MustHaveHappened();
      }
      catch (Exception ex)
      {
        throw new ExpectationException($"While verifying a call to Log with message: \"{message}\"", ex);
      }
    }

    public static void VerifyLogMustNotHaveHappened<T>(this ILogger<T> logger, LogLevel level, string message)
    {
      try
      {
        logger.VerifyLog(level, message)
            .MustNotHaveHappened();
      }
      catch (Exception ex)
      {
        throw new ExpectationException($"While verifying a call to Log with message: \"{message}\"", ex);
      }
    }

    public static IVoidArgumentValidationConfiguration VerifyLog<T>(this ILogger<T> logger, LogLevel level, string message)
    {
      return A.CallTo(() => logger.Log(level, A<EventId>._,
          A<FormattedLogValues>.That.Matches(e => e.ToString().Contains(message)), A<Exception>._,
          A<Func<object, Exception, string>>._));

    }


  }
}
