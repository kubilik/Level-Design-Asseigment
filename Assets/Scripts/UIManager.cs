using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Playground 1");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
