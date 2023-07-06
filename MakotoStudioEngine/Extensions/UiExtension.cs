using MakotoStudioEngine.Interfaces;

namespace MakotoStudioEngine.Extensions {
  public static class UiExtension {
    public static void Render(this List<IUiInterface> uis) {
      uis.ForEach(ui => ui.Render());
    }

    public static void Update(this List<IUiInterface> uis) {
      uis.ForEach(ui => ui.Update());
    }
  }
}