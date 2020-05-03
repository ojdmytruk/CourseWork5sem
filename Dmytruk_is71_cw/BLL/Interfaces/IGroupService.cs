﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IGroupService
    {
        GroupDTO GetGroup(int id);
        IEnumerable<GroupDTO> Get();
        void AddGroup(GroupDTO groupDTO);
        void DeleteGroup(int id);
        void EditGroup(GroupDTO groupDTO);
        void Dispose();
    }
}
