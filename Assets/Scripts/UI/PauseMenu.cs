using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseScreen;
    [HideInInspector] public bool IsPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePauseState();
        }

        Time.timeScale = IsPaused ? 0f : 1f;
    }

    public void LevelSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(6);
    }

    public void ChangePauseState()
    {
        IsPaused = !IsPaused;
        _pauseScreen.SetActive(IsPaused);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
