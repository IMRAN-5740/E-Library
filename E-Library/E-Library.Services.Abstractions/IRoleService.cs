using E_Library.Services.Abstractions.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Services.Abstractions
{
    public interface IRoleService:IService<IdentityRole>
    {
    }
}
