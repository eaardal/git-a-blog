using Moq;

namespace Gitablog.TestFramework
{
    static class MoqExtensions
    {
        public static Mock<T> AsMock<T>(this T obj) where T : Mock
        {
            return Mock.Get(obj);
        }
    }
}
