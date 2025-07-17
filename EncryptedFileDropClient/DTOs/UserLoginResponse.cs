using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EncryptedFileDropClient.DTOs
{
    public class UserLoginResponse
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
    }
}
