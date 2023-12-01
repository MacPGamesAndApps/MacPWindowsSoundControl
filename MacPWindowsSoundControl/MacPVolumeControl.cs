using System;
using System.Runtime.InteropServices;

namespace MacPWindowsSoundControl
{
    public class MacPVolumeControl
    {
        private IntPtr _handle;
        private int _steps;

        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,
            IntPtr wParam, IntPtr lParam);

        public MacPVolumeControl(IntPtr handle)
        {
            _handle = handle;
            _steps = 0;
        }

        public void TurnVolumeUp(int steps)
        {
            _steps = steps;
            for (int count = 0; count <= steps; count++)
            {
                SendMessageW(_handle, WM_APPCOMMAND, _handle,
                    (IntPtr)APPCOMMAND_VOLUME_UP);
            }
        }

        public void TurnVolumeDown(int steps)
        {
            int stepsDown = Math.Min(steps, _steps);
            for (int count = 0; count <= stepsDown; count++)
            {
                SendMessageW(_handle, WM_APPCOMMAND, _handle,
                    (IntPtr)APPCOMMAND_VOLUME_DOWN);
            }
        }
    }
}
