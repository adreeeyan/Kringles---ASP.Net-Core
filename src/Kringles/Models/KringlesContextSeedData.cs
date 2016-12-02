using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kringles.Models
{
    public class KringlesContextSeedData
    {
        private KringlesContext _context;
        private UserManager<KringlesUser> _userManager;

        public KringlesContextSeedData(KringlesContext context, UserManager<KringlesUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedData()
        {
            if (await _userManager.FindByEmailAsync("user1@test.com") == null)
            {
                SeedUsers();
            }

            if (!_context.Kringles.Any())
            {
                SeedKringles();
            }
            
            await _context.SaveChangesAsync();
        }

        private async void SeedUsers()
        {
            var user1 = new KringlesUser()
            {
                UserName = "user1",
                Email = "user1@test.com"
            };

            await _userManager.CreateAsync(user1, "P@ssw0rd!");

            var user2 = new KringlesUser()
            {
                UserName = "user2",
                Email = "user2@test.com"
            };

            await _userManager.CreateAsync(user2, "P@ssw0rd!");

            var user3 = new KringlesUser()
            {
                UserName = "user3",
                Email = "user3@test.com"
            };

            await _userManager.CreateAsync(user3, "P@ssw0rd!");
        }

        private void SeedKringles()
        {
            var firstKringle = new Kringle()
            {
                Name = "The first payment",
                Date = DateTime.UtcNow,
                Participants = new List<Participant>()
                {
                    new Participant() { User = "user1", Wishlist = "Something something" },
                    new Participant() { User = "user2", Wishlist = "Something something 2" },
                    new Participant() { User = "user3", Wishlist = "Something something 3" }
                }
            };

            _context.Kringles.Add(firstKringle);
            _context.Participants.AddRange(firstKringle.Participants);

            var secondKringle = new Kringle()
            {
                Name = "The second payment",
                Date = DateTime.UtcNow,
                Participants = new List<Participant>()
                {
                    new Participant() { User = "user2", Wishlist = "Something something 2" },
                    new Participant() { User = "user3", Wishlist = "Something something 3" }
                }
            };

            _context.Kringles.Add(secondKringle);
            _context.Participants.AddRange(secondKringle.Participants);
        }
    }
}
