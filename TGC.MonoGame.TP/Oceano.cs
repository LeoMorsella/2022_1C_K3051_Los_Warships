using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP
{
    public class Oceano
    {
        private readonly Effect Effect;
        private float m_scaleXZ = 1;
        private readonly Texture2D oceanTexture;
        private VertexBuffer vbPlane;

        private int Length;
        private int Width;

        public Oceano(int length, int width, int scale, Vector3 position, GraphicsDevice graphicsDevice, Effect effect, Texture2D textura)
        {
            //Shader
            Effect = effect;

            Length = length;
            Width = width;

            // cargo el plano
            LoadPlane(graphicsDevice, scale, position);
            
            oceanTexture = textura;
        }

        public Vector3 Center { get; private set; }

        /// <summary>
        ///     Renderiza el oceano
        /// </summary>
        public void Draw(Matrix World, Matrix View, Matrix Projection, float time)
        {
            var graphicsDevice = Effect.GraphicsDevice;

            Effect.Parameters["oceanTexture"].SetValue(oceanTexture);
            Effect.Parameters["World"].SetValue(World);
            Effect.Parameters["View"].SetValue(View);
            Effect.Parameters["Projection"].SetValue(Projection);
            /* Effect.Parameters["DiffuseColor"].SetValue(Color.Blue.ToVector3()); */
            Effect.Parameters["Time"].SetValue(time);

            graphicsDevice.SetVertexBuffer(vbPlane);

            //Render con shader
            foreach (var pass in Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vbPlane.VertexCount / 3);
            }
        }

        /// <summary>
        ///     Crea la malla del plano
        /// </summary>
        /// <param name="scaleXZ">Escala para los ejes X y Z</param>
        /// <param name="center">Centro de la malla del plano</param>
        public void LoadPlane(GraphicsDevice graphicsDevice, float scaleXZ, Vector3 center)
        {
            Center = center;

            m_scaleXZ = scaleXZ;

            // texture scale
            float tx_scale = 1; // 50f;

            //Cargar vertices
            var totalVertices = 2 * 3 * (Width - 1) * (Length - 1);
            var dataIdx = 0;
            var data = new VertexPositionNormalTexture[totalVertices];

            center.X = center.X * scaleXZ - Width / 2f * scaleXZ;
            center.Z = center.Z * scaleXZ - Length / 2f * scaleXZ;

            var N = new Vector3[Width, Length];
            for (var i = 0; i < Width - 1; i++)
            for (var j = 0; j < Length - 1; j++)
            {
                var v1 = new Vector3(center.X + i * scaleXZ, center.Y, center.Z + j * scaleXZ);
                var v2 = new Vector3(center.X + i * scaleXZ, center.Y, center.Z + (j + 1) * scaleXZ);
                var v3 = new Vector3(center.X + (i + 1) * scaleXZ, center.Y, center.Z + j * scaleXZ);
                N[i, j] = Vector3.Normalize(Vector3.Cross(v2 - v1, v3 - v1));
            }

            for (var i = 0; i < Width - 1; i++)
            for (var j = 0; j < Length - 1; j++)
            {
                //Vertices
                var v1 = new Vector3(center.X + i * scaleXZ, center.Y, center.Z + j * scaleXZ);
                var v2 = new Vector3(center.X + i * scaleXZ, center.Y, center.Z + (j + 1) * scaleXZ);
                var v3 = new Vector3(center.X + (i + 1) * scaleXZ, center.Y, center.Z + j * scaleXZ);
                var v4 = new Vector3(center.X + (i + 1) * scaleXZ, center.Y, center.Z + (j + 1) * scaleXZ);

                //Coordendas de textura
                var t1 = new Vector2(i / (float) Width, j / (float) Length) * tx_scale;
                var t2 = new Vector2(i / (float) Width, (j + 1) / (float) Length) * tx_scale;
                var t3 = new Vector2((i + 1) / (float) Width, j / (float) Length) * tx_scale;
                var t4 = new Vector2((i + 1) / (float) Width, (j + 1) / (float) Length) * tx_scale;

                //Cargar triangulo 1
                data[dataIdx] = new VertexPositionNormalTexture(v1, N[i, j], t1);
                data[dataIdx + 1] = new VertexPositionNormalTexture(v2, N[i, j + 1], t2);
                data[dataIdx + 2] = new VertexPositionNormalTexture(v4, N[i + 1, j + 1], t4);

                //Cargar triangulo 2
                data[dataIdx + 3] = new VertexPositionNormalTexture(v1, N[i, j], t1);
                data[dataIdx + 4] = new VertexPositionNormalTexture(v4, N[i + 1, j + 1], t4);
                data[dataIdx + 5] = new VertexPositionNormalTexture(v3, N[i + 1, j], t3);

                dataIdx += 6;
            }

            //Crear vertexBuffer
            vbPlane = new VertexBuffer(graphicsDevice, VertexPositionNormalTexture.VertexDeclaration, totalVertices, BufferUsage.WriteOnly);
            vbPlane.SetData(data);
        }
    }
}