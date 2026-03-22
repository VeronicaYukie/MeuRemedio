using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AppLembreteMedicacao.Helpers
{
public static class SecurityHelper
 
{ public static string GerarHash(string senha)
 
{
  using var sha256 = SHA256.Create();
  var bytes = Encoding.UTF8.GetBytes(senha);
  var hash = sha256.ComputeHash(bytes);
  return Convert.ToBase64String(hash);

        }
    }
}
