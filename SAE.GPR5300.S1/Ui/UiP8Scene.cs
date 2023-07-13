using System.Numerics;
using ImGuiNET;
using MSE.Engine.Interfaces;

namespace SAE.GPR5300.S1.Ui {
  public class UiP8Scene : IUiInterface {
    private int _hue = 255;
    private float _value = 0;
    private float _saturation = 0;
    private Vector4 _color;

    private Vector4 Color {
      get => _color;
      set {
        _color = value;
        SetColorVector3(_color);
      }
    }

    public static Action<Vector3> ColorEvent;

    public UiP8Scene() {
      Color = HsvToRgb();
    }

    public Vector2 _buttonSize = new(300, 20);

    public void UpdateUi() {
      ImGui.SetNextWindowPos(new Vector2(0, 0));
      ImGui.SetNextWindowSize(new Vector2(300, 200));
      ImGui.Begin("Shader Setting", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove);

      if (ImGui.SliderInt("Hue", ref _hue, 0, 360))
        Color = HsvToRgb();

      if (ImGui.SliderFloat("Value", ref _value, 0, 1))
        Color = HsvToRgb();

      if (ImGui.SliderFloat("Saturation", ref _saturation, 0, 1))
        Color = HsvToRgb();


      ImGui.ColorButton("Color", Color, ImGuiColorEditFlags.None, new Vector2(100, 100));

      ImGui.End();
    }

    private void SetColorVector3(Vector4 color) {
      ColorEvent.Invoke(new Vector3(color.X, color.Y, color.Z));
    }

    public void RenderUi() {
      UiController.Instance.ImGuiController.Render();
    }

    private Vector4 HsvToRgb() {
      double hue = _hue;
      while (hue < 0) {
        hue += 360;
      }

      while (hue >= 360) {
        hue -= 360;
      }

      double red, green, blue;
      if (_value <= 0) {
        red = green = blue = 0;
      }
      else if (_saturation <= 0) {
        red = green = blue = _value;
      }
      else {
        double hf = hue / 60.0;
        int i = (int)Math.Floor(hf);
        double f = hf - i;
        double pv = _value * (1 - _saturation);
        double qv = _value * (1 - _saturation * f);
        double tv = _value * (1 - _saturation * (1 - f));
        switch (i) {
          case 0:
            red = _value;
            green = tv;
            blue = pv;
            break;
          case 1:
            red = qv;
            green = _value;
            blue = pv;
            break;
          case 2:
            red = pv;
            green = _value;
            blue = tv;
            break;
          case 3:
            red = pv;
            green = qv;
            blue = _value;
            break;
          case 4:
            red = tv;
            green = pv;
            blue = _value;
            break;
          case 5:
            red = _value;
            green = pv;
            blue = qv;
            break;
          case 6:
            red = _value;
            green = tv;
            blue = pv;
            break;
          case -1:
            red = _value;
            green = pv;
            blue = qv;
            break;
          default:
            red = green = blue = _value;
            break;
        }
      }

      return new Vector4((float)red, (float)green, (float)blue, 0f);
    }
  }
}