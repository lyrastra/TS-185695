namespace Moedelo.CommonV2.Webpack.Services.Renderers
{
    public abstract class RendererAbstract : IRenderer
    {
        protected readonly string Pattern;

        protected RendererAbstract(string pattern)
        {
            Pattern = pattern;
        }

        public string Render(string bundle)
        {
            return string.Format(Pattern, bundle);
        }
    }
}