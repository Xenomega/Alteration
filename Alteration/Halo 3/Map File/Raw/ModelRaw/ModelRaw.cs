using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HaloDeveloper.Map;
using HaloDeveloper.IO;
using System.IO;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
namespace HaloDeveloper.Raw.ModelRaw
{
    public struct HaloVertex
    {
        public static readonly VertexFormats FVF = VertexFormats.Position | VertexFormats.Diffuse | VertexFormats.Texture1;

        Vector3 pos;
        public Vector3 Position
        {
            get { return pos; }
            set { pos = value; }
        }
        int color;
        Vector2 tex;
        public Vector2 Tex
        {
            get { return tex; }
            set { tex = value; }
        }

        public HaloVertex(Vertex vertex)
        {
            pos = new Vector3(vertex.X, vertex.Y, vertex.Z);

            color = Color.White.ToArgb();
            tex = new Vector2(vertex.textureU, vertex.textureV);
            //tex = new Vector2(vertex.textureU, vertex.textureV);
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", pos.X, pos.Y, pos.Z);
        }
    }

    public class ModelInfo
    {
        public string Name;
        public List<Region> Regions;
        public List<ModelPart> ModelParts;
        public BoundingBox BoundingBox;
        public List<Node> Nodes;
        public List<MarkerGroup> MarkerGroups;
        public byte[] Data;
        public int[] shaderid;
        public H3Shader[] shader;
        public Renderer renderer;

        public void InitializeBuffers(Renderer Renderer, HaloMap map)
        {
            renderer = Renderer;
            BoundingBox.CreateBoundingBoxMesh(renderer);
            for (int x = 0; x < ModelParts.Count; x++)
            {
                ModelPart mp = ModelParts[x];
                mp.CreateIndexBuffer(renderer);
                mp.CreateVertexBuffer(renderer);
                ModelParts[x] = mp;
            }

            shader = new H3Shader[shaderid.Length];
            for (int x = 0; x < shaderid.Length; x++)
                shader[x] = new H3Shader(map, shaderid[x], renderer);
        }

        public void RenderModel()
        {
            RenderModel(0);
        }

        public void RenderModel(int LOD)
        {
            //renderer.Render_Device.SetTexture(0, null);
            for (int x = 0; x < Regions.Count; x++)
            {
                ushort permIndex = 0;
                //if (Regions[x].PermutationsIndexs.Count <= LOD)
                //    permIndex = Regions[x].PermutationsIndexs[Regions[x].PermutationsIndexs.Count - 1];
                //else
                //    permIndex = Regions[x].PermutationsIndexs[LOD];

                ModelPart mp = ModelParts[permIndex];

                renderer.Device.VertexFormat = HaloVertex.FVF;
                renderer.Device.SetStreamSource(0, mp.vertexBuffer, 0);
                renderer.Device.Indices = mp.IndexBuffer;

                for (int y = 0; y < mp.Submeshes.Count; y++)
                {
                    renderer.Device.SetTexture(0, shader[mp.Submeshes[y].ShaderIndex].texture);
                    for (int z = mp.Submeshes[y].SubsetIndex;
                        z < mp.Submeshes[y].SubsetIndex +
                        mp.Submeshes[y].SubsetCount; z++)
                    {
                        Subset subset = mp.Subsets[z];
                        // renderer.Render_Device.DrawPrimitives(PrimitiveType.TriangleStrip,
                        //      0, subset.FaceLength);//mp.Submeshes[y].VertexCount,subset.FaceLength,m, 0, subset.FaceStart, 
                        renderer.Device.DrawIndexedPrimitives(PrimitiveType.TriangleStrip,
                            0, 0, mp.Subsets[y].VertexCount, subset.FaceStart, subset.FaceLength);
                    }
                }
            }
        }
    }

    public struct Region
    {
        public string Name;
        public List<Permutation> Permutations;

        public override string ToString() { return Name; }
    }

    public struct Permutation
    {
        public string Name;
        public List<short> LODs;

        public enum LOD
        {
            Lowest = 5,
            Low = 4,
            MediumLow = 3,
            MediumHigh = 2,
            High = 1,
            Highest = 0,
        }

        public override string ToString() { return Name; }
    }

    public struct BoundingBox
    {
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;
        public float zMin;
        public float zMax;
        public float uMin;
        public float uMax;
        public float vMin;
        public float vMax;

        public Mesh Mesh;

        public void CreateBoundingBoxMesh(Renderer Renderer)
        {
            Mesh = Mesh.Box(Renderer.Device,
                xMax - xMin,
                yMax - yMin,
                zMax - zMin);
        }
    }

    public struct MarkerGroup
    {
        public string Name;
        public List<Marker> Markers;

        public MarkerGroup(HaloMap map, EndianReader er)
        {
            Name = map.StringTable.StringItems[map.StringTable.GetStringItemIndexByID(map, er.ReadInt32())].Name;
            int count = er.ReadInt32();
            int pointer = er.ReadInt32() - map.MapHeader.mapMagic;
            Markers = new List<Marker>();
            er.SeekTo(pointer);
            for (int x = 0; x < count; x++)
                Markers.Add(new Marker(er));
        }

