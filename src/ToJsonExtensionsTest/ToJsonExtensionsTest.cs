namespace JsonExtensions;

using System;
using System.IO;
using System.Text.Json;
using Xunit;
using ToJsonExtensions;

public class JsonExtensionsTests
{
    private class Person
    {
        public string FirstName { get; set; } = string.Empty;
        public int Age { get; set; }
    }

    [Fact]
    public void ToJsonString_ReturnsValidJson()
    {
        var person = new Person { FirstName = "Michell", Age = 50 };
        string json = person.ToJsonString();

        Assert.Contains("\"FirstName\"", json);  // PascalCase by default
        Assert.Contains("50", json);
    }

    [Fact]
    public void ToJsonString_WithIndentedTrue_ProducesFormattedJson()
    {
        var person = new Person { FirstName = "Michell", Age = 50 };
        string json = person.ToJsonString(indented: true);

        Assert.Contains(Environment.NewLine, json); // Pretty-printed JSON
    }

    [Fact]
    public void ToJsonString_WithNullObject_ReturnsNullString()
    {
        object? obj = null;
        string json = JsonExtensions.ToJsonString((object?)obj);
        Assert.Equal("null", json);
    }

    [Fact]
    public void ToJsonFile_WritesToDisk()
    {
        var person = new Person { FirstName = "Test", Age = 42 };
        string path = Path.Combine(Path.GetTempPath(), "test_person.json");

        person.ToJsonFile(path, indented: true);

        Assert.True(File.Exists(path));
        string content = File.ReadAllText(path);
        Assert.Contains("\"FirstName\"", content);
    }

    [Fact]
    public async Task ToJsonFileAsync_WritesToDisk()
    {
        var person = new Person { FirstName = "Async", Age = 99 };
        string path = Path.Combine(Path.GetTempPath(), "test_person_async.json");

        await person.ToJsonFileAsync(path, indented: false);

        Assert.True(File.Exists(path));
        string content = File.ReadAllText(path);
        Assert.Contains("async", content.ToLower());
    }

    [Fact]
    public void ToJsonString_UsesPropertyNames_WhenNoNamingPolicy()
    {
        var person = new Person { FirstName = "Michell", Age = 50 };
        string json = person.ToJsonString(namingPolicy: null);
        Assert.Contains("FirstName", json); // Skal være med stort F
        Assert.Contains("50", json);
    }

    [Fact]
    public async Task ToJsonStreamAsync_WithNullObject_WritesNull()
    {
        using var stream = new MemoryStream();
        await JsonExtensions.ToJsonStreamAsync((object?)null, stream);
        stream.Position = 0;
        using var reader = new StreamReader(stream);
        string result = reader.ReadToEnd();
        Assert.Equal("null", result);
    }
}
