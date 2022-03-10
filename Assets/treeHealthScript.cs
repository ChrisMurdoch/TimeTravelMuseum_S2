using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeHealthScript : MonoBehaviour
{

    public int maxHits;
    public TreeCutter axe;
    private int currentHits;


    // Update is called once per frame
    void Update()
    {
        if(currentHits >= maxHits)
        {
            axe.IncreaseNumTreesCut();
            Destroy(this.gameObject); //replace with falling tree animation
        }
    }


    // need to add function to carve wedge
    public void DamageTree() {
        currentHits++;
        Debug.Log(currentHits + " hits");
    }
}
