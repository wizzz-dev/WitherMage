using UnityEngine;
using UnityEngine.Rendering;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager Instance;
    [SerializeField] private AudioSource soundEffectsObject;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }
    public void PlaySoundFXClip(AudioClip audio,Transform transform,float volume)
    {
        AudioSource audioSource = Instantiate(soundEffectsObject, transform.position, Quaternion.identity);
        audioSource.pitch = 0.25f;//DELETE ME AFTER RECORDING
        audioSource.clip = audio;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

}
