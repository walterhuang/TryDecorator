using System;

namespace TryDecorator
{
    // 1. Component
    public interface IDataSource
    {
        void WriteData(string data);

        void ReadData();
    }

    // 2. Concrete Component
    public class FileDataSource : IDataSource
    {
        private readonly string _filename;

        public FileDataSource(string filename)
        {
            _filename = filename;
        }

        public void WriteData(string data)
        {
            Console.WriteLine($"Write {data} to {_filename}");
        }

        public void ReadData()
        {
            Console.WriteLine($"Read data from {_filename}.");
        }
    }

    // 3. Base Decorator
    public abstract class DataSourceDecorator : IDataSource
    {
        protected IDataSource _wrappee;

        public DataSourceDecorator(IDataSource source)
        {
            _wrappee = source;
        }

        public virtual void WriteData(string data)
        {
            _wrappee.WriteData(data);
        }

        public virtual void ReadData()
        {
            _wrappee.ReadData();
        }
    }

    // 4. Concrete Decorators
    public class EncryptionDecorator : DataSourceDecorator
    {
        public EncryptionDecorator(IDataSource source) : base(source)
        {
        }

        public override void WriteData(string data)
        {
            data = $"<encrypt>{data}</encrypt>";
            base.WriteData(data);
        }

        public override void ReadData()
        {
            base.ReadData();
            Console.WriteLine("Decrypt the data.");
        }
    }

    public class CompressionDecorator : DataSourceDecorator
    {
        public CompressionDecorator(IDataSource source) : base(source)
        {
        }

        public override void WriteData(string data)
        {
            data = $"<compress>{data}</compress>";
            base.WriteData(data);
        }

        public override void ReadData()
        {
            base.ReadData();
            Console.WriteLine("Decompress the data.");
        }
    }
}