namespace MSE.Engine.Extensions {
  public abstract class Disposable : IDisposable {
    ~Disposable() => this.Dispose(false);

    public void Dispose() {
      Dispose();
      GC.SuppressFinalize(this);
    }

    private void Dispose(bool isDisposing) {
      if (!isDisposing)
        return;
      DisposeInner();
    }

    protected virtual void DisposeInner() {
    }
  }
}