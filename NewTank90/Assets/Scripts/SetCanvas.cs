using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCanvas : MonoBehaviour
{
    public CanvasScaler Scaler;
    public Slider UISlider;

	// Use this for initialization
	void Start () {
        this.UISlider.onValueChanged.AddListener(this.SetScaler);	
	}
    private void SetScaler(float xx)
    {
        this.Scaler.matchWidthOrHeight = xx;
    }
}
