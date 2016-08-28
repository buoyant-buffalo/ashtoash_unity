using UnityEngine;
using System.Collections;
using UnityEditor;

public class FullTreeNode : BaseInputNode {

    //private string inputValue = "";


    public FullTreeNode()
    {
        windowTitle = "FullTree";
        hasInputs = false;

    }
    public override void DrawWindow()
    {
        base.DrawWindow();

        //inputValue = EditorGUILayout.TextField("Value", inputValue);
        if (GUILayout.Button("Create Tree"))
        {
            GameObject go = new GameObject(windowTitle);
            //go.transform.position = new Vector3(0, 0, 0);

            //calculateRandom();
        }
    }

}
