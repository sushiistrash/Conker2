using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int level = 1;

    public void LoadLevel()
    {
        SceneManager.LoadScene(level);
    }
}
