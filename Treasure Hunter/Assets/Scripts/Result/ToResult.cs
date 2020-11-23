using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToResult : MonoBehaviour
{
    public int coin;
    public float clearTime;
    public long point;

    public void SetValue(int co, float cl, long po)
    {
        coin = co;
        clearTime = cl;
        point = po;
    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
