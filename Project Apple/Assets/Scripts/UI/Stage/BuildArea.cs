using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildArea : MonoBehaviour
{
    [SerializeField] private float colliderSizeZ;
    [SerializeField] private float copiedBlockAlpha;
    RectTransform rectTransform;
    BoxCollider boxCollider;

    Block selectedBlock;
    Block copiedBlock;

    public void Initialize()
    {
        rectTransform = GetComponent<RectTransform>();
        boxCollider = GetComponent<BoxCollider>();

        boxCollider.size = new Vector3(rectTransform.rect.width, rectTransform.rect.height, colliderSizeZ);
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (var ele in hits)
        {
            if (ele.collider.TryGetComponent(out selectedBlock))
            {
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
        }

    }
    private void OnMouseExit()
    {
        selectedBlock = null;
    }

    void CopySelectedBlock()
    {
        copiedBlock = Instantiate(selectedBlock, selectedBlock.transform.position, Quaternion.identity);
        Renderer render = copiedBlock.GetComponent<Renderer>();
        render.material = new Material(render.material);
        Color color = render.material.color;
        color.a = copiedBlockAlpha;
        render.material.color = color;

        MakeMaterialTransparent(render.material);
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
