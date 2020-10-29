using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public AudioClip mainMenuMusic;
    
    private void Start()
    {
        AudioManager.Instance.ChangeMusic(mainMenuMusic);
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene(1);
    }
}
