using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrdering.Shared.Dto
{
    public class UserLoginResponseDto
    {
        public String ApiToken { get; set; }

        public UserDto User { get; set; }
    }
}
