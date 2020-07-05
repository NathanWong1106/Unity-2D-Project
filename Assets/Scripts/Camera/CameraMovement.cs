using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector2 originalPos = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Vector2 targetPos = new Vector2(Random.Range(-1f, 1f) * magnitude, Random.Range(-1f, 1f) * magnitude);
            transform.localPosition = Vector2.Lerp(transform.localPosition, targetPos, 0.1f);
            elapsedTime += Time.deltaTime;

            yield return null; //wait one frame
        }

        transform.localPosition = originalPos;
    }
}
