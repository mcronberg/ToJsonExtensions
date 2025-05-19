namespace ToJsonExtensions;

using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

public static class JsonExtensions
{
    /// <summary>
    /// Serializes the object to a JSON string.
    /// </summary>
    /// <param name="obj">The object to serialize.</param>
    /// <param name="indented">Whether to indent the JSON output (default: false).</param>
    /// <param name="namingPolicy">The naming policy to apply (default: camelCase).</param>
    /// <returns>A JSON-formatted string.</returns>
    public static string ToJsonString(this object? obj, bool indented = false, JsonNamingPolicy? namingPolicy = null)
    {
        if (obj == null)
            return "null";

        namingPolicy ??= JsonNamingPolicy.CamelCase;

        var options = new JsonSerializerOptions
        {
            WriteIndented = indented,
            PropertyNamingPolicy = namingPolicy
        };

        return JsonSerializer.Serialize(obj, obj.GetType(), options);
    }

    /// <summary>
    /// Asynchronously writes the JSON serialization of the object to a stream.
    /// </summary>
    /// <param name="obj">The object to serialize.</param>
    /// <param name="stream">The target stream to write JSON to.</param>
    /// <param name="indented">Whether to indent the JSON output (default: false).</param>
    /// <param name="namingPolicy">The naming policy to apply (default: camelCase).</param>
    public static async Task ToJsonStreamAsync(this object obj, Stream stream, bool indented = false, JsonNamingPolicy? namingPolicy = null)
    {
        namingPolicy ??= JsonNamingPolicy.CamelCase;

        var options = new JsonSerializerOptions
        {
            WriteIndented = indented,
            PropertyNamingPolicy = namingPolicy
        };

        await JsonSerializer.SerializeAsync(stream, obj, obj.GetType(), options);
    }

    /// <summary>
    /// Serializes the object and writes it to a file asynchronously.
    /// </summary>
    /// <param name="obj">The object to serialize.</param>
    /// <param name="path">The full file path to write to.</param>
    /// <param name="indented">Whether to indent the JSON output (default: false).</param>
    /// <param name="namingPolicy">The naming policy to apply (default: camelCase).</param>
    public static async Task ToJsonFileAsync(this object obj, string path, bool indented = false, JsonNamingPolicy? namingPolicy = null)
    {
        using var stream = File.Create(path);
        await obj.ToJsonStreamAsync(stream, indented, namingPolicy);
    }

    /// <summary>
    /// Serializes the object and writes it to a file synchronously.
    /// </summary>
    /// <param name="obj">The object to serialize.</param>
    /// <param name="path">The full file path to write to.</param>
    /// <param name="indented">Whether to indent the JSON output (default: false).</param>
    /// <param name="namingPolicy">The naming policy to apply (default: camelCase).</param>
    public static void ToJsonFile(this object obj, string path, bool indented = false, JsonNamingPolicy? namingPolicy = null)
    {
        var json = obj.ToJsonString(indented, namingPolicy);
        File.WriteAllText(path, json);
    }
}
