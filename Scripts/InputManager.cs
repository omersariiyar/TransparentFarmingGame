using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InventorySystem inventorySystem;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    void HandleClick()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            FarmPlot plot = hit.collider.GetComponent<FarmPlot>();

            if (plot != null)
            {
                Debug.Log("Tarlaya tıklandı: " + hit.collider.gameObject.name);
                
                plot.Interact();
            }
        }
    }
}