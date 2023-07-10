using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    Animator alienAnimator;

    [SerializeField]
    AudioSource audioSource;

    [Header("Alien Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 5f;
    [SerializeField] float aggroRange = 25f;

    [SerializeField]
    float alienHealth = 3;

    
    public GameObject vrPlayer;
    
    NavMeshAgent alienNavMesh;
    float timePassed;
    float newDestinationCD = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        alienAnimator = GetComponent<Animator>();
        alienNavMesh = GetComponent<NavMeshAgent>();

        StartCoroutine(AlienVoices(0));
        //TakeDamage(3);
    }

    // Update is called once per frame
    void Update()
    {
        alienAnimator.SetFloat("Speed", alienNavMesh.velocity.magnitude / alienNavMesh.speed);
        if(timePassed > attackCD)
        {
            if(Vector3.Distance(vrPlayer.transform.position, transform.position) <= attackRange)
            {
                alienAnimator.SetTrigger("Attack");
                timePassed = 0;
            }
        }
        timePassed += Time.deltaTime;

        if(newDestinationCD <= 0 && Vector3.Distance(vrPlayer.transform.position, transform.position) <= aggroRange)
        {
            newDestinationCD = 0.5f;
            alienNavMesh.SetDestination(vrPlayer.transform.position);
        }
        newDestinationCD -= Time.deltaTime;
        Vector3 targetPosition = new Vector3(vrPlayer.transform.position.x, transform.position.y, vrPlayer.transform.position.z);
        transform.LookAt(targetPosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

    public void TakeDamage(float damageAmount)
    {
        alienHealth -= damageAmount;
        alienAnimator.SetTrigger("Damage");

        if(alienHealth <= 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        alienAnimator.SetTrigger("Death");
        yield return null;
    }


    IEnumerator AlienVoices(int i)
    {
        for(i = 0; i < 10; i++)
        {
            audioSource.Play();
            yield return new WaitForSeconds(7);
            i++;
        }
        
    }
}
