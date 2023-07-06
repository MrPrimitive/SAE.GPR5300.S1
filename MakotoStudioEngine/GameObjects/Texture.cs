using Silk.NET.OpenGL;

namespace MakotoStudioEngine.GameObjects {
  public class Texture : IDisposable {
    private uint _handle;
    private GL _gl;

    public uint Handler() => _handle;

    public unsafe Texture(GL gl, string textureName) {
      _gl = gl;

      _handle = _gl.GenTexture();
      Bind();

      using (var img = Image.Load<Rgba32>("textures/" + textureName)) {
        gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba8, (uint)img.Width, (uint)img.Height, 0,
          PixelFormat.Rgba, PixelType.UnsignedByte, null);

        img.ProcessPixelRows(accessor => {
          for (int y = 0; y < accessor.Height; y++) {
            fixed (void* data = accessor.GetRowSpan(y)) {
              gl.TexSubImage2D(TextureTarget.Texture2D, 0, 0, y, (uint)accessor.Width, 1, PixelFormat.Rgba,
                PixelType.UnsignedByte, data);
            }
          }
        });
      }

      SetParameters();
    }

    public unsafe Texture(GL gl,
      Span<byte> data,
      uint width,
      uint height) {
      _gl = gl;

      _handle = _gl.GenTexture();
      Bind();

      fixed (void* d = &data[0]) {
        _gl.TexImage2D(TextureTarget.Texture2D, 0, (int)InternalFormat.Rgba, width, height, 0, PixelFormat.Rgba,
          PixelType.UnsignedByte, d);
        SetParameters();
      }
    }

    public unsafe Texture(GL gl,
      List<string> textureNames) {
      _gl = gl;

      _handle = _gl.GenTexture();
      _gl.BindTexture(TextureTarget.TextureCubeMap, _handle);

      for (int i = 0; i < textureNames.Count; i++) {
        using (var img = Image.Load<Rgba32>("textures/" + textureNames[i])) {
          var target = TextureTarget.TextureCubeMapPositiveX + i;
          gl.TexImage2D(target, 0, InternalFormat.Rgba, (uint)img.Width,
            (uint)img.Height, 0,
            PixelFormat.Rgba, PixelType.UnsignedByte, null);

          img.ProcessPixelRows(accessor => {
            for (int y = 0; y < accessor.Height; y++) {
              fixed (void* data = accessor.GetRowSpan(y)) {
                gl.TexSubImage2D(target, 0, 0, y, (uint)accessor.Width, 1,
                  PixelFormat.Rgba,
                  PixelType.UnsignedByte, data);
              }
            }
          });
        }
      }

      _gl.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)GLEnum.Linear);
      _gl.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);
      _gl.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)GLEnum.ClampToEdge);
      _gl.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)GLEnum.ClampToEdge);
      _gl.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)GLEnum.ClampToEdge);
    }

    // unsigned int loadCubemap(vector<std::string> faces)
    // {
    //   unsigned int textureID;
    //   glGenTextures(1, &textureID);
    //   glBindTexture(GL_TEXTURE_CUBE_MAP, textureID);
    //
    //   int width, height, nrChannels;
    //   for (unsigned int i = 0; i < faces.size(); i++)
    //   {
    //     unsigned char *data = stbi_load(faces[i].c_str(), &width, &height, &nrChannels, 0);
    //     if (data)
    //     {
    //       glTexImage2D(GL_TEXTURE_CUBE_MAP_POSITIVE_X + i, 0, GL_RGB, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, data);
    //       stbi_image_free(data);
    //     }
    //     else
    //     {
    //       std::cout << "Cubemap texture failed to load at path: " << faces[i] << std::endl;
    //       stbi_image_free(data);
    //     }
    //   }
    //   glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    //   glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
    //   glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
    //   glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
    //   glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_WRAP_R, GL_CLAMP_TO_EDGE);
    //
    //   return textureID;
    // }

    private void SetParameters() {
      _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)GLEnum.ClampToEdge);
      _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)GLEnum.ClampToEdge);
      _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.LinearMipmapLinear);
      _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);
      _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
      _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 8);
      _gl.GenerateMipmap(TextureTarget.Texture2D);
    }

    public void Bind(TextureUnit textureSlot = TextureUnit.Texture0) {
      _gl.ActiveTexture(textureSlot);
      _gl.BindTexture(TextureTarget.Texture2D, _handle);
    }

    public void Dispose() {
      _gl.DeleteTexture(_handle);
    }
  }
}