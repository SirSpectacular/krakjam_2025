using System;
using System.Linq;
using Server;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

namespace Input
{
// This example uses a MonoBehaviour with [ExecuteInEditMode]
// on it to run the setup code. You can do this many other ways.
    [ExecuteInEditMode]
    public class MyDeviceSupport : MonoBehaviour
    {
        public void OnDeviceAdded()
        {
            // Feed a description of the Device into the system. In response, the
            // system matches it to the layouts it has and creates a Device.
            InputSystem.AddDevice(
                new InputDeviceDescription
                {
                    interfaceName = "ThirdPartyAPI",
                    product = "defaultName"
                });
        }


        public void OnDeviceRemoved()
        {
            var device = InputSystem.devices.FirstOrDefault(
                x => x.description == new InputDeviceDescription
                {
                    interfaceName = "ThirdPartyAPI",
                    product = "defaultName",
                });

            if (device != null)
                InputSystem.RemoveDevice(device);
        }

        // Move the registration of MyDevice from the
        // static constructor to here, and change the
        // registration to also supply a matcher.
        protected void Awake()
        {
            // Add a match that catches any Input Device that reports its
            // interface as "ThirdPartyAPI".
            InputSystem.RegisterLayout<MyInputDevice>(
                matches: new InputDeviceMatcher()
                    .WithInterface("ThirdPartyAPI"));
        }
    }
}