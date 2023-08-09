using System.Numerics;
using ImGuiNET;
using MSE.Engine.Interfaces;

namespace SAE.GPR5300.S1.Ui.SolarSystemUi {
  public class UiSolarSystemSetting : IUserInterface {
    public static Action<float>? SolarSystemMultiplierEvent;
    
    private readonly uint _dockId;
    private float _solarSystemMultiplier;

    public UiSolarSystemSetting(uint dockId) {
      _dockId = dockId;
      SolarSystemMultiplierEvent?.Invoke(0.02f);
    }

    public void UpdateUi() {
      ImGui.SetNextWindowDockID(_dockId, ImGuiCond.Always);
      ImGui.Begin("Overall Setting");
      ImGui.Text("Solar System Speed Multiplier");
      if (ImGui.SliderFloat("Multiply By", ref _solarSystemMultiplier, -100.0f, 100.0f)) {
        SolarSystemMultiplierEvent?.Invoke(_solarSystemMultiplier);
      }

      if (ImGui.Button("Zero", new Vector2(285, 20))) {
        _solarSystemMultiplier = 0f;
        SolarSystemMultiplierEvent?.Invoke(_solarSystemMultiplier);
      }

      if (ImGui.Button("Set Slow", new Vector2(285, 20))) {
        _solarSystemMultiplier = 0.02f;
        SolarSystemMultiplierEvent?.Invoke(_solarSystemMultiplier);
      }

      ImGui.End();
    }
  }
}