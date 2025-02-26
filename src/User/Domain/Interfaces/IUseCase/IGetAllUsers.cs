﻿using Domain.Interfaces.IDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IUseCase
{
    public interface IGetAllUsers
    {
        Task<IEnumerable<IUserDtoOut>> GetAll(bool onlyActive);
    }
}
