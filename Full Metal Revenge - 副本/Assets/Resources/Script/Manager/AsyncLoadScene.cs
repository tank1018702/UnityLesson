using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoadScene : MonoBehaviour
{
    public Image loadingSlider;

    public Text loadingText;

    private float loadingSpeed = 1;

    private float targetValue;

    private AsyncOperation operation;

    // Use this for initialization  
    void Start()
    {
        loadingSlider.fillAmount = 0.0f;

        if (SceneManager.GetActiveScene().name == "Loading")
        {
            //启动协程  
            StartCoroutine(AsyncLoading());
        }
    }

    IEnumerator AsyncLoading()
    {
        operation = SceneManager.LoadSceneAsync(Globe.nextSceneName);
        //阻止当加载完成自动切换  
        operation.allowSceneActivation = false;

        yield return operation;
    }

    // Update is called once per frame  
    void Update()
    {
        targetValue = operation.progress;

        if (operation.progress >= 0.9f)
        {
            
            //operation.progress的值最大为0.9  
            targetValue = 1.0f;
        }

        if (targetValue != loadingSlider.fillAmount)
        {
            //插值运算  
            loadingSlider.fillAmount = Mathf.Lerp(loadingSlider.fillAmount, targetValue, Time.deltaTime * loadingSpeed);
            if (Mathf.Abs(loadingSlider.fillAmount - targetValue) < 0.01f)
            {
                loadingSlider.fillAmount = targetValue;
            }
        }
        if(loadingSlider.fillAmount>0.9)
        {
            Destroy(GameObject.Find("BGM"));
        }
        loadingText.text = ((int)(loadingSlider.fillAmount * 100)).ToString() + "%";

        if ((int)(loadingSlider.fillAmount * 100) == 100)
        {
            //允许异步加载完毕后自动切换场景  
            operation.allowSceneActivation = true;
        }
    }
}

public class Globe
{
    public static string nextSceneName;
}