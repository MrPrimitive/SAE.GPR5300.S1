using System.Numerics;
using ImGuiNET;
using MSE.Engine.Interfaces;
using SAE.GPR5300.S1.Assets.Shaders.Settings;

namespace SAE.GPR5300.S1.Ui.SolarSystemUi {
  public class UiEarth : IUserInterface {
    public static Action<ShaderEarthLightOptions>? ShaderEarthLightOptionsEvent;
    private readonly uint _dockId;
    private ShaderEarthLightOptions _shaderEarthLightOptions = ShaderEarthLightOptions.Default;
    private float _diffuseNight = 1.14f;

    public UiEarth(uint dockId) {
      _dockId = dockId;
    }

    public void UpdateUi() {
      ImGui.SetNextWindowDockID(_dockId, ImGuiCond.Always);
      ImGui.Begin("Earth");

      if (ImGui.SliderFloat("Night Light", ref _diffuseNight, 0, 1.14f))
        ShaderEarthLightOptionsEvent?.Invoke(_shaderEarthLightOptions);

      _shaderEarthLightOptions = _shaderEarthLightOptions with {
        DiffuseNight = new Vector3(_diffuseNight)
      };

      ImGui.End();
    }
  }
}