using UnityEngine;

public class RandomizeTiles : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;

    [ContextMenu("Randomize")]
    private void Randomize()
    {
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(sr, "Randomize Sprites");
#endif
            sr.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}