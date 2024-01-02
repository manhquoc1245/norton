using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Dừng thời gian
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // Khôi phục thời gian
            pauseMenu.SetActive(false);
        }
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Khôi phục thời gian
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Khôi phục thời gian
        SceneManager.LoadScene("MainMenu"); // Đổi tên Scene chính của bạn
    }

}
