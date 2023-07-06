using System.Numerics;
using ImGuiNET;
using MakotoStudioEngine.Interfaces;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace SAE.GPR5300.S1.Ui {
  public class UITest : IUiInterface {
    private GL _gl;
    private ImGuiController _controller;
    
    private bool _isColor;
    private bool _isTexture;
    private float _alpha;
    private Vector3 _colorValue;
    private Vector3 _scaleValue;

    public UITest(GL gl, ImGuiController imGuiController) {
      _gl = gl;
      _controller = imGuiController;
    }

    public void LoadDemoWindow() {
      ImGui.ShowDemoWindow();
    }

    public void LoadDebugWindow() {
      ImGui.ShowDebugLogWindow();
    }

    public void Update() {
      ImGui.Begin("UI Settings");
      ImGui.Text("Texture must be active for Texture blend with alpha chânnel");
      ImGui.Checkbox("Color", ref _isColor);
      ImGui.Checkbox("Texture", ref _isTexture);
      ImGui.SliderFloat("alpha", ref _alpha, 0.0f, 1.0f);
      ImGui.DragFloat3("Scale", ref _scaleValue, 0.1f, 0.01f, 5.0f);
      ImGui.ColorEdit3("Color", ref _colorValue);
      ImGui.End();
      LoadDebugWindow();
    }

    public void Render() {
      _controller.Render();
    }
  }
}