using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{
    public int DetectDistance = 20;

    public int FoundDistance = 15;

    private GameObject Mark;
    private CharacterController controller;

    void Start()
    {
        Mark = transform.GetChild(0).gameObject;
        controller = transform.GetComponent<CharacterController>();
        Mark.SetActive(false);
    }
    void Update()
    {
        DetectField();
    }
    void DetectField()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, DetectDistance, LayerMask.GetMask("Enemy"));
        if (cols.Length > 0)
        {
            Mark.SetActive(true);
            for (int i = 0; i < cols.Length; i++)
            {
                FoundField(cols[i].transform.position);
            }
        }
        else
        {
            Mark.SetActive(false);
        }
    }

    void FoundField(Vector3 Position)
    {
        RaycastHit hitinfo;
        Ray ray = new Ray(transform.position, (Position - transform.position).normalized);
        if (Physics.Raycast(ray, out hitinfo, FoundDistance, LayerMask.GetMask("Obstacle", "Enemy")))
        {
            if (hitinfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                OnEnemySpotted(hitinfo.transform.gameObject);
            }
        }
        Debug.DrawRay(transform.position, (Position - transform.position).normalized, Color.red);
    }
    void OnEnemySpotted(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().spottedFrame = Time.frameCount;
    }
    void Movement()
    {
       

        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * 5f;

        controller.SimpleMove(velocity);
    }
}
