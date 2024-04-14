using System.Web.Mvc;
using System.Web.Routing;

public class RouteConfig
{
    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        // Rota para VerificarBeneficiario
        routes.MapRoute(
            name: "VerificarBeneficiario",
            url: "Beneficiario/VerificarBeneficiario/{idCliente}",
            defaults: new { controller = "Beneficiario", action = "VerificarBeneficiario", idCliente = UrlParameter.Optional }
        );

        // Rota padrão
        routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        );
    }
}
