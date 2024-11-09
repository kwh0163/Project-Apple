using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
enum BuildState
{
    None,
    Moving,
    Connecting
}


public class BuildArea : MonoBehaviour
{
    [SerializeField] private float copiedBlockAlpha;
    [SerializeField] RectTransform scrollview;
    [SerializeField] private float blockHoldTime;

    GameObject objectPrefab;

    MovableObject selectedObject;
    MovableObject copiedObject;
    Renderer copiedObjectRenderer;
    TriggerBlock selectedTrigger;

    BuildState currentState;

    bool ignoreButtonDown;

    bool ignoreFlip;
    Vector2 firstClickPosition;

    float buttonDownCounter;
    


    public void Initialize()
    {
        SetState(BuildState.None);
        ignoreButtonDown = false;
        ignoreButtonDown = false;
        buttonDownCounter = 0;
    }

    private void Update()
    {
        if (GameManager.Instance.Stage.CurrentState != StageState.Prepare)
            return;

        if (currentState == BuildState.Moving)
            MoveBlock();
        CheckMouseDown();
        CheckMouseUp();
    }
    void CheckMouseDown()
    {
        if (!Input.GetMouseButton(0))
            return;

        if (ignoreButtonDown)
            return;

        if (currentState == BuildState.Connecting)
            ConnectInteract();
        else if (currentState == BuildState.None && buttonDownCounter < blockHoldTime)
            buttonDownCounter += Time.deltaTime;
        else
            SelectMoveBlock();
    }
    void CheckMouseUp()
    {
        if (!Input.GetMouseButtonUp(0))
            return;
        if (currentState == BuildState.Moving)
            SetBlockAsCopied();
        else if(buttonDownCounter < blockHoldTime)
            SelectConnectBlock();

        ignoreButtonDown = false;
        buttonDownCounter = 0;
    }
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    private void SelectConnectBlock()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (var ele in hits)
        {
            if (ele.collider.TryGetComponent(out selectedTrigger))
            {
                GameManager.Instance.Sound.PlaySound(SoundEnum.SelectBlock);
                GameManager.Instance.Stage.StageObject.EnableInteract(selectedTrigger.ConnectType);
                SetState(BuildState.Connecting);
                break;
            }
        }
    }
    private void ConnectInteract()
    {
        bool disconnect = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (var ele in hits)
        {
            if (ele.collider.TryGetComponent(out InteractableBlock interact))
            {
                GameManager.Instance.Sound.PlaySound(SoundEnum.SelectBlock);
                selectedTrigger.Connect(interact);
                disconnect = false;
                break;
            }
        }
        if (disconnect)
        {
            GameManager.Instance.Sound.PlaySound(SoundEnum.Error);
            selectedTrigger.Disconnect();
        }
        GameManager.Instance.Stage.StageObject.DisableInteract();
        SetState(BuildState.None);
        selectedTrigger = null;
        ignoreButtonDown = true;
    }
    private void SelectMoveBlock()
    {
        if (currentState == BuildState.Moving)
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
                selectedObject.Select(Color.green);
                break;
            }
        }
    }
    private void MoveBlock()
    {
        if (Input.touchCount == 1)
        {
            firstClickPosition = Input.mousePosition;
            ignoreFlip = false;
        }

        if (GameManager.Instance.UI.Stage.Select.IsOpened
            &&firstClickPosition.y <= scrollview.rect.height)
        {
            copiedObject.gameObject.SetActive(false);
            return;
        }
        else
            copiedObject.gameObject.SetActive(true);

        Ray ray = Camera.main.ScreenPointToRay(firstClickPosition);

        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("BuildArea")))
        {
            Vector3 nextPos = hit.point;
            nextPos.z = 0;
            copiedObject.MovePosition(nextPos);
            bool isBlockContained = GameManager.Instance.Stage.StageObject.CheckIsContained(copiedObject.Collider);           
            ChangeColor(isBlockContained && !copiedObject.CheckOverlapped(selectedObject?.gameObject));
        }

        if(Input.touchCount > 1)
        {
            if (ignoreFlip)
                return;
            ignoreFlip = true;
            GameManager.Instance.Sound.PlaySound(SoundEnum.SelectBlock);
            copiedObject.FlipBlock();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.Sound.PlaySound(SoundEnum.SelectBlock);
            copiedObject.FlipBlock();
        }
    }

    public void CopySelectedObject(GameObject @object)
    {
        if (selectedObject == null)
            objectPrefab = @object;
        GameManager.Instance.Sound.PlaySound(SoundEnum.SelectBlock);
        copiedObject = Instantiate(@object, @object.transform.position, @object.transform.rotation).GetComponent<MovableObject>();
        copiedObject.Initialize();


        SetCopiedMesh();
        SetState(BuildState.Moving);
    }
    void SetBlockAsCopied()
    {
        if (copiedObject == null)
            return;

        if (GameManager.Instance.UI.Stage.Select.IsOpened
            && Input.mousePosition.y <= scrollview.rect.height)
        {
            GameManager.Instance.Sound.PlaySound(SoundEnum.SelectBlock);
            GameManager.Instance.UI.Stage.Select.RemoveObject(copiedObject.Type);
            if (selectedObject != null)
            {
                GameManager.Instance.Stage.StageObject.RemoveMovableObject(selectedObject);
                selectedObject.DestoryObject();
                Destroy(selectedObject.gameObject);
            }
        }
        else
        {
            bool isBlockContained = GameManager.Instance.Stage.StageObject.CheckIsContained(copiedObject.Collider);
            if(isBlockContained && !copiedObject.CheckOverlapped(selectedObject?.gameObject))
            {
                GameManager.Instance.Sound.PlaySound(SoundEnum.SelectBlock);
                MovableObject temp;
                if (selectedObject == null)
                {
                    temp = Instantiate(objectPrefab, GameManager.Instance.Stage.BlockParentTransform).GetComponent<MovableObject>();
                    GameManager.Instance.Stage.StageObject.AddMovableObject(temp);
                }
                else
                    temp = selectedObject;
                temp.MovePosition(copiedObject.transform.position);
                if (copiedObject.IsFlipped != temp.IsFlipped)
                    temp.FlipBlock();
                temp.Initialize();
                CheckTrigger(temp);
            }
            else
            {
                GameManager.Instance.Sound.PlaySound(SoundEnum.Error);
                if (selectedObject == null)
                {
                    GameManager.Instance.UI.Stage.Select.RemoveObject(copiedObject.Type);
                }
                else
                {
                    selectedObject.Release();
                }
            }
        }
        copiedObject.DestoryObject();
        Destroy(copiedObject.gameObject);
        SetState(BuildState.None);
        selectedObject = null;
    }
    void CheckTrigger(MovableObject temp)
    {
        if (temp is TriggerBlock)
        {
            if (((TriggerBlock)copiedObject).IsConnected)
                ((TriggerBlock)temp).Connect(((TriggerBlock)copiedObject).ConnectedInteract);
            else
                ((TriggerBlock)temp).Disconnect();
        }
    }
    void ChangeColor(bool isAble)
    {
        if (isAble)
            copiedObjectRenderer.material.color = new Color(0, 1, 0, copiedBlockAlpha);
        else
            copiedObjectRenderer.material.color = new Color(1, 0, 0, copiedBlockAlpha);
    }

    void SetCopiedMesh()
    {
        if (!copiedObject.TryGetComponent(out copiedObjectRenderer))
            copiedObjectRenderer = ((AppleObject)copiedObject).GetCurrentPrefab.GetComponent<MeshRenderer>();
        copiedObjectRenderer.material = new Material(copiedObjectRenderer.material);
        Color color = copiedObjectRenderer.material.color;
        color.a = copiedBlockAlpha;
        copiedObjectRenderer.material.color = color;

        MakeMaterialTransparent(copiedObjectRenderer.material);
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

    void SetState(BuildState state)
    {
        if(state == BuildState.None)
        {
            GameManager.Instance.UI.Stage.ShowButton();
        }
        else if(state == BuildState.Moving)
        {
            GameManager.Instance.UI.Stage.HideButton();
        }
        else if(state == BuildState.Connecting)
        {
            GameManager.Instance.UI.Stage.HideButton();
        }
        currentState = state;
    }
}
