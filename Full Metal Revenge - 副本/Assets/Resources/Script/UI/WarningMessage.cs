using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class WarningMessage : MonoBehaviour
{
    Text text;

	
	void Start ()
    {
        text = GetComponent<Text>();

        StartCoroutine(OnStart());
	}
	
	
	

    

    IEnumerator OnStart()
    {
        Tweener WariningMove = transform.DOLocalMove(new Vector3(0, 200, 0), 0.5f);
        WariningMove.SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(0.5f);
        float a=text.color.a;
       
        for(int i=0;i<10;i++)
        {
            a -= 0.1f;
            Vector4 Color = new Vector4(1, 0, 0, a);
            text.color = Color;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject, 0.1f);
    }
}
