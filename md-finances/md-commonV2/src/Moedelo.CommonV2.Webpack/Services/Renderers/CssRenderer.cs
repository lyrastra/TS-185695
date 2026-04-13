using Moedelo.CommonV2.Webpack.Resources;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.CommonV2.Webpack.Services.Renderers
{
    [InjectAsSingleton]
    public class CssRenderer : RendererAbstract, ICssRenderer
    {
        public CssRenderer() : base(RenderPatterns.Css)
        {
        }
    }
}