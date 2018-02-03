using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : BulletBase
{
    public override void Start()
    {
        base.Start();
        this.CollisionEnterCallBack = this.ColliderEnterCallBack;
        this.CollisionExitCallBack = this.ColliderExitCallBack;
        this.CollisionStayCallBack = this.ColliderStayCallBack;
    }

    public override void SetSenderAndDirection(Transform sender, Vector3 direction)
    {
        base.SetSenderAndDirection(sender, direction);
    }

    public void ColliderEnterCallBack(Collision2D collider)
    {
        if (collider.gameObject.name.Contains("Bullet")) return;

        if (collider.gameObject.name.Contains("Monster") || collider.gameObject.name.Contains("monster"))
        {
            GameObject boomEffect = ResuorceManager.Instance.GetElseCreateBoomEffect("Boom");
            boomEffect.SetActive(false);
            boomEffect.transform.position = this.transform.position;
            boomEffect.transform.SetParent(this.UICanvas);
            boomEffect.SetActive(true);

            TimeEvent eventtime = boomEffect.AddComponent<TimeEvent>();
            //爆炸特效在规定的时间内需要删除（放在特效管理器中）
            
            this.TimerManager.AddTimer(eventtime,1.0f,()=>
            {
                boomEffect.SetActive(false);
                this.ResuorceManagers.RecoverBoomEffect(boomEffect);
                this.TimerManager.RemoveTimer(eventtime);
            });
        }
        ///子弹在销毁的时候  并不是真的销毁 而是放入了 资源管理器 中
        this.gameObject.SetActive(false);
    }


    public void ColliderStayCallBack(Collision2D collider)
    {

    }
    public void ColliderExitCallBack(Collision2D collider)
    {

    }

    public void Update()
    {
        this.MoveMent = this.MoveSpeed * this.MoveDirection ;
    }

    public override void DoMove()
    {
        if (!this.IsDoMove) return;
        base.DoMove();
        this.rigidbody2d.velocity = this.MoveMent;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        this.DoMove();
    }

    private void OnDisable()
    {
        this.ResuorceManagers.RecoverBullet(this.gameObject);
    }
}
