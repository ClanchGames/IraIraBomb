using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM
{
    Home,
    Battle
}

/// <summary>
/// 使い方
/// </summary>
public class SE : MonoBehaviour
{
    static public SE se;

    public AudioClip GameOverSE;


    public AudioSource HomeBGM;
    public AudioSource BattleBGM;
    AudioSource audioSource;

    [SerializeField]
    float delay = 0.06f;

    float realtimelastplay = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        if (se == null)
        {
            se = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator TestSE()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="audioClip"></param>
    void PlaySE(AudioClip audioClip)
    {
        Debug.Log("playse");
        if (Time.realtimeSinceStartup - realtimelastplay > delay)
        {
            realtimelastplay = Time.realtimeSinceStartup;
            audioSource.PlayOneShot(audioClip);
            Debug.Log("se");
        }

    }

    /// <summary>
    /// true=play,false=stop
    /// </summary>
    /// <param name="bgm"></param>
    /// <param name="playORstop"></param>
    public void PlayBGM(BGM bgm, bool playORstop)
    {
        if (bgm == BGM.Home)
        {
            if (playORstop)
            {
                HomeBGM.Play();
            }
            else
            {
                HomeBGM.Stop();
            }
        }

        if (bgm == BGM.Battle)
        {

            if (playORstop)
            {
                BattleBGM.Play();
            }
            else
            {
                BattleBGM.Stop();
            }
        }
    }

    public void PlayGameOverSE()
    {
        PlaySE(GameOverSE);
    }
}

