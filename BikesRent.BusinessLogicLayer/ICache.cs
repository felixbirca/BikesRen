namespace BikesRent.BusinessLogicLayer;

public interface ICache
{
    T Get<T>(string key);
    void Set<T>(string key, T value, int? cacheTime = null);
    bool IsSet(string key);
    void Remove(string key);
    void RemoveByPattern(string pattern);
    void Clear();
}