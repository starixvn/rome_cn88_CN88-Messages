namespace Alpha.Messages.Site.Helpers
{
    public interface BrowserHelper
    {
        bool IsMobile();

        bool IsTablet();

        BrowserInfo GetBrowserInfo();
    }
}