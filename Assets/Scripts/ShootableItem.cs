using UnityEngine;

public class ShootableItem : MonoBehaviour
{
    public int scoreValue = 5; 

    void OnMouseDown()
    {
        if (GameManager.I != null && GameManager.I.ammo > 0)
        {
            GameManager.I.ammo -= 1;
            GameManager.I.AddCoin(scoreValue);
            
            UnityEngine.Debug.Log("Object Shot! Ammo left: " + GameManager.I.ammo);
            Destroy(gameObject);
        }
        else
        {
            UnityEngine.Debug.Log("No ammo or GameManager missing!");
        }
    }
}