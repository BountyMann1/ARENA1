using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    // Tracks how many collectible items the player has picked up
    private int _itemsCollected = 0;
    // Tracks the player's current health
    private int _playerHP = 10;
    // How many items are required to win the game
    public int MaxItems = 4;
    // UI text elements shown on screen
    public TMP_Text HealthText;     // Displays player health
    public TMP_Text ItemText;       // Displays item count
    public TMP_Text ProgressText;   // Displays status messages

    // UI buttons for win/lose screens
    public Button WinButton;
    public Button LossButton;

    //sets Time.timescale to 0
    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
        Time.timeScale = 0f;
    }

    // Property for item count, updates UI and checks win condition
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;

            // Update UI text
            ItemText.text = "Items: " + _itemsCollected;

            // Check if player has collected enough items to win
            if (_itemsCollected >= MaxItems)
            {
                ProgressText.text = "You've found all the items!";
                WinButton.gameObject.SetActive(true); // Show win button
                UpdateScene("You've found all the items!"); //new update scene method
            }
            else
            {
                // Show how many items remain
                ProgressText.text = "Items found, only " + (MaxItems - _itemsCollected) + " more to go!";
            }
        }
    }

    // Property for player health, updates UI and checks loss condition
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;

            // Update UI text
            HealthText.text = "Health: " + _playerHP;

            // If health reaches zero, trigger loss state
            if (_playerHP <= 0)
            {
                ProgressText.text = "You want another life with that?";
                LossButton.gameObject.SetActive(true); // Show loss button
                Time.timeScale = 0f;                   // Pause the game
            }
            else
            {
                // Show damage feedback
                ProgressText.text = "Ouch... that's gotta hurt.";
            }
        }
    }

    void Start()
    {
        // Initialize UI with starting values
        ItemText.text = "Items: " + _itemsCollected;
        HealthText.text = "Health: " + _playerHP;

        // Hide win/lose buttons at the start
        WinButton.gameObject.SetActive(false);
        LossButton.gameObject.SetActive(false);
    }

    // Reloads the scene and resets the game
    public void RestartScene()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f; // Resume normal time
    }
}
