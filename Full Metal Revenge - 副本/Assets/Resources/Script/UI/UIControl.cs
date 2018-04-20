using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }


    void Update()
    {

    }

    public void Onclick(GameObject ob)
    {



        transform.position = ob.transform.position;

        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
