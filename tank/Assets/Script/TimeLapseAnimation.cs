using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLapseAnimation : MonoBehaviour
{
    public float TimeLapse = 0.5f;

    public AudioClip ExplosionAudio;
	
	void Start ()
    {
		
	}
	
	void Update ()
    {
        AudioSource.PlayClipAtPoint(ExplosionAudio, transform.position);
        TimeLapse -= Time.deltaTime;
        if(TimeLapse<=0)
        {
            TimeLapse = 0.5f;
            
            Destroy(gameObject);
        }
		
	}
}
