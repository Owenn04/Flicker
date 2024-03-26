using UnityEngine;

public class NoteInteraction : MonoBehaviour
{
    public GameObject noteCanvas1;

    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            noteCanvas1.SetActive(true); // Activate the canvas when player enters
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            noteCanvas1.SetActive(false); // Deactivate the canvas when player exits
        }
    }


}
