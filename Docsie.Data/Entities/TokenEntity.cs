using Docsie.Data.Entities.Base;

namespace Docsie.Data.Entities
{
    public class TokenEntity: Entity
    {
        public string TenantId { get; set; }
        public string Token { get; set; }
    }
}
