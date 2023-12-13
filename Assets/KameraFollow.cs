using UnityEngine;

public class KameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek karakterin transformu
    public float smoothSpeed = 0.125f; // Kamera hareketinin yumuþaklýðý
    public float tabXsiniri, tavXsiniri;
    public float tabYsiniri, tavYsiniri;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 poz = new Vector3(Mathf.Clamp(target.transform.position.x, tabXsiniri, tavXsiniri), 
                Mathf.Clamp(target.transform.position.y, tabYsiniri, tavYsiniri), transform.position.z);

            transform.position = Vector3.Lerp(transform.position, poz, smoothSpeed);
        }
    }
}
