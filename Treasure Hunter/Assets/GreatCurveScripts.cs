using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class GreatCurveScripts : MonoBehaviour
{
    private enum CurveVector
    {
        front,
        right,
        left,
        back
    };

    [SerializeField] private CurveVector curveVector;   //曲がる方向を選んで
    private PlayerMoveSystem plms;                      //プレイヤーの動きを司るやつ
    [SerializeField] private GameObject curveMid;       //カーブトンネルの中心
    [SerializeField] private GameObject curveEnd;       //カーブトンネルの出口
    private Vector3 curveEnd_pos;                       //カーブの終わり位置
    private Vector3 curveMid_pos;                       //カーブの中間位置  
    private Vector3 endPos;                             //プレイヤーの行き先

    [NonSerialized, Tooltip("Curve Speed")]                            
    public float curveSpeed;                                            //曲がる速度

    [NonSerialized]                                                     
    private Vector3 diff;                                               //距離を求めるのに使う

    [NonSerialized, Tooltip("Distance to Curve's End")]                
    public float magni;                                                 //ゴールまでの距離

    [NonSerialized, Tooltip("Original Distance")]                      
    public float originMagni;                                           //ゴールまでの最大距離

    [NonSerialized, Tooltip("Percentage of Distance to Curve's End")]  
    public float perDistanceToEnd;                                      //ゴールまでの割合

    private void Awake()
    {
        plms = GameObject.Find("Wizard").GetComponent<PlayerMoveSystem>();
        curveMid_pos = curveMid.transform.position;
        curveEnd_pos = curveEnd.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            plms.curveFlg = true;
            CalcMagnitude();
            originMagni = magni;
            plms.autoRunVec = curveVector.ToString();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            plms.curveFlg = false;

            //出た時に速度リセット
            switch (curveVector)
            {
                case CurveVector.front:
                case CurveVector.back:
                    plms.runSpd = new Vector3(0f, 0f, this.name == "back" ? -plms.RunSpeed : plms.RunSpeed);
                    break;

                case CurveVector.right:
                case CurveVector.left:
                    plms.runSpd = new Vector3(this.name == "left" ? -plms.RunSpeed : plms.RunSpeed, 0f, 0f);
                    break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            curveSpeed = 1f * (plms.RunSpeed / plms.playerOriginSpeed);

            CalcMagnitude();

            //plms.Wiz_TF.position = Vector3.MoveTowards(plms.Wiz_TF.position, curveEnd_pos, curveSpeed);
            //plms.Wiz_TF.position = Vector3.MoveTowards(plms.Wiz_TF.position, curveMid_pos, curveSpeed);

            endPos = Vector3.Lerp(curveMid_pos, curveEnd_pos, (1f - perDistanceToEnd));
            plms.Wiz_TF.position = Vector3.MoveTowards(plms.Wiz_TF.position, endPos, curveSpeed);
        }
    }

    void CalcMagnitude() {
        diff = curveEnd_pos - plms.Wiz_TF.position;
        magni = diff.magnitude;
        perDistanceToEnd = magni / originMagni;
    }
}
