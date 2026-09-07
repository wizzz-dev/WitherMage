using UnityEngine;

public class WitherSpriteController : MonoBehaviour
{
    public Transform[] waypoints;//exposed array of GameObjects plotting patroll path
    private int target;//iterator for waypoint array
    private float speed = 8f;//movement speed
    private Vector3 targetCoordinates;
    public string gameState = "patrol";

    public Rigidbody rb;//allows application of forces
    public readonly float maxHealth = 12;
    public float currentHealth;
    private float knockBack = 8f;
    private float hitHeight = 3f;
    public Vector3 startPosition;
    public Quaternion startRotation;

    public GameObject projectile;//attack preFab
    public GameObject damageEffect;
    public HealthBarScript healthbar;
    private float timer = 0f;//timer for stunned period and player out of range
    private float attackTimer = 0f;

    //below for pursuing state
    private GameObject player;
    private float playerDistance;
    private float sightDistance = 10;
    private Vector3 attackSpawn;

    public AudioClip hurtAudio;

    void Start()
    {
        //start transforms for restart
        startPosition = transform.position;
        startRotation = transform.rotation;
        targetCoordinates = waypoints[target].position;
        player = GameObject.Find("Player");

        //sets up healthbar but sets as inactive until player within range
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);
        healthbar.gameObject.SetActive(false);
    }


    void Update()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);

        //gamestate management
        if(playerDistance < sightDistance)
        {
            gameState = "pursuing";
            
        }
        if(gameState=="pursuing")
        {
            healthbar.gameObject.SetActive(true);
            attackSpawn = transform.position + (transform.forward * 2);
            attackTimer += Time.deltaTime;
            if (attackTimer > 5f)
            {
                Instantiate(projectile, attackSpawn, transform.rotation);
                attackTimer = 0f;
            }
            targetCoordinates = player.transform.position;
            if (playerDistance < sightDistance &&gameState!="stunned")
            {
                timer = 0f;
                if (playerDistance < 5)
                {
                    targetCoordinates = transform.position;
                }
            }
            if (playerDistance < 5)
            {
                targetCoordinates = transform.position;
                transform.LookAt(player.transform);
            }
            else
            {
                timer += Time.deltaTime;
                if (timer > 3f)
                {
                    gameState = "patrol";
                    healthbar.gameObject.SetActive(false);
                    targetCoordinates = waypoints[target].position;
                }
            }
        }
        
        if (gameState == "stunned")
        {
            transform.Rotate(Vector3.up, 1f * Time.deltaTime);
            timer += Time.deltaTime;
            if (timer > 2.5f)
            {
                timer = 0f;
                if(playerDistance<sightDistance)
                {
                    gameState = "pursuing";
                }
                else
                {
                    gameState = "patrol";
                    healthbar.gameObject.SetActive(false);
                    targetCoordinates = waypoints[target].position;
                }
            }

        }
        else if(gameState!="stunned")
        {
            transform.position = Vector3.MoveTowards(transform.position, targetCoordinates, speed * Time.deltaTime);
            transform.LookAt(targetCoordinates);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "waypoint" && gameState == "patrol")
            //automates movement for enemy sprite
        {
            if (target < waypoints.Length - 1)
            {
                target++;
            }
            else
            {
                target = 0;
            }
            targetCoordinates = waypoints[target].position;

        }
        if (other.tag == "Projectile")
        {
            gameState = "stunned";
            Instantiate(damageEffect, transform.position, Quaternion.identity);
            SoundEffectsManager.Instance.PlaySoundFXClip(hurtAudio, transform, 1);
            Destroy(other.gameObject);
            currentHealth -= 5;
            healthbar.UpdateHealthBar(maxHealth, currentHealth);
            KnockBack(other);
            if (currentHealth < 1)
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void KnockBack(Collider other)
    {
        rb.AddForce(other.transform.forward * knockBack, ForceMode.Impulse);
        rb.AddForce(Vector3.up * hitHeight, ForceMode.Impulse);
    }
}
