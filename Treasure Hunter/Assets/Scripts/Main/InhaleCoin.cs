using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class InhaleCoin : MonoBehaviour
{
    private Transform pl;
    private PlayerMoveSystem plms;
    private GameManager gm;
    [SerializeField]private bool inhale = true;

    private float time = 0f;

    private void Start()
    {
        gm = GameObject.Find("GameSystem").GetComponent<GameManager>();
        GameObject player = GameObject.Find("Wizard");
        pl = player.transform;
        plms = player.GetComponent<PlayerMoveSystem>();
    }


    private void FixedUpdate()
    {
        if (inhale)
        {

            time += 0.003f;
            //time = 0.05f;

            transform.position = Vector3.MoveTowards(transform.position, pl.position, plms.RunSpeed * time);

        }

        float distance = (transform.position - pl.position).sqrMagnitude;
        if (distance < 4f)
        {
            gameObject.SetActive(false);
            Destroy(this.gameObject);
            gm.GetCoin();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CollectCoin") inhale = true;
    }
}
