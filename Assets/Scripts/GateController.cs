using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum GateType {fatterType, thinnerType, tallerType, shoterType }
public class GateController : MonoBehaviour
{
    public int gateValue;
    public RawImage gateImage;
    public TextMeshProUGUI gateText;
    public Texture[] textures;
    public GateType gateType;
    public GameObject playerGO;
    public PlayerController playerScript;
    bool hasGateUsed;

    GateHolderController gateHolderScript;
    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGO.GetComponent<PlayerController>();
        gateHolderScript = transform.parent.gameObject.GetComponent<GateHolderController>();
        AddGateValueAndSymbol();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGateValueAndSymbol()
    {
        gateText.text = gateValue.ToString();

        switch(gateType)
        {
            case GateType.fatterType:
                gateImage.texture = textures[0];
                break;

            case GateType.thinnerType:
                gateImage.texture = textures[1];
                break;

            case GateType.tallerType:
                gateImage.texture = textures[2];
                break;

            case GateType.shoterType:
                gateImage.texture = textures[3];
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !hasGateUsed)
        {
            hasGateUsed = true;
            playerScript.GatePassed(gateType, gateValue);

            if(gateHolderScript != null)
            {
                gateHolderScript.CloseGate();
            }
            Destroy(gameObject);
        }
    }
}
