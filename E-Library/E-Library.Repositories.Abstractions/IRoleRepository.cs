﻿using E_Library.Repositories.Abstractions.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Repositories.Abstractions
{
    public interface IRoleRepository:IRepository<IdentityRole>
    {
    }
}
