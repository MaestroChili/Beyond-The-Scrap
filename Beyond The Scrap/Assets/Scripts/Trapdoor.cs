
using System.Collections;
using UnityEngine;

public class Trapdoor : MonoBehaviour
{
    [SerializeField] private float openDuration = 1f;

    public void Open()
    {
        StartCoroutine(OpenTrapdoor());
    }

    IEnumerator OpenTrapdoor()
    {
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 90);

        while (elapsedTime < openDuration)
        {
            transform.rotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                elapsedTime / openDuration
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }
}
