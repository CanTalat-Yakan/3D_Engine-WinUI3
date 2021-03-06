using SharpDX.D3DCompiler;
using UWP3DEngine.Assets.Engine.Data;
using UWP3DEngine.Assets.Engine.Helper;
using UWP3DEngine.Assets.Engine.Utilities;
using D3D11 = SharpDX.Direct3D11;
using DXGI = SharpDX.DXGI;

namespace UWP3DEngine.Assets.Engine.Components
{
    internal class CMaterial
    {
        CRenderer m_d3d;

        D3D11.VertexShader m_vertexShader;
        D3D11.PixelShader m_pixelShader;
        D3D11.GeometryShader m_geometryShader;

        D3D11.InputLayout m_inputLayout;

        D3D11.ShaderResourceView m_resourceView;
        D3D11.SamplerState m_sampler;
        D3D11.Buffer m_model;

        internal CMaterial(string _shaderFileName, string _imageFileName, bool _includeGeometryShader = false)
        {
            #region //Get Instances
            m_d3d = CRenderer.Instance;
            #endregion

            #region //Create InputLayout
            var inputElements = new D3D11.InputElement[] {
                new D3D11.InputElement("POSITION", 0, DXGI.Format.R32G32B32_Float, 0, 0),
                new D3D11.InputElement("TEXCOORD", 0, DXGI.Format.R32G32_Float, D3D11.InputElement.AppendAligned, 0),
                new D3D11.InputElement("NORMAL", 0, DXGI.Format.R32G32B32_Float, D3D11.InputElement.AppendAligned, 0)};
            #endregion

            #region //Create VertexShader
            CompilationResult vsResult;
            using (vsResult = ShaderBytecode.CompileFromFile(_shaderFileName, "VS", "vs_4_0", ShaderFlags.None))
            {
                m_vertexShader = new D3D11.VertexShader(m_d3d.m_Device, vsResult.Bytecode.Data);
                m_inputLayout = new D3D11.InputLayout(m_d3d.m_Device, vsResult.Bytecode, inputElements);
            }
            #endregion

            #region //Create PixelShader 
            using (var psResult = ShaderBytecode.CompileFromFile(_shaderFileName, "PS", "ps_4_0", ShaderFlags.None))
                m_pixelShader = new D3D11.PixelShader(m_d3d.m_Device, psResult.Bytecode.Data);
            #endregion

            #region //Create GeometryShader
            if (_includeGeometryShader)
                using (var psResult = ShaderBytecode.CompileFromFile(_shaderFileName, "GS", "gs_4_0", ShaderFlags.None))
                    m_geometryShader = new D3D11.GeometryShader(m_d3d.m_Device, psResult.Bytecode.Data);
            #endregion

            #region //Create ConstantBuffers for Model
            SPerModelConstantBuffer cbModel = new SPerModelConstantBuffer();

            D3D11.BufferDescription bufferDescription = new D3D11.BufferDescription
            {
                BindFlags = D3D11.BindFlags.ConstantBuffer,
                CpuAccessFlags = D3D11.CpuAccessFlags.Write,
                Usage = D3D11.ResourceUsage.Dynamic,
            };

            m_model = D3D11.Buffer.Create(m_d3d.m_Device, D3D11.BindFlags.ConstantBuffer, ref cbModel);
            #endregion

            #region //Create Texture and Sampler
            var texture = CImgLoader.CreateTexture2DFromBitmap(m_d3d.m_Device, CImgLoader.LoadBitmap(new SharpDX.WIC.ImagingFactory2(), _imageFileName));
            m_resourceView = new D3D11.ShaderResourceView(m_d3d.m_Device, texture);

            D3D11.SamplerStateDescription samplerStateDescription = new D3D11.SamplerStateDescription
            {
                Filter = D3D11.Filter.Anisotropic,
                AddressU = D3D11.TextureAddressMode.Clamp,
                AddressV = D3D11.TextureAddressMode.Clamp,
                AddressW = D3D11.TextureAddressMode.Clamp,
                ComparisonFunction = D3D11.Comparison.Always,
                MaximumAnisotropy = 16,
                MinimumLod = 0,
                MaximumLod = float.MaxValue,
            };

            m_sampler = new D3D11.SamplerState(m_d3d.m_Device, samplerStateDescription);
            #endregion
        }

        internal void Render(SPerModelConstantBuffer _data)
        {
            m_d3d.m_DeviceContext.InputAssembler.InputLayout = m_inputLayout;
            m_d3d.m_DeviceContext.VertexShader.Set(m_vertexShader);
            m_d3d.m_DeviceContext.PixelShader.Set(m_pixelShader);
            m_d3d.m_DeviceContext.GeometryShader.Set(m_geometryShader);

            m_d3d.m_DeviceContext.UpdateSubresource(ref _data, m_model);
            m_d3d.m_DeviceContext.VertexShader.SetConstantBuffer(1, m_model);

            m_d3d.m_DeviceContext.PixelShader.SetShaderResource(0, m_resourceView);
            m_d3d.m_DeviceContext.PixelShader.SetSampler(0, m_sampler);
        }
    }
}
