using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePoint : MonoBehaviour
{
    public AudioClip audio;

    public ParticleSystem particle;
	
	void Start ()
    {
		
	}




    private void OnTriggerEnter(Collider other)
    {
        if (tag == "PlayerBuilding")
        {
            if (other.transform.tag == "Enemy")
            {
                particle.Play();
                AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position, 0.2f);
                GameManager.PlayerBaseHP -= other.transform.GetComponent<RobotInfo>()._current_hp;
                Destroy(other.gameObject, 0.1f);
            }
        }
        else
        {
            if (other.transform.tag == "Player")
            {
                particle.Play();
                AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position, 0.2f);
                GameManager.EnemyBaseHP -= other.transform.GetComponent<RobotInfo>()._current_hp;
                Destroy(other.gameObject, 0.1f);
            }
        }
    }
}
