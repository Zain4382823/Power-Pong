using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsUI : MonoBehaviour
{
    [Header("Results Texts")]
    public TextMeshProUGUI playerWins;
    public TextMeshProUGUI winnerNumber;

    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;

    bool upscroll = false;

    // Start is called before the first frame update
    void Start()
    {
        if (P1.p1score > 2)
            winnerNumber.text = "1";  // player 1 wins!!
        else
            winnerNumber.text = "2";  // player 2 wins!!

        // display final score
        p1ScoreText.text = P1.p1score.ToString();
        p2ScoreText.text = P2.p2score.ToString();

        // reset player scores
        P1.p1score = 0;
        P2.p2score = 0;

        //Make playerwins & winnernumber glow rainbow!
        StartCoroutine(RainbowGlow());

        //Start the timer that handles movement, and switching to diff select scene!
        StartCoroutine(MoveTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (upscroll)
        {
            playerWins.rectTransform.anchoredPosition += new Vector2(0, 0.4f);
            winnerNumber.rectTransform.anchoredPosition += new Vector2(0, 0.4f);

            finalScore.rectTransform.anchoredPosition += new Vector2(0, 0.4f);
            p1ScoreText.rectTransform.anchoredPosition += new Vector2(0, 0.4f);
            p2ScoreText.rectTransform.anchoredPosition += new Vector2(0, 0.4f);
        }
            
    }

    IEnumerator MoveTimer()
    {
        yield return new WaitForSeconds(1.75f);
        upscroll = true;
        yield return new WaitForSeconds(1.7f);
        upscroll = false;
        yield return new WaitForSeconds(2.25f);
        SceneManager.LoadScene("Difficulty");  // back to the start - load up diff select scene!
    }

    IEnumerator RainbowGlow()
    {
        playerWins.color = Color.red;
        winnerNumber.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        playerWins.color = Color.Lerp(Color.red, Color.yellow, 0.5f);  // orange
        winnerNumber.color = Color.Lerp(Color.red, Color.yellow, 0.5f);
        yield return new WaitForSeconds(0.1f);
        playerWins.color = Color.yellow;
        winnerNumber.color = Color.yellow;
        yield return new WaitForSeconds(0.1f);
        playerWins.color = Color.green;
        winnerNumber.color = Color.green;
        yield return new WaitForSeconds(0.1f);
        playerWins.color = Color.cyan;
        winnerNumber.color = Color.cyan;
        yield return new WaitForSeconds(0.1f);
        playerWins.color = Color.blue;
        winnerNumber.color = Color.blue;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(RainbowGlow());
    }
}
