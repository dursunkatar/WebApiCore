﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class RefreshTokenResponseDto
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
