using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            StartCoroutine(EndLevelCoroutine(player));
        }
    }

    private IEnumerator EndLevelCoroutine(Player player)
    {
        AudioSystem.Instance.Play("Level Victory");
        player.StopInput = true;
        _camera.Follow = null;

        UISystem.Instance.ShowLevelCompleteText();
        yield return new WaitForSeconds(2f);

        UISystem.Instance.FadeToBlack();
        yield return new WaitForSeconds((1f / UISystem.Instance.FadeSpeed) + .25f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
