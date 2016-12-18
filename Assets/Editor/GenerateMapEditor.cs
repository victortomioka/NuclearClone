using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GenerateMap))]
public class GenerateMapEditor : Editor {

	public override void OnInspectorGUI(){

		DrawDefaultInspector();

		GenerateMap gen = (GenerateMap)target;
		if(GUILayout.Button("Generate!"))
		{
			gen.createMap();
		}

	}
}