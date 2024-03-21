using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;

public class BuildManager : MonoBehaviour
{
	[UnityEditor.Callbacks.DidReloadScripts]
	private static void OnScriptsReloaded()
	{
		EditorApplication.Beep();
	}
}