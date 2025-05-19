# ToJsonExtensions

Extension methods for quickly serializing any object to JSON using `System.Text.Json`.

## Features

- `ToJsonString()` — convert any object to JSON
- `ToJsonFile()` / `ToJsonFileAsync()` — write JSON directly to file
- Optional indentation and naming policies
- Defaults to `camelCase` naming

## Example

```csharp
using ToJsonExtensions;

var person = new { FirstName = "Michell", Age = 50 };

// Compact JSON string
string json = person.ToJsonString();

// Indented
string pretty = person.ToJsonString(indented: true);

// Write to file
person.ToJsonFile("person.json", indented: true);
```