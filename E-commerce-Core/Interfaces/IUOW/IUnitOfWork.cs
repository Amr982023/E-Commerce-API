using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Interfaces.Unit_Of_Work_Interface
{
    public interface IUnitOfWork : IDisposable
    {
        //another interfaces


        int Complete();       

    }
}
