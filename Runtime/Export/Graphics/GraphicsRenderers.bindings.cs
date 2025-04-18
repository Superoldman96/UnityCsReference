// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Scripting;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

// RayTracingMode enum will be moved into UnityEngine.Rendering in the future.
using RayTracingMode = UnityEngine.Experimental.Rendering.RayTracingMode;

using LT = UnityEngineInternal.LightmapType;

namespace UnityEngine
{
    [RequireComponent(typeof(Transform))]
    [UsedByNativeCode]
    public partial class Renderer : Component
    {
        // called when the object became visible by any camera.
        // void OnBecameVisible();

        // called when the object is no longer visible by any camera.
        // void OnBecameInvisible();
    }

    [NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
    public partial class Renderer : Component
    {
        extern public Bounds bounds
        {
            [FreeFunction(Name = "RendererScripting::GetWorldBounds", HasExplicitThis = true)] get;
            [NativeName("SetWorldAABB")] set;
        }
        extern public Bounds localBounds
        {
            [FreeFunction(Name = "RendererScripting::GetLocalBounds", HasExplicitThis = true)] get;
            [NativeName("SetLocalAABB")] set;
        }
        [NativeName("ResetWorldAABB")] extern public void ResetBounds();
        [NativeName("ResetLocalAABB")] extern public void ResetLocalBounds();
        [NativeName("HasCustomWorldAABB")] extern internal bool Internal_HasCustomBounds();
        [NativeName("HasCustomLocalAABB")] extern internal bool Internal_HasCustomLocalBounds();

        [FreeFunction(Name = "RendererScripting::SetStaticLightmapST", HasExplicitThis = true)] extern private void SetStaticLightmapST(Vector4 st);

        [FreeFunction(Name = "RendererScripting::GetMaterial", HasExplicitThis = true)] extern private Material GetMaterial();
        [FreeFunction(Name = "RendererScripting::GetSharedMaterial", HasExplicitThis = true)] extern private Material GetSharedMaterial();
        [FreeFunction(Name = "RendererScripting::SetMaterial", HasExplicitThis = true)] extern private void SetMaterial(Material m);

        [FreeFunction(Name = "RendererScripting::GetMaterialArray", HasExplicitThis = true)] extern private Material[] GetMaterialArray();
        [FreeFunction(Name = "RendererScripting::GetMaterialArray", HasExplicitThis = true)] extern private void CopyMaterialArray([Out] Material[] m);
        [FreeFunction(Name = "RendererScripting::GetSharedMaterialArray", HasExplicitThis = true)] extern private void CopySharedMaterialArray([Out] Material[] m);
        [FreeFunction(Name = "RendererScripting::SetMaterialArray", HasExplicitThis = true)] extern private void SetMaterialArray([NotNull] Material[] m, int length);

        private void SetMaterialArray(Material[] m) { SetMaterialArray(m, m != null ? m.Length : 0); }

        [FreeFunction(Name = "RendererScripting::SetPropertyBlock", HasExplicitThis = true)] extern internal void Internal_SetPropertyBlock(MaterialPropertyBlock properties);
        [FreeFunction(Name = "RendererScripting::GetPropertyBlock", HasExplicitThis = true)] extern internal void Internal_GetPropertyBlock([NotNull] MaterialPropertyBlock dest);
        [FreeFunction(Name = "RendererScripting::SetPropertyBlockMaterialIndex", HasExplicitThis = true)] extern internal void Internal_SetPropertyBlockMaterialIndex(MaterialPropertyBlock properties, int materialIndex);
        [FreeFunction(Name = "RendererScripting::GetPropertyBlockMaterialIndex", HasExplicitThis = true)] extern internal void Internal_GetPropertyBlockMaterialIndex([NotNull] MaterialPropertyBlock dest, int materialIndex);
        [FreeFunction(Name = "RendererScripting::HasPropertyBlock", HasExplicitThis = true)] extern public bool HasPropertyBlock();

        public void SetPropertyBlock(MaterialPropertyBlock properties) { Internal_SetPropertyBlock(properties); }
        public void SetPropertyBlock(MaterialPropertyBlock properties, int materialIndex) { Internal_SetPropertyBlockMaterialIndex(properties, materialIndex); }
        public void GetPropertyBlock(MaterialPropertyBlock properties) { Internal_GetPropertyBlock(properties); }
        public void GetPropertyBlock(MaterialPropertyBlock properties, int materialIndex) { Internal_GetPropertyBlockMaterialIndex(properties, materialIndex); }

        [FreeFunction(Name = "RendererScripting::GetClosestReflectionProbes", HasExplicitThis = true)] extern private void GetClosestReflectionProbesInternal(object result);
    }

    [NativeHeader("Runtime/Graphics/Renderer.h")]
    public partial class Renderer : Component
    {
        extern public bool enabled   { get; set; }
        extern public bool isVisible {[NativeName("IsVisibleInScene")] get; }

        extern public ShadowCastingMode shadowCastingMode { get; set; }
        extern public bool              receiveShadows { get; set; }

        extern public bool              forceRenderingOff { get; set; }
        extern internal bool            allowGPUDrivenRendering { get; set; }
        extern internal bool            smallMeshCulling { get; set; }

        [NativeName("GetIsStaticShadowCaster")] extern private bool GetIsStaticShadowCaster();
        [NativeName("SetIsStaticShadowCaster")] extern private void SetIsStaticShadowCaster(bool value);

        public bool staticShadowCaster { get { return GetIsStaticShadowCaster(); } set { SetIsStaticShadowCaster(value); } }

        extern public MotionVectorGenerationMode motionVectorGenerationMode { get; set; }
        extern public LightProbeUsage            lightProbeUsage { get; set; }
        extern public ReflectionProbeUsage       reflectionProbeUsage { get; set; }
        extern public UInt32                     renderingLayerMask { get; set; }
        extern public int                        rendererPriority { get; set; }
        extern public RayTracingMode             rayTracingMode { get; set; }
        extern public RayTracingAccelerationStructureBuildFlags rayTracingAccelerationStructureBuildFlags { get; set; }
        extern public bool                       rayTracingAccelerationStructureBuildFlagsOverride { get; set; }

        extern public   string sortingLayerName  { get; set; }
        extern public   int    sortingLayerID    { get; set; }
        extern public   int    sortingOrder      { get; set; }
        extern internal UInt32 sortingKey        { get; }
        extern internal int    sortingGroupID    { get; set; }
        extern internal int    sortingGroupOrder { get; set; }
        extern internal UInt32 sortingGroupKey   { get; }

        extern public bool isLOD0 {[NativeName("IsLOD0")] get; }

        internal extern byte stagePriority { get; set; }

        [NativeProperty("IsDynamicOccludee")] extern public bool allowOcclusionWhenDynamic { get; set; }

        [NativeProperty("ForceMeshLod")] extern public Int16 forceMeshLod { get; set; }
        [NativeProperty("MeshLodSelectionBias")] extern public float meshLodSelectionBias { get; set; }

        [NativeProperty("StaticBatchRoot")] extern internal Transform staticBatchRootTransform { get; set; }
        extern internal int staticBatchIndex { get; }
        extern internal void SetStaticBatchInfo(int firstSubMesh, int subMeshCount);
        extern public bool isPartOfStaticBatch {[NativeName("IsPartOfStaticBatch")] get; }

        extern public Matrix4x4 worldToLocalMatrix { get; }
        extern public Matrix4x4 localToWorldMatrix { get; }


        extern public GameObject lightProbeProxyVolumeOverride { get; set; }
        extern public Transform  probeAnchor { get; set; }

        [NativeName("GetLightmapIndexInt")] extern private int  GetLightmapIndex(LT lt);
        [NativeName("SetLightmapIndexInt")] extern private void SetLightmapIndex(int index, LT lt);
        [NativeName("GetLightmapST")] extern private Vector4 GetLightmapST(LT lt);
        [NativeName("SetLightmapST")] extern private void    SetLightmapST(Vector4 st, LT lt);

        public int lightmapIndex         { get { return GetLightmapIndex(LT.StaticLightmap); }  set { SetLightmapIndex(value, LT.StaticLightmap); } }
        public int realtimeLightmapIndex { get { return GetLightmapIndex(LT.DynamicLightmap); } set { SetLightmapIndex(value, LT.DynamicLightmap); } }

        public Vector4 lightmapScaleOffset         { get { return GetLightmapST(LT.StaticLightmap); }  set { SetStaticLightmapST(value); } }
        public Vector4 realtimeLightmapScaleOffset { get { return GetLightmapST(LT.DynamicLightmap); } set { SetLightmapST(value, LT.DynamicLightmap); } }

        extern private int GetMaterialCount();
        [NativeName("GetMaterialArray")] extern private Material[] GetSharedMaterialArray();

        // this is needed to extract check for persistent from cpp to cs
        extern internal bool IsPersistent();

        public Material[] materials
        {
            get
            {
                if (IsPersistent())
                {
                    Debug.LogError("Not allowed to access Renderer.materials on prefab object. Use Renderer.sharedMaterials instead", this);
                    return null;
                }
                return GetMaterialArray();
            }
            set { SetMaterialArray(value); }
        }

        public Material material
        {
            get
            {
                if (IsPersistent())
                {
                    Debug.LogError("Not allowed to access Renderer.material on prefab object. Use Renderer.sharedMaterial instead", this);
                    return null;
                }
                return GetMaterial();
            }
            set { SetMaterial(value); }
        }

        public Material sharedMaterial { get { return GetSharedMaterial(); } set { SetMaterial(value); } }
        public Material[] sharedMaterials { get { return GetSharedMaterialArray(); } set { SetMaterialArray(value); } }

        public void GetMaterials(List<Material> m)
        {
            if (m == null)
                throw new ArgumentNullException("The result material list cannot be null.", "m");
            if (IsPersistent())
                throw new InvalidOperationException("Not allowed to access Renderer.materials on prefab object. Use Renderer.sharedMaterials instead");

            NoAllocHelpers.EnsureListElemCount(m, GetMaterialCount());
            CopyMaterialArray(NoAllocHelpers.ExtractArrayFromList(m));
        }

        public void SetSharedMaterials(List<Material> materials)
        {
            if (materials == null)
                throw new ArgumentNullException("The material list to set cannot be null.", "materials");

            SetMaterialArray(NoAllocHelpers.ExtractArrayFromList(materials), materials.Count);
        }

        public void SetMaterials(List<Material> materials)
        {
            if (materials == null)
                throw new ArgumentNullException("The material list to set cannot be null.", "materials");

            SetMaterialArray(NoAllocHelpers.ExtractArrayFromList(materials), materials.Count);
        }

        public void GetSharedMaterials(List<Material> m)
        {
            if (m == null)
                throw new ArgumentNullException("The result material list cannot be null.", "m");

            NoAllocHelpers.EnsureListElemCount(m, GetMaterialCount());
            CopySharedMaterialArray(NoAllocHelpers.ExtractArrayFromList(m));
        }

        public void GetClosestReflectionProbes(List<ReflectionProbeBlendInfo> result)
        {
            GetClosestReflectionProbesInternal(result);
        }

        extern public LODGroup LODGroup { get; }
    }

    [NativeHeader("Runtime/Graphics/TrailRenderer.h")]
    public sealed partial class TrailRenderer : Renderer
    {
        extern public float time                { get; set; }
        extern internal float previewTimeScale  { get; set; }
        extern public float startWidth          { get; set; }
        extern public float endWidth            { get; set; }
        extern public float widthMultiplier     { get; set; }
        extern public bool  autodestruct        { get; set; }
        extern public bool  emitting            { get; set; }
        extern public int   numCornerVertices   { get; set; }
        extern public int   numCapVertices      { get; set; }
        extern public float minVertexDistance   { get; set; }

        extern public Color startColor          { get; set; }
        extern public Color endColor            { get; set; }

        [NativeProperty("PositionsCount")] extern public int positionCount { get; }
        extern public void SetPosition(int index, Vector3 position);
        extern public Vector3 GetPosition(int index);

        extern public Vector2 textureScale      { get; set; }
        extern public float shadowBias          { get; set; }

        extern public bool generateLightingData { get; set; }
        extern public bool applyActiveColorSpace { get; set; }

        extern public LineTextureMode textureMode { get; set; }
        extern public LineAlignment   alignment   { get; set; }
        extern public SpriteMaskInteraction maskInteraction { get; set; }

        extern public void Clear();

        public void BakeMesh(Mesh mesh, bool useTransform = false) { BakeMesh(mesh, Camera.main, useTransform); }
        extern public void BakeMesh([NotNull] Mesh mesh, [NotNull] Camera camera, bool useTransform = false);

        public AnimationCurve widthCurve    { get { return GetWidthCurveCopy(); }    set { SetWidthCurve(value); } }
        public Gradient       colorGradient { get { return GetColorGradientCopy(); } set { SetColorGradient(value); } }

        // these are direct glue to TrailRenderer methods to simplify properties code (and have null checks generated)

        extern private AnimationCurve GetWidthCurveCopy();
        extern private void SetWidthCurve([NotNull] AnimationCurve curve);

        extern private Gradient GetColorGradientCopy();
        extern private void SetColorGradient([NotNull] Gradient curve);
    }

    [NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
    public sealed partial class TrailRenderer : Renderer
    {
        [FreeFunction(Name = "TrailRendererScripting::GetPositions", HasExplicitThis = true)]
        extern public int GetPositions([NotNull][Out] Vector3[] positions);

        [FreeFunction(Name = "TrailRendererScripting::GetVisiblePositions", HasExplicitThis = true)]
        extern public int GetVisiblePositions([NotNull][Out] Vector3[] positions);

        [FreeFunction(Name = "TrailRendererScripting::SetPositions", HasExplicitThis = true)]
        extern public void SetPositions([NotNull] Vector3[] positions);

        [FreeFunction(Name = "TrailRendererScripting::AddPosition", HasExplicitThis = true)]
        extern public void AddPosition(Vector3 position);

        [FreeFunction(Name = "TrailRendererScripting::AddPositions", HasExplicitThis = true)]
        extern public void AddPositions([NotNull] Vector3[] positions);

        public void SetPositions(NativeArray<Vector3> positions) { unsafe { SetPositionsWithNativeContainer((IntPtr)positions.GetUnsafeReadOnlyPtr(), positions.Length); } }
        public void SetPositions(NativeSlice<Vector3> positions) { unsafe { SetPositionsWithNativeContainer((IntPtr)positions.GetUnsafeReadOnlyPtr(), positions.Length); } }

        public int GetPositions([Out] NativeArray<Vector3> positions) { unsafe { return GetPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr(), positions.Length); } }
        public int GetPositions([Out] NativeSlice<Vector3> positions) { unsafe { return GetPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr(), positions.Length); } }

        public int GetVisiblePositions([Out] NativeArray<Vector3> positions) { unsafe { return GetVisiblePositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr(), positions.Length); } }
        public int GetVisiblePositions([Out] NativeSlice<Vector3> positions) { unsafe { return GetVisiblePositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr(), positions.Length); } }

        public void AddPositions([Out] NativeArray<Vector3> positions) { unsafe { AddPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr(), positions.Length); } }
        public void AddPositions([Out] NativeSlice<Vector3> positions) { unsafe { AddPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr(), positions.Length); } }

        [FreeFunction(Name = "TrailRendererScripting::SetPositionsWithNativeContainer", HasExplicitThis = true)]
        extern private void SetPositionsWithNativeContainer(IntPtr positions, int count);

        [FreeFunction(Name = "TrailRendererScripting::GetPositionsWithNativeContainer", HasExplicitThis = true)]
        extern private int GetPositionsWithNativeContainer(IntPtr positions, int length);

        [FreeFunction(Name = "TrailRendererScripting::GetVisiblePositionsWithNativeContainer", HasExplicitThis = true)]
        extern private int GetVisiblePositionsWithNativeContainer(IntPtr positions, int length);

        [FreeFunction(Name = "TrailRendererScripting::AddPositionsWithNativeContainer", HasExplicitThis = true)]
        extern private void AddPositionsWithNativeContainer(IntPtr positions, int length);
    }

    [NativeHeader("Runtime/Graphics/LineRenderer.h")]
    public sealed partial class LineRenderer : Renderer
    {
        extern public float startWidth          { get; set; }
        extern public float endWidth            { get; set; }
        extern public float widthMultiplier     { get; set; }
        extern public int   numCornerVertices   { get; set; }
        extern public int   numCapVertices      { get; set; }
        extern public bool  useWorldSpace       { get; set; }
        extern public bool  loop                { get; set; }

        extern public Color startColor          { get; set; }
        extern public Color endColor            { get; set; }

        [NativeProperty("PositionsCount")] extern public int positionCount { get; set; }
        extern public void SetPosition(int index, Vector3 position);
        extern public Vector3 GetPosition(int index);

        extern public Vector2 textureScale      { get; set; }
        extern public float shadowBias          { get; set; }

        extern public bool generateLightingData { get; set; }
        extern public bool applyActiveColorSpace { get; set; }

        extern public LineTextureMode textureMode { get; set; }
        extern public LineAlignment   alignment   { get; set; }
        extern public SpriteMaskInteraction maskInteraction { get; set; }

        extern public void Simplify(float tolerance);

        public void BakeMesh(Mesh mesh, bool useTransform = false) { BakeMesh(mesh, Camera.main, useTransform); }
        extern public void BakeMesh([NotNull] Mesh mesh, [NotNull] Camera camera, bool useTransform = false);

        public AnimationCurve widthCurve    { get { return GetWidthCurveCopy(); }    set { SetWidthCurve(value); } }
        public Gradient       colorGradient { get { return GetColorGradientCopy(); } set { SetColorGradient(value); } }

        // these are direct glue to TrailRenderer methods to simplify properties code (and have null checks generated)

        extern private AnimationCurve GetWidthCurveCopy();
        extern private void SetWidthCurve([NotNull] AnimationCurve curve);

        extern private Gradient GetColorGradientCopy();
        extern private void SetColorGradient([NotNull] Gradient curve);
    }

    [NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
    public sealed partial class LineRenderer : Renderer
    {
        [FreeFunction(Name = "LineRendererScripting::GetPositions", HasExplicitThis = true)]
        extern public int GetPositions([NotNull][Out] Vector3[] positions);

        [FreeFunction(Name = "LineRendererScripting::SetPositions", HasExplicitThis = true)]
        extern public void SetPositions([NotNull] Vector3[] positions);

        public void SetPositions(NativeArray<Vector3> positions) { unsafe { SetPositionsWithNativeContainer((IntPtr)positions.GetUnsafeReadOnlyPtr(), positions.Length); } }
        public void SetPositions(NativeSlice<Vector3> positions) { unsafe { SetPositionsWithNativeContainer((IntPtr)positions.GetUnsafeReadOnlyPtr(), positions.Length); } }

        public int GetPositions([Out] NativeArray<Vector3> positions) { unsafe { return GetPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr(), positions.Length); } }
        public int GetPositions([Out] NativeSlice<Vector3> positions) { unsafe { return GetPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr(), positions.Length); } }

        [FreeFunction(Name = "LineRendererScripting::SetPositionsWithNativeContainer", HasExplicitThis = true)]
        extern private void SetPositionsWithNativeContainer(IntPtr positions, int count);

        [FreeFunction(Name = "LineRendererScripting::GetPositionsWithNativeContainer", HasExplicitThis = true)]
        extern private int GetPositionsWithNativeContainer(IntPtr positions, int length);
    }

    [NativeHeader("Runtime/Graphics/Mesh/SkinnedMeshRenderer.h"), RequiredByNativeCode /* used by VisualEffect, returns type */]
    public partial class SkinnedMeshRenderer : Renderer
    {
        extern public SkinQuality quality { get; set; }
        extern public bool updateWhenOffscreen  { get; set; }
        extern public bool forceMatrixRecalculationPerRender  { get; set; }

        extern public Transform rootBone { get; set; }
        extern internal Transform actualRootBone { get; }
        extern public Transform[] bones { get; set; }

        [NativeProperty("Mesh")] extern public Mesh sharedMesh { get; set; }
        [NativeProperty("SkinnedMeshMotionVectors")]  extern public bool skinnedMotionVectors { get; set; }

        extern public float GetBlendShapeWeight(int index);
        extern public void  SetBlendShapeWeight(int index, float value);
        public void BakeMesh(Mesh mesh) { BakeMesh(mesh, false); }
        extern public void  BakeMesh([NotNull] Mesh mesh, bool useScale);

        public GraphicsBuffer GetVertexBuffer()
        {
            if (this == null)
                throw new NullReferenceException();
            var buf = GetVertexBufferImpl();
            if (buf != null)
                buf.AddBufferToLeakDetector();
            return buf;
        }

        public GraphicsBuffer GetPreviousVertexBuffer()
        {
            if (this == null)
                throw new NullReferenceException();
            var buf = GetPreviousVertexBufferImpl();
            if (buf != null)
                buf.AddBufferToLeakDetector();
            return buf;
        }

        [FreeFunction(Name = "SkinnedMeshRendererScripting::GetVertexBufferPtr", HasExplicitThis = true)]
        extern GraphicsBuffer GetVertexBufferImpl();
        [FreeFunction(Name = "SkinnedMeshRendererScripting::GetPreviousVertexBufferPtr", HasExplicitThis = true)]
        extern GraphicsBuffer GetPreviousVertexBufferImpl();

        public extern GraphicsBuffer.Target vertexBufferTarget { get; set; }
    }

    [NativeHeader("Runtime/Graphics/Mesh/MeshRenderer.h")]
    public partial class MeshRenderer : Renderer
    {
        [RequiredByNativeCode]  // MeshRenderer is used in the VR Splash screen.
        private void DontStripMeshRenderer() {}

        extern public Mesh additionalVertexStreams { get; set; }
        extern public Mesh enlightenVertexStream { get; set; }
        extern public int subMeshStartIndex {[NativeName("GetSubMeshStartIndex")] get; }
        extern public float scaleInLightmap { get; set; }
        extern public ReceiveGI receiveGI { get; set; }
        extern public bool stitchLightmapSeams { get; set; }
        extern public UInt16 globalIlluminationMeshLod { get; set; }
    }

    [NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
    public static partial class RendererExtensions
    {
        [FreeFunction("RendererScripting::UpdateGIMaterialsForRenderer")] extern static internal void UpdateGIMaterialsForRenderer(Renderer renderer);
    }
}
