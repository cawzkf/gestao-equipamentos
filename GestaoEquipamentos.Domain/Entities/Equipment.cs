using GestaoEquipamentos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoEquipamentos.Domain.Entities
{
    public class Equipment
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string SerialNumber { get; set; }

        public required string Model { get; set; }

        public DateTime PurchaseDate { get; set; }

        public EquipmentStatusEnum Status { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int SupplierId { get; set; }

        public Supplier? Supplier { get; set; }
    }
}
