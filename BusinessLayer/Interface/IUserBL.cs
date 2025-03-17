
using BusinessLayer.Service;
using ModelLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public bool RegisterUser(RegisterUser request);
        public string? LoginUser(UserLogin request);
    }
}