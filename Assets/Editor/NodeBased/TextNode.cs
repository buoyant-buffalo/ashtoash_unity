using UnityEngine;
using System.Collections;
using UnityEditor;


public class TextNode : BaseInputNode
{

    private BaseInputNode input1;
    private Rect input1Rect;

    private TextType textType;

    public enum TextType
    {
        Option,
        Subtitle
    }

    public TextNode(int globalID)
    {
        //windowTitle = "Text Node";
        hasInputs = true;
        numberID += globalID;
        windowTitle = numberID.ToString();

    }

    public override void DrawWindow()
    {
        base.DrawCurves();

        Event e = Event.current;

        textType = (TextType)EditorGUILayout.EnumPopup("", textType);
        


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

        base.content = EditorGUILayout.TextField("Content");
        base.length = EditorGUILayout.TextField("Time");

        EditorGUILayout.TextField("Notes");
    }

    public void SetType()
    {
        switch (textType)
        {
            case TextType.Option:
                base.myType = 1;
                break;

            case TextType.Subtitle:
                base.myType = 2;
                windowTitle = "Subtitle";
                break;
        }
    }

    public override void SetInput(BaseInputNode input, Vector2 clickPos)
    {
        clickPos.x -= windowRect.x;
        clickPos.y -= windowRect.y;

        if (input1Rect.Contains(clickPos))
        {
            input1 = input;

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

        return retVal;
    }

    public override void NodeDeleted(BaseNode node)
    {
        if (node.Equals(input1))
        {
            input1 = null;
        }
    }

    public override string getResult()
    {
        //its the ID of the window btw
        return windowTitle;
    }

}
