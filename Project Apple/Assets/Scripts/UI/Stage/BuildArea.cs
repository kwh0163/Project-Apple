using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildArea : MonoBehaviour
{
    [SerializeField] private float colliderSizeZ;
    [SerializeField] private float copiedBlockAlpha;
    [SerializeField] private float changeAngleMult;
    RectTransform rectTransform;
    BoxCollider boxCollider;

    Block selectedBlock;
    Block copiedBlock;
    Renderer copiedBlockRenderer;

    bool isBlockMoving = false;

    public void Initialize()
    {
        rectTransform = GetComponent<RectTransform>();
        boxCollider = GetComponent<BoxCollider>();

        boxCollider.size = new Vector3(rectTransform.rect.width, rectTransform.rect.height, colliderSizeZ);
    }

    public void SetActice(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    private void OnMouseDown()
    {
        if (isBlockMoving)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (var ele in hits)
        {
            if (ele.collider.TryGetComponent(out selectedBlock))
            {
                if (selectedBlock.IsStatic)
                {
                    selectedBlock = null;
                    return;
                }
                isBlockMoving = true;
                CopySelectedBlock();
                break;
            }
        }
    }
    private void OnMouseDrag()
    {
        if (selectedBlock == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("BuildArea")))
        {
            Vector3 nextPos = hit.point;
            nextPos.z = 0;
            copiedBlock.MovePosition(nextPos);
            ChangeColor(copiedBlock.CheckIsContained(boxCollider));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            copiedBlock.FlipBlock();
        }

    }
    private void OnMouseUp()
    {
        if (selectedBlock == null)
            return;

        isBlockMoving = false;
        if (copiedBlock.CheckIsContained(boxCollider))
        {
            selectedBlock.MovePosition(copiedBlock.transform.position);
            if (copiedBlock.IsFlipped != selectedBlock.IsFlipped)
                selectedBlock.FlipBlock();
        }
        Destroy(copiedBlock.gameObject);
        selectedBlock = null;
    }

    void CopySelectedBlock()
    {
        copiedBlock = Instantiate(selectedBlock, selectedBlock.transform.position, selectedBlock.transform.rotation);
        copiedBlock.Initialize();

        copiedBlockRenderer = copiedBlock.GetComponent<Renderer>();
        copiedBlockRenderer.material = new Material(copiedBlockRenderer.material);
        Color color = copiedBlockRenderer.material.color;
        color.a = copiedBlockAlpha;
        copiedBlockRenderer.material.color = color;

        MakeMaterialTransparent(copiedBlockRenderer.material);
    }

    void ChangeColor(bool isAble)
    {
        if (isAble)
            copiedBlockRenderer.material.color = new Color(0, 1, 0, copiedBlockAlpha);
        else
            copiedBlockRenderer.material.color = new Color(1, 0, 0, copiedBlockAlpha);
    }

    void MakeMaterialTransparent(Material material)
    {
        material.SetFloat("_Mode", 3);  // 3은 Transparent 모드
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;  // 투명 객체의 렌더링 순서 설정
    }
}
