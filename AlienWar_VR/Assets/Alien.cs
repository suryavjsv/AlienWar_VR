using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    Animator alienAnimator;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    float alienHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        alienAnimator = GetComponent<Animator>();
        StartCoroutine(AlienVoices(0));
        //TakeDamage(3);
    }

    // Update is called once per frame
    void Update()
    {
        
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
