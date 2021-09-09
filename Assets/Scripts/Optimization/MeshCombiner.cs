using System;
using UnityEngine;

namespace Optimization
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    [RequireComponent(typeof(MeshFilter))]
    public class MeshCombiner : MonoBehaviour
    {
        private void Start()
        {
            CombineMeshes();
        }

        private void CombineMeshes()
        {
            var meshFilters = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combineInstances = new CombineInstance[meshFilters.Length];

            for (int i = 0; i < meshFilters.Length; i++)
            {
                combineInstances[i].mesh = meshFilters[i].sharedMesh;
                combineInstances[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);
            }

            MeshFilter meshFilter = transform.GetComponent<MeshFilter>();
            meshFilter.mesh = new Mesh();
            meshFilter.mesh.CombineMeshes(combineInstances);
            GetComponent<MeshCollider>().sharedMesh = meshFilter.mesh;
            transform.gameObject.SetActive(true);
        }
    }
}
