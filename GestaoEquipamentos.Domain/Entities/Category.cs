using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoEquipamentos.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
