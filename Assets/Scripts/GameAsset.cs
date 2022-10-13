using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAsset : MonoBehaviour
{
    public Sprite[] HEADS;
    public Sprite[] SEGMENTS;

    private Sprite head;
    private Sprite segment;

    void Awake()
    {
        // HEADS = (Sprite[])Resources.LoadAll("Sprites/Heads");
        // SEGMENTS = (Sprite[])Resources.LoadAll("Sprites/Segments");
        int i = Random.Range(0, HEADS.Length);
        Debug.Log("HEADS: " + i);
        head = HEADS[i];
        i = Random.Range(0, SEGMENTS.Length);
        Debug.Log("SEGMENTS: " + i);
        segment = SEGMENTS[i];
        DontDestroyOnLoad(this.gameObject);
    }

    public Sprite GetHead()
    {
        return head;
    }

    public Sprite GetSegment()
    {
        return segment;
    }
}
