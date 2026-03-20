using System.Collections;
using UnityEngine.Events;
public interface ISerializer
{
    string Serialize<T>(T obj);
    T Deserialize<T>(string json);
}
