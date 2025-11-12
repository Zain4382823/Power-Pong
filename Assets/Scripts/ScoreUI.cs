using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI Instance;
    [HideInInspector] public TextMeshProUGUI[] scoreTexts = new TextMeshProUGUI[2];

    [Header("Score Texts")]
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;

    void Awake() { Instance = this; scoreTexts[0] = p1ScoreText; scoreTexts[1] = p2ScoreText; }

    void Update()
    {
        // live update from your static scores (polls every frame—super cheap)
        p1ScoreText.text = P1.p1score.ToString();
        p2ScoreText.text = P2.p2score.ToString();
    }
    public void FlashScore(int playerIndex) // 0=P1, 1=P2
    {
        StartCoroutine(FlashCoroutine(playerIndex));
    }

    IEnumerator FlashCoroutine(int idx)
    {
        scoreTexts[idx].color = Color.yellow;
        yield return new WaitForSeconds(0.3f);
        scoreTexts[idx].color = Color.white;
    }
}