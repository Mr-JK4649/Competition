using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundMgr : MonoBehaviour
{
    public enum SE_TYPE { ACTION };

    public AudioClip[] bgm;         // bgmを入れるための変数
    public AudioClip[] actionSE;    // seを入れるための変数
    //public AudioClip[]
    [SerializeField] [Range(0, 1)] float bgmVolume = 1.0f;  // bgmのボリューム調整用
    [SerializeField] [Range(0, 1)] float seVolume = 1.0f;   // seのボリューム調整用

    AudioSource bgmAudio;       // bgm用のオーディオソース
    AudioSource seAudio;        // se用のオーディオソース

    // Start is called before the first frame update
    void Awake()
    {
        bgmAudio = gameObject.AddComponent<AudioSource>();
        seAudio = gameObject.AddComponent<AudioSource>();

    }

    private void Update()
    {
        //PlayBGM(0);
        //SoundBGM();
    }
    // bgm再生 numに0未満を指定すると停止
    public void PlayBGM(int num)
    {
        if(num >= 0)
        {
            bgmAudio.clip = bgm[num];
            bgmAudio.loop = true;
            bgmAudio.volume = bgmVolume;
            bgmAudio.Play();
        }
        else
        {
            bgmAudio.Stop();
        }
    }

    // se再生
    public void PlaySE(SE_TYPE seTYPE, int num)
    {
        switch (seTYPE)
        {
            case SE_TYPE.ACTION:
                seAudio.PlayOneShot(actionSE[num], seVolume);
                break;
        }
    }

    void SoundBGM()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "TitleScene":
                PlayBGM(0);
                break;

            case "MainScene":
                PlayBGM(1);

                break;

            case "ResultScene":
                PlayBGM(2);
                break;
        }
    }
}
