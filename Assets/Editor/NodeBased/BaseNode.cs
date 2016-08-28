﻿using UnityEngine;
using System.Collections;
using UnityEditor;

public abstract class BaseNode : ScriptableObject 
{
	public Rect windowRect;

	public bool hasInputs = false;

    // 0 = branch, 1 = option, 2 = sub
    public int myType;
    public int numberID;
    public string length;
    public string content = "";




    public string windowTitle = "";

	public virtual void DrawWindow()
	{
		windowTitle = EditorGUILayout.TextField("Title", windowTitle);
	}

	public abstract void DrawCurves();

	public virtual void SetInput(BaseInputNode input, Vector2 clickPos)
	{

	}

	public virtual void NodeDeleted(BaseNode node)
	{}

	public virtual BaseInputNode ClickedOnInput(Vector2 pos)
	{
		return null;
	}
}
