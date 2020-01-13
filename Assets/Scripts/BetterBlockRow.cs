using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterBlockRow : MonoBehaviour
{

    public GameObject blockPrefab;
    public List<GameObject> columns;
    public Bounds bounds;
    public int numberOfBlocks;
    public Metronome met;

    public void Start()
    {
    }

    public void CleanInstance()
    {
        for (int i = 0; i<columns.Count; i++)
        {
            Destroy(columns[i]);
        }
        columns.Clear();
    }

    public void Setup(Bounds bounds, int numberOfBlocks, GameObject prefab, Transform parent) 
    {
        if (columns == null)
            columns = new List<GameObject>();

        this.bounds = bounds;
        this.numberOfBlocks = numberOfBlocks;
        this.blockPrefab = prefab;
        this.transform.SetParent(parent);

        if (met == null)
            met = this.gameObject.AddComponent<Metronome>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw()
    {
        if (blockPrefab != null && columns != null)
        {
            for (int i=0; i<numberOfBlocks; i++)
            {
                columns.Add((GameObject)Instantiate(blockPrefab, 
                                        this.transform.position,
                                        Quaternion.identity));
                columns[i].transform.SetParent(this.transform);
                columns[i].transform.localPosition = new Vector3(0,0,0);
                //columns[i].transform.localScale = new Vector3(1,1,1);

                columns[i].AddComponent<Block>();
                columns[i].GetComponent<Block>()
                         .Setup(bounds.GetWidth()/numberOfBlocks,
                                bounds.GetHeight(),
                                10.0f,
                                (i+0.5f)*(bounds.GetWidth()/numberOfBlocks)-bounds.GetWidth()/2.0f, 
                                bounds.GetHeight()-bounds.GetHeight()/2,
                                columns[i].transform);

            }
        }
    }
}
