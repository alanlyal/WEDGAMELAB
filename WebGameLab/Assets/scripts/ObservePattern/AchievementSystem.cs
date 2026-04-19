using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    [SerializeField] private VoidEventChannel voidEvent;
    [SerializeField] private GameDataEventChannel GameDataChannel;
    [SerializeField] private FloatEventChannel flaotEventChannel;
    private int achievementjumps = 10;
    private int currentjumps = 0;
    private void OnEnable()
    {
        voidEvent.OnEventRaised += EventCalled;
        GameDataChannel.OnEventRaised += GameDataEventCalled;
    }

    private void OnDisable()
    {
        voidEvent.OnEventRaised -= EventCalled;
        GameDataChannel.OnEventRaised -= GameDataEventCalled;
    }

    private void EventCalled()
    {
        Debug.Log("event called");
        currentjumps++;
        if (currentjumps == achievementjumps)
        {
            Debug.Log("achievment completed");
        }
    }
    public void GameDataEventCalled(GameData data)
    {
        Debug.Log("gameData called " + data.fileName );
    }
}
