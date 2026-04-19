using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Game Data Event Channel")]
public class GameDataEventChannel : ScriptableObject
{
    public UnityAction<GameData> OnEventRaised;

    public void RaiseEvent(GameData value)
    {
        OnEventRaised?.Invoke(value);
    }
}