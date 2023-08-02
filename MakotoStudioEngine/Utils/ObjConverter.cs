using System.Numerics;
using MSE.Engine.Constants;

namespace MSE.Engine.Utils {
  public class ObjConverter {
    public float[] Vertices => _vertices;
    public uint[] Indices => _indices;

    private static readonly string FileLoaderPath = AppContext.BaseDirectory + "models/";
    private readonly string _fileFullPath;
    private string _mtlFileName;

    private float[] _vertices = Array.Empty<float>();
    private uint[] _indices = Array.Empty<uint>();

    #region TempData Holder

    private List<float> _tempVertices = new();
    private List<uint> _tempIndices = new();

    private List<int> _tempVerticesIndices = new();
    private List<int> _tempNormalIndices = new();
    private List<int> _tempUvIndices = new();

    private List<Vector3> _originVertices = new();
    private List<Vector3> _originNormals = new();
    private List<Vector2> _originUvs = new();

    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">File path</param>
    public ObjConverter(string name) {
      _fileFullPath = FileLoaderPath + name + ".obj";
      Load();
    }

    private void Load() {
      if (!File.Exists(_fileFullPath))
        throw new Exception($"FILE NOT FOUND: {_fileFullPath}");

      try {
#if DEBUG
        var startTime = DateTime.Now;
#endif
        var sr = new StreamReader(_fileFullPath);
        ReadObjData(sr);
        ProcessData();
        ReleaseTempData();
        sr.Close();
#if DEBUG
        var endTime = DateTime.Now;
        var time = endTime - startTime;
        Console.WriteLine($"Execution Read OBJ File: {time}");
#endif
      }
      catch (Exception e) {
        Console.WriteLine("Unable to read OBJ file!");
        Console.WriteLine("Exception: " + e.Message);
      }
    }

    private void ReadObjData(StreamReader sr) {
      var line = sr.ReadLine();
      while (line != null) {
        if (!line.StartsWith(ObjFileKeys.CommandKey)) {
          if (line.StartsWith(ObjFileKeys.MtlLibKey)) {
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            _mtlFileName = parts[1];
          }
          else if (line.StartsWith(ObjFileKeys.UvsKey)) {
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            _originUvs.Add(new Vector2(float.Parse(parts[1]), float.Parse(parts[2])));
          }
          else if (line.StartsWith(ObjFileKeys.NormalsKey)) {
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            _originNormals.Add(new Vector3(float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3])));
          }
          else if (line.StartsWith(ObjFileKeys.VertexKey)) {
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            _originVertices.Add(new Vector3(float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3])));
          }
          else if (line.StartsWith(ObjFileKeys.FacesKey)) {
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            // ---
            var faceVertexFirst = parts[1].Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            _tempVerticesIndices.Add(int.Parse(faceVertexFirst[0]));
            _tempUvIndices.Add(int.Parse(faceVertexFirst[1]));
            _tempNormalIndices.Add(int.Parse(faceVertexFirst[2]));
            // ---
            var faceVertexSecond = parts[2].Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            _tempVerticesIndices.Add(int.Parse(faceVertexSecond[0]));
            _tempUvIndices.Add(int.Parse(faceVertexSecond[1]));
            _tempNormalIndices.Add(int.Parse(faceVertexSecond[2]));
            // ---
            var faceVertexThird = parts[3].Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            _tempVerticesIndices.Add(int.Parse(faceVertexThird[0]));
            _tempUvIndices.Add(int.Parse(faceVertexThird[1]));
            _tempNormalIndices.Add(int.Parse(faceVertexThird[2]));
          }
        }

        line = sr.ReadLine();
      }
    }

    private void ProcessData() {
      for (var i = 0; i < _tempVerticesIndices.Count; i++) {
        int index = _tempVerticesIndices[i];
        Vector3 vertex = _originVertices[index - 1];
        _tempVertices.Add(vertex.X);
        _tempVertices.Add(vertex.Y);
        _tempVertices.Add(vertex.Z);
        int indexNormals = _tempNormalIndices[i];
        Vector3 normal = _originNormals[indexNormals - 1];
        _tempVertices.Add(normal.X);
        _tempVertices.Add(normal.Y);
        _tempVertices.Add(normal.Z);
        int indexUvs = _tempUvIndices[i];
        Vector2 uv = _originUvs[indexUvs - 1];
        _tempVertices.Add(uv.X);
        _tempVertices.Add(uv.Y);
        _tempIndices.Add((uint)i);
      }

      _vertices = _tempVertices.ToArray();
      _indices = _tempIndices.ToArray();
    }

    private void ReleaseTempData() {
      _tempIndices.Clear();
      _tempVertices.Clear();

      _tempVerticesIndices.Clear();
      _tempNormalIndices.Clear();
      _tempUvIndices.Clear();

      _originVertices.Clear();
      _originNormals.Clear();
      _originUvs.Clear();
    }
  }
}