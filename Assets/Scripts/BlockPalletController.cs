using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPalletController : MonoBehaviour
{
    public MeshRenderer PreviewBlockRenderer;

    [SerializeField]
    private int desiredBlockIndex = 0;
    public GameObject[] BlockArr = new GameObject[3];
    private void Start()
    {
        UpdatePreviewBlock();
    }

    public void IncrumentBlockIndex()
    {
        desiredBlockIndex = (desiredBlockIndex + 1) % (BlockArr.Length);
        UpdatePreviewBlock();
    }
    public void DecrumentBlockIndex()
    {
        desiredBlockIndex = (desiredBlockIndex <= 0) ? BlockArr.Length - 1 : desiredBlockIndex--;
        UpdatePreviewBlock();
    }
    public GameObject GetCurrentBlock()
    {
        return BlockArr[desiredBlockIndex];
    }

    public void UpdatePreviewBlock()
    {
        GameObject CurrentBlock = GetCurrentBlock();
        MeshRenderer CurrentRenderer = CurrentBlock.GetComponent<MeshRenderer>();
        PreviewBlockRenderer.material = CurrentRenderer.sharedMaterial;
    }
}
