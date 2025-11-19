using UnityEngine;

public class FarmPlot : MonoBehaviour
{
    private InventorySystem inventory;
    public int plotID; 

    public enum PlotState { Empty, Seeded, Growing, Harvestable }
    public PlotState currentState = PlotState.Empty;

    public float timeToGrow = 10f;
    
    private SpriteRenderer plantRenderer; 
    public Sprite emptySprite;

    public Sprite plantedSprite;
    public Sprite growingSprite1;
    public Sprite growingSprite2;
    public Sprite harvestableSeedSprite;

    public Sprite plantedCarrotSprite;
    public Sprite growingCarrotSprite1;
    public Sprite growingCarrotSprite2;
    public Sprite harvestableCarrotSprite;

    private float growthTimer = 0f;
    private string currentPlantType = "";

    void Start()
    {
        inventory = FindObjectOfType<InventorySystem>();
        plantRenderer = GetComponent<SpriteRenderer>();
        
        LoadPlotData();
        
        if (currentState == PlotState.Empty)
        {
            plantRenderer.sprite = emptySprite;
        }
    }

    void Update()
    {
        if (currentState == PlotState.Growing)
        {
            growthTimer += Time.deltaTime;
            UpdateGrowthVisuals();
            SavePlotData();

            if (growthTimer >= timeToGrow)
            {
                currentState = PlotState.Harvestable;
                
                if (currentPlantType == "Seed")
                {
                    plantRenderer.sprite = harvestableSeedSprite;
                }
                else if (currentPlantType == "Carrot")
                {
                    plantRenderer.sprite = harvestableCarrotSprite;
                }
                Debug.Log(currentPlantType + " hasat edilebilir!");
                SavePlotData();
            }
        }
    }
    
    void OnMouseDown()
    {
        Interact();
    }

    public void Interact()
    {
        if (inventory == null) return;

        if (currentState == PlotState.Empty)
        {
            string selectedSeedType = GetSelectedSeedType();

            if (!string.IsNullOrEmpty(selectedSeedType) && CanPlant(selectedSeedType))
            {
                PlantSeed(selectedSeedType);
            }
        }
        else if (currentState == PlotState.Harvestable)
        {
            Harvest();
        }
    }

    private string GetSelectedSeedType()
    {
        if (inventory.SelectedSeed == 1 && inventory.seedCount > 0)
        {
            return "Seed";
        }
        else if (inventory.SelectedSeed == 2 && inventory.carrotCount > 0)
        {
            return "Carrot";
        }
        return "";
    }

    private bool CanPlant(string seedType)
    {
        if (seedType == "Seed" && inventory.seedCount > 0) return true;
        if (seedType == "Carrot" && inventory.carrotCount > 0) return true;
        return false;
    }

    private void PlantSeed(string seedType)
    {
        if (seedType == "Seed")
        {
            inventory.seedCount--;
            plantRenderer.sprite = plantedSprite;
        }
        else if (seedType == "Carrot")
        {
            inventory.carrotCount--;
            plantRenderer.sprite = plantedCarrotSprite;
        }

        currentPlantType = seedType;
        currentState = PlotState.Growing;
        growthTimer = 0f;
        
        inventory.SaveInventory();
        inventory.UpdateUI();
        SavePlotData();
        Debug.Log(seedType + " ekildi. Kalan: " + (seedType == "Seed" ? inventory.seedCount : inventory.carrotCount));
    }

    private void Harvest()
    {
        int seedDropAmount = Random.Range(1, 3); 

        if (currentPlantType == "Seed")
        {
            inventory.HarvestProduct();
            inventory.AddSeeds(seedDropAmount, "Seed");
            inventory.seedGrowCount++;
        }
        else if (currentPlantType == "Carrot")
        {
            inventory.HarvestProduct();
            inventory.AddSeeds(seedDropAmount, "Carrot");
            inventory.carrotGrowCount++;
        }
        
        currentState = PlotState.Empty;
        currentPlantType = "";
        plantRenderer.sprite = emptySprite;
        
        inventory.SaveInventory();
        inventory.UpdateUI();
        SavePlotData();
        Debug.Log("Hasat tamamlandı. " + " ürün ve " + seedDropAmount + " tohum elde edildi.");
    }
    
    private void UpdateGrowthVisuals()
    {
        float ratio = growthTimer / timeToGrow;
        
        if (currentPlantType == "Seed")
        {
            if (ratio < 0.33f)
            {
                plantRenderer.sprite = plantedSprite;
            }
            else if (ratio < 0.66f)
            {
                plantRenderer.sprite = growingSprite1;
            }
            else
            {
                plantRenderer.sprite = growingSprite2;
            }
        }
        else if (currentPlantType == "Carrot")
        {
            if (ratio < 0.33f)
            {
                plantRenderer.sprite = plantedCarrotSprite;
            }
            else if (ratio < 0.66f)
            {
                plantRenderer.sprite = growingCarrotSprite1;
            }
            else
            {
                plantRenderer.sprite = growingCarrotSprite2;
            }
        }
    }


    private void SavePlotData()
    {
        PlayerPrefs.SetInt("PlotState_" + plotID, (int)currentState);
        PlayerPrefs.SetString("PlantType_" + plotID, currentPlantType);
        PlayerPrefs.SetFloat("GrowthTimer_" + plotID, growthTimer);
        PlayerPrefs.Save();
    }

    private void LoadPlotData()
    {
        if (PlayerPrefs.HasKey("PlotState_" + plotID))
        {
            currentState = (PlotState)PlayerPrefs.GetInt("PlotState_" + plotID);
            currentPlantType = PlayerPrefs.GetString("PlantType_" + plotID);
            growthTimer = PlayerPrefs.GetFloat("GrowthTimer_" + plotID);

            if (currentState == PlotState.Growing)
            {
                UpdateGrowthVisuals(); 
            }
            else if (currentState == PlotState.Harvestable)
            {
                if (currentPlantType == "Seed")
                {
                    plantRenderer.sprite = harvestableSeedSprite;
                }
                else if (currentPlantType == "Carrot")
                {
                    plantRenderer.sprite = harvestableCarrotSprite;
                }
            }
            else if (currentState == PlotState.Empty)
            {
                plantRenderer.sprite = emptySprite;
                currentPlantType = "";
                growthTimer = 0f;
            }
        }
    }
}