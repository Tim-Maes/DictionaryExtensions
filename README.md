# DictionaryExtensions

A collection of extension methods for Dictionary in C#.

## Installation :wrench: 

Install via NuGet:

```bash
Install-Package DictionaryExtensions
```

Or just copy whatever you need from this repository :) 

## Usage

`var result = dictionary.MethodName();`

## Features :star:

### Generic Dictionary methods

| Method Name                              | Description                                                                                              |
|------------------------------------------|----------------------------------------------------------------------------------------------------------|
| `AddIfNotExists<TKey, TValue>()`         | Adds a key-value pair to the dictionary if the key does not already exist.                               |
| `AddOrUpdate<TKey, TValue>()`            | Adds or updates a key-value pair in the dictionary. If the key exists, its value is updated.             |
| `AddRange<TKey, TValue>()`               | Adds a range of key-value pairs to the dictionary.                                                       |
| `AsReadOnly<TKey, TValue>()`             | Returns a read-only view of the dictionary.                                                              |
| `CombineWith<TKey, TValue, TResult>()`   | Combines the values of two dictionaries with the same keys using a specified selector function.          |
| `DeepCopy<TKey, TValue>()`               | Creates a deep copy of the dictionary if the values are of a reference type.                            |
| `FindKeysForValue<TKey, TValue>()`       | Finds all keys that have a given value.                                                                  |
| `Filter<TKey, TValue>()`                 | Filters the dictionary based on a predicate.                                                             |
| `ForEach<TKey, TValue>()`                | Performs an action on each key-value pair in the dictionary.                                             |
| `FromJson<TKey, TValue>()`               | Deserializes a JSON string to a dictionary.                                                              |
| `GetKeys<TKey, TValue>()`                | Retrieves all keys as a list.                                                                            |
| `GetOrAdd<TKey, TValue>()`               | Gets a value from the dictionary safely. If the key does not exist, initializes a new value.             |
| `GetValues<TKey, TValue>()`              | Retrieves all values as a list.                                                                          |
| `GetValueOrDefault<TKey, TValue>()`      | Tries to get a value from the dictionary. Returns a default value if the key is not found.               |
| `HasDuplicateValues<TKey, TValue>()`     | Checks if the dictionary contains any duplicate values.                                                  |
| `Increment<TKey>()`                      | Increments the value of a key if it exists, otherwise adds the key with a specified initial value.       |
| `Invert<TKey, TValue>()`                 | Inverts the dictionary, swapping keys with values. Assumes that the values are unique.                    |
| `MapValues<TKey, TValue, TResult>()`     | Transforms the values of the dictionary using a specified function, leaving keys unchanged.              |
| `Merge<TKey, TValue>()`                  | Merges another dictionary into the current dictionary. Existing keys will be updated with the new values.|
| `RemoveWhere<TKey, TValue>()`            | Removes entries from the dictionary that match the predicate.                                            |
| `SortByKey<TKey, TValue>()`              | Sorts the dictionary by its keys.                                                                        |
| `SortByValue<TKey, TValue>()`            | Sorts the dictionary by its values.                                                                      |
| `ToJson<TKey, TValue>()`                 | Serializes the dictionary to a JSON string.                                                              |
| `ToConcurrentDictionary<TKey, TValue>()` | Converts the Dictionary to a ConcurrentDictionary.                                                       |
| `ToList<TKey, TValue>()`                 | Converts the dictionary to a List of KeyValuePair.                                                       |
| `ToQueryString<TKey, TValue>()`          | Converts the dictionary into a query string format.                                                      |
| `TransformValues<TKey, TValue, TResult>()`| Applies a transformation to the values of the dictionary.                                                |
| `TryAdd<TKey, TValue>()`                 | Attempts to add a key-value pair to the dictionary and returns a boolean indicating whether it was successful. |

## License

[MIT](https://opensource.org/licenses/MIT)
