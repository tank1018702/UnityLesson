using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesBuilding : MonoBehaviour
{
    public Material[] FlagMaterial;
    public GameObject Bar;
    public GameObject Flag;
    public int MaxHp;

    int _currentHp;

    MeshRenderer FlagRenderer;
    Image ProgressBar;





    void Start()
    {
        transform.tag = "neutral";
        FlagRenderer = Flag.GetComponent<MeshRenderer>();
        FlagRenderer.material = FlagMaterial[2];
        _currentHp = 0;
        GameManager.NeutralList.Add(gameObject);
        if (Bar)
        {
            ProgressBar = Bar.transform.GetChild(0).GetComponent<Image>();
        }

    }


    void Update()
    {
        StateUpdate();
    }

    public void Neutral_GetDamage(Transform shooter, int damege)
    {
        if (transform.tag == "neutral")
        {
            if (shooter)
            {
                if (shooter.tag == "Player")
                {
                    _currentHp = _currentHp <= -MaxHp ? -MaxHp : _currentHp - damege;

                }
                else
                {
                    _currentHp = _currentHp >= MaxHp ? MaxHp : _currentHp + damege;
                }
            }

        }
        else
        {
            _currentHp = _currentHp < 0 ? 0 : _currentHp - damege;
        }
    }

    void StateUpdate()
    {
        if (tag == "neutral" && ProgressBar)
        {
            if (_currentHp < 0)
            {
                ProgressBar.color = Color.red;
            }
            else
            {
                ProgressBar.color = Color.blue;
                //Bar.transform.Rotate(transform.up, 180);
            }
            ProgressBar.fillAmount = (Mathf.Abs(_currentHp) * 1f) / (MaxHp * 1f);
            if (_currentHp == -MaxHp)
            {
                GameManager.NeutralList.Remove(gameObject);
                tag = "Player";
                _currentHp = MaxHp;
                FlagRenderer.material = FlagMaterial[0];
                GameManager.PlayerControlResources += 1;
                GameManager.PlayerList.Add(gameObject);
            }
            else if (_currentHp == MaxHp)
            {
                GameManager.NeutralList.Remove(gameObject);
                tag = "Enemy";
                FlagRenderer.material = FlagMaterial[1];
                GameManager.EnemyControlResources += 1;
                GameManager.EnemyList.Add(gameObject);
            }
        }
        else if(ProgressBar)
        {
            if (_currentHp == 0)
            {
                if (tag == "Player")
                {
                    GameManager.PlayerList.Remove(gameObject);
                    GameManager.PlayerControlResources -= 1;
                }
                else
                {
                    GameManager.EnemyList.Remove(gameObject);
                    GameManager.EnemyControlResources -= 1;
                }
                tag = "neutral";
                FlagRenderer.material = FlagMaterial[2];
                GameManager.NeutralList.Add(gameObject);
               
            }
            ProgressBar.fillAmount = (Mathf.Abs(_currentHp) * 1f) / (MaxHp * 1f);
        }
        
    }

}
