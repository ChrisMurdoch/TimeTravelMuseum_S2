using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeHealthScript : MonoBehaviour
{

    public int maxHits;
    public TreeCutter axe;
    public GameObject currentTreeModel;
    public GameObject[] DamagedTrees;
    public GameObject[] DeadTree;
    public float treeBottomY;
    public float treeTopY;

    private int currentHits;


    // Update is called once per frame
    void Update()
    {
    }


    // need to add function to carve wedge
    public void DamageTree() {
        currentHits++;
        Debug.Log("DAMAGETREE CALLED, currentHits = " + currentHits);

        if(currentHits < maxHits) {
            Debug.Log("MAX HITS NOT REACHED");
            GameObject newTree = Instantiate(DamagedTrees[currentHits - 1], this.transform) as GameObject;
            Vector3 treeRotation = new Vector3(currentTreeModel.transform.rotation.x, currentTreeModel.transform.rotation.y, currentTreeModel.transform.rotation.z);
            SetParent(newTree, currentTreeModel.transform.position, treeRotation);
            Destroy(currentTreeModel.gameObject);
            currentTreeModel = newTree;
       }

       else {
           Debug.Log("MAX HITS REACHED");
           
           GameObject newTreeTop = Instantiate(DeadTree[0], this.transform) as GameObject;
            GameObject newTreeBottom = Instantiate(DeadTree[1], this.transform) as GameObject;

           Vector3 treeTopPos = new Vector3 (currentTreeModel.transform.position.x, treeTopY, currentTreeModel.transform.position.z);
           Vector3 treeBottomPos = new Vector3(currentTreeModel.transform.position.x, treeBottomY, currentTreeModel.transform.position.z);
           Vector3 treeRotation = new Vector3(currentTreeModel.transform.rotation.x, currentTreeModel.transform.rotation.y, currentTreeModel.transform.rotation.z);

           SetParent(newTreeTop, treeTopPos, treeRotation);
           SetParent(newTreeBottom, treeBottomPos, treeRotation);

           Destroy(currentTreeModel.gameObject);

           currentTreeModel = newTreeTop;
           this.gameObject.tag = "Untagged";

            //helps tree fall on its side
           currentTreeModel.GetComponent<Rigidbody>().AddTorque(transform.right * 300);
           
           axe.IncreaseNumTreesCut();
       }
    }

    private void SetParent(GameObject childObject, Vector3 position, Vector3 rotation) {
        childObject.transform.parent = this.transform;
        childObject.transform.position = position;
        //childObject.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
}
