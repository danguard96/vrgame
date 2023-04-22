using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 30f;

    Transform target;
    NavMeshAgent agent;
    public float health;

    AudioSource sound;

    public float timeBetweenAttacks = 1.5f;
    bool alreadyAttacked;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
        }

        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();
        }

        AttackPlayer();
    }

    void FaceTarget()
    {
        Vector3 direction  = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
    }

    private void AttackPlayer()
    {
        if (!alreadyAttacked)
        {
            sound.Play();
            GameObject spawnedBullet = Instantiate(bullet);
            spawnedBullet.tag = "Bullet";
            spawnedBullet.transform.position = spawnPoint.position;
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void onDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
