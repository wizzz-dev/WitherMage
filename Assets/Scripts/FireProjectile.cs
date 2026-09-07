using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class FireProjectile : MonoBehaviour
{
    private float speed = 20;
    private float attackRange = 20f;
    private float lifeSpan = 10f;
    private GameObject[] entities;
    private Transform target;
    private Vector3 origin;
    private GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
        FindEntities();
        origin = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(origin, target.position) < attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.LookAt(target);
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Vector3.Distance(origin, transform.position) > lifeSpan)
        {
            Destroy(gameObject);
        }
    }
    private void FindEntities()
    {
        entities = GameObject.FindGameObjectsWithTag("entity").ToList().OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).ToArray();
        if (entities[0] != player)
        {
            target = player.transform;
        }
        else
        {
            target = entities[1].transform;
        }
    }
}
