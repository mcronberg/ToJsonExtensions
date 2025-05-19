# ToJsonExtensions

Extension methods for quickly serializing any object to JSON using `System.Text.Json`.

## Features

- `ToJsonString()` — convert any object to JSON
- `ToJsonFile()` / `ToJsonFileAsync()` — write JSON directly to file
- Optional indentation and naming policies
- **Defaults to PascalCase property names** (same as your C# properties)

## Example

```csharp
using ToJsonExtensions;

var person = new { FirstName = "Michell", Age = 50 };

// Compact JSON string (PascalCase)
string json = person.ToJsonString();
// json: {"FirstName":"Michell","Age":50}

// Indented
string pretty = person.ToJsonString(indented: true);

// Write to file
person.ToJsonFile("person.json", indented: true);

// With camelCase (if desired):
string camel = person.ToJsonString(namingPolicy: System.Text.Json.JsonNamingPolicy.CamelCase);
// camel: {"firstName":"Michell","age":50}
```

## Notes
- If you do not specify a namingPolicy, the JSON property names will match your C# property names (PascalCase).
- If you want camelCase, use `namingPolicy: JsonNamingPolicy.CamelCase`.