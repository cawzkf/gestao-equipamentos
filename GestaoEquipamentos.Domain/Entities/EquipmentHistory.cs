using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoEquipamentos.Domain.Entities
{
    public class EquipmentHistory
    {
        public int Id { get; set; }

        public int EquipmentId { get; set; }

        public required string Action { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
