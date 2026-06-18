using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score : MonoBehaviour
{
    private int scor = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {
            gamemanager.instance.AddFruit();

            Debug.Log("Score: " + scor);

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("tractor"))
    {
        print("barkhord");
        gamemanager.instance.LoseGame();
    }

    }
}
