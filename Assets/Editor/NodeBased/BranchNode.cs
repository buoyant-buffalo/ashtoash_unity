using UnityEngine;
using System.Collections;
using UnityEditor;


public class BranchNode : BaseInputNode {


    private BaseInputNode input1;
    private Rect input1Rect;

    private BaseInputNode input2;
    private Rect input2Rect;

    private BaseInputNode input3;
    private Rect input3Rect;

    

    public BranchNode(int globalID)
    {
        //windowTitle = "BranchNode";
        base.myType = 0;
        hasInputs = true;
        numberID += globalID;
        windowTitle = numberID.ToString();
}

    public override void DrawWindow()
    {
        //GUILayout.Label("Branch");
        base.DrawCurves();
        //GUILayout.Label("ID: " + numberID);
        
        Event e = Event.current;

        //calculationType = (CalculationType)EditorGUILayout.EnumPopup("Calculation Type", calculationType);
        //base.content = EditorGUILayout.TextField("", base.content);


        string input1Title = "None";

        if (input1)
        {
            input1Title = input1.getResult();
        }
        GUILayout.Label("Input 1: " + input1Title);
        if (e.type == EventType.Repaint)
        {
            input1Rect = GUILayoutUtility.GetLastRect();
        }
       

        string input2Title = "None";

        if (input2)
        {
            input2Title = input2.getResult();
        }
        GUILayout.Label("Input 2: " + input2Title);
        if (e.type == EventType.Repaint)
        {
            input2Rect = GUILayoutUtility.GetLastRect();
        }
       

        string input3Title = "None";

        if (input3)
        {
            input3Title = input3.getResult();
        }
        GUILayout.Label("Input 3: " + input3Title);

        if (e.type == EventType.Repaint)
        {
            input3Rect = GUILayoutUtility.GetLastRect();
        }
        
        base.length = EditorGUILayout.TextField("Time");





    }

    public override void SetInput(BaseInputNode input, Vector2 clickPos)
    {
        clickPos.x -= windowRect.x;
        clickPos.y -= windowRect.y;

        if (input1Rect.Contains(clickPos))
        {
            input1 = input;

        }
        else if (input2Rect.Contains(clickPos))
        {
            input2 = input;
        }
        else if (input3Rect.Contains(clickPos))
        {
            input3 = input;
        }
    }

    public override void DrawCurves()
    {
        if (input1)
        {
            Rect rect = windowRect;
            rect.x += input1Rect.x;
            rect.y += input1Rect.y + input1Rect.height / 2;
            rect.width = 1;
            rect.height = 1;

            NodeEditor.DrawNodeCurve(input1.windowRect, rect);
        }

        if (input2)
        {
            Rect rect = windowRect;
            rect.x += input2Rect.x;
            rect.y += input2Rect.y + input2Rect.height / 2;
            rect.width = 1;
            rect.height = 1;

            NodeEditor.DrawNodeCurve(input2.windowRect, rect);
        }

        if (input3)
        {
            Rect rect = windowRect;
            rect.x += input3Rect.x;
            rect.y += input3Rect.y + input3Rect.height / 2;
            rect.width = 1;
            rect.height = 1;

            NodeEditor.DrawNodeCurve(input3.windowRect, rect);
        }
    }


    public override BaseInputNode ClickedOnInput(Vector2 pos)
    {
        BaseInputNode retVal = null;

        pos.x -= windowRect.x;
        pos.y -= windowRect.y;

        if (input1Rect.Contains(pos))
        {
            retVal = input1;
            input1 = null;
        }
        else if (input2Rect.Contains(pos))
        {
            retVal = input2;
            input2 = null;
        }
        else if (input3Rect.Contains(pos))
        {
            retVal = input3;
            input3 = null;
        }

        return retVal;
    }

    public override void NodeDeleted(BaseNode node)
    {
        if (node.Equals(input1))
        {
            input1 = null;
        }

        if (node.Equals(input2))
        {
            input2 = null;
        }

        if (node.Equals(input3))
        {
            input3 = null;
        }
    }

    public override string getResult()
    {
        //its the ID of the window btw
        return windowTitle;
    }

}
