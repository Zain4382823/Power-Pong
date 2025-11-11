using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelector : MonoBehaviour
{
    public void SetDifficulty(string difficulty)
    {
        PlayerPrefs.SetString("Difficulty", difficulty);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Power Pong");
    }
}
