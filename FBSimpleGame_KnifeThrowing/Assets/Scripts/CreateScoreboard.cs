using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateScoreboard : MonoBehaviour
{
    public float scoreHeight = 100f;

    void Start()
    {
        transform.GetComponent<RectTransform>().sizeDelta =
           new Vector2(transform.GetComponent<RectTransform>().sizeDelta.x,
           GameManager.GM.Scores.Count * scoreHeight);

        int i = 0;
        foreach(KeyValuePair<string, object> pair in GameManager.GM.Scores)
        {
            Debug.Log(pair.Key + "," + pair.Value);
            GameObject score = new GameObject("Score" + ++i);
            score.layer = LayerMask.NameToLayer("UI");

            LayoutElement scoreLayout = score.AddComponent<LayoutElement>();
            scoreLayout.preferredHeight = scoreHeight;

            GameObject nickname = new GameObject("Nickname");
            nickname.layer = LayerMask.NameToLayer("UI");

            Text nicknameText = nickname.AddComponent<Text>();
            nicknameText.text = pair.Key;
            nicknameText.font = Font.CreateDynamicFontFromOSFont("Arial", 0);
            nicknameText.alignment = TextAnchor.MiddleLeft;
            nicknameText.resizeTextForBestFit = true;
            nicknameText.color = Color.black;

            nicknameText.transform.SetParent(score.transform, false);
            score.transform.SetParent(transform, false);

            RectTransform ntrt = nicknameText.GetComponent<RectTransform>();
            ntrt.anchorMin = Vector2.zero;
            ntrt.anchorMax = new Vector2(0.8f, 1f);
            ntrt.sizeDelta = Vector2.zero;
        }
    }
}
