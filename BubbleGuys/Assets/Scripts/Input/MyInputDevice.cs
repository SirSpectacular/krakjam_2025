// InputControlLayoutAttribute attribute is only necessary if you want
// to override the default behavior that occurs when you register your Device
// as a layout.
// The most common use of InputControlLayoutAttribute is to direct the system
// to a custom "state struct" through the `stateType` property. See below for details.

using Input;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
[InputControlLayout(displayName = "My Device", stateType = typeof(MyDeviceState))]
public class MyInputDevice : InputDevice
{
    private static MyInputDevice _instance;
    
    public ButtonControl button { get; private set; }
    public AxisControl axis { get; private set; }

    // The Input System calls this method after it constructs the Device,
    // but before it adds the device to the system. Do any last-minute setup
    // here.
    protected override void FinishSetup()
    {
        base.FinishSetup();
        _instance = this;
        button = GetChildControl<ButtonControl>("button");
        axis = GetChildControl<AxisControl>("axis");
    }

    public static void OnUpdate(MyDeviceState state)
    {
        InputSystem.QueueStateEvent(_instance, state);
    }
    
    // You still need a way to trigger execution of the static constructor
    // in the Player. To do this, you can add the RuntimeInitializeOnLoadMethod
    // to an empty method.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeInPlayer() {}
}