using UnityEngine;
using UnityEngine.SceneManagement;
public class meu : MonoBehaviour
{
    public void startGame() 
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
    }
    public void StartGameSingle()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
