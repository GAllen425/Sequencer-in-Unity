using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClicking : MonoBehaviour
{
    public GameObject buttonBar, bpmMinus, bpmPlus;
    public BetterBlockAreaManager blockAreaManager;
    public Text bpmText;
    int bpm = 120;

    void Start()
    {
        buttonBar = this.gameObject.transform.GetChild(0).gameObject;
        blockAreaManager = GameObject.FindGameObjectWithTag("BlockManager")
                                     .GetComponent<BetterBlockAreaManager>();
    }

    void Update(){
        if (Input.GetMouseButtonDown(0)){ // if left button pressed...
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                Debug.Log(hit);
                if (hit.transform.gameObject == bpmMinus)
                {
                    bpm--;
                    blockAreaManager.SetBpm(bpm);
                }
                else if (hit.transform.gameObject == bpmPlus)
                {
                    bpm++;
                    blockAreaManager.SetBpm(bpm);
                }
            }
        }

        bpmText.text = this.bpm.ToString();
    }
}
