using UnityEngine;

using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Material interactingMat;
    [SerializeField] private Material completedMat;
    [SerializeField] private MeshRenderer meshRenderer;

    public void SetInteractingMaterial()
    {
        meshRenderer.material = interactingMat;
    }

    public void SetCompletedMaterial()
    {
        meshRenderer.material = completedMat;

        var mats = meshRenderer.materials;
        mats[0] = completedMat; // or whatever index you need
        meshRenderer.materials = mats;

    }

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClicked()
    {

    }

}
