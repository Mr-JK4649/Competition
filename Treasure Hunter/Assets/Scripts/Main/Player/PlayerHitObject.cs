using UnityEngine;
using Cinemachine;

public class PlayerHitObject : MonoBehaviour
{
    private PlayerMoveSystem plms;                  //プレイヤーの移動を司るスクリプト
    private GameManager gm;                         //ゲームの進行などを司るスクリプト

    [SerializeField] bool blockHitFlg = false;      //障害物に当たるかのフラグ
    
    [SerializeField] GameObject bom;                //爆発

    GameObject cm1;     // バーチャルカメラ１の情報
    GameObject cm2;     // バーチャルカメラ２の情報
    GameObject cm3;     // バーチャルカメラ３の情報
    int[] cmBuf = new int[3];     // バーチャルカメラの情報を記憶
    enum VirCamType     // バーチャルカメラの種類
    {
        CM1,
        CM2,
        CM3,
        MAX
    }
    private void Start()
    {

        plms = this.GetComponent<PlayerMoveSystem>();
        gm = GameObject.Find("GameSystem").GetComponent<GameManager>();

        cm1 = GameObject.Find("CM vcam1");     // バーチャルカメラ１の情報
        cm2 = GameObject.Find("CM vcam2");     // バーチャルカメラ２の情報
        cm3 = GameObject.Find("CM vcam3");     // バーチャルカメラ３の情報
        cmBuf[(int)VirCamType.CM1] = cm1.GetComponent<CinemachineVirtualCamera>().Priority;      // バーチャルカメラの情報を記憶
        cmBuf[(int)VirCamType.CM2] = cm2.GetComponent<CinemachineVirtualCamera>().Priority;      // バーチャルカメラの情報を記憶
        cmBuf[(int)VirCamType.CM3] = cm3.GetComponent<CinemachineVirtualCamera>().Priority;      // バーチャルカメラの情報を記憶

    }

    private void Update()
    {
        ////障害物に当たった時の処理
        //if (deathFlg)
        //{
        //    if(blockHitFlg)
        //        plms.Wiz_RB.velocity = new Vector3(0f, -9.81f, 0f);

        //}
    }

    

    private void OnCollisionEnter(Collision other)
    {
        if (blockHitFlg)
        {
            switch (other.gameObject.tag) {
                case "Block":
                    if (gm.aaa <= 0)
                    {
                        //Instantiate(bom, this.gameObject.transform);
                        plms.accelCount = 0;
                        plms.RunSpeed = 0f;
                        gm.GameOver();
                    }
                    break;

                case "Wall":
                    Debug.Log("壁に衝突");
                    break;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        switch (other.gameObject.tag)
        {
            case "Coin":
                //other.gameObject.SetActive(false);
                //Destroy(other.gameObject);
                //sco += coinsScore;
                //coinCount += 1;
                //Score.text = sco.ToString("0000000") + " 点\n" + coinCount.ToString("000") + " 枚";
                //CoinBar.value += 0.1f;
                break;

            case "CheckPoint":
                plms.lastCheckPoint = other.gameObject.transform.position;
                plms.lastVec = plms.autoRunVec;
                plms.lastRunSpd = plms.runSpd;
                gm.CoinSave();
                SavePriority();
                break;

            case "Goal":
                gm.GameClearText.enabled = true;
                gm.GameDataSave();
                //Destroy(GameObject.Find("Wizard"));
                break;

            case "Curve":
                plms.StopAllCoroutines();
                break;

            case "correct":
                GameObject.Find("LanePanel").transform.position = other.gameObject.transform.position;
                //plms.Wiz_TF.position = plms.laneObj[4].transform.position;
                break;
        }


    }
    public void SavePriority()
    {
        cmBuf[(int)VirCamType.CM1] = cm1.GetComponent<CinemachineVirtualCamera>().Priority;
        cmBuf[(int)VirCamType.CM2] = cm2.GetComponent<CinemachineVirtualCamera>().Priority;
        cmBuf[(int)VirCamType.CM3] = cm3.GetComponent<CinemachineVirtualCamera>().Priority;
    }

    public void GetPriority()
    {
        cm1.GetComponent<CinemachineVirtualCamera>().Priority = cmBuf[(int)VirCamType.CM1];
        cm2.GetComponent<CinemachineVirtualCamera>().Priority = cmBuf[(int)VirCamType.CM2];
        cm3.GetComponent<CinemachineVirtualCamera>().Priority = cmBuf[(int)VirCamType.CM3];
    }
}
