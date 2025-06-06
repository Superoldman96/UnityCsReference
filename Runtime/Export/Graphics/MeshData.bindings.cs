// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
    public sealed partial class Mesh
    {
        [StaticAccessor("MeshDataBindings", StaticAccessorType.DoubleColon)]
        [NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
        public partial struct MeshData
        {
            [NativeMethod(IsThreadSafe = true)] static extern bool HasVertexAttribute(IntPtr self, VertexAttribute attr);
            [NativeMethod(IsThreadSafe = true)] static extern int GetVertexAttributeDimension(IntPtr self, VertexAttribute attr);
            [NativeMethod(IsThreadSafe = true)] static extern VertexAttributeFormat GetVertexAttributeFormat(IntPtr self, VertexAttribute attr);
            [NativeMethod(IsThreadSafe = true)] static extern int GetVertexAttributeStream(IntPtr self, VertexAttribute attr);
            [NativeMethod(IsThreadSafe = true)] static extern int GetVertexAttributeOffset(IntPtr self, VertexAttribute attr);

            [NativeMethod(IsThreadSafe = true)] static extern int GetVertexCount(IntPtr self);
            [NativeMethod(IsThreadSafe = true)] static extern int GetVertexBufferCount(IntPtr self);

            [NativeMethod(IsThreadSafe = true)] static extern IntPtr GetVertexDataPtr(IntPtr self, int stream);
            [NativeMethod(IsThreadSafe = true)] static extern ulong GetVertexDataSize(IntPtr self, int stream);
            [NativeMethod(IsThreadSafe = true)] static extern int GetVertexBufferStride(IntPtr self, int stream);

            [NativeMethod(IsThreadSafe = true)] static extern void CopyAttributeIntoPtr(IntPtr self, VertexAttribute attr, VertexAttributeFormat format, int dim, IntPtr dst);
            [NativeMethod(IsThreadSafe = true)] static extern void CopyIndicesIntoPtr(IntPtr self, int submesh, int meshLod, bool applyBaseVertex, int dstStride, IntPtr dst);

            [NativeMethod(IsThreadSafe = true)] static extern IndexFormat GetIndexFormat(IntPtr self);
            [NativeMethod(IsThreadSafe = true)] static extern int GetIndexCount(IntPtr self, int submesh, int meshlod);

            [NativeMethod(IsThreadSafe = true)] static extern IntPtr GetIndexDataPtr(IntPtr self);
            [NativeMethod(IsThreadSafe = true)] static extern ulong GetIndexDataSize(IntPtr self);

            [NativeMethod(IsThreadSafe = true)] static extern int GetSubMeshCount(IntPtr self);

            [NativeMethod(IsThreadSafe = true)] static extern int GetLodCount(IntPtr self);
            [NativeMethod(IsThreadSafe = true)] static extern void SetLodCount(IntPtr self, int count);
            [NativeMethod(IsThreadSafe = true)] static extern LodSelectionCurve GetLodSelectionCurve(IntPtr self);
            [NativeMethod(IsThreadSafe = true, ThrowsException = true)] static extern void SetLodSelectionCurve(IntPtr self, LodSelectionCurve lodSelectionCurve);
            [NativeMethod(IsThreadSafe = true)] static extern MeshLodRange GetLod(IntPtr self, int submesh, int level);
            [NativeMethod(IsThreadSafe = true, ThrowsException = true)] static extern void SetLod(IntPtr self, int submesh, int level, MeshLodRange levelRange, MeshUpdateFlags flags);
            [NativeMethod(IsThreadSafe = true, ThrowsException = true)] static extern SubMeshDescriptor GetSubMesh(IntPtr self, int index);
            [NativeMethod(IsThreadSafe = true, ThrowsException = true)] static extern void SetVertexBufferParamsFromPtr(IntPtr self, int vertexCount, IntPtr attributesPtr, int attributesCount);
            [NativeMethod(IsThreadSafe = true, ThrowsException = true)] static extern void SetVertexBufferParamsFromArray(IntPtr self, int vertexCount, params VertexAttributeDescriptor[] attributes);
            [NativeMethod(IsThreadSafe = true, ThrowsException = true)] static extern void SetIndexBufferParamsImpl(IntPtr self, int indexCount, IndexFormat indexFormat);
            [NativeMethod(IsThreadSafe = true)] static extern void SetSubMeshCount(IntPtr self, int count);
            [NativeMethod(IsThreadSafe = true, ThrowsException = true)] static extern void SetSubMeshImpl(IntPtr self, int index, SubMeshDescriptor desc, MeshUpdateFlags flags);
        }

        [StaticAccessor("MeshDataArrayBindings", StaticAccessorType.DoubleColon)]
        public partial struct MeshDataArray
        {
            static extern unsafe void AcquireReadOnlyMeshData([NotNull] Mesh mesh, IntPtr* datas);
            static extern unsafe void AcquireReadOnlyMeshDatas([NotNull] Mesh[] meshes, IntPtr* datas, int count);

            static extern unsafe void AcquireMeshDataCopy([NotNull] Mesh mesh, IntPtr* datas);
            static extern unsafe void AcquireMeshDatasCopy([NotNull] Mesh[] meshes, IntPtr* datas, int count);

            static extern unsafe void ReleaseMeshDatas(IntPtr* datas, int count);

            static extern unsafe void CreateNewMeshDatas(IntPtr* datas, int count);
            [NativeThrows] static extern unsafe void ApplyToMeshesImpl([NotNull] Mesh[] meshes, IntPtr* datas, int count, MeshUpdateFlags flags);
            [NativeThrows] static extern void ApplyToMeshImpl([NotNull] Mesh mesh, IntPtr data, MeshUpdateFlags flags);
        }
    }
}
