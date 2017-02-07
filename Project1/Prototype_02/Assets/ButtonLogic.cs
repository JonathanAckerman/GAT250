using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonLogic : MonoBehaviour {

	public void OnPlay()
    {
        SceneManager.LoadScene("DefaultScene");
    }
    public void OnHowTo()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public void OnQuit()
    {
        Application.Quit();
    }
    public void OnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
