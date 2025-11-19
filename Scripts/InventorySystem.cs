using UnityEngine;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    public int SelectedSeed = 0;

    public TextMeshProUGUI SeedText;
    public int seedCount = 0;
    public TextMeshProUGUI CarrotSeedText;
    public int carrotCount = 0;
    
    public TextMeshProUGUI SeedGrowText;
    public int seedGrowCount = 0;
    public TextMeshProUGUI CarrotGrowText;
    public int carrotGrowCount = 0;
    
    void Start()
    {
        LoadInventory();
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }
    
    public void SelectSeed()
    {
        SelectedSeed = 1;
    }

    public void SelectCarrotSeed()
    {
        SelectedSeed = 2;
    }

    public void SelectOther()
    {
        SelectedSeed = 0;
    }

    public void LoadInventory()
    {
        seedCount = PlayerPrefs.GetInt("SeedCount", 5);
        carrotCount = PlayerPrefs.GetInt("CarrotCount", 5);
        seedGrowCount = PlayerPrefs.GetInt("SeedGrowCount", 0);
        carrotGrowCount = PlayerPrefs.GetInt("CarrotGrowCount", 0);
    }
    
    public void SaveInventory()
    {
        PlayerPrefs.SetInt("SeedCount", seedCount);
        PlayerPrefs.SetInt("CarrotCount", carrotCount);
        
        PlayerPrefs.SetInt("SeedGrowCount", seedGrowCount);
        PlayerPrefs.SetInt("CarrotGrowCount", carrotGrowCount);

        PlayerPrefs.Save();
        Debug.Log("Envanter kaydedildi.");
    }
    
    public void UpdateUI()
    {
        SeedText.text = seedCount.ToString();
        CarrotSeedText.text = carrotCount.ToString();
        SeedGrowText.text = seedGrowCount.ToString();
        CarrotGrowText.text = carrotGrowCount.ToString();
    }

    public void AddSeeds(int amount, string seedType)
    {
        if (seedType == "Seed")
        {
            seedCount += amount;
        }
        else if (seedType == "Carrot")
        {
            carrotCount += amount;
        }
        SaveInventory(); 
        UpdateUI();
    }

    public void HarvestProduct()
    {
        int amount = 1;
        
        SaveInventory();
        UpdateUI();
    }
}