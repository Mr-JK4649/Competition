using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndAnimSc : MonoBehaviour
{
    [SerializeField] private Resultscripts rs;
    [SerializeField] Text tx;
    [SerializeField] Slider clearPer;

    void func() {
        rs.EndAnimFunc();
    }

    void func2_1_tr() {
        tx.enabled = true;
        float num = rs.coinScore + rs.coinScore * clearPer.value;
        tx.text = num.ToString("00000000");
    }

    void func2_2() { 
        rs.StartCoroutine("ClearStageEvaluation");
    }
}
