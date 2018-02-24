using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAGBullet : MonoBehaviour
{
    public float MoveSpeed = 5;

    public GameObject ExplosionAnimation;

    public AudioClip HitAudio;

    public int damage;

    Timer timer;

    private void Start()
    {
        damage = 1;

        
    }
    private void OnEnable()
    {
        timer = new Timer(1f);
        timer.OnEnd = () => { gameObject.SetActive(false); };
        timer.Start();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * MoveSpeed * Time.fixedDeltaTime);
    }
    

}