        public override string ToString() { return Name; }
    }

    public struct Marker
    {
        public byte RegionIndex;
        public byte PermutationIndex;
        public short NodeIndex;
        public float TranslationX;
        public float TranslationY;
        public float TranslationZ;
        public float RotationI;
        public float RotationJ;
        public float RotationK;
        public float RotationW;
        public float Scale;

        public Marker(EndianReader er)
        {
            RegionIndex = er.ReadByte();
            PermutationIndex = er.ReadByte();
            NodeIndex = er.ReadInt16();
            TranslationX = er.ReadSingle();
            TranslationY = er.ReadSingle();
            TranslationZ = er.ReadSingle();
            RotationI = er.ReadSingle();
            RotationJ = er.ReadSingle();
            RotationK = er.ReadSingle();
            RotationW = er.ReadSingle();
            Scale = er.ReadSingle();
        }
    }

    public struct Node
    {
        public string Name;
        public short ParentNodeIndex;
        public short FirstChildNodeIndex;
        public short NextSiblingNodeIndex;
        public short ImportNodeIndex;
        public float DefaultTranslationX;
        public float DefaultTranslationY;
        public float DefaultTranslationZ;
        public float DefaultRotationI;
        public float DefaultRotationJ;
        public float DefaultRotationK;
        public float DefaultRotationW;
        public float InverseFowardI;
        public float InverseFowardJ;
        public float InverseFowardK;
        public float InverseLeftI;
        public float InverseLeftJ;
        public float InverseLeftK;
        public float InverseUpI;
        public float InverseUpJ;
        public float InverseUpK;
        public float InversePositionI;
        public float InversePositionJ;
        public float InversePositionK;
        public float InverseScale;
        public float DistanceFromParent;

        public Node(HaloMap map, EndianReader er)
        {
            Name = map.StringTable.StringItems[map.StringTable.GetStringItemIndexByID(map, er.ReadInt32())].Name;
            ParentNodeIndex = er.ReadInt16();
            FirstChildNodeIndex = er.ReadInt16();
            NextSiblingNodeIndex = er.ReadInt16();
            ImportNodeIndex = er.ReadInt16();
            DefaultTranslationX = er.ReadSingle();
            DefaultTranslationY = er.ReadSingle();
            DefaultTranslationZ = er.ReadSingle();
            DefaultRotationI = er.ReadSingle();
            DefaultRotationJ = er.ReadSingle();
            DefaultRotationK = er.ReadSingle();
            DefaultRotationW = er.ReadSingle();
            InverseFowardI = er.ReadSingle();
            InverseFowardJ = er.ReadSingle();
            InverseFowardK = er.ReadSingle();
            InverseLeftI = er.ReadSingle();
            InverseLeftJ = er.ReadSingle();
            InverseLeftK = er.ReadSingle();
            InverseUpI = er.ReadSingle();
            InverseUpJ = er.ReadSingle();
            InverseUpK = er.ReadSingle();
            InversePositionI = er.ReadSingle();
            InversePositionJ = er.ReadSingle();
            InversePositionK = er.ReadSingle();
            InverseScale = er.ReadSingle();
            DistanceFromParent = er.ReadSingle();
        }

        public override string ToString() { return Name; }
    }

    public struct ModelPart
    {
        public List<Submesh> Submeshes;
        public List<Subset> Subsets;
        public int RawIdentifier;
        public byte VertexFormat;
        public byte UnknownFormat;

        public int totalVertexCount;
        public int totalFaceCount;
        public Vertex[] VertexData;
        public ushort[] triangleStripData;
        public VertexBuffer vertexBuffer;
        public IndexBuffer IndexBuffer;

        public void CreateVertexBuffer(Renderer Renderer)
        {
            vertexBuffer = new VertexBuffer(typeof(HaloVertex),
                totalVertexCount,
                Renderer.Device, Usage.WriteOnly, HaloVertex.FVF, Pool.Managed);
            GraphicsStream gs = vertexBuffer.Lock(0, 0, 0);

            for (int x = 0; x < totalVertexCount; x++)
                gs.Write(new HaloVertex(VertexData[x]));

            vertexBuffer.Unlock();
        }

        public void CreateIndexBuffer(Renderer Renderer)
        {
            IndexBuffer = new IndexBuffer(typeof(short),
                totalFaceCount,
                Renderer.Device, Usage.WriteOnly, Pool.Managed);
            GraphicsStream gs = IndexBuffer.Lock(0, 0, 0);

            gs.Write(triangleStripData);

            IndexBuffer.Unlock();
        }
    }

    public struct Submesh
    {
        public short ShaderIndex;
        public short FaceStart;
        public short FaceLength;
        public short SubsetIndex;
        public short SubsetCount;
        public short Flags;
        public short VertexCount;
    }

