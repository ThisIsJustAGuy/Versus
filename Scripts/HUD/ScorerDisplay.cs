using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScorerDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }
    public IEnumerator DisplayScorer(string scorer)
    {
        text.enabled = true;
        text.text = $"{scorer} scored!";
        yield return new WaitForSeconds(3f);
        text.enabled = false;
    }

    public IEnumerator DisplayWinner(string winner)
    {
        text.enabled = true;
        text.text = $"{winner} won!";
        yield return new WaitForSeconds(5f);
        text.enabled = false;
        SceneManager.LoadScene("Main Menu");
    }
}
