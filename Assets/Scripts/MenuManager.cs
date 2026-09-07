using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject customCursor;


    //label text for gameOver UIToolkit menu
    public string deathText;

    //misc
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemies;
    [SerializeField] private PlayerController playerController;
    private GameObject currentMenu;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
 
    private void Start()
    {
        MainMenu();
    }
    public void MainMenu()//already going to be active on game start
    {
        player.SetActive(false);
        enemies.SetActive(false);
    }

    public void PauseGame()
    {
        FreezeTime();
        pauseMenu.SetActive(true);
        customCursor.SetActive(true);
        currentMenu = pauseMenu;
    }
    public void QuitGame()
    {

        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;// for use in editor
    }
    public void ResumeGame()
    {
        RestartTime();
        pauseMenu.SetActive(false);
        customCursor.SetActive(false);
    }
    public void RestartGame()
    {
        currentMenu.SetActive(false);
        mainMenu.SetActive(true);
        GameManager.Instance.ResetPositionsAndValues();
        MainMenu(); 
        if(BackgroundMusicManager.Instance.mainMusic.volume<0.1f)
        {
            BackgroundMusicManager.Instance.ResumeBackgroundMusic();
        }

    }
    public void GameOver()
    {
        deathText = playerController.deathReason;
        gameOverScreen.SetActive(true);
        customCursor.SetActive(true);
        currentMenu = gameOverScreen;
        BackgroundMusicManager.Instance.StopBackgroundMusic();
    }
    public void EndGame()
    {
        endScreen.SetActive(true);
        customCursor.SetActive(true);
        currentMenu = endScreen;
    }
    private void FreezeTime()
    {
        Time.timeScale = 0f;
        playerController.DisableControlls();
    }
    private void RestartTime()
    {
        Time.timeScale = 1f;
        playerController.EnableControlls();
    }
    public void SpawnEntities()
    {
        player.SetActive(true);
        enemies.SetActive(true);
        playerController = player.GetComponent<PlayerController>();
    }
}
