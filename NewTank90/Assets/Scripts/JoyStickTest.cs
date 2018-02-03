using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class JoyStickTest : MonoBehaviour
{
    // Update is called once per frame

	void Update ()
    {
        var valus = Enum.GetValues(typeof(KeyCode));
        for (int i =0;i<valus.Length;i++)
        {
            if (Input.GetKeyDown((KeyCode)valus.GetValue(i)))
            {
                Debug.LogError(valus.GetValue(i).ToString());

            }
        }
	}


}
