using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace StupidTemplate.Classes
{
    using UnityEngine;

    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class AnimatedGradient : MonoBehaviour
    {
        public float Speed = 1f;

        private Mesh meshInstance;
        private Mesh originalMesh;

        private Color[] colors;
        private float hueShift;

        void Start()
        {
            MeshFilter mf = GetComponent<MeshFilter>();
            if (mf == null || mf.mesh == null)
            {
                Debug.LogWarning("No mesh found on this object!");
                return;
            }

            // Make a unique instance of the mesh
            meshInstance = Mesh.Instantiate(mf.mesh);
            originalMesh = Mesh.Instantiate(mf.mesh);
            mf.mesh = meshInstance;

            // Assign a material that supports vertex colors
            MeshRenderer mr = GetComponent<MeshRenderer>();
            Material mat = new Material(Shader.Find("Sprites/Default")); // supports vertex colors
            mat.SetInt("_ZTest", 4);
            mat.SetInt("_ZWrite", 1);
            mr.material = mat;
        }

        void Update()
        {
            if (meshInstance == null || originalMesh == null) return;

            int vertexCount = meshInstance.vertexCount;
            colors = new Color[vertexCount];

            hueShift += Time.deltaTime * Speed;

            for (int i = 0; i < vertexCount; i++)
            {
                Vector3 worldPos = transform.TransformPoint(originalMesh.vertices[i]);
                transform.rotation = transform.parent ? transform.parent.rotation : transform.rotation;
                float hue = Mathf.Repeat(hueShift + (-worldPos.x - worldPos.y), 1f);
                colors[i] = Color.HSVToRGB(hue, 1f, 0.6f); // adjust brightness if needed
            }

            meshInstance.colors = colors;
        }
    }
}
