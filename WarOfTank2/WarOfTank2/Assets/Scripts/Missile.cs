using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float MoveSpeed = 10;
    public GameObject BoomOBJ;
    public AudioClip DieAudio;

	// Update is called once per frame
	void Update () {
        this.transform.Translate(MoveSpeed *  Time.deltaTime * Vector3.right);
	}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
       
    //}

    //private void OnCollisionStay2D(Collision2D collision)
    //{

    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag )
        {
            case "Wall":
                GameObject.Destroy(collision.gameObject);
                GameObject.Destroy(gameObject);
                break;
            case "TieWall":
                break;
            case "Enemy":
                GameObject go =  GameObject.Instantiate(this.BoomOBJ,collision.transform.position,Quaternion.identity);
                go.transform.localScale = 1.5f * Vector3.one;// new Vector3(3,3,3);
                //GameObject audio = new GameObject();
                AudioSource audio = go.AddComponent<AudioSource>();

                if (audio != null)
                {
                    audio.clip = DieAudio;
                    audio.Play();
                }
               // AudioSource clip =  audio.AddComponent<AudioSource>();
                //clip.
               // go.transform.localPosition = new Vector3(collision.transform.position.x + 10, collision.transform.position.y, collision.transform.position.z);
                GameObject.Destroy(collision.gameObject);
                GameObject.Destroy(gameObject);
                break;
            case "Player":
                break;
            case "Boss":
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.LogError("2 " + collision.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.LogError("3 " + collision.gameObject.name);

    }
}
