using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BetterBlockAreaManager : MonoBehaviour
{
    public UnityEvent invokeMethod;

    public GameObject blockPrefab;
    public List<GameObject> rows;
    public float originDisplacement = -50.0f;
    public Bounds bounds;

    void Start()
    {
        invokeMethod.AddListener(addRowAndDraw);

        bounds = new Bounds(0.0f,0.0f, 200.0f, 100.0f);
        rows = new List<GameObject>();
        AddRow();
        DrawRows();
    }

    void Update()
    {
        if (Input.GetKeyDown("w") && invokeMethod != null)
        {
            invokeMethod.Invoke();
        }
    }

    void DrawRows()
    {
        float vertDisplacement = bounds.GetHeight()/rows.Count;

        for (int i=0; i<rows.Count; i++)
        {
            rows[i].transform.SetParent(this.transform);
            rows[i].transform.localPosition = new Vector3(0,0, originDisplacement + vertDisplacement * (float)i);
            rows[i].transform.localScale =  new Vector3(this.transform.localScale.x,
                                                        this.transform.localScale.y, 
                                                        (1.0f/rows.Count) * this.transform.localScale.z);

            BetterBlockRow bbr = rows[i].GetComponent<BetterBlockRow>();
            Bounds tempBounds = new Bounds();
            tempBounds.Copy(bounds);

            bbr.Setup(tempBounds, 4, blockPrefab, rows[i].transform);
            bbr.Draw();
            bbr.GetComponent<Metronome>().StartMetronome();
        }

    }

    void AddRow()
    {
        if (blockPrefab != null)
        {
            rows.Add(new GameObject("BlockRow"));
            rows[rows.Count - 1].transform.SetParent(this.transform);
            rows[rows.Count - 1].AddComponent<BetterBlockRow>();
        }
    }

    void CleanRows()
    {
        for (int i = 0; i < rows.Count; i++)
            rows[i].GetComponent<BetterBlockRow>().CleanInstance();
    }

    void addRowAndDraw()
    {
        CleanRows();
        AddRow();
        DrawRows();
    }

    public void SetBpm(int bpm)
    {
        for (int i=0; i<rows.Count; i++)
        {
            rows[i].GetComponent<BetterBlockRow>().met.nextBPM = bpm;
        }
    }
}

public class Bounds
{
    public float minX, minY, maxX, maxY;

    public Bounds () 
    {
        this.minX = 0.0f;
        this.minY = 0.0f;
        this.maxX = 1.0f;
        this.maxY = 1.0f;
    }

    public Bounds (float minX, float minY, float maxX, float maxY)
    {
        this.minX = minX;
        this.minY = minY;
        this.maxX = maxX;
        this.maxY = maxY;
    }

    public float GetWidth()
    {
        return Mathf.Abs(maxX-minX);
    }

    public float GetHeight()
    {
        return Mathf.Abs(maxY-minY);
    }

    public float widthPerBlock(int num)
    {
        return this.GetWidth()/(float)num;
    }

    public void Copy(Bounds b)
    {
        this.minX = b.minX;
        this.minY = b.minY;
        this.maxX = b.maxX;
        this.maxY = b.maxY;
    }
}

