using System.Reflection;
using Ioc;

namespace TRANS4D.Tests
{
    public class NamedIocContainerTests
    {
        public NamedIocContainerTests()
        {
            // Clear singleton registrations before each test for isolation
            var registrationsField = typeof(NamedIocContainer).GetField("_registrations", BindingFlags.NonPublic | BindingFlags.Instance);
            var dict = (System.Collections.Concurrent.ConcurrentDictionary<string, object>)registrationsField.GetValue(NamedIocContainer.Instance);
            dict.Clear();
        }

        [Fact]
        public void RegisterAndGet_ReturnsSameInstance()
        {
            var container = NamedIocContainer.Instance;
            var obj = "test";
            container.Register("myString", obj);

            var result = container.Get<string>("myString");
            Assert.Equal(obj, result);
        }

        [Fact]
        public void Get_ThrowsIfNotRegistered()
        {
            var container = NamedIocContainer.Instance;
            Assert.Throws<KeyNotFoundException>(() => container.Get<int>("missing"));
        }

        [Fact]
        public void Register_ThrowsIfNameNull()
        {
            var container = NamedIocContainer.Instance;
            Assert.Throws<ArgumentNullException>(() => container.Register<string>(null, "value"));
        }

        [Fact]
        public void Register_ThrowsIfInstanceNull()
        {
            var container = NamedIocContainer.Instance;
            Assert.Throws<ArgumentNullException>(() => container.Register<string>("name", null));
        }

        [Fact]
        public void Get_ThrowsIfNameNull()
        {
            var container = NamedIocContainer.Instance;
            Assert.Throws<ArgumentNullException>(() => container.Get<string>(null));
        }

        [Fact]
        public void RegisteringSameName_OverridesPrevious()
        {
            var container = NamedIocContainer.Instance;
            container.Register("item", "first");
            container.Register("item", "second");
            var result = container.Get<string>("item");
            Assert.Equal("second", result);
        }

        [Fact]
        public void Get_WrongType_Throws()
        {
            var container = NamedIocContainer.Instance;
            container.Register("item", 42);
            Assert.Throws<KeyNotFoundException>(() => container.Get<string>("item"));
        }

        [Fact]
        public void IsRegistered_ReturnsTrueIfRegistered()
        {
            var container = NamedIocContainer.Instance;
            container.Register("foo", 123);
            Assert.True(container.IsRegistered("foo"));
        }

        [Fact]
        public void IsRegistered_ReturnsFalseIfNotRegistered()
        {
            var container = NamedIocContainer.Instance;
            Assert.False(container.IsRegistered("bar"));
        }

        [Fact]
        public void IsRegistered_ThrowsIfNameNull()
        {
            var container = NamedIocContainer.Instance;
            Assert.Throws<ArgumentNullException>(() => container.IsRegistered(null));
        }

        [Fact]
        public void Constructor_IsNotPublicOrInternal()
        {
            var ctor = typeof(NamedIocContainer).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
            Assert.NotNull(ctor);
            Assert.False(ctor.IsPublic, "Constructor should not be public");
            Assert.False(ctor.IsAssembly, "Constructor should not be internal");
            Assert.True(ctor.IsFamily, "Constructor should be protected");
        }
    }
}
