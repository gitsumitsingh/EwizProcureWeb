namespace EwizProcureWeb.Interfaces
{
    public interface IGlobalSettings
    {
        string IDKey { get; }

        string GetCDNLink();

        string GetAssetsFolder();

        string GetElasticServerUrl();

        string GetNodeServerUrl();

        string GetLayoutView();
    }
}
