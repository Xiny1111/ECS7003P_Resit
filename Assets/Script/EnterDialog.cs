
using UnityEngine;

public class EnterDialog : MonoBehaviour
{
    //next level enter dialog
    public GameObject enterDialog;
    //show next level entrance panel
    private void OnTriggerEnter2D(Collider2D collision)
   {
        if(collision.tag == "Player")
        {
            enterDialog.SetActive(true);
        }

   }

    //close the entrance panel
   private void OnTriggerExit2D(Collider2D collision)
   {
        if(collision.tag == "Player")
        {
            enterDialog.SetActive(false);
        }
   }
}
