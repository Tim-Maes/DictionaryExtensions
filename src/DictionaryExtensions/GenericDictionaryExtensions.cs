using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace DictionaryExtensions;

public static class DictionaryExtensions
{
    /// <summary>
    /// Adds a key-value pair to the dictionary if the key does not already exist.
    /// </summary>
    public static void AddIfNotExists<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (!dictionary.ContainsKey(key))
        {
            dictionary.Add(key, value);
        }
    }

    /// <summary>
    /// Tries to get a value from the dictionary. Returns a default value if the key is not found.
    /// </summary>
    public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default)
    {
        return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
    }

    /// <summary>
    /// Merges another dictionary into the current dictionary. 
    /// Existing keys in the current dictionary will be updated with the new values.
    /// </summary>
    public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TValue> other)
    {
        foreach (var kvp in other)
        {
            dictionary[kvp.Key] = kvp.Value;
        }
    }

    /// <summary>
    /// Inverts the dictionary, swapping keys with values. Assumes that the values are unique.
    /// </summary>
    public static IDictionary<TValue, TKey> Invert<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        var invertedDict = new Dictionary<TValue, TKey>();
        foreach (var kvp in dictionary)
        {
            invertedDict.Add(kvp.Value, kvp.Key);
        }
        return invertedDict;
    }

    /// <summary>
    /// Filters the dictionary based on a predicate.
    /// </summary>
    public static IDictionary<TKey, TValue> Filter<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, TValue, bool> predicate)
    {
        var filteredDict = new Dictionary<TKey, TValue>();
        foreach (var kvp in dictionary)
        {
            if (predicate(kvp.Key, kvp.Value))
            {
                filteredDict.Add(kvp.Key, kvp.Value);
            }
        }
        return filteredDict;
    }

    /// <summary>
    /// Converts the dictionary to a List of KeyValuePair.
    /// </summary>
    public static List<KeyValuePair<TKey, TValue>> ToList<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return dictionary.ToList();
    }

    /// <summary>
    /// Adds or updates a key-value pair in the dictionary. If the key exists, its value is updated.
    /// </summary>
    public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        dictionary[key] = value;
    }

    /// <summary>
    /// Removes entries from the dictionary that match the predicate.
    /// </summary>
    public static void RemoveWhere<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, TValue, bool> predicate)
    {
        var keysToRemove = dictionary.Where(kvp => predicate(kvp.Key, kvp.Value)).Select(kvp => kvp.Key).ToList();
        foreach (var key in keysToRemove)
        {
            dictionary.Remove(key);
        }
    }

    /// <summary>
    /// Increments the value of a key if it exists, otherwise adds the key with a specified initial value.
    /// </summary>
    public static void Increment<TKey>(this IDictionary<TKey, int> dictionary, TKey key, int initialValue = 1)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key]++;
        }
        else
        {
            dictionary[key] = initialValue;
        }
    }

    /// <summary>
    /// Combines the values of two dictionaries with the same keys using a specified selector function.
    /// </summary>
    public static IDictionary<TKey, TResult> CombineWith<TKey, TValue, TResult>(
        this IDictionary<TKey, TValue> first,
        IDictionary<TKey, TValue> second,
        Func<TValue, TValue, TResult> combiner)
    {
        var combined = new Dictionary<TKey, TResult>();
        foreach (var key in first.Keys)
        {
            if (second.ContainsKey(key))
            {
                combined[key] = combiner(first[key], second[key]);
            }
        }
        return combined;
    }

    /// <summary>
    /// Serializes the dictionary to a JSON string.
    /// </summary>
    public static string ToJson<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return System.Text.Json.JsonSerializer.Serialize(dictionary);
    }

    /// <summary>
    /// Deserializes a JSON string to a dictionary.
    /// </summary>
    public static IDictionary<TKey, TValue> FromJson<TKey, TValue>(string json)
    {
        return System.Text.Json.JsonSerializer.Deserialize<IDictionary<TKey, TValue>>(json);
    }

    /// <summary>
    /// Converts the dictionary into a query string format.
    /// </summary>
    public static string ToQueryString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return string.Join("&", dictionary.Select(kvp => $"{Uri.EscapeDataString(kvp.Key.ToString())}={Uri.EscapeDataString(kvp.Value.ToString())}"));
    }

    /// <summary>
    /// Gets a value from the dictionary safely. If the key does not exist, initializes a new value.
    /// </summary>
    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory)
    {
        if (!dictionary.TryGetValue(key, out var value))
        {
            value = valueFactory();
            dictionary[key] = value;
        }
        return value;
    }

    /// <summary>
    /// Sorts the dictionary by its keys.
    /// </summary>
    public static IDictionary<TKey, TValue> SortByKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return new SortedDictionary<TKey, TValue>(dictionary);
    }

    /// <summary>
    /// Sorts the dictionary by its values.
    /// </summary>
    public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> SortByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return dictionary.OrderBy(kvp => kvp.Value);
    }

    /// <summary>
    /// Checks if the dictionary contains any duplicate values.
    /// </summary>
    public static bool HasDuplicateValues<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return dictionary.Values.Count() != dictionary.Values.Distinct().Count();
    }

    /// <summary>
    /// Returns a read-only view of the dictionary.
    /// </summary>
    public static IReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return new ReadOnlyDictionary<TKey, TValue>(dictionary);
    }

    /// <summary>
    /// Applies a transformation to the values of the dictionary.
    /// </summary>
    public static IDictionary<TKey, TResult> TransformValues<TKey, TValue, TResult>(
        this IDictionary<TKey, TValue> dictionary,
        Func<TValue, TResult> transformer)
    {
        return dictionary.ToDictionary(kvp => kvp.Key, kvp => transformer(kvp.Value));
    }

    /// <summary>
    /// Gets the value associated with the specified key or adds a new key-value pair to the dictionary with the provided value if the key does not exist.
    /// </summary>
    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (!dictionary.TryGetValue(key, out TValue existingValue))
        {
            dictionary[key] = value;
            return value;
        }
        return existingValue;
    }

    /// <summary>
    /// Adds a range of key-value pairs to the dictionary.
    /// </summary>
    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> pairs)
    {
        foreach (var pair in pairs)
        {
            dictionary.Add(pair.Key, pair.Value);
        }
    }


    /// <summary>
    /// Attempts to add a key-value pair to the dictionary and returns a boolean indicating whether the addition was successful.
    /// </summary>
    public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (!dictionary.ContainsKey(key))
        {
            dictionary.Add(key, value);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Retrieves all keys as a list.
    /// </summary>
    public static List<TKey> GetKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return dictionary.Keys.ToList();
    }

    /// <summary>
    /// Retrieves all values as a list;
    /// </summary>
    public static List<TValue> GetValues<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return dictionary.Values.ToList();
    }

    /// <summary>
    /// Finds all keys that have a given value.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IEnumerable<TKey> FindKeysForValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
    {
        return dictionary.Where(pair => EqualityComparer<TValue>.Default.Equals(pair.Value, value)).Select(pair => pair.Key);
    }

    /// <summary>
    /// Transforms the values of the dictionary using a specified function, leaving keys unchanged.
    /// </summary>
    public static IDictionary<TKey, TResult> MapValues<TKey, TValue, TResult>(
        this IDictionary<TKey, TValue> dictionary,
        Func<TValue, TResult> transformer)
    {
        return dictionary.ToDictionary(pair => pair.Key, pair => transformer(pair.Value));
    }

    /// <summary>
    /// Creates a deep copy of the dictionary if the values are of a reference type.
    /// </summary>
    public static IDictionary<TKey, TValue> DeepCopy<TKey, TValue>(this IDictionary<TKey, TValue> original)
        where TValue : ICloneable
    {
        var newDictionary = new Dictionary<TKey, TValue>();
        foreach (var kvp in original)
        {
            newDictionary.Add(kvp.Key, (TValue)kvp.Value.Clone());
        }
        return newDictionary;
    }

    /// <summary>
    /// Performs an action on each key-value pair in the dictionary.
    /// </summary>
    public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<TKey, TValue> action)
    {
        foreach (var kvp in dictionary)
        {
            action(kvp.Key, kvp.Value);
        }
    }
    /// <summary>
    /// Converts the Dictionary to a ConcurrentDictionary.
    /// </summary>
    public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return new ConcurrentDictionary<TKey, TValue>(dictionary);
    }
}