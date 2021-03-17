using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace TryDecorator
{
    [TestClass]
    public class DecoratorTests
    {
        [TestMethod]
        public void TestNotifier()
        {
            var stack = new Notifier();
            stack = new FacebookDecorator(stack);
            stack = new SlackDecorator(stack);
            var app = new MyApplication();
            app.SetNotifier(stack);
            app.DoSomething();
        }

        [TestMethod]
        public void TestDataSource()
        {
            string salaryRecords = "$199/mo";
            IDataSource source = new FileDataSource("somefile.dat");
            source.WriteData(salaryRecords);
            // The target file has been written with plain data.

            source = new CompressionDecorator(source);
            source.WriteData(salaryRecords);
            // The target file has been written with compressed
            // data.

            source = new EncryptionDecorator(source);
            // The source variable now contains this:
            // Encryption > Compression > FileDataSource
            source.WriteData(salaryRecords);
            // The file has been written with compressed and
            // encrypted data.
        }

        [TestMethod]
        public void TestDataSource_DependencyInjection()
        {
            // Arrange
            var kernel = new StandardKernel();
            kernel.Bind<IDataSource>().To<EmailDecorator>();

            kernel.Bind<IDataSource>().To<LoggingDecorator>()
                .WhenInjectedInto<EmailDecorator>();

            kernel.Bind<IDataSource>().To<EncryptionDecorator>()
                .WhenInjectedInto<LoggingDecorator>();

            kernel.Bind<IDataSource>().To<CompressionDecorator>()
                .WhenInjectedInto<EncryptionDecorator>();

            kernel.Bind<IDataSource>().To<FileDataSource>()
                .WhenInjectedInto<CompressionDecorator>()
                .WithConstructorArgument("filename", "somefile.dat");

            var target = kernel.Get<IDataSource>();
            string salaryRecords = "$199/mo";

            // Act
            target.WriteData(salaryRecords);
        }
    }
}