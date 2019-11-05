////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: DeathPlane.cs
//FileType: Visual C# Source file
//Author : Rohan Juneja - 300987725
//Created On : 11/02/2019
//Last Modified On : 11/04/2019
//Copy Rights : Artificial Order
//Description : Class for defining Death plane that kills the player whenever he falls
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathPlane : MonoBehaviour
{
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = spawnPoint.transform.position;
            HeroController.livesCount -= 1;
        }
    }
}
