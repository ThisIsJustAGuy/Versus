using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{
    public int score = 0;
    public ScorerDisplay scdp;
    public PlayerCombat pc;
    int reqScore;
    bool once = true;

    private void Start()
    {
        if (PlayerPrefs.HasKey("reqScore"))
            reqScore = PlayerPrefs.GetInt("reqScore");
        else
            reqScore = 1;
    }
    private void Update()
    {
        if (pc.isDead && once)
        {
            score++;
            UpdateDisplayedScore();
            once = false;
        }
        else if (!pc.isDead)
            once = true;
    }
    public void UpdateDisplayedScore()
    {
        PlayerPrefs.DeleteKey("reqScore");
        PlayerPrefs.DeleteKey("gameMode");
        gameObject.GetComponent<TextMeshProUGUI>().text = score.ToString();
        if (score == reqScore)
        {
            if (gameObject.name.Contains("P1"))
                StartCoroutine(scdp.DisplayWinner("Player 1"));
            else
                StartCoroutine(scdp.DisplayWinner("Player 2"));
        }
        else
        {
            if (gameObject.name.Contains("P1"))
                StartCoroutine(scdp.DisplayScorer("Player 1"));
            else
                StartCoroutine(scdp.DisplayScorer("Player 2"));
        }
    }
}
