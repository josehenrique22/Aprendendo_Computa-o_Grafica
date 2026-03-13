using OpenTK.Graphics.OpenGL4;

namespace OpenTKTemplate.src.Shaders;

public class ShaderPreCompilation
{
    const string VERTEX_SHADER_PATH = "src/Shaders/Shader.vert";
    const string FRAGMENT_SHADER_PATH = "src/Shaders/Shader.vert";


    int _handle;

    public ShaderPreCompilation(string _vertexPath, string _fragmentPath)
    {
        string vertexShaderSource = File.ReadAllText(_vertexPath);
        string fragmentShaderSource = File.ReadAllText(_fragmentPath);

        int _vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(_vertexShader, vertexShaderSource);

        int _fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(_fragmentShader, fragmentShaderSource);

        GL.CompileShader(_vertexShader);

        int success;
        GL.GetShader(_vertexShader, ShaderParameter.CompileStatus, out success);
        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(_vertexShader);
            Console.WriteLine($"Error in compile the shader: {infoLog}");
        }

        GL.CompileShader(_fragmentShader);

        GL.GetShader(_vertexShader, ShaderParameter.CompileStatus, out success);
        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(_fragmentShader);
            Console.WriteLine($"Error in compile the shader: {infoLog}");
        }
        
        _handle = GL.CreateProgram();

        GL.AttachShader(_handle, _vertexShader);
        GL.AttachShader(_handle, _fragmentShader);

        GL.LinkProgram(_handle);

        GL.GetProgram(_handle, GetProgramParameterName.LinkStatus, out success);
        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(_fragmentShader);
            Console.WriteLine($"Error in compile Handle: {infoLog}");
        }

        GL.DetachShader(_handle, _vertexShader);
        GL.DetachShader(_handle, _fragmentShader);
        GL.DeleteShader(_vertexShader);
        GL.DeleteShader(_fragmentShader);
    }

    public void Use()
    {
        GL.UseProgram(_handle);
    }

    private bool _disposedValue = false;
    protected virtual void Dispose(bool _disposing)
    {
        if (!_disposedValue)
        {
            GL.DeleteProgram(_handle);

            _disposedValue = true;
        }
    }

    ~ShaderPreCompilation()
    {
        if (_disposedValue == false)
        {
            Console.WriteLine("CPU resource Leak!, Did you forget to call Dispose()?");
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
