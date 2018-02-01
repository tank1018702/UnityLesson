using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float MoveSpeed = 10;

    public GameObject ExplosionAnimation;

    public AudioClip HitAudio;
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.up * Time.deltaTime * MoveSpeed);
	}
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {

            case "wall":
                Destroy(collision.gameObject);
                AudioSource.PlayClipAtPoint(HitAudio, transform.position);
                Destroy(gameObject);
                break;
            case "enemy":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                GameObject Explosion = Instantiate(ExplosionAnimation, collision.transform.position, Quaternion.identity);
                Explosion.SetActive(true);
                break;
            case "ironwall":
                AudioSource.PlayClipAtPoint(HitAudio, transform.position);
                Destroy(gameObject);
                break;
            case "exteriorwall":
                AudioSource.PlayClipAtPoint(HitAudio, transform.position);
                Destroy(gameObject);
                break;
            default:
                break;
                
        }
    }
}
