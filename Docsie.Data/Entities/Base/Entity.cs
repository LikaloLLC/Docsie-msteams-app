using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Docsie.Data.Entities.Base
{
    public class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column(Order = 1)]
        public string Id { get; set; }
    }
}
