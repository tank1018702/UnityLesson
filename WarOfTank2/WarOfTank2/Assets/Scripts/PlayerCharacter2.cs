using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter2 : Character
{
    // Update is called once per frame
    
    private void FixedUpdate()
    {
        this.H = Input.GetAxis("Horizontal");
        this.V = Input.GetAxis("Vertical");

        this.Move();

    }

    private void Update()
    {
        this.Attack();
    }

    public override void Attack()
    {
        base.Attack();
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    public override void Move()
    {
        base.Move();
    }
}
