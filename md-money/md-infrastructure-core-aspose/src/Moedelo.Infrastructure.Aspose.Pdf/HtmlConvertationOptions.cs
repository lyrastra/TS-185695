namespace Moedelo.Infrastructure.Aspose.Pdf
{
    public class HtmlConvertationOptions
    {
        public bool IsRenderToSinglePage { get; }
        public bool IsResetPageMargins { get; }
        public bool IsOptimizeOutput { get; }

        public HtmlConvertationOptions()
        {
        }

        public HtmlConvertationOptions(bool isRenderToSinglePage, bool isResetPageMargins, bool isOptimizeOutput)
        {
            IsRenderToSinglePage = isRenderToSinglePage;
            IsResetPageMargins = isResetPageMargins;
            IsOptimizeOutput = isOptimizeOutput;
        }
    }
}
