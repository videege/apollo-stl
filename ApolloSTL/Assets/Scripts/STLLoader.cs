using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STLLoader : MonoBehaviour
{
    [SerializeField] private GameObject MeshRendererPrefab;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    [SerializeField] private CameraController cameraController;

    private Vector3 targetSize;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        targetSize = meshRenderer.bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSTL(string filePath)
    {
        var meshes = Parabox.Stl.Importer.Import(filePath);
        CombineAndSetMeshes(meshes);
        ResizeMesh();
        // Tell the camera to refocus on the new center of the object
        cameraController.Refocus(meshRenderer.bounds.center);
    }

    float GetMaxElement(Vector3 vector) 
    {
        return Mathf.Max(Mathf.Max(vector.x, vector.y), vector.z);
    }

    private void ResizeMesh()
    {
        // Reset the scale of the meshRenderer so subsequent model loads don't get hosed up.
        meshRenderer.transform.localScale = targetSize;

        var size = meshRenderer.bounds.size;
        var max = GetMaxElement(size);
        Debug.Log($"Maximum = {max}");
        var scaleFactor = 1.0f / max;
        Debug.Log($"Scale factor = {scaleFactor}");
        // Scale the mesh down/up to the same size
        meshRenderer.transform.localScale = targetSize * scaleFactor;
    }

    private void CombineAndSetMeshes(Mesh[] meshes)
    {
        var combine = new CombineInstance[meshes.Length];
        for (var i = 0; i < meshes.Length; i++)
        {
            var mesh = meshes[i];
            var obj = Instantiate(MeshRendererPrefab, new Vector3(0,0,0), Quaternion.identity);
            obj.transform.parent = meshRenderer.transform;
            
            MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
            meshFilter.mesh = mesh;       

            combine[i].mesh = meshFilter.sharedMesh;
            combine[i].transform = meshFilter.transform.localToWorldMatrix;
            meshFilter.gameObject.SetActive(false);         
        }

        foreach (Transform child in meshRenderer.transform) 
        {
            Destroy(child.gameObject);
        }

        meshFilter.mesh = new Mesh();
        meshFilter.mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        meshFilter.mesh.CombineMeshes(combine);
    }
}
