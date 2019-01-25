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
        // PlayerMove
        m_PlayerMove = asset.GetActionMap("PlayerMove");
        m_PlayerMove_Move = m_PlayerMove.GetAction("Move");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        if (m_PlayerMoveActionsCallbackInterface != null)
        {
            PlayerMove.SetCallbacks(null);
        }
        m_PlayerMove = null;
        m_PlayerMove_Move = null;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        var PlayerMoveCallbacks = m_PlayerMoveActionsCallbackInterface;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
        PlayerMove.SetCallbacks(PlayerMoveCallbacks);
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // PlayerMove
    private InputActionMap m_PlayerMove;
    private IPlayerMoveActions m_PlayerMoveActionsCallbackInterface;
    private InputAction m_PlayerMove_Move;
    public struct PlayerMoveActions
    {
        private InputMaster m_Wrapper;
        public PlayerMoveActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move { get { return m_Wrapper.m_PlayerMove_Move; } }
        public InputActionMap Get() { return m_Wrapper.m_PlayerMove; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(PlayerMoveActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMoveActions instance)
        {
            if (m_Wrapper.m_PlayerMoveActionsCallbackInterface != null)
            {
                Move.started -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnMove;
                Move.performed -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnMove;
                Move.cancelled -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_PlayerMoveActionsCallbackInterface = instance;
            if (instance != null)
            {
                Move.started += instance.OnMove;
                Move.performed += instance.OnMove;
                Move.cancelled += instance.OnMove;
            }
        }
    }
    public PlayerMoveActions @PlayerMove
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new PlayerMoveActions(this);
        }
    }
}
public interface IPlayerMoveActions
{
    void OnMove(InputAction.CallbackContext context);
}
