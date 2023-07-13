﻿using MSE.Engine.Interfaces;
using MSE.Engine.Utils;

namespace SAE.GPR5300.S1.Assets.Models {
  public class Sphere : IModel {
    public static Sphere Instance => Lazy.Value;
    private static readonly Lazy<Sphere> Lazy = new(() => new Sphere());
    public float[] Vertices { get; }
    public uint[] Indices { get; }

    private Sphere() {
      var objWizard = new ObjConverter("spheres.obj");
      Vertices = objWizard.Vertices;
      Indices = objWizard.Indices;
    }
  }
}