using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

public class TreasureBoxAnimTutorial : MonoBehaviour
{
    public Animator anim;
    public PlayerMoveSystem plms;
    public PostProcessVolume volume;

    [SerializeField] private float speed;

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
            plms.RunSpeed = 0f;
            plms.runSpd = Vector3.zero;
            plms.accelCount = 0;
            plms.accelForce = 0;
            StopAllCoroutines();
            StartCoroutine("GetCloseToBox");
            GameObject.Find("Wizard4").GetComponent<TutorialShootSpiral>().enabled = false;
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

    public float num = 0f;

    IEnumerator GetCloseToBox()
    {
        Debug.Log("宝箱システム起動.......リザルト移行シーケンス実行まで3.....2.....1");

        Vector3 sta_Pos = plms.Wiz_TF.position;     //距離
        Vector3 end_Pos = transform.position;

        while (num < 1f)
        {
            Debug.Log("宝箱システム起動.......リザルト移行シーケンス実行まで3.....2.....1");

            num += speed;

            if (num >= 1f) num = 1f;

            plms.Wiz_TF.position = Vector3.Lerp(sta_Pos, end_Pos, num);

            //diff = cur_Dis / org_Dis;

            yield return null;
        }

    }

}