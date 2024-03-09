using UnityEngine;

public class Item : MonoBehaviour
{
    public int healthBonus = 10;
    public int waterBonus = 5;
    public int foodBonus = 8;

    private void Update()
    {
        
    }

    public void UseItem()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Stats playerStats = player.GetComponent<Stats>();

            if (playerStats != null)
            {
                playerStats.AddHealth(healthBonus);
                playerStats.AddWater(waterBonus);
                playerStats.AddFood(foodBonus);

                // You can add more logic here if needed

                Destroy(gameObject); // Destroy the item after use
            }
        }
    }
}
