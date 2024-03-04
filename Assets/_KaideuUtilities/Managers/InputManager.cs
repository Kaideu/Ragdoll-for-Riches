
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Kaideu.Input
{
    public class InputManager : Utils.SingletonPattern<InputManager>
    {

        public PlayerControls Controls { get; private set; }

        private InputActionMap[] _actionMaps;

        public void Initialize()
        {
            Controls = new PlayerControls();
            ToggleControls(false);

            //Set keep a list of all action maps in action asset
            _actionMaps = new InputActionMap[] { Controls.UI };
        }

        public void ToggleControls(bool isEnabled)
        {
            if (isEnabled)
                Controls.Enable();
            else
                Controls.Disable();
        }

        public void SwitchTo(InputActionMap actionMapName)
        {
            foreach (var map in _actionMaps)
                map.Disable();

            actionMapName.Enable();
            print($"Current ActionMap: {actionMapName.name}");
        }
    }
}
/**/
