using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void uzuu()
    {
        var animator = GetComponent<Animator>();
        animator.Play("idle");
        Debug.Log("uzuu呼び出されています");
    }
}
