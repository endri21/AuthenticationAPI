using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;


namespace eProtokoll.Core.Authentication
{
    public  static class JwtParser
    {

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims =new List<Claim>();
            var payLoad = jwt.Split('.')[1];//0=> head info, 1 paiload info, 2 validation info
            var jsonBytes = ParseBase64WithoutPadding(payLoad);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            ExtractRolesFromJwt(claims, keyValuePairs);
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }


        private static void ExtractRolesFromJwt(List<Claim> claims, Dictionary<string, object> keyValuePairs)
        {
            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);
            if(roles is not null)
            {
                var parsedRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');
                if (parsedRoles.Length >1)
                {
                    foreach (var role in parsedRoles )
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Trim('"')));
                    }

                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));

                }
                keyValuePairs.Remove(ClaimTypes.Role);
            }
        }
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 = $"{base64}==";
                    break;
                case 3:
                    base64 = $"{base64 }=";
                    break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