    public struct Subset
    {
        public short FaceStart;
        public short FaceLength;
        public short SubmeshParent;
        public short VertexCount;
    }

    public struct Vertex
    {
        public float X;
        public float Y;
        public float Z;
        public byte[] padding; // 2 bytes
        public float textureU;
        public float textureV;
        public byte[] extra; // 12 bytes

        // Only if the vertex type is 2 will it contain this data below
        public byte boneID1;
        public byte boneID2;
        public byte boneID3;
        public byte boneID4;
        public float VertWeight1;
        public float VertWeight2;
        public float VertWeight3;
        public float VertWeight4;
    }

    public class ModelFunctions
    {
        public static ModelInfo GetModelInfo(HaloMap Map, int TagIndex)
        {
            // Create our structure to hold the info
            ModelInfo mi = new ModelInfo();

            // Get our Name
            Map.IO.SeekTo(Map.IndexItems[TagIndex].Offset);
            mi.Name = Map.StringTable.StringItems[(Map.StringTable.GetStringItemIndexByID(Map, Map.IO.In.ReadInt32()))].Name;

            // Get our Region info first
            Map.IO.SeekTo(Map.IndexItems[TagIndex].Offset + 12);
            int regionCount = Map.IO.In.ReadInt32();
            int regionPointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;
            mi.Regions = new List<Region>();
            for (int x = 0; x < regionCount; x++)
            {
                Region region = new Region();

                Map.IO.SeekTo(regionPointer + (16 * x));
                region.Name = Map.StringTable.StringItems[Map.StringTable.GetStringItemIndexByID(Map,Map.IO.In.ReadInt32())].Name;
                int permutationCount = Map.IO.In.ReadInt32();
                int permutationPointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;
                region.Permutations = new List<Permutation>();
                Map.IO.SeekTo(permutationPointer);
                for (int y = 0; y < permutationCount; y++)
                {
                    Permutation perm = new Permutation();
                    perm.Name = Map.StringTable.StringItems[Map.StringTable.GetStringItemIndexByID(Map, Map.IO.In.ReadInt32())].Name;
                    perm.LODs = new List<short>();
                    for (int l = 0; l < 6; l++)
                        perm.LODs.Add(Map.IO.In.ReadInt16());
                    region.Permutations.Add(perm);
                }

                mi.Regions.Add(region);
            }

            // Get our nodes
            Map.IO.SeekTo(Map.IndexItems[TagIndex].Offset + 48);
            int nodeCount = Map.IO.In.ReadInt32();
            int nodePointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;
            mi.Nodes = new List<Node>();
            Map.IO.SeekTo(nodePointer);
            for (int x = 0; x < nodeCount; x++)
                mi.Nodes.Add(new Node(Map, Map.IO.In));

            // Get our marker groups
            Map.IO.SeekTo(Map.IndexItems[TagIndex].Offset + 60);
            int markerGroupCount = Map.IO.In.ReadInt32();
            int markerGroupPointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;
            mi.MarkerGroups = new List<MarkerGroup>();
            for (int x = 0; x < markerGroupCount; x++)
            {
                Map.IO.SeekTo(markerGroupPointer + (16 * x));
                mi.MarkerGroups.Add(new MarkerGroup(Map, Map.IO.In));
            }

            // Read our shaders
            Map.IO.SeekTo(Map.IndexItems[TagIndex].Offset + 72);
            mi.shaderid = new int[Map.IO.In.ReadInt32()];
            int pointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;
            for (int x = 0; x < mi.shaderid.Length; x++)
            {
                Map.IO.SeekTo(pointer + (x * 0x24) + 0xC);
                mi.shaderid[x] = Map.IO.In.ReadInt32();
            }

            // Get our model info first
            Map.IO.SeekTo(Map.IndexItems[TagIndex].Offset + 104);
            int modelPartCount = Map.IO.In.ReadInt32();
            int modelPartPointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;
            mi.ModelParts = new List<ModelPart>();
            for (int x = 0; x < modelPartCount; x++)
            {
                ModelPart part = new ModelPart();

                Map.IO.SeekTo(modelPartPointer + (76 * x));
                int submeshCount = Map.IO.In.ReadInt32();
                int submeshPointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;
                part.Submeshes = new List<Submesh>();
                Map.IO.SeekTo(submeshPointer);
                for (int y = 0; y < submeshCount; y++)
                {
                    Submesh submesh = new Submesh();

                    submesh.ShaderIndex = Map.IO.In.ReadInt16();
                    Map.IO.In.ReadInt16();
                    submesh.FaceStart = Map.IO.In.ReadInt16();
                    submesh.FaceLength = Map.IO.In.ReadInt16();
                    submesh.SubsetIndex = Map.IO.In.ReadInt16();
                    submesh.SubsetCount = Map.IO.In.ReadInt16();
                    submesh.Flags = Map.IO.In.ReadInt16();
                    submesh.VertexCount = Map.IO.In.ReadInt16();

                    part.totalVertexCount += submesh.VertexCount;
                    part.totalFaceCount += submesh.FaceLength;

                    part.Submeshes.Add(submesh);
                }

                Map.IO.SeekTo(modelPartPointer + (76 * x) + 12);
                int subsetCount = Map.IO.In.ReadInt32();
                int subsetPointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;
                part.Subsets = new List<Subset>();
                Map.IO.SeekTo(subsetPointer);
                for (int y = 0; y < subsetCount; y++)
                {
                    Subset subset = new Subset();

                    subset.FaceStart = Map.IO.In.ReadInt16();
                    subset.FaceLength = Map.IO.In.ReadInt16();
                    subset.SubmeshParent = Map.IO.In.ReadInt16();
                    subset.VertexCount = Map.IO.In.ReadInt16();

                    part.Subsets.Add(subset);
                }

                Map.IO.SeekTo(modelPartPointer + (76 * x) + 32);
                part.RawIdentifier = Map.IO.In.ReadInt32();

                Map.IO.SeekTo(modelPartPointer + (76 * x) + 46);
                part.VertexFormat = Map.IO.In.ReadByte();
                part.UnknownFormat = Map.IO.In.ReadByte();

                part.triangleStripData = new ushort[part.totalFaceCount];
                part.VertexData = new Vertex[part.totalVertexCount];

                mi.ModelParts.Add(part);
            }

            Map.IO.SeekTo(Map.IndexItems[TagIndex].Offset + 116);
            int boundingBoxCount = Map.IO.In.ReadInt32();
            int boundingBoxPointer = Map.IO.In.ReadInt32() - Map.MapHeader.mapMagic;
            mi.BoundingBox = new BoundingBox();
            Map.IO.SeekTo(boundingBoxPointer + 4);
            mi.BoundingBox.xMin = Map.IO.In.ReadSingle();
            mi.BoundingBox.xMax = Map.IO.In.ReadSingle();
            mi.BoundingBox.yMin = Map.IO.In.ReadSingle();
            mi.BoundingBox.yMax = Map.IO.In.ReadSingle();
            mi.BoundingBox.zMin = Map.IO.In.ReadSingle();
            mi.BoundingBox.zMax = Map.IO.In.ReadSingle();
            mi.BoundingBox.uMin = Map.IO.In.ReadSingle();
            mi.BoundingBox.uMax = Map.IO.In.ReadSingle();
            mi.BoundingBox.vMin = Map.IO.In.ReadSingle();
            mi.BoundingBox.vMax = Map.IO.In.ReadSingle();

            // Get our raw ID now
            Map.IO.SeekTo(Map.IndexItems[TagIndex].Offset + 224);
            int rawid = Map.IO.In.ReadInt32();

            // Lets load our raw data now
            mi.Data = Map.RawInformation.GetRawDataFromID(rawid);

            // Now lets start to breakdown our raw data
            EndianIO rawIo = new EndianIO(mi.Data, EndianType.BigEndian);
            rawIo.Open();

            for (int x = 0; x < mi.ModelParts.Count; x++)
            {
                // Get our model part
                ModelPart mp = mi.ModelParts[x];

                int unknownSize = 1;
                if (mp.UnknownFormat == 0 ||
                    mp.UnknownFormat == 2)
                    unknownSize = 4;
                else if (mp.UnknownFormat == 3)
                    unknownSize = 12;

                for (int z = 0; z < mi.ModelParts[x].totalVertexCount; z++)
                {
                    mp.VertexData[z].X =
                        decompressShort(rawIo.In.ReadUInt16(), mi.BoundingBox.xMin, mi.BoundingBox.xMax);
                    mp.VertexData[z].Y =
                      decompressShort(rawIo.In.ReadUInt16(), mi.BoundingBox.yMin, mi.BoundingBox.yMax);
                    mp.VertexData[z].Z =
                      decompressShort(rawIo.In.ReadUInt16(), mi.BoundingBox.zMin, mi.BoundingBox.zMax);
                    mp.VertexData[z].padding = rawIo.In.ReadBytes(2);
                    mp.VertexData[z].textureU =
                      decompressShort(rawIo.In.ReadUInt16(), mi.BoundingBox.uMin, mi.BoundingBox.uMax);
                    mp.VertexData[z].textureV = 1 -
                      decompressShort(rawIo.In.ReadUInt16(), mi.BoundingBox.vMin, mi.BoundingBox.vMax);
                    mp.VertexData[z].extra = rawIo.In.ReadBytes(12);
                    if (mp.VertexFormat == 2)
                    {
                        mp.VertexData[z].boneID1 = rawIo.In.ReadByte();
                        mp.VertexData[z].boneID2 = rawIo.In.ReadByte();
                        mp.VertexData[z].boneID3 = rawIo.In.ReadByte();
                        mp.VertexData[z].boneID4 = rawIo.In.ReadByte();
                        mp.VertexData[z].VertWeight1 = (float)rawIo.In.ReadByte() / 0xFF;
                        mp.VertexData[z].VertWeight2 = (float)rawIo.In.ReadByte() / 0xFF;
                        mp.VertexData[z].VertWeight3 = (float)rawIo.In.ReadByte() / 0xFF;
                        mp.VertexData[z].VertWeight4 = (float)rawIo.In.ReadByte() / 0xFF;
                    }
                }

                //Read the unkown
                byte[] unknwon = rawIo.In.ReadBytes(mp.totalVertexCount * unknownSize);

                //Make sure we are padded
                rawIo.Stream.Position = getPaddedOffset((int)rawIo.Stream.Position);

                //Save back our model part
                mi.ModelParts[x] = mp;
            }

            //Now that we have read the vertex data lets read the face data
            for (int x = 0; x < mi.ModelParts.Count; x++)
            {
                // Get our model part
                ModelPart mp = mi.ModelParts[x];

                for (int y = 0; y < mp.totalFaceCount; y++)
                    mp.triangleStripData[y] = rawIo.In.ReadUInt16();

                rawIo.Stream.Position = getPaddedOffset((int)rawIo.Stream.Position);
            }

            // Lets return our bitmap now
            return mi;
        }

