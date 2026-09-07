using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //Input actions handling player input
    public InputAction moveAction;
    public InputAction jump;
    public InputAction cast;
    private Vector2 moveInput;
    public InputAction pauseGame;

    //stats
    public float maxHealth = 100f;
    public float currentHealth;
    private float speed = 10f;
    private float fallSpeed = 20f;
    private float jumpForce = 7f;
    private float knockBack = 8f;
    private float hitHeight = 3f;
    public Vector3 attackSpawn;
    public Vector3 startPosition;
    public Quaternion startRotation;
    public string deathReason;

    //slot for projectile prefab
    public GameObject projectile;
    public GameObject damageEffect;
    public HealthBarScript healthbar;

    //players rigidbody for force applying
    private Rigidbody rb;


    //sprite animator
    public Animator anim;

    //boolean checking if player is on ground
    private bool onGround;

    //AudioClips
    public AudioClip walkingAudio;
    public AudioClip castMagicAudio;
    public AudioClip hurtAudio;
    public AudioClip jumpAudio;
    public AudioClip healthPotionAudio;

    
    void OnEnable()
    {
        EnableControlls();
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);
        startPosition = transform.position;
        startRotation = transform.rotation;
    }
    void OnDisable()
    {
        DisableControlls();
    }
    private void Start()
    {
        SpawnIn();
    }
    void SpawnIn()
    {
        transform.Translate(Vector3.up * 10f);
        rb.AddForce(Vector3.down * 50f, ForceMode.Impulse);
    }
    private void Update()
    {
        //movement based on user input
        moveInput = moveAction.ReadValue<Vector2>();
        
        //Up and down
        transform.Translate(Vector3.right * speed * Time.deltaTime * moveInput.x, Space.World);
        //Left and right
        transform.Translate(Vector3.forward * speed * Time.deltaTime * moveInput.y, Space.World);
        anim.SetBool("isWalking", moveInput != Vector2.zero ? true : false);

        //position projectile spawned in at
        attackSpawn = transform.position + (transform.forward * 2) + (transform.up * 2);
      
        if (cast.triggered)
        {
            anim.SetBool("isAttacking", true);
        }
        //jumping and falling physics when input recieved
        if (jump.triggered && onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            SoundEffectsManager.Instance.PlaySoundFXClip(jumpAudio, transform, 1);
            onGround = false;
            anim.SetBool("isJumping", true);
        }
        if (rb.linearVelocity.y < 0)
        {
            rb.AddForce(Vector3.down * fallSpeed);
        }
        if(pauseGame.triggered)
        {
            MenuManager.Instance.PauseGame();
        }
        if(currentHealth<1)
        {
            deathReason = "You fell to the dark wither magic skulking within the dungeon";
            GameOver();
        }
       

    }
    private void LateUpdate()
    {
        if (moveInput != Vector2.zero)
        {
            Vector3 lookDirection = new Vector3(moveInput.x, 0, moveInput.y);
            transform.rotation = Quaternion.LookRotation(lookDirection);          
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "EnemyProjectile(Clone)")//change to enemy projectile so no collision with own
        {
            Destroy(other.gameObject);
            SoundEffectsManager.Instance.PlaySoundFXClip(hurtAudio, transform, 1);
            Instantiate(damageEffect, transform.position, Quaternion.identity);
            currentHealth -= 5;
            healthbar.UpdateHealthBar(maxHealth, currentHealth);
            KnockBack(other);
        }
        if (other.name == "Ground")
        {
            onGround = true;
        }
        if(other.name== "EndTrigger")
        {
            MenuManager.Instance.EndGame();
            DisableControlls();
        }
        if(other.name=="HealthPotion")
        {
            SoundEffectsManager.Instance.PlaySoundFXClip(healthPotionAudio, transform, 1);
            currentHealth += 5;
            healthbar.UpdateHealthBar(maxHealth, currentHealth);
        }
        if (other.name == "FallTrigger")
        {
            deathReason = "Your fell to your death from the platform edge";
            GameOver();
        }
    }
    private void GameOver()
    {
        MenuManager.Instance.GameOver();
        gameObject.SetActive(false);
    }
    public void EnableControlls()
    {
        moveAction.Enable();
        jump.Enable();
        cast.Enable();
        pauseGame.Enable();
    }
    public void DisableControlls()
    {
        moveAction.Disable();
        jump.Disable();
        cast.Disable();
        pauseGame.Disable();
    }
    private void KnockBack(Collider other)
    {
        rb.AddForce(other.transform.forward* knockBack, ForceMode.Impulse);
        rb.AddForce(Vector3.up * hitHeight, ForceMode.Impulse);
    }
}
