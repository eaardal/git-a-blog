namespace Gitablog.TestFramework
{
    public interface ITestFixture<out T>
    {
        T CreateSut();
    }
}
