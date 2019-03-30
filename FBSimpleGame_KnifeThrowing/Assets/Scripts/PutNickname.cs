using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutNickname : MonoBehaviour
{
    public Button startBtn;

    InputField inputField;

    void Start()
    {
        inputField = GetComponent<InputField>();
        inputField.onValueChanged.AddListener(delegate { NicknameCheck(); });
    }

    void NicknameCheck()
    {
        if (inputField.text.Equals("")) startBtn.interactable = false;
        else startBtn.interactable = true;
    }
}
