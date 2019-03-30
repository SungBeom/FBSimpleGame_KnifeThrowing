using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickStart : MonoBehaviour
{
    public Text nickname;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }

    void Click()
    {
        GameManager.GM.Nickname = nickname.text;
        SceneManager.LoadScene("Play");
    }
}