        private static ushort[] createTriangleList(ushort[] TriangleStripData, int start, int length)
        {
            int stripIndicesCount = length;
            int triangleCount = stripIndicesCount - 2;

            if (triangleCount < 1)
                return new ushort[0];

            int listIndicesCount = triangleCount * 3;
            ushort[] listIndices = new ushort[listIndicesCount];

            for (int c = start + 2, index = 0; c < start + stripIndicesCount; c++)
            {
                if (TriangleStripData[c - 2] == TriangleStripData[c] ||
                    TriangleStripData[c - 2] == TriangleStripData[c - 1] ||
                    TriangleStripData[c - 1] == TriangleStripData[c])
                    continue;

                if ((c % 2) == 0)
                {
                    listIndices[index++] = TriangleStripData[c - 2];
                    listIndices[index++] = TriangleStripData[c - 1];
                    listIndices[index++] = TriangleStripData[c];
                }
                else
                {
                    listIndices[index++] = TriangleStripData[c];
                    listIndices[index++] = TriangleStripData[c - 1];
                    listIndices[index++] = TriangleStripData[c - 2];
                }
            }

            return listIndices;
        }

        private static float decompressShort(ushort Value, float Min, float Max)
        {
            return ((float)Value / 65535.0f) * (Max - Min) + Min;
        }

