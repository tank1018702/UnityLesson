using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo1 : MonoBehaviour
{
    public List<GameObject> RobotList;

    public Transform Point;

   public Transform target;

    GameObject Current;
  
    void Start()
    {
        Current = null;
    }


    void Update()
    {
        Born();
    }

    void Born()
    {
       while(!Current)
        {
            GameObject go= Instantiate(RobotList[Random.Range(0, RobotList.Count)], Point.position,Quaternion.identity);
            go.GetComponent<RobotDemo>().NavMesh.SetDestination(target.position);
            go.GetComponent<RobotDemo>().NavMesh.speed = 20;
            go.GetComponent<RobotDemo>().NavMesh.acceleration = 100;
            Current = go;
        }
    }
    public void LoadNewScene()
    {
        Time.timeScale = 1;
        
        Globe.nextSceneName = "game";

        SceneManager.LoadScene("Loading");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
