using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class meu : PersistentSingleton<meu>
{
    [SerializeField] private Button savebtn;
    [SerializeField] private Button loadbtn;
    private void Start()
    {
        if (savebtn == null || loadbtn == null)
        {
            Debug.LogError("Buttons not assigned in the Inspector for 'meu' script!");
            return;
        }

        savebtn.onClick.AddListener(() =>
        {
            SaveLoadSystem.Instance.gameData.fileName = "Menu";
            SaveLoadSystem.Instance.gameData.sceneName = "SampleScene";
            SaveLoadSystem.Instance.SaveGame();
        });

        loadbtn.onClick.AddListener(() =>
        {
            SaveLoadSystem.Instance.LoadGame("Menu");
        });
    }
}
