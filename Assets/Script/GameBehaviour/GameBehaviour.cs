using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;
using UnityEditor;

public class GameBehaviour : MonoBehaviour
{
    private int _itemsCollected = 0;
    private int _playerHP = 10;
    private int MaxItems = 4;
    
    public TMP_Text ItemText;
    public TMP_Text HealthText;
    public TMP_Text ProgressText;
    public Button Winbutton;
    public Button LossButton;

    void Start()
    {
        ItemText.text = "Items Collected: " + _itemsCollected;
        HealthText.text = "Player HP: " + _playerHP;

        if(Winbutton != null)
        {
            Winbutton.gameObject.SetActive(false);
        }
        if (LossButton != null) 
        {
            LossButton.gameObject.SetActive(false);
        }

    }

    public int itemsCollected
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            ItemText.text = "Items Collected: " + _itemsCollected;

            if (_itemsCollected >= MaxItems)
            {
                ProgressText.text = "You found all the items!";
                if (Winbutton != null)
                {
                    Winbutton.gameObject.SetActive(true);
                    Time.timeScale = 0f;
                }
                else
                {
                    int remaining = MaxItems - _itemsCollected;
                    ProgressText.text = "Collect " + remaining + " more items to win!";
                }
            }
        }
    }

    public int HP
    {
        get { return _playerHP; }
        set
        {
            if (_playerHP <= 0)
            {
                UpdateScene("You want another life with that?");
                if(LossButton != null)
                {
                    LossButton.gameObject.SetActive(true);
                }
                else
                {
                    ProgressText.text = "Ouchh!!! That's Hurt.";
                }
            }

            _playerHP = value;
            HealthText.text = "Health: " + _playerHP;
            Debug.LogFormat("Lives: {0}", _playerHP);   
        }
    }

    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
        Time.timeScale = 0f;

    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
