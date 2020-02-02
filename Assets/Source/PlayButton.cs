using Source;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public Button playButton;
    public Button restartButton;

    private void Start()
    {
        this.playButton.onClick.AddListener(() => this.Play());
        this.restartButton.onClick.AddListener(() => this.Reset());
    }


    public void Play()
    {
        GameMode.instance.Play();
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }
}
