using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public string gameOverSceneName;
    public float delay = 0.5f;
    public GameObject fadeout;

    private bool playerInsideTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = true;
            fadeout.SetActive(true);
            Invoke("LoadGameOverScene", delay);
        }
    }

    private void LoadGameOverScene()
    {
        if (playerInsideTrigger)
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
    }
}
