// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class InputMaster : InputActionAssetReference
{
    public InputMaster()
    {
    }
    public InputMaster(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // Movement
        m_Movement = asset.GetActionMap("Movement");
        m_Movement_Move = m_Movement.GetAction("Move");
        // Cutscenes
        m_Cutscenes = asset.GetActionMap("Cutscenes");
        m_Cutscenes_Confirm = m_Cutscenes.GetAction("Confirm");
        // Inventory
        m_Inventory = asset.GetActionMap("Inventory");
        m_Inventory_Tabs = m_Inventory.GetAction("Tabs");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        if (m_MovementActionsCallbackInterface != null)
        {
            Movement.SetCallbacks(null);
        }
        m_Movement = null;
        m_Movement_Move = null;
        if (m_CutscenesActionsCallbackInterface != null)
        {
            Cutscenes.SetCallbacks(null);
        }
        m_Cutscenes = null;
        m_Cutscenes_Confirm = null;
        if (m_InventoryActionsCallbackInterface != null)
        {
            Inventory.SetCallbacks(null);
        }
        m_Inventory = null;
        m_Inventory_Tabs = null;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        var MovementCallbacks = m_MovementActionsCallbackInterface;
        var CutscenesCallbacks = m_CutscenesActionsCallbackInterface;
        var InventoryCallbacks = m_InventoryActionsCallbackInterface;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
        Movement.SetCallbacks(MovementCallbacks);
        Cutscenes.SetCallbacks(CutscenesCallbacks);
        Inventory.SetCallbacks(InventoryCallbacks);
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // Movement
    private InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private InputAction m_Movement_Move;
    public struct MovementActions
    {
        private InputMaster m_Wrapper;
        public MovementActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move { get { return m_Wrapper.m_Movement_Move; } }
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                Move.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                Move.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                Move.cancelled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                Move.started += instance.OnMove;
                Move.performed += instance.OnMove;
                Move.cancelled += instance.OnMove;
            }
        }
    }
    public MovementActions @Movement
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new MovementActions(this);
        }
    }
    // Cutscenes
    private InputActionMap m_Cutscenes;
    private ICutscenesActions m_CutscenesActionsCallbackInterface;
    private InputAction m_Cutscenes_Confirm;
    public struct CutscenesActions
    {
        private InputMaster m_Wrapper;
        public CutscenesActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Confirm { get { return m_Wrapper.m_Cutscenes_Confirm; } }
        public InputActionMap Get() { return m_Wrapper.m_Cutscenes; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(CutscenesActions set) { return set.Get(); }
        public void SetCallbacks(ICutscenesActions instance)
        {
            if (m_Wrapper.m_CutscenesActionsCallbackInterface != null)
            {
                Confirm.started -= m_Wrapper.m_CutscenesActionsCallbackInterface.OnConfirm;
                Confirm.performed -= m_Wrapper.m_CutscenesActionsCallbackInterface.OnConfirm;
                Confirm.cancelled -= m_Wrapper.m_CutscenesActionsCallbackInterface.OnConfirm;
            }
            m_Wrapper.m_CutscenesActionsCallbackInterface = instance;
            if (instance != null)
            {
                Confirm.started += instance.OnConfirm;
                Confirm.performed += instance.OnConfirm;
                Confirm.cancelled += instance.OnConfirm;
            }
        }
    }
    public CutscenesActions @Cutscenes
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new CutscenesActions(this);
        }
    }
    // Inventory
    private InputActionMap m_Inventory;
    private IInventoryActions m_InventoryActionsCallbackInterface;
    private InputAction m_Inventory_Tabs;
    public struct InventoryActions
    {
        private InputMaster m_Wrapper;
        public InventoryActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Tabs { get { return m_Wrapper.m_Inventory_Tabs; } }
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
            {
                Tabs.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnTabs;
                Tabs.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnTabs;
                Tabs.cancelled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnTabs;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                Tabs.started += instance.OnTabs;
                Tabs.performed += instance.OnTabs;
                Tabs.cancelled += instance.OnTabs;
            }
        }
    }
    public InventoryActions @Inventory
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new InventoryActions(this);
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get

        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.GetControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
}
public interface IMovementActions
{
    void OnMove(InputAction.CallbackContext context);
}
public interface ICutscenesActions
{
    void OnConfirm(InputAction.CallbackContext context);
}
public interface IInventoryActions
{
    void OnTabs(InputAction.CallbackContext context);
}
