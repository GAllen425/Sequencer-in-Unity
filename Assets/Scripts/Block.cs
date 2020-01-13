using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Block : MonoBehaviour {
    public float width;
    public float height;
    public float blockThickness;

    private float x;
    private float z;

    private float Epsilon = 2.0f;

    public void Update()
    {
        if (this.transform.localScale.x > TargetScale().x ||
            this.transform.localScale.y > TargetScale().y ||
            this.transform.localScale.z > TargetScale().z)
        {
            this.transform.localScale = TargetScale();
        }
    }

    public void Setup (float width,
                       float height,
                       float blockThickness, 
                       float x,
                       float z,
                       Transform parent)
    {
        this.width = width;
        this.height = height;
        this.blockThickness = blockThickness;
        this.x = x;
        this.z = z;
        this.transform.SetParent(parent);
        UpdateTransform();
    }

    public void UpdateTransform ()
    {
        this.transform.localPosition = new Vector3(x,
                                                   blockThickness/2,
                                                   z);

        this.transform.localScale = DefaultScale();
    }

    public Vector3 DefaultScale()
    {
        return new Vector3(0.7f*width, blockThickness, 0.7f*height);
    }

    public Vector3 TargetScale()
    {
        return new Vector3(0.9f*width, blockThickness, 0.9f*height);
    }

    public void BounceAnimation()
    {
        IEnumerator coroutine = ChangeScale(DefaultScale(), TargetScale(), 10.0f);
        StopCoroutine(coroutine);
        this.transform.localScale = DefaultScale();
        StartCoroutine(coroutine);
    }

    IEnumerator ChangeScale(Vector3 start, Vector3 target, float speed)
    {
        this.transform.localScale = start;
        Vector3 move = target - start;

        Vector3 diff = move;
        while (Mathf.Abs(diff.x) > Epsilon ||
               Mathf.Abs(diff.y) > Epsilon ||
               Mathf.Abs(diff.z) > Epsilon)
        {
            diff = target - this.transform.localScale;
            this.transform.localScale += new Vector3(move.x, move.y, move.z) * speed * Time.deltaTime;
            yield return null;
        }

        yield return null;

        diff = start - this.transform.localScale;
        while (Mathf.Abs(diff.x) > Epsilon ||
               Mathf.Abs(diff.y) > Epsilon ||
               Mathf.Abs(diff.z) > Epsilon)
        {
            diff = start - this.transform.localScale;
            this.transform.localScale -= new Vector3(move.x, move.y, move.z) * speed * Time.deltaTime;
            yield return null;
        }

        yield break;
    }
}

