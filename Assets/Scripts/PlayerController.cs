using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5f;
    float xSpeed;
    float maxXValue = 4.28f;//inspectorden ayarlad�k karakterimiz en fazla x ekseninde 4.28 kadar sa�a sola gitsini ayarl�caz
    bool isPlayerMoving;

    public GameObject headBoxGO;//kafada yapaca��m�z de�i�ikler olaca�� i�in bunu buraya �a��r�caz
    private ScaleCalculator scaleCalculator;

    Renderer headBoxRenderer;
    void Start()
    {
        isPlayerMoving = true;
        scaleCalculator = new ScaleCalculator();
        headBoxRenderer = headBoxGO.transform.GetChild(0).GetComponent<Renderer>();//headboxgonun child�n�n componenti oldupu i�in b�yle �a��r�yoruz
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerMoving == false)
        {
            return;//e�er isplayermoving false ise return yap yani fonksiyonun i�indeki a�a��daki k�sm� �al��t�rma yukar� d�n
        }
        float touchX = 0;
        float newXValue;//x ekseninde hareket ettirmek i�in bir de�er olu�turduk

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)//ekrana dokundu�umuzda ve parma��m�z� hareket ettirdi�imizde
        {
            xSpeed = 250;
            touchX = Input.GetTouch(0).deltaPosition.x / Screen.width;//buralarda girdi�imiz de�erleri uygulad���m�z �eyleri
                                                //newxvalueya ekleyiyoruz alt k�s�mda (32)
        }
        else if(Input.GetMouseButton(0))
        {
            xSpeed = 250f;
            touchX = Input.GetAxis("Mouse X");
        }
        newXValue = transform.position.x + xSpeed * touchX * Time.deltaTime;//newxvalue'yu ayarl�yoruz ve karakteri hareket ettirdi�imiz noktaya ekliyoruz(34)
        newXValue = Mathf.Clamp(newXValue, -maxXValue, maxXValue);//newxvalu en fazla maxvalue de�erini en az -maxxvalue'yu al�cak

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
        Debug.Log("Kap�danGe�ildi");
    }

    public void TouchedToColorBox(Material boxMat)
    {
        //karakterin kafas�ndaki rengi de�i�tir
        headBoxRenderer.material = boxMat;
    }
}
