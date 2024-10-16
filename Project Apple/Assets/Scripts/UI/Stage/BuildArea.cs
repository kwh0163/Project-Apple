using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BuildState
{
    None,
    Selecting,
    Moving,
    Connecting
}


public class BuildArea : MonoBehaviour
{
    [SerializeField] private float copiedBlockAlpha;
    [SerializeField] RectTransform scrollview;

    GameObject objectPrefab;

    MovableObject selectedObject;
    MovableObject copiedObject;
    Renderer copiedObjectRenderer;

    bool isBlockMoving = false;

    public void Initialize()
    {

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
        if (GameManager.Instance.Stage.CurrentState != StageState.Prepare)
            return;

        if (isBlockMoving)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (var ele in hits)
        {
            if (ele.collider.TryGetComponent(out selectedObject))
            {
                if (selectedObject.IsStatic)
                {
                    selectedObject = null;
                    return;
                }
                CopySelectedObject(selectedObject.gameObject);
                break;
            }
        }
    }
    private void MoveBlock()
    {
        if (GameManager.Instance.UI.Stage.Select.IsOpened
            &&Input.mousePosition.y <= scrollview.rect.height)
        {
            copiedObject.gameObject.SetActive(false);
            return;
        }
        else
            copiedObject.gameObject.SetActive(true);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("BuildArea")))
        {
            Vector3 nextPos = hit.point;
            nextPos.z = 0;
            copiedObject.MovePosition(nextPos);
            bool isBlockContained = GameManager.Instance.Stage.StageObject.CheckIsContained(copiedObject.Collider);
            ChangeColor(isBlockContained && !copiedObject.CheckOverlapped(selectedObject?.gameObject));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            copiedObject.FlipBlock();
        }

    }

    public void CopySelectedObject(GameObject @object)
    {
        if (selectedObject == null)
            objectPrefab = @object;
        isBlockMoving = true;
        copiedObject = Instantiate(@object, @object.transform.position, @object.transform.rotation).GetComponent<MovableObject>();
        copiedObject.Initialize();

        copiedObjectRenderer = copiedObject.GetComponent<Renderer>();
        copiedObjectRenderer.material = new Material(copiedObjectRenderer.material);
        Color color = copiedObjectRenderer.material.color;
        color.a = copiedBlockAlpha;
        copiedObjectRenderer.material.color = color;

        MakeMaterialTransparent(copiedObjectRenderer.material);
    }
    void SetBlockAsCopied()
    {
        if (copiedObject == null)
            return;

        if (GameManager.Instance.UI.Stage.Select.IsOpened
            && Input.mousePosition.y <= scrollview.rect.height)
        {
            GameManager.Instance.UI.Stage.Select.RemoveObject(copiedObject.Type);
            if (selectedObject != null)
            {
                GameManager.Instance.Stage.StageObject.PlacedObjectList.Remove(selectedObject);
                Destroy(selectedObject.gameObject);
            }
        }
        else
        {
            bool isBlockContained = GameManager.Instance.Stage.StageObject.CheckIsContained(copiedObject.Collider);
            if(isBlockContained && !copiedObject.CheckOverlapped(selectedObject?.gameObject))
            {
                MovableObject temp;
                if (selectedObject == null)
                    temp = Instantiate(objectPrefab, GameManager.Instance.Stage.BlockParentTransform).GetComponent<MovableObject>();
                else
                    temp = selectedObject;
                temp.MovePosition(copiedObject.transform.position);
                if (copiedObject.IsFlipped != temp.IsFlipped)
                    temp.FlipBlock();
                GameManager.Instance.Stage.StageObject.PlacedObjectList.Add(temp);
                temp.Initialize();
            }
            else if(selectedObject == null){
                GameManager.Instance.UI.Stage.Select.RemoveObject(copiedObject.Type);
            }
        }
        
        Destroy(copiedObject.gameObject);
        isBlockMoving = false;
        selectedObject = null;
    }

    void ChangeColor(bool isAble)
    {
        if (isAble)
            copiedObjectRenderer.material.color = new Color(0, 1, 0, copiedBlockAlpha);
        else
            copiedObjectRenderer.material.color = new Color(1, 0, 0, copiedBlockAlpha);
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
