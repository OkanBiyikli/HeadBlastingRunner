using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5f;
    float xSpeed;
    float maxXValue = 4.28f;//inspectorden ayarladýk karakterimiz en fazla x ekseninde 4.28 kadar saða sola gitsini ayarlýcaz
    bool isPlayerMoving;

    public GameObject headBoxGO;//kafada yapacaðýmýz deðiþikler olacaðý için bunu buraya çaðýrýcaz
    private ScaleCalculator scaleCalculator;

    Renderer headBoxRenderer;
    void Start()
    {
        isPlayerMoving = true;
        scaleCalculator = new ScaleCalculator();
        headBoxRenderer = headBoxGO.transform.GetChild(0).GetComponent<Renderer>();//headboxgonun childýnýn componenti oldupu için böyle çaðýrýyoruz
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerMoving == false)
        {
            return;//eðer isplayermoving false ise return yap yani fonksiyonun içindeki aþaðýdaki kýsmý çalýþtýrma yukarý dön
        }
        float touchX = 0;
        float newXValue;//x ekseninde hareket ettirmek için bir deðer oluþturduk

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)//ekrana dokunduðumuzda ve parmaðýmýzý hareket ettirdiðimizde
        {
            xSpeed = 250;
            touchX = Input.GetTouch(0).deltaPosition.x / Screen.width;//buralarda girdiðimiz deðerleri uyguladýðýmýz þeyleri
                                                //newxvalueya ekleyiyoruz alt kýsýmda (32)
        }
        else if(Input.GetMouseButton(0))
        {
            xSpeed = 250f;
            touchX = Input.GetAxis("Mouse X");
        }
        newXValue = transform.position.x + xSpeed * touchX * Time.deltaTime;//newxvalue'yu ayarlýyoruz ve karakteri hareket ettirdiðimiz noktaya ekliyoruz(34)
        newXValue = Mathf.Clamp(newXValue, -maxXValue, maxXValue);//newxvalu en fazla maxvalue deðerini en az -maxxvalue'yu alýcak

        Vector3 newPlayerPosition = new Vector3(newXValue, transform.position.y, transform.position.z + playerSpeed * Time.deltaTime);
        transform.position = newPlayerPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "FinishLine")
        {
            isPlayerMoving = false;//finishlinea dokununca karakteri durdur
        }
    }

    public void GatePassed(GateType gateType, int gateValue)
    {
        headBoxGO.transform.localScale = scaleCalculator.CalculatePlayerHeadSize(gateType, gateValue, headBoxGO.transform);
        Debug.Log("KapýdanGeçildi");
    }

    public void TouchedToColorBox(Material boxMat)
    {
        //karakterin kafasýndaki rengi deðiþtir
        headBoxRenderer.material = boxMat;
    }
}
