using System;
using UnityEngine;

public class RandomShake : MonoBehaviour
{
    [SerializeField]
        private float length = 4.0f;
    [SerializeField]
        private float speed = 2.0f;
    private Vector3 sPos;



    // Start is called before the first frame update
    void Start()
    {
        sPos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3((Mathf.Sin((Time.time) * speed) * length + sPos.x), sPos.y, sPos.z);

    }
}
