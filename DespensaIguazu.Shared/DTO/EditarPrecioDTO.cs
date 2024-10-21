﻿using DespensaIguazu.BD.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespensaIguazu.Shared.DTO
{
    public class EditarPrecioDTO : EntityBase
    {
        [Required(ErrorMessage = "El precio del producto es obligatorio")]
        public decimal Precio { get; set; }
    }
}