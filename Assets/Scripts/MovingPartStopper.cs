using UnityEngine;

public class MovingPartStopper : MonoBehaviour
{
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider == null)
        {
            return;
        }
        if (hit.transform.TryGetComponent<MovingPart>(out MovingPart Component))
        {
            if (!Component.CanBeStopped)
                return;
            Component.IsActive = !Component.IsActive;
        }

    }
}
