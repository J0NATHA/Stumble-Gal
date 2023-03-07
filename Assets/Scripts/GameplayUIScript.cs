using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayUIScript : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI highScoreLabel;
    // Start is called before the first frame update
    void Start()
    {
        scoreLabel.text = GameManager.Instance.GetScore().ToString();
        highScoreLabel.text = GameManager.Instance.GetHighScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = GameManager.Instance.GetScore().ToString();
        highScoreLabel.text = GameManager.Instance.GetHighScore().ToString();
    }
}
