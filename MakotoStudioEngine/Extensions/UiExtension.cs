using MSE.Engine.Interfaces;

namespace MSE.Engine.Extensions {
  public static class UiExtension {
    public static void Render(this List<IUiInterface> uis) {
      uis.ForEach(ui => ui.RenderUi());
    }

    public static void Update(this List<IUiInterface> uis) {
      uis.ForEach(ui => ui.UpdateUi());
    }
  }
}