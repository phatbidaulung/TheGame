using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class Test : MonoBehaviour
{
    public ScrollRect scrollView;
    public GameObject centeredObject;
    public TMP_Text indexSkin;

    void Update()
    {
        // Get the center point of the scroll view's viewport in world space
        Vector3 viewportCenterWorld = scrollView.viewport.TransformPoint(scrollView.viewport.rect.center);

        // Convert the center point to local space of the content
        Vector3 viewportCenterLocal = scrollView.content.InverseTransformPoint(viewportCenterWorld);

        // Find the closest child GameObject to the center point
        float closestDistance = Mathf.Infinity;
        foreach (Transform child in scrollView.content.transform)
        {
            float distance = Mathf.Abs(child.transform.localPosition.x - viewportCenterLocal.x);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                centeredObject = child.gameObject;
                indexSkin = child.Find("IndexSkin").gameObject.GetComponent<TMP_Text>();
            }
            Debug.LogWarning($"Name index skin {child.Find("IndexSkin").name} Value skin: {indexSkin.text}");
            // Debug.LogWarning($"Value 01: {closestDistance} Value 02: {closestDistance}");
        }
    }
    private void LateUpdate() {
            // scrollView.horizontalNormalizedPosition = 0.1f;
        
    }
}