        private static int getPaddedOffset(int Offset)
        {
            if (Offset % 4 != 0)
                Offset += (4 - (Offset % 4));

            return Offset;
        }

        public static void WriteAllToObj(string ExtractDirectory, ModelInfo Mi)
        {
            for (int x = 0; x < Mi.ModelParts.Count; x++)
            {
                ModelPart mp = Mi.ModelParts[x];

                FileStream fs = new FileStream(ExtractDirectory +
                   x.ToString() + ".obj",
                   FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine("# ----- ----------------------------------");
                sw.WriteLine("# Halo 3 Model - Extracted with Alteration");
                sw.WriteLine("# ----------------------------------------");

                WriteModelPart(sw, mp, 0);

                sw.Close();
            }
        }

        public static void WriteToObj(string ExtractDirectory, ModelInfo Mi)
        {
            WriteToObj(ExtractDirectory, Mi, 0);
        }

        public static void WriteToSingleObj(string ExtractDirectory, ModelInfo Mi)
        {
            WriteToSingleObj(ExtractDirectory, Mi, 0);
        }

        public static void WriteToObj(string ExtractDirectory, ModelInfo Mi, Permutation.LOD LOD)
        {
            for (int x = 0; x < Mi.Regions.Count; x++)
            {
                Region region = Mi.Regions[x];
                for (int y = 0; y < region.Permutations.Count; y++)
                {
                    Permutation perm = region.Permutations[y];
                    ModelPart mp = Mi.ModelParts[perm.LODs[(int)LOD]];

                    FileStream fs = new FileStream(ExtractDirectory +
                        Mi.Name + "_" + region.Name + "_" +
                        perm.Name + ".obj", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);

                    sw.WriteLine("# ----- ----------------------------------");
                    sw.WriteLine("# Halo 3 Model - Extracted with Alteration");
                    sw.WriteLine("# ----------------------------------------");

                    WriteModelPart(sw, mp, 0);

                    sw.Close();
                }
            }
        }

        public static void WriteToSingleObj(string ExtractDirectory, ModelInfo Mi, Permutation.LOD LOD)
        {
            FileStream fs = new FileStream(ExtractDirectory + LOD.ToString() + ".obj",
                FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine("# ----- ----------------------------------");
            sw.WriteLine("# Halo 3 Model - Extracted with Alteration");
            sw.WriteLine("# ----------------------------------------");

            //for (int x = 0; x < Mi.Regions.Count; x++)
            //    if (LOD < Mi.Regions[x].Permutations.Count)
            //    {
            //        WriteModelPart(sw, Mi.ModelParts[Mi.Regions[x].PermutationsIndexs[LOD]], 0);
            //    }

            sw.Close();
        }

        private static void WriteModelPart(StreamWriter sw, ModelPart mp, int startIndex)
        {
            for (int z = 0; z < mp.totalVertexCount; z++)
                sw.WriteLine("v {0} {1} {2}",
                    mp.VertexData[z].X,
                    mp.VertexData[z].Y,
                    mp.VertexData[z].Z);

            for (int z = 0; z < mp.totalVertexCount; z++)
                sw.WriteLine("vt {0} {1}",
                    mp.VertexData[z].textureU,
                    mp.VertexData[z].textureV);

            for (int z = 0; z < mp.totalVertexCount; z++)
                sw.WriteLine("vn {0} {1} {2}", 1.0, 1.0, 1.0);

            for (int y = 0; y < mp.Submeshes.Count; y++)
            {
                for (int z = mp.Submeshes[y].SubsetIndex;
                    z < mp.Submeshes[y].SubsetIndex +
                    mp.Submeshes[y].SubsetCount; z++)
                {
                    ushort[] fd = createTriangleList(
                        mp.triangleStripData,
                        mp.Subsets[z].FaceStart,
                        mp.Subsets[z].FaceLength);
                    for (int i = 0; i < fd.Length; i += 3)
                    {
                        string temps = "f " + (fd[i] + 1 + startIndex) + "/" + (fd[i] + 1 + startIndex) + "/" + (fd[i] + 1 + startIndex)
                                    + " " + (fd[i + 1] + 1 + startIndex) + "/" + (fd[i + 1] + 1 + startIndex) + "/" + (fd[i + 1] + 1 + startIndex)
                                    + " " + (fd[i + 2] + 1 + startIndex) + "/" + (fd[i + 2] + 1 + startIndex) + "/" + (fd[i + 2] + 1 + startIndex);
                        sw.WriteLine(temps);
                    }
                }
            }
        }

        public static void WriteEMF(string FilePath, ModelInfo Mi, Permutation.LOD LOD,
            bool ExportNodes, bool ExportMakers, System.Windows.Forms.TreeView treeView)
        {
            // Create our file
            EndianIO io = new EndianIO(FilePath, EndianType.LittleEndian);
            io.Open();

            // Write out our header
            io.Out.Write(0x21666D65); // emf!
            io.Out.Write(0x00000001); // version #1

            // Write our model name
            io.Out.Write(Mi.Name.ToCharArray());
            io.Out.Write((byte)0x00);

            // Write our nodes
            if (ExportNodes)
            {
                io.Out.Write(Mi.Nodes.Count);
                for (int x = 0; x < Mi.Nodes.Count; x++)
                {
                    io.Out.Write(Mi.Nodes[x].Name.ToCharArray());
                    io.Out.Write((byte)0x00);
                    io.Out.Write(Mi.Nodes[x].ParentNodeIndex);
                    io.Out.Write(Mi.Nodes[x].DefaultTranslationX);
                    io.Out.Write(Mi.Nodes[x].DefaultTranslationY);
                    io.Out.Write(Mi.Nodes[x].DefaultTranslationZ);
                    io.Out.Write(Mi.Nodes[x].DefaultRotationI);
                    io.Out.Write(Mi.Nodes[x].DefaultRotationJ);
                    io.Out.Write(Mi.Nodes[x].DefaultRotationK);
                    io.Out.Write(Mi.Nodes[x].DefaultRotationW);
                }
            }
            else
                io.Out.Write((int)0);

            // Write out our marker groups
            if (ExportMakers)
            {
                io.Out.Write(Mi.MarkerGroups.Count);
                for (int x = 0; x < Mi.MarkerGroups.Count; x++)
                {
                    // Write our group name
                    io.Out.Write(Mi.MarkerGroups[x].Name.ToCharArray());
                    io.Out.Write((byte)0x00);

                    // Write out our markers
                    io.Out.Write(Mi.MarkerGroups[x].Markers.Count);
                    for (int y = 0; y < Mi.MarkerGroups[x].Markers.Count; y++)
                    {
                        io.Out.Write(Mi.MarkerGroups[x].Markers[y].RegionIndex);
                        io.Out.Write(Mi.MarkerGroups[x].Markers[y].PermutationIndex);
                        io.Out.Write(Mi.MarkerGroups[x].Markers[y].NodeIndex);
                        io.Out.Write(Mi.MarkerGroups[x].Markers[y].TranslationX);
                        io.Out.Write(Mi.MarkerGroups[x].Markers[y].TranslationY);
                        io.Out.Write(Mi.MarkerGroups[x].Markers[y].TranslationZ);
                        io.Out.Write(Mi.MarkerGroups[x].Markers[y].RotationI);
                        io.Out.Write(Mi.MarkerGroups[x].Markers[y].RotationJ);
                        io.Out.Write(Mi.MarkerGroups[x].Markers[y].RotationK);
                        io.Out.Write(Mi.MarkerGroups[x].Markers[y].RotationW);
                        io.Out.Write(Mi.MarkerGroups[x].Markers[y].Scale);
                    }
                }
            }
            else
                io.Out.Write((int)0);

            // See how many regions are checked
            int count = 0;
            for (int x = 0; x < treeView.Nodes.Count; x++)
                if (treeView.Nodes[x].Checked)
                    count++;

            // Write out our regions
            io.Out.Write(count);
            for (int x = 0; x < treeView.Nodes.Count; x++)
            {
                // Only if this region is slected
                if (treeView.Nodes[x].Checked)
                {
                    // Write our region name
                    io.Out.Write(Mi.Regions[x].Name.ToCharArray());
                    io.Out.Write((byte)0x00);

                    // Get our perm count
                    int pcount = 0;
                    for (int y = 0; y < treeView.Nodes[x].Nodes.Count; y++)
                        if (treeView.Nodes[x].Nodes[y].Checked)
                            pcount++;

                    // Write our perms
                    io.Out.Write(pcount);
                    for (int y = 0; y < treeView.Nodes[x].Nodes.Count; y++)
                    {
                        // Only if this perm is slected
                        if (treeView.Nodes[x].Nodes[y].Checked)
                        {

                            // Write our perm name
                            io.Out.Write(Mi.Regions[x].Permutations[y].Name.ToCharArray());
                            io.Out.Write((byte)0x00);

                            // Get our model part from this perms LOD
                            ModelPart part = Mi.ModelParts[Mi.Regions[x].Permutations[y].LODs[(int)LOD]];

                            // Write out our vertex format
                            io.Out.Write(part.VertexFormat);

                            #region Old Code
                            /*
                            // Write out our submeshs
                            io.Out.Write(part.Submeshes.Count);
                            for (int z = 0; z < part.Submeshes.Count; z++)
                            {
                                // Write our vertex data
                                io.Out.Write(part.totalVertexCount);
                                for (int v = 0; v < part.totalVertexCount; v++)
                                {
                                    io.Out.Write(part.VertexData[v].X);
                                    io.Out.Write(part.VertexData[v].Y);
                                    io.Out.Write(part.VertexData[v].Z);
                                    io.Out.Write(part.VertexData[v].textureU);
                                    io.Out.Write(part.VertexData[v].textureV);

                                    // If we have bone id's and skin weights
                                    if (part.VertexFormat == 2)
                                    {
                                        io.Out.Write(part.VertexData[v].boneID1);
                                        io.Out.Write(part.VertexData[v].boneID2);
                                        io.Out.Write(part.VertexData[v].boneID3);
                                        io.Out.Write(part.VertexData[v].boneID4);
                                        io.Out.Write(part.VertexData[v].VertWeight1);
                                        io.Out.Write(part.VertexData[v].VertWeight2);
                                        io.Out.Write(part.VertexData[v].VertWeight3);
                                        io.Out.Write(part.VertexData[v].VertWeight4);
                                    }
                                }

                                // Lets create our triangle list first
                                List<ushort> triangleList = new List<ushort>();
                                for (int s = 0; s < part.Submeshes.Count; s++)
                                {
                                    for (int i = part.Submeshes[s].SubsetIndex;
                                        i < part.Submeshes[s].SubsetIndex +
                                        part.Submeshes[s].SubsetCount; i++)
                                    {
                                        triangleList.AddRange(createTriangleList(
                                              part.triangleStripData,
                                              part.Subsets[i].FaceStart,
                                              part.Subsets[i].FaceLength));
                                    }
                                }

                                // Write our face data
                                io.Out.Write(triangleList.Count / 3);
                                for (int f = 0; f < triangleList.Count; f += 3)
                                {
                                    io.Out.Write(triangleList[f]);
                                    io.Out.Write(triangleList[f + 1]);
                                    io.Out.Write(triangleList[f + 2]);
                                }
                            }*/
                            #endregion

                            // We only have 1 submeshs
                            io.Out.Write((int)1);

                            // Lets write all our verts
                            // Write our vertex data
                            io.Out.Write(part.totalVertexCount);
                            for (int v = 0; v < part.totalVertexCount; v++)
                            {
                                io.Out.Write(part.VertexData[v].X);
                                io.Out.Write(part.VertexData[v].Y);
                                io.Out.Write(part.VertexData[v].Z);
                                io.Out.Write(part.VertexData[v].textureU);
                                io.Out.Write(part.VertexData[v].textureV);

                                // If we have bone id's and skin weights
                                if (part.VertexFormat == 2)
                                {
                                    io.Out.Write(part.VertexData[v].boneID1);
                                    io.Out.Write(part.VertexData[v].boneID2);
                                    io.Out.Write(part.VertexData[v].boneID3);
                                    io.Out.Write(part.VertexData[v].boneID4);
                                    io.Out.Write(part.VertexData[v].VertWeight1);
                                    io.Out.Write(part.VertexData[v].VertWeight2);
                                    io.Out.Write(part.VertexData[v].VertWeight3);
                                    io.Out.Write(part.VertexData[v].VertWeight4);
                                }
                            }

                            // Lets create our triangle list first
                            List<ushort> triangleList = new List<ushort>();
                            /*for (int s = 0; s < part.Submeshes.Count; s++)
                            {
                                for (int i = part.Submeshes[s].SubsetIndex;
                                    i < part.Submeshes[s].SubsetIndex +
                                    part.Submeshes[s].SubsetCount; i++)
                                {
                                    triangleList.AddRange(createTriangleList(
                                          part.triangleStripData,
                                          part.Subsets[i].FaceStart,
                                          part.Subsets[i].FaceLength));
                                }
                            }*/
                            triangleList.AddRange(createTriangleList(
                                          part.triangleStripData,
                                          0,
                                          part.totalFaceCount));

                            // Write our face data
                            io.Out.Write(triangleList.Count / 3);
                            for (int f = 0; f < triangleList.Count; f += 3)
                            {
                                io.Out.Write(triangleList[f]);
                                io.Out.Write(triangleList[f + 1]);
                                io.Out.Write(triangleList[f + 2]);
                            }
                        }
                    }
                }
            }

            // Close our new file
            io.Close();
        }

        public static void WriteTxt(string FilePath, ModelInfo Mi, Permutation.LOD LOD)
        {
            // Create a stream writer
            StreamWriter sw = new StreamWriter(FilePath + Mi.Name + ".txt", false, Encoding.ASCII);

            // Write out our regions
            sw.WriteLine("Region Count: {0}", Mi.Regions.Count);
            for (int x = 0; x < Mi.Regions.Count; x++)
            {
                // Write our region name
                sw.WriteLine("Region Name: {0}", Mi.Regions[x].Name);

                // Write our perms
                sw.WriteLine("Permutations Count: {0}", Mi.Regions[x].Permutations.Count);
                for (int y = 0; y < Mi.Regions[x].Permutations.Count; y++)
                {
                    // Write our perm name
                    sw.WriteLine("Permutation Name: {0}", Mi.Regions[x].Permutations[y].Name);

                    // Get our model part from this perms LOD
                    ModelPart part = Mi.ModelParts[Mi.Regions[x].Permutations[y].LODs[(int)LOD]];

                    // Write out our vertex format
                    sw.WriteLine("Vertex Format: {0}", part.VertexFormat);

                    // Write out our submeshs
                    sw.WriteLine("Submesh Count: {0}", part.Submeshes.Count);
                    for (int z = 0; z < part.Submeshes.Count; z++)
                    {
                        // Write our vertex data
                        sw.WriteLine("Vertex Count: {0}", part.totalVertexCount);
                        for (int v = 0; v < part.totalVertexCount; v++)
                        {
                            sw.Write("{0} {1} {2} {3} {4}",
                            part.VertexData[v].X,
                            part.VertexData[v].Y,
                            part.VertexData[v].Z,
                            part.VertexData[v].textureU,
                            part.VertexData[v].textureV);

                            // If we have bone id's and skin weights
                            if (part.VertexFormat == 2)
                            {
                                sw.Write(" {0} {1} {2} {3} {4} {5} {6} {7}",
                                part.VertexData[v].boneID1,
                                part.VertexData[v].boneID2,
                                part.VertexData[v].boneID3,
                                part.VertexData[v].boneID4,
                                part.VertexData[v].VertWeight1,
                                part.VertexData[v].VertWeight2,
                                part.VertexData[v].VertWeight3,
                                part.VertexData[v].VertWeight4);
                            }

                            sw.Write("\r\n");
                        }

                        // Lets create our triangle list first
                        List<ushort> triangleList = new List<ushort>();
                        for (int s = 0; s < part.Submeshes.Count; s++)
                        {
                            for (int i = part.Submeshes[s].SubsetIndex;
                                i < part.Submeshes[s].SubsetIndex +
                                part.Submeshes[s].SubsetCount; i++)
                            {
                                triangleList.AddRange(createTriangleList(
                                      part.triangleStripData,
                                      part.Subsets[i].FaceStart,
                                      part.Subsets[i].FaceLength));
                            }
                        }

                        // Write our face data
                        sw.WriteLine("Face Count: {0}", triangleList.Count / 3);
                        for (int f = 0; f < triangleList.Count; f += 3)
                        {
                            sw.WriteLine("{0} {1} {2}",
                            triangleList[f],
                            triangleList[f + 1],
                            triangleList[f + 2]);
                        }
                        sw.WriteLine();
                    }
                    sw.WriteLine();
                }
                sw.WriteLine();
            }
            sw.Close();
        }
    }
}