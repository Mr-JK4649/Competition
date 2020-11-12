using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //void Awake()
    //{
    //    DontDestroyOnLoad(this.gameObject);
    //}
    //[SerializeField] private Transform pTransform; // プレイヤーの座標を格納
    /*[NonSerialized]*/
    [SerializeField] public static Transform pTransformBuf;
    [SerializeField] public static bool checkFlg;

    private void Start()
    {
        checkFlg = false;
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
       
        pTransformBuf = other.transform;
        checkFlg = true;
    }

    public Transform CheckPointTransForm()
    {
       return pTransformBuf;
    }

    public bool CheckPointFlg()
    {
        return checkFlg;
    }

}
