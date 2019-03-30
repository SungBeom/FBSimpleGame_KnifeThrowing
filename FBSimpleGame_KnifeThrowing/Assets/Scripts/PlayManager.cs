using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public Transform penMakePosTf;
    public GameObject penObj;
    public float makeDelay = 1f;
    public Text scoreText;
    public AudioSource penThrowAs;
    public GameObject scoreBtn;
    public bool isGameOver = false;

    private int score = 0;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreText.text = score.ToString();
        }
    }

    private static PlayManager instance;
    public static PlayManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void Throw()
    {
        if (Knife.instance == null)
            return;
        Knife.instance.Throw();
        StartCoroutine(MakeKnifeCoroutine(makeDelay));
    }

    IEnumerator MakeKnifeCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!isGameOver)
            Instantiate(penObj).transform.position = penMakePosTf.position;
    }
}
