using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float throwPower = 400f;
    private Rigidbody2D rb2d;
    private bool isOver = false;
    const string appleTag = "Apple";

    float makeDelay = 3f;

    public static Knife instance;

    public virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Throw();
    }

    public virtual void Throw()
    {
        instance = null;
        PlayManager.Instance.penThrowAs.Play();
        rb2d.AddForce(Vector3.up * throwPower);
    }


    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (isOver) return;
        isOver = true;

        if (col.transform.tag.Equals(appleTag))
        {
            Destroy(rb2d);
            transform.SetParent(col.transform);
            PlayManager.Instance.Score++;
            Apple.instance.Hit();
        }
        else
        {
            if (GameManager.GM.AddCheck)
            {
                GameManager.GM.AddCheck = false;
                GameManager.GM.Score = PlayManager.Instance.Score;
                GameManager.GM.AddScore();

                StartCoroutine(ShowScoreButton(makeDelay));
            }

            PlayManager.Instance.isGameOver = true;
            rb2d.gravityScale = 3f;
            rb2d.AddTorque(400f);
        }
    }

    IEnumerator ShowScoreButton(float delay)
    {
        yield return new WaitForSeconds(delay);

        PlayManager.Instance.scoreBtn.SetActive(true);
        GameManager.GM.AddCheck = true;
    }
}
