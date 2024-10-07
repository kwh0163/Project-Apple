using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildArea : MonoBehaviour
{
    [SerializeField] private float colliderSizeZ;
    [SerializeField] private float copiedBlockAlpha;
    [SerializeField] private float changeAngleMult;
    [SerializeField] RectTransform scrollview;
    RectTransform rectTransform;
    BoxCollider boxCollider;

    Block blockPrefab;

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

    private void Update()
    {
        if (isBlockMoving)
            MoveBlock();
        if (Input.GetMouseButtonDown(0))
            SelectBlock();
        if (Input.GetMouseButtonUp(0))
            SetBlockAsCopied();
        
    }
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    private void SelectBlock()
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
                CopySelectedBlock(selectedBlock);
                break;
            }
        }
    }
    private void MoveBlock()
    {

        if (Input.mousePosition.y <= scrollview.rect.height)
        {
            copiedBlock.gameObject.SetActive(false);
            return;
        }
        else
            copiedBlock.gameObject.SetActive(true);


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("BuildArea")))
        {
            Vector3 nextPos = hit.point;
            nextPos.z = 0;
            copiedBlock.MovePosition(nextPos);
            ChangeColor(copiedBlock.CheckIsContained(boxCollider) && !copiedBlock.IsOverlapped);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            copiedBlock.FlipBlock();
        }

    }

    public void CopySelectedBlock(Block block)
    {
        if (selectedBlock == null)
            blockPrefab = block;
        isBlockMoving = true;
        copiedBlock = Instantiate(block, block.transform.position, block.transform.rotation);
        copiedBlock.Initialize();

        copiedBlockRenderer = copiedBlock.GetComponent<Renderer>();
        copiedBlockRenderer.material = new Material(copiedBlockRenderer.material);
        Color color = copiedBlockRenderer.material.color;
        color.a = copiedBlockAlpha;
        copiedBlockRenderer.material.color = color;

        MakeMaterialTransparent(copiedBlockRenderer.material);
    }
    void SetBlockAsCopied()
    {
        if (copiedBlock == null)
            return;

        if (Input.mousePosition.y <= scrollview.rect.height)
        {
            GameManager.Instance.UI.Stage.Select.RemoveBlock(copiedBlock.Type);
            if (selectedBlock != null)
            {
                Destroy(selectedBlock.gameObject);
                GameManager.Instance.Stage.StageObject.SpareBlock.Remove(selectedBlock);
            }
        }
        else if (copiedBlock.CheckIsContained(boxCollider) && !copiedBlock.IsOverlapped)
        {
            Block temp;
            if(selectedBlock == null)
                temp = Instantiate(blockPrefab, GameManager.Instance.Stage.BlockParentTransform).GetComponent<Block>();
            else
                temp = selectedBlock;
            temp.MovePosition(copiedBlock.transform.position);
            if (copiedBlock.IsFlipped != temp.IsFlipped)
                temp.FlipBlock();
            GameManager.Instance.Stage.StageObject.SpareBlock.Add(temp);
        }
        Destroy(copiedBlock.gameObject);
        isBlockMoving = false;
        selectedBlock = null;
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
