using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire1 : MonoBehaviour
{

    private Timer timer;
    void Start ()
    {
        timer = new Timer(0.8f);
        timer.OnEnd = () =>
        {
            gameObject.SetActive(false);
        };
        timer.Start();
    }
	
	
	void Update ()
    {
		
	}
}
