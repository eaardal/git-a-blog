using Moq;

namespace Gitablog.BlogContentProcessor.UnitTests
{
    static class MoqExtensions
    {
        public static Mock<T> AsMock<T>(this T obj) where T : Mock
        {
            return Mock.Get(obj);
        }
    }
}
