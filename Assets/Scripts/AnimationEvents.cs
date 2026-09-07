using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private PlayerController playerControllerScript;
    void Start()
    {
        playerControllerScript = GetComponentInParent<PlayerController>();
    }
    void Update()
    {
        
    }
    public void Cast()
    {
        Instantiate(playerControllerScript.projectile, playerControllerScript.attackSpawn, transform.rotation);
        SoundEffectsManager.Instance.PlaySoundFXClip(playerControllerScript.castMagicAudio, transform, 1);

    }
    public void CastDone()
    {
        playerControllerScript.anim.SetBool("isAttacking", false);
    }
    void Landed()
    {
        playerControllerScript.anim.SetBool("isJumping", false);
    }
    void PlaySound()
    {
        SoundEffectsManager.Instance.PlaySoundFXClip(playerControllerScript.walkingAudio, transform, 1);
    }
}
