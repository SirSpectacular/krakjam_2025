// InputControlLayoutAttribute attribute is only necessary if you want
// to override the default behavior that occurs when you register your Device
// as a layout.
// The most common use of InputControlLayoutAttribute is to direct the system
// to a custom "state struct" through the `stateType` property. See below for details.

using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace Input
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    [InputControlLayout(displayName = "My Device", stateType = typeof(MyDeviceState))]
    public class MyInputDevice : InputDevice
    {
        private static MyInputDevice _instance;
    
        static MyInputDevice()
        {
            InputSystem.AddDevice<MyInputDevice>();
            // RegisterLayout() adds a "Control layout" to the system.
            // These can be layouts for individual Controls (like sticks)
            // or layouts for entire Devices (which are themselves
            // Controls) like in our case.
            InputSystem.RegisterLayout<MyInputDevice>();
        }

        // In the state struct, you added two Controls that you now want to
        // surface on the Device, for convenience. The Controls
        // get added to the Device either way. When you expose them as properties,
        // it is easier to get to the Controls in code.

        // public ButtonControl button { get; private set; }
        // public AxisControl axis { get; private set; }

        // The Input System calls this method after it constructs the Device,
        // but before it adds the device to the system. Do any last-minute setup
        // here.
        protected override void FinishSetup()
        {
            base.FinishSetup();
            _instance = this;
            // button = GetChildControl<ButtonControl>("button1");
            // axis = GetChildControl<AxisControl>("axis1_H");
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
}