using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreateScoreboard : MonoBehaviour
{
    public float scoreHeight = 105f;
    int fontMaxSize = 200;

    void Start()
    {
        transform.GetComponent<RectTransform>().sizeDelta =
           new Vector2(transform.GetComponent<RectTransform>().sizeDelta.x,
           GameManager.GM.Scores.Count * scoreHeight);

        int i = 0;
        foreach (KeyValuePair<string, object> pair in GameManager.GM.Scores.OrderByDescending(index => index.Value))
        {
            GameObject scoreObj = new GameObject("Score" + ++i);
            scoreObj.layer = LayerMask.NameToLayer("UI");

            LayoutElement scoreLayout = scoreObj.AddComponent<LayoutElement>();
            scoreLayout.preferredHeight = scoreHeight;

            if (GameManager.GM.Nickname.Equals(pair.Key))
            {
                GameObject background = new GameObject("Background");
                background.layer = LayerMask.NameToLayer("UI");

                Image backgroundImage = background.AddComponent<Image>();
                backgroundImage.color = new Color(1f, 1f, 1f, 0.5f);

                background.transform.SetParent(scoreObj.transform, false);

                RectTransform birt = backgroundImage.GetComponent<RectTransform>();
                birt.anchorMin = Vector2.zero;
                birt.anchorMax = Vector2.one;
                birt.sizeDelta = Vector2.zero;
            }

            GameObject rank = new GameObject("Rank");
            rank.layer = LayerMask.NameToLayer("UI");

            Text rankText = rank.AddComponent<Text>();
            rankText.text = " " + i.ToString();
            rankText.font = Font.CreateDynamicFontFromOSFont("Arial", 0);
            rankText.alignment = TextAnchor.MiddleLeft;
            rankText.resizeTextForBestFit = true;
            //rankText.resizeTextMaxSize = 200;
            rankText.color = Color.black;

            GameObject nickname = new GameObject("Nickname");
            nickname.layer = LayerMask.NameToLayer("UI");

            Text nicknameText = nickname.AddComponent<Text>();
            nicknameText.text = " " + pair.Key;
            nicknameText.font = Font.CreateDynamicFontFromOSFont("Arial", 0);
            nicknameText.alignment = TextAnchor.MiddleLeft;
            nicknameText.resizeTextForBestFit = true;
            //nicknameText.resizeTextMaxSize = 200;
            nicknameText.color = Color.black;

            GameObject score = new GameObject("Score");
            score.layer = LayerMask.NameToLayer("UI");

            Text scoreText = score.AddComponent<Text>();
            scoreText.text = pair.Value.ToString() + "  ";
            scoreText.font = Font.CreateDynamicFontFromOSFont("Arial", 0);
            scoreText.alignment = TextAnchor.MiddleRight;
            scoreText.resizeTextForBestFit = true;
            //scoreText.resizeTextMaxSize = 200;
            scoreText.color = Color.black;

            rank.transform.SetParent(scoreObj.transform, false);
            nickname.transform.SetParent(scoreObj.transform, false);
            score.transform.SetParent(scoreObj.transform, false);
            scoreObj.transform.SetParent(transform, false);

            RectTransform sort = scoreObj.GetComponent<RectTransform>();
            sort.sizeDelta = new Vector2(0f, scoreHeight);

            RectTransform rtrt = rankText.GetComponent<RectTransform>();
            rtrt.anchorMin = Vector2.zero;
            rtrt.anchorMax = new Vector2(0.1f, 1f);
            rtrt.sizeDelta = Vector2.zero;

            RectTransform ntrt = nicknameText.GetComponent<RectTransform>();
            ntrt.anchorMin = new Vector2(0.1f, 0f);
            ntrt.anchorMax = new Vector2(0.8f, 1f);
            ntrt.sizeDelta = Vector2.zero;

            RectTransform strt = scoreText.GetComponent<RectTransform>();
            strt.anchorMin = new Vector2(0.8f, 0f);
            strt.anchorMax = Vector2.one;
            strt.sizeDelta = Vector2.zero;
        }
    }
}
