using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public static int TriggerCount = 0;

    public Character Player;
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Player.CurrentMoveSpeed = Player.MoveSpeed * 0.1f;
        TriggerCount++;
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        TriggerCount--;
        if (TriggerCount > 0) return;
        Player.CurrentMoveSpeed = Player.MoveSpeed;
    }
}
