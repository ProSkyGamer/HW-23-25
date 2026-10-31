#region

using Controllers;
using UnityEngine;

#endregion

public class MedKitsSpawnerController : Controller
{
    private readonly MedKitSpawner _medKitSpawner;

    private const KeyCode SwitchStateKeyCode = KeyCode.F;

    public MedKitsSpawnerController(MedKitSpawner medKitSpawner)
    {
        _medKitSpawner = medKitSpawner;
    }

    protected override void UpdateInternal(float deltaTime)
    {
        if (Input.GetKeyDown(SwitchStateKeyCode))
            if (_medKitSpawner.IsActive)
                _medKitSpawner.Disable();
            else
                _medKitSpawner.Enable();
    }
}