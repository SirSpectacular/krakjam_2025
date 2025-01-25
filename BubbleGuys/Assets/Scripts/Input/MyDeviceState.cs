using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace Input
{
// A "state struct" describes the memory format that a Device uses. Each Device can
// receive and store memory in its custom format. InputControls then connect to
// the individual pieces of memory and read out values from them.
//
// If it's important for the memory format to match 1:1 at the binary level
// to an external representation, it's generally advisable to use
// LayoutLind.Explicit.
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public struct MyDeviceState : IInputStateTypeInfo
    {
        //TODO: Each phone separatly or not?
        public FourCC format => new FourCC('P', 'H', 'O', 'N');

        // InputControlAttributes on fields tell the Input System to create Controls
        // for the public fields found in the struct.

        // Assume a 16bit field of buttons. Create one button that is tied to
        // bit #3 (zero-based). Note that buttons don't need to be stored as bits.
        // They can also be stored as floats or shorts, for example. The
        // InputControlAttribute.format property determines which format the
        // data is stored in. If omitted, the system generally infers it from the value
        // type of the field.
        [InputControl(layout = "Button", bit = 3)] [FieldOffset(0)]
        public ushort button1;
        
        [InputControl(layout = "Button", bit = 3)] [FieldOffset(1)]
        public ushort button2;
        
        [InputControl(layout = "Button", bit = 3)] [FieldOffset(2)]
        public ushort button3;
        
        [InputControl(layout = "Button", bit = 3)] [FieldOffset(3)]
        public ushort button4;
        
        [InputControl(layout = "Axis")] [FieldOffset(4)] public short axis1_H;
        [InputControl(layout = "Axis")] [FieldOffset(5)] public short axis1_V;
        [InputControl(layout = "Axis")] [FieldOffset(6)] public short axis2_H;
        [InputControl(layout = "Axis")] [FieldOffset(7)] public short axis2_V;
        [InputControl(layout = "Axis")] [FieldOffset(8)] public short axis3_H;
        [InputControl(layout = "Axis")] [FieldOffset(9)] public short axis3_V;
        [InputControl(layout = "Axis")] [FieldOffset(10)] public short axis4_H;
        [InputControl(layout = "Axis")] [FieldOffset(11)] public short axis4_V;
    }
}