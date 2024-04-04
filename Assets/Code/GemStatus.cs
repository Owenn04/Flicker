using UnityEngine;
using TMPro; 

public class GemPickup : MonoBehaviour
{
    public TextMeshProUGUI gemText;
    public altWalking altWalking;

    private int totalGems = 2; // Total number of gems in the level
    private int gemsPickedUp = 0; // Number of gems currently picked up

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gemsPickedUp++;
            altWalking.hasMoonGem = true;
            UpdateUI();

            Invoke("ClearGemText", 2f);

            gameObject.SetActive(false);
        }
    }

    private void UpdateUI()
    {

        if (gemText != null && altWalking.hasExitGem){
            gemsPickedUp++;
            gemText.text = "Gems Collected: " + gemsPickedUp + "/" + totalGems;
            
        }else if(gemText != null){
            gemText.text = "Gems Collected: " + gemsPickedUp + "/" + totalGems;
        }
    }

    private void ClearGemText()
    {
        if (gemText != null)
        {
            gemText.text = "";
        }
    }
}