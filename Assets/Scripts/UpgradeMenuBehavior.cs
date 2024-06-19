using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UpgradeMenuBehavior : MonoBehaviour
{
    public Button[] upgradeMenus;
    public Sprite[] upgradeImages;
    string[] upgradeTexts = new string[6];

    // Start is called before the first frame update
    void Start()
    {
        upgradeTexts[0] = "Health + 20% : Speed - 10%";
        upgradeTexts[1] = "Speed + 50% : Health - 15%";
        upgradeTexts[2] = "Damage + 20% : Jump - 30%";
        upgradeTexts[3] = "Jump + 100% : Dash - 100%";
        upgradeTexts[4] = "Dash + 100% : Cooldown + 10%";
        upgradeTexts[5] = "Melee Cooldown - 50% : Damage - 15%";
       
        RandomThreeUpgrades();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeButton(int upgradeIndex, int menuIndex)
{
    if (upgradeMenus == null || upgradeMenus.Length <= menuIndex || upgradeMenus[menuIndex] == null)
    {
        Debug.LogError("upgradeMenus is null or does not contain element at index: " + menuIndex);
        return;
    }

    if (upgradeImages == null || upgradeImages.Length <= upgradeIndex || upgradeImages[upgradeIndex] == null)
    {
        Debug.LogError("upgradeImages is null or does not contain element at index: " + upgradeIndex);
        return;
    }

    if (upgradeTexts == null || upgradeTexts.Length <= upgradeIndex || upgradeTexts[upgradeIndex] == null)
    {
        Debug.LogError("upgradeTexts is null or does not contain element at index: " + upgradeIndex);
        return;
    }

    Image menuImage = upgradeMenus[menuIndex].GetComponentInChildren<Image>();
    TextMeshProUGUI menuText = upgradeMenus[menuIndex].GetComponentInChildren<TextMeshProUGUI>();

    if (menuImage == null)
    {
        Debug.LogError("Image component not found in children of upgradeMenus at index: " + menuIndex);
        return;
    }

    if (menuText == null)
    {
        Debug.LogError("Text component not found in children of upgradeMenus at index: " + menuIndex);
        return;
    }

    menuImage.sprite = upgradeImages[upgradeIndex];
    menuText.text = upgradeTexts[upgradeIndex];
}

    public void ClickedUpgrade(int selectedMenu)
    {
        // Set the player's upgrades based on the selected upgrades
        if (upgradeMenus[selectedMenu].GetComponentInChildren<TextMeshProUGUI>().text == upgradeTexts[0]) {
            // Health + 20% : Speed - 10%
            PlayerHealth.healthMultiplier += 0.2f;
            PlayerAttack.speedMultiplier = Mathf.Max(0.1f, PlayerAttack.speedMultiplier - 0.1f);
        }
        else if (upgradeMenus[selectedMenu].GetComponentInChildren<TextMeshProUGUI>().text == upgradeTexts[1]) {
            // Speed + 50% : Health - 15%
            PlayerAttack.speedMultiplier += 0.5f;
            PlayerHealth.healthMultiplier = Mathf.Max(0.1f, PlayerHealth.healthMultiplier - 0.15f);
        }
        else if (upgradeMenus[selectedMenu].GetComponentInChildren<TextMeshProUGUI>().text == upgradeTexts[2]) {
            // Damage + 20% : Jump - 30%
            PlayerAttack.attackDamageMultiplier += 0.2f;
            PlayerAttack.jumpHeightMultiplier = Mathf.Max(0.1f, PlayerAttack.jumpHeightMultiplier - 0.3f);
        }
        else if (upgradeMenus[selectedMenu].GetComponentInChildren<TextMeshProUGUI>().text == upgradeTexts[3]) {
            // Jump + 100% : Dash - 90%
            PlayerAttack.jumpHeightMultiplier *= 2;
            CharacterAttributes.dashDistanceMultiplier = Mathf.Max(0.05f, CharacterAttributes.dashDistanceMultiplier - 0.9f);
        }
        else if (upgradeMenus[selectedMenu].GetComponentInChildren<TextMeshProUGUI>().text == upgradeTexts[4]) {
            // Dash + 100% : Cooldown + 10%
            CharacterAttributes.dashDistanceMultiplier *= 2;
            PlayerAttack.attackCooldownMultiplier += 0.1f;
        }
        else if (upgradeMenus[selectedMenu].GetComponentInChildren<TextMeshProUGUI>().text == upgradeTexts[5]) {
            // Melee Cooldown - 50% : Damage - 15%
            PlayerAttack.attackCooldownMultiplier = Mathf.Max(0.1f, PlayerAttack.attackCooldownMultiplier - 0.5f); 
            PlayerAttack.attackDamageMultiplier = Mathf.Max(0.1f, PlayerAttack.attackDamageMultiplier - 0.15f);
        }

        Debug.Log("Upgrade " + selectedMenu + " selected.");

        ContinueToNextLevel();
    }


    public void ReturnToMainMenu()
    {
        // Return to the main menu
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }

    public void ContinueToNextLevel()
    {
        // Load the next level if it exists
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            // Load the main menu if there are no more levels
            Debug.Log("No more levels to load. Returning to main menu.");
            SceneManager.LoadScene(0);
        }
    }

    public void RandomThreeUpgrades()
    {
        // Randomly select three upgrades
        int[] selectedUpgrades = new int[3];
        for (int i = 0; i < 3; i++)
        {
            int randUpgrade = Random.Range(0, 6);
            while (System.Array.IndexOf(selectedUpgrades, randUpgrade) != -1)
            {
                randUpgrade = Random.Range(0, 6);
            }
            selectedUpgrades[i] = randUpgrade;
            ChangeButton(randUpgrade, i);

        }
    }
}
