﻿using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TreasureBoxAnim : MonoBehaviour
{
    public Animator anim;
    public PlayerMoveSystem plms;
    public PostProcessVolume volume;


    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.tag == "Player") {
    //        anim.SetBool("Open",true);
            
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetBool("Open", true);
            plms.clearFlg = true;
            plms.accelCount = 0;
            plms.accelForce = 0;
            StopAllCoroutines();
            GameObject.Find("Wizard4").GetComponent<ShootSpiral>().enabled = false;
        }

        
    }

    [SerializeField] float inten = 0;
    [SerializeField] float add = 0;

    private void OnTriggerStay(Collider other)
    {
        

        if (other.gameObject.tag == "Player")
        {
            Bloom bl = ScriptableObject.CreateInstance<Bloom>();
            bl.enabled.Override(true);
            bl.intensity.Override(inten);
            

            volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 0f, bl);
        }
    }
    
}
