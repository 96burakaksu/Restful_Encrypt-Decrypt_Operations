using Core;
using Core.GeneralResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoService.Interfaces
{
    public interface ICryptoService
    {
        DataResult<byte[]> Encrypt(CriptoDataRequest request);
        DataResult<byte[]> Decrypt(CriptoDataRequest request);
    }
}
