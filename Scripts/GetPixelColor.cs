using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GetPixelColor : MonoBehaviour, IPointerDownHandler
{
    public Texture2D baseTexture;
    public Texture2D paintTexture;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        int x = Mathf.FloorToInt(localPoint.x + GetComponent<RectTransform>().rect.width / 2);
        int y = Mathf.FloorToInt(localPoint.y + GetComponent<RectTransform>().rect.height / 2);
        int rx = Mathf.FloorToInt(GetComponent<RectTransform>().rect.x);
        int ry = Mathf.FloorToInt(GetComponent<RectTransform>().rect.y);
        int width = Mathf.FloorToInt(GetComponent<RectTransform>().rect.width);
        int height = Mathf.FloorToInt(GetComponent<RectTransform>().rect.height);
        Color32 pixelColor = baseTexture.GetPixel(x, y);
        Color32[] pixelArray = baseTexture.GetPixels32(0);
        Color32[] paintPixelArray = paintTexture.GetPixels32(0);

        int sameCount = 0;
        bool isSame = false;
        int collationCount = 10000;
        for (int i = 0; i < pixelArray.Length; i++)
        {
            if (pixelArray[i].a == paintPixelArray[i].a)
            {
                sameCount++;
            }
            if (sameCount > pixelArray.Length * 0.7f)
            {
                isSame = true;
                break;
            }
            if (i > pixelArray.Length * 0.5f && sameCount < pixelArray.Length * 0.2f)
            {
                break;
            }
        }

        if (isSame)
            Debug.Log("collect");
        else
            Debug.Log("wrong");
        // Debug.Log("Clicked position: " + eventData.position + ", Pixel color: " + pixelColor);
    }
}