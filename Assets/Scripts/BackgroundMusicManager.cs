using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance;
    public AudioSource mainMusic;
    private bool stopping = true;
    private bool starting;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        
    }
    void Update()
    {
        if(stopping)
        {
            mainMusic.volume = Mathf.Lerp(mainMusic.volume, 0f, 0.5f * Time.deltaTime);
            if (mainMusic.volume == 0f)
            {
                stopping = false;
            }
        }
        if (starting)
        {
            mainMusic.volume = Mathf.Lerp(mainMusic.volume, 1f, 0.5f * Time.deltaTime);
            if (mainMusic.volume == 1f)
            {
                starting = false;
            }
        }
    }
    public void StopBackgroundMusic()
    {
        stopping = true;
    }
    public void ResumeBackgroundMusic()
    {
        starting = true;
    }

}
