using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTKTemplate.src.Shaders;

namespace OpenTkGraphic
{
    public class Game : GameWindow
    {
        const string VERTEX_SHADER_PATH = "C:/C#/GraphicsProgram/OpenTKTemplate/src/Shaders/Shader.vert";
        const string FRAGMENT_SHADER_PATH = "C:/C#/GraphicsProgram/OpenTKTemplate/src/Shaders/Shader.frag";

        public Game(int width = 800, int height = 600, string title = "BasicOpenTK")
            : base(
                GameWindowSettings.Default,
                new NativeWindowSettings()
                {
                    ClientSize = (width, height),
                    Title = title,
                })
        {
            this.CenterWindow();
        }


        private int _vertexBufferObject;
        private int _vertexArrayObject;

        // X Y Z
        private float[] _vertices =
        {
                -0.5f, -0.5f, 0.0f,
                0.5f, -0.5f, 0.0f,
                0.0f, 0.5f, 0.0f
        };

        private ShaderPreCompilation? _shader;

        protected override void OnLoad()
        {
            base.OnLoad();

            _shader = new ShaderPreCompilation(
            VERTEX_SHADER_PATH,
            FRAGMENT_SHADER_PATH);

            GL.ClearColor(0.0f, 0.2f, 0.8f, 0.8f);

            _vertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            _shader.Use();
        }


        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);


            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            _shader.Dispose();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

        }

    }
}