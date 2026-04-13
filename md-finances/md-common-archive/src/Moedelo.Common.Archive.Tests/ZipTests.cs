using Moedelo.Common.Archive.Exceptions;

namespace Moedelo.Common.Archive.Tests;

[TestFixture]
public class ZipTests
{
    [Test]
    public async Task PackUnpackTest()
    {
        var sourceEntries = new(string Name, int Size)[]
        {
            ("name.pdf", 100),
            ("name 2.pdf", 200),
            ("name 3", 3000),
            ("name 4.pdf", 100)
        };

        var result =
            await Zip.PackAsync(sourceEntries.Select(x => 
                new ZipItem(x.Name, GetByteArray(x.Size))).ToArray());
        Assert.That(result, Is.Not.Empty);

       var entries = await Zip.UnpackAsync(result);
       for(var i = 0; i < sourceEntries.Length; i++)
       {
            Assert.Multiple(() =>
            {
                Assert.That(entries[i].Name, Is.EqualTo(sourceEntries[i].Name));
                Assert.That(entries[i].Data.Length, Is.EqualTo(sourceEntries[i].Size));
            });
        }
    }
    
    [Test]
    public async Task PackWithUniqueNamesTest()
    {
        var sourceEntries = new(string Name, int Size, string UniqueName)[]
        {
            ("name.pdf", 100, "name.pdf"),
            ("name2.pdf", 200, "name2.pdf"),
            ("name.pdf", 3000, "name(2).pdf"),
            ("name.pdf", 100, "name(3).pdf"),
            ("name.jpg", 200, "name.jpg"),
        };

        var result =
            await Zip.PackWithUniqueNamesAsync(sourceEntries.Select(x => 
                new ZipItem(x.Name, GetByteArray(x.Size))).ToArray());
        Assert.That(result, Is.Not.Empty);

        var entries = await Zip.UnpackAsync(result);
        for(var i = 0; i < sourceEntries.Length; i++)
        {
            Assert.Multiple(() =>
            {
                Assert.That(entries[i].Name, Is.EqualTo(sourceEntries[i].UniqueName));
                Assert.That(entries[i].Data.Length, Is.EqualTo(sourceEntries[i].Size));
            });
        }
    }

    [Test]
    public Task PackNullFilenameTest()
    {
        var sourceEntries = new(string Name, int Size)[]
        {
            ("name.pdf", 100),
            (null, 200)
        };
        
        Assert.ThrowsAsync<EmptyFileNameArchiveException>(async() => await Zip.PackAsync(sourceEntries.Select(x => 
                new ZipItem(x.Name, GetByteArray(x.Size))).ToArray()));
        return Task.CompletedTask;
    }
    
    [Test]
    public Task PackEmptyFilenameTest()
    {
        var sourceEntries = new(string Name, int Size)[]
        {
            ("name.pdf", 100),
            (string.Empty, 200)
        };
        
        Assert.ThrowsAsync<EmptyFileNameArchiveException>(async() => await Zip.PackAsync(sourceEntries.Select(x => 
            new ZipItem(x.Name, GetByteArray(x.Size))).ToArray()));
        return Task.CompletedTask;
    }
    
    [Test]
    public Task PackNullDataTest()
    {
        Assert.ThrowsAsync<EmptyFileArchiveException>(async() => await Zip.PackAsync([
            new ZipItem("name", null)
        ]));
        return Task.CompletedTask;
    }
    
    [Test]
    public Task PackEmptyDataTest()
    {
        Assert.ThrowsAsync<EmptyFileArchiveException>(async() => await Zip.PackAsync([
            new ZipItem("name", GetByteArray(0))
        ]));
        return Task.CompletedTask;
    }
    
    [Test]
    public Task PackNothingTest()
    {
        Assert.ThrowsAsync<EmptyFileArchiveException>(async() => await Zip.PackAsync([]));
        return Task.CompletedTask;
    }

    private static byte[]  GetByteArray(int size)
    {
        var rnd = new Random();
        var bytes = new byte[size];
        rnd.NextBytes(bytes);
        return bytes;
    }
}