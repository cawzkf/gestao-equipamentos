using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoEquipamentos.Domain.Entities
{
    internal class User
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string PasswordHash { get; set; }
    }
}
