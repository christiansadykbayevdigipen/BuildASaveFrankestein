// Christian S
// Writes the score into the score text box in the Failure Scene.

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreWriter : MonoBehaviour
{
    // Editor Fields
    public TMP_Text ScoreTextBox;

    private void Start()
    {
        ScoreTextBox.text = $"Overall Score: {GameManager.OverallScore} PTS";
    }
}
