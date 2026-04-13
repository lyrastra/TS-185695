using Moedelo.CommonV2.Webpack.Resources;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.CommonV2.Webpack.Services.Renderers
{
    [InjectAsSingleton]
    public class JavaScriptRenderer : RendererAbstract, IJavaScriptRenderer
    {
        public JavaScriptRenderer() : base(RenderPatterns.Js)
        {
        }
    }
}
