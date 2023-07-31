using ImGuiNET;
using MSE.Engine.Interfaces;

namespace SAE.GPR5300.S1.Ui {
  public class UiBlinnPhongLightingScene : IUiInterface {
    private int _lightingTech = 0;
    private int _gamma = 0;
    private float _exponentBlinn = 31;
    private float _exponentPhong = 8;
    private bool _isBlinnChecked = false;
    private bool _isGammaChecked = false;

    public static Action<int> LightingTechEvent;
    public static Action<int> IsGammaEvent;
    public static Action<float> ExponentBlinnEvent;
    public static Action<float> ExponentPhongEvent;

    public void UpdateUi() {
      ImGui.Begin("Shader Setting", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove);
      if (ImGui.Checkbox("Blinn-Phong Light", ref _isBlinnChecked)) {
        _lightingTech = _isBlinnChecked ? 1 : 0;
        LightingTechEvent.Invoke(_lightingTech);
      }
      
      if (ImGui.Checkbox("Gamma", ref _isGammaChecked)) {
        _gamma = _isGammaChecked ? 1 : 0;
        IsGammaEvent.Invoke(_gamma);
      }

      if (ImGui.SliderFloat("ExponentBlinnEvent", ref _exponentBlinn, 0, 100)) {
        ExponentBlinnEvent.Invoke(_exponentBlinn);
      }

      if (ImGui.SliderFloat("ExponentPhongEvent", ref _exponentPhong, 0, 100)) {
        ExponentPhongEvent.Invoke(_exponentPhong);
      }

      ImGui.End();
    }

    public void RenderUi() {
      UiController.Instance.ImGuiController.Render();
    }
  }
}