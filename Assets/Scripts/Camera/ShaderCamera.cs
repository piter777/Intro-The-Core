using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ShaderCamera : MonoBehaviour {
	public Material Mat;
	// Use this for initialization

	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, Mat);
	}
	// Update is called once per frame

}
