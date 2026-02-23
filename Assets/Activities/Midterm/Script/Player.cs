using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int playerFolCount;
    [SerializeField] EnemyAI enemyAI;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("EnemyLeader"))
        {
            if (playerFolCount > enemyAI.enemyFolCount)
            {
                winPanel.SetActive(true);
                Time.timeScale = 0.1f;
            }
            else if (playerFolCount < enemyAI.enemyFolCount)
            {
                losePanel.SetActive(true);
                Time.timeScale = 0.1f;
            }

        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}