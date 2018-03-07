using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class Characher : MonoBehaviour
{
    //组件
    protected Rigidbody rigid;
    protected Animator anim;
    protected CapsuleCollider capsule;
    /// <summary>
    /// 胶囊碰撞体的高度
    /// </summary>
    protected float defaultCapsuleHeight;
    /// <summary>
    /// 胶囊碰撞体的中心点
    /// </summary>
    protected Vector3 defaultCapsuleCenter;
    //移动控制变量
    /// <summary>
    /// 转向的量
    /// </summary>
    protected float turnAmount;
    /// <summary>
    ///  向前移动的量
    /// </summary>
    protected float forwardAmount;
    /// <summary>
    /// 向右移动的量
    /// </summary>
    protected float rightAmount;
    protected Vector3 velocity;
    protected Vector3 moveRaw;
    //状态
    protected bool isDead = false;
    protected bool isAiming = false;
    protected bool isCrouching = false;
    protected bool isGrounded = false;
    //
    [Range(1f, 1000)]
    [SerializeField]
    protected float angularSpeed = 360;


    [Range(0.1f, 3)]
    [SerializeField]
    protected float speed = 3;




    void Start ()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        defaultCapsuleHeight = capsule.height;
        defaultCapsuleCenter = capsule.center;

    }
	
	
	void Update ()
    {
        if (!isDead)
        {
            UpdateControl();
        }
        UpdateMovement();

    }
    protected virtual void UpdateControl()
    {

    }
    protected virtual void UpdateMovement()
    {
        if (isAiming)
        {
            velocity = (transform.forward * forwardAmount + transform.right * rightAmount) * speed;
        }
        else
        {
            transform.Rotate(0, turnAmount * angularSpeed * Time.deltaTime, 0);
            velocity = transform.forward * forwardAmount * speed;
        }

        if (isCrouching || isAiming) velocity *= 0.5f;
    }
    protected virtual void UpdateAnimator()
    {
        anim.SetFloat("Forward", forwardAmount, 0.01f, Time.deltaTime);
        anim.SetFloat("Right", rightAmount, 0.01f, Time.deltaTime);
        anim.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
        anim.SetFloat("Speed", velocity.magnitude, 0.01f, Time.deltaTime);
        anim.SetBool("Crouch", isCrouching);
        anim.SetBool("OnGround", isGrounded);
        anim.SetBool("Aiming", isAiming);
    }
}
