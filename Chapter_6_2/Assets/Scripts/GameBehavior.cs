using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using CustomExtensions;
using System.Collections.Generic;
using System.Linq;

public class GameBehavior : MonoBehaviour
{
    private string _state;
    private string State
    {
        get { return _state; }
        set { _state = value; }
    }
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
    public Stack<Loot> LootStack = new Stack<Loot>();

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

        Initialize();
    }
    public void Initialize()
    {
        _state = "Game Manager Initialized..";
        _state.FancyDebug();
        Debug.Log(_state);
        LootStack.Push(new Loot("Sword of Doom", 5));
        LootStack.Push(new Loot("Golden Key", 3));
        LootStack.Push(new Loot("Pair of Winged Boots", 2));
        LootStack.Push(new Loot("Mythril Bracer", 4));
        FilterLoot();
    }
    // Reloads the scene and resets the game
    public void RestartScene()
    {
        Utilities.RestartLevel(0);
    }
    public void PrintLootReport()
    {
        var currentItem = LootStack.Pop();
        var nextItem = LootStack.Peek();
        Debug.LogFormat("You got {0}! You've got a good chance of finding a {1} next!", currentItem.Name, nextItem.Name);
        Debug.LogFormat("There are {0} random loot items waiting for you!", LootStack.Count);
    }
    public void FilterLoot()
    {
        var rareLoot = from item in LootStack
        where item.Rarity >= 3
        orderby item.Rarity
        select item;
        foreach (var item in rareLoot)
        {
            Debug.LogFormat("Rare item: {0}!", item.Name);
        }
    }
    public bool LootPredicate(Loot loot)
    {
        return loot.Rarity >= 3;
    }
}
