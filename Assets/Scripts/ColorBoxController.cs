using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBoxController : MonoBehaviour
{
    public Material boxMat;
    private GameObject playerGO;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerController = playerGO.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerController.TouchedToColorBox(boxMat);
            //playercontrollera s�yle, bir renkli kutuya dokundu ve dokundu�u kutunun materiyali bu.    
            Destroy(gameObject);
        }
    }
}
