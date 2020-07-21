using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    private PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (player.score > 1)
        {
            scoreText.text = player.score.ToString();
        }
    }
}
