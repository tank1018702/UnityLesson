using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public GameObject Target;
    //public Animator Animator;
    public int HP;

    

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        HP = 100;
    }

    void Update()
    {
        transform.Translate(Vector3.up * MoveSpeed * Time.deltaTime);
        if (Target != null)
        {
            Vector3 TargetPosition = Target.transform.position;
            Vector3 Direcition = (TargetPosition - transform.position).normalized;
            Direcition.z = 0;
            
            float angle = Vector3.Angle(transform.up, Direcition);
        
            transform.Rotate(Vector3.Cross(transform.up, Direcition), Mathf.Min(1f,angle));
        }
        if(HP<=0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        GameObject animaation = AAGBulletPool.BulletPool.GetExplosionAnimation1(transform.position, Quaternion.identity);
        animaation.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        
    }



}
