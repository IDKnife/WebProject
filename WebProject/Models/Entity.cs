﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public Entity(int id)
        {
            Id = id;
        }
    }
}
