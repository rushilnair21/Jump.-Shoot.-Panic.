using UnityEngine;

public class ShootableItem : MonoBehaviour
{
    public int scoreValue = 5; 

    void OnMouseDown()
    {
        if (GameManager.I != null && GameManager.I.TrySpendAmmo(1))
        {
            GameManager.I.AddCoin(scoreValue); 
            
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            
            if (playerObj != null)
            {
                LineRenderer lr = playerObj.GetComponent<LineRenderer>();
                
                if (lr != null)
                {
                    
                    lr.SetPosition(0, playerObj.transform.position);
                    lr.SetPosition(1, transform.position);
                }
            }

            Destroy(gameObject); 
        }
    }
}