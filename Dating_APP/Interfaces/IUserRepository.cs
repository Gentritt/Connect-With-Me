using Dating_APP.Dtos;
using Dating_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Interfaces
{
   public interface IUserRepository
	{
		void Update(AppUser user);
		Task<bool> SaveAllAsync();
		Task<IEnumerable<AppUser>> GetUsersAsync();
		Task<AppUser> GetUserByIdAsync(int id);
		Task<AppUser> GetUserByUsernameAsync(string username);

		Task<IEnumerable<MemberDto>> GetMembersAsync();
		Task<MemberDto> GetMemberAsync(string username);

	}
}
