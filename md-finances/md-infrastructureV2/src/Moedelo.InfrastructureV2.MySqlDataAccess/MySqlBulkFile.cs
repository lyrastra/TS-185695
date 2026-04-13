using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.InfrastructureV2.MySqlDataAccess;

public class MySqlBulkFile : IDisposable
{
    private bool disposed;
        
    public FileStream Stream { get; private set; }
    public string Name { get; private set; }
        
    public MySqlBulkFile(string fileName)
    {
        Create(fileName);
        disposed = false;
    }

    private void Create(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentNullException(nameof(fileName));
            
        var path = Path.GetTempPath();
        var name = $"{path}/{fileName}";
            
        Stream = File.Create(name);
        Name = name;
    }

    public async Task WriteAsync(string data)
    {
        CheckDisposed();
            
        var bytes = Encoding.ASCII.GetBytes(data);

        await Stream.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
    }

    public void Close()
    {
        CheckDisposed();
        Stream.Close();
    }
        
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
            return;

        if (disposing && Stream != null && string.IsNullOrEmpty(Stream.Name))
        {
            var fileName = Stream.Name;
                
            Stream.Dispose();
            Stream = null;
                
            File.Delete(fileName);
        }

        disposed = true;
    }

    private void CheckDisposed()
    {
        if (disposed)
        {
            throw new ObjectDisposedException("MySqlBulkFile");
        }
    }
}