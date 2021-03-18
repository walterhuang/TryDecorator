using System;

namespace TryDecorator
{
    // 1. Component
    public interface IDataSource
    {
        string WriteData(string data);

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

        public string WriteData(string data)
        {
            Console.WriteLine($"Write {data} to {_filename}");
            return data;
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

        public virtual string WriteData(string data)
        {
            data = _wrappee.WriteData(data);
            return data;
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

        public override string WriteData(string data)
        {
            data = $"<encrypt>{data}</encrypt>";
            data = base.WriteData(data);
            return data;
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

        public override string WriteData(string data)
        {
            data = $"<compress>{data}</compress>";
            data = base.WriteData(data);
            return data;
        }

        public override void ReadData()
        {
            base.ReadData();
            Console.WriteLine("Decompress the data.");
        }
    }

    // 5. Simple Decorator
    public class LoggingDecorator : IDataSource
    {
        private readonly IDataSource _dataSource;

        public LoggingDecorator(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public void ReadData()
        {
            Console.WriteLine("Read Data Begin.");
            _dataSource.ReadData();
            Console.WriteLine("Read Data End.");
        }

        public string WriteData(string data)
        {
            Console.WriteLine("Write Data Begin.");
            data = _dataSource.WriteData(data);
            Console.WriteLine("Write Data End.");
            return data;
        }
    }

    public class EmailDecorator : IDataSource
    {
        private readonly IDataSource _dataSource;

        public EmailDecorator(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public void ReadData()
        {
            _dataSource.ReadData();
        }

        public string WriteData(string data)
        {
            data = _dataSource.WriteData(data);
            Console.WriteLine($"Paycheck {data} sent.");
            return data;
        }
    }
}