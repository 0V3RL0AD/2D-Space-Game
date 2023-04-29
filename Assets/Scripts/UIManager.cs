using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ScoreText;
    [SerializeField]
    private Image _LivesImage;
    [SerializeField]
    private Sprite[] _SpriteDisplay;
    [SerializeField]
    private Text _GameOverText;
    [SerializeField]
    private Text _RestartGameText;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _ScoreText.text = "Score: " + 0;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    public void ScoreUpdate(int PlayerScore)
    {
        _ScoreText.text = "Score: " + PlayerScore.ToString();
    }

    //updates the UI that displays the player's lives
    public void UpdateLives(int currentLives)
    {
        //display image sprites
        //change sprite based on lives left
        _LivesImage.sprite = _SpriteDisplay[currentLives];

        if (currentLives == 0)
        {
            IsGameOver();
        }
    }

    //activate the game over stuff since player dead
    void IsGameOver()
    {
        _gameManager.GameOver();
        _GameOverText.gameObject.SetActive(true);
        _RestartGameText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
    }

    //make the game over text flicker
    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _GameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _GameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
