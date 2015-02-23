namespace Gitablog.BlogContentProcessor.UnitTests
{
    interface ITestFixture<out T>
    {
        T CreateSut();
    }
}
