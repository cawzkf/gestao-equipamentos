using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoEquipamentos.Domain.Entities
{
    public class Supplier
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string ContactEmail { get; set; }
    }
}
