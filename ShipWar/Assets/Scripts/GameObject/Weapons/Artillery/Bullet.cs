using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float MoveSpeed = 50;

    public GameObject ExplosionAnimation;

    public AudioClip HitAudio;

    public int damage;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * MoveSpeed*Time.deltaTime);
    }
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
