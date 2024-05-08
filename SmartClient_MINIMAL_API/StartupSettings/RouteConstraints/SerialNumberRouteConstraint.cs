using Microsoft.AspNetCore.Routing.Constraints;
using System.Text.RegularExpressions;

namespace SmartClient.MinimalApi.StartupSettings.RouteConstraints
{
    public class SerialNumberRouteConstraint : RegexRouteConstraint
    {
        public SerialNumberRouteConstraint() : base(@"[\s\S]*")
        {
        }
    }
}
