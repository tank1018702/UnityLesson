using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public  ParticleSystem particle;

    public AudioClip audio;
    
	
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="Player"||other.transform.tag=="Enemy") 
        {
            if (particle)
            {
                particle.Play();
                AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position,0.2f);
            }
        }
        
    }
}
