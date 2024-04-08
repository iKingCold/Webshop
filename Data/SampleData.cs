using Microsoft.EntityFrameworkCore;
using Webshop.Models;

namespace Webshop.Data
{
    public class SampleData
    {
        public static void Create(AppDbContext database)
        {
            // If there are no fake accounts, add some.
            string fakeIssuer = "https://example.com";
            if (!database.Accounts.Any(a => a.OpenIDIssuer == fakeIssuer))
            {
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "1111111111",
                    Name = "Brad"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "2222222222",
                    Name = "Angelina"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "3333333333",
                    Name = "Will"
                });
            }

            // Seed Categories
            if (!database.Categories.Any())
            {
                database.Categories.AddRange(
                   new Category { Name = "Sjunkande" },
                   new Category { Name = "Jigg" },
                   new Category { Name = "Flytande" },
                   new Category { Name = "Mjukplast" },
                   new Category { Name = "Popper" },
                   new Category { Name = "Spinnerbait" },
                   new Category { Name = "Hoppande" },
                   new Category { Name = "Walk the dog" }
                );

                database.SaveChanges();
            }

            //Seed database if there's no products
            if (!database.Products.Any())
            {
                var sjunkandeCategory = database.Categories.Single(c => c.Name == "Sjunkande");
                var jiggCategory = database.Categories.Single(c => c.Name == "Jigg");
                var flytandeCategory = database.Categories.Single(c => c.Name == "Flytande");
                var mjukplastCategory = database.Categories.Single(c => c.Name == "Mjukplast");
                var popperCategory = database.Categories.Single(c => c.Name == "Popper");
                var spinnerbaitCategory = database.Categories.Single(c => c.Name == "Spinnerbait");
                var hoppandeCategory = database.Categories.Single(c => c.Name == "Hoppande");
                var walkTheDogCategory = database.Categories.Single(c => c.Name == "Walk the dog");

                database.Products.AddRange(
                    new Product { Name = "Storm WildEye Swim Shad", Image = "storm_wildeye_swim_shad.jpg", Price = 6.79m, Category = jiggCategory, Description = "Högkvalitativt jiggbete med naturtrogna rörelser som lockar till sig predatorfiskar." },
                    new Product { Name = "Savage Gear 3D Craw", Image = "savage_gear_3d_craw.jpg", Price = 8.29m, Category = mjukplastCategory, Description = "Livaktigt formgivet mjukplastbete som efterliknar en kräfta." },
                    new Product { Name = "Megabass Vision Oneten", Image = "megabass_vision_oneten.jpg", Price = 24.99m, Category = sjunkandeCategory, Description = "Premium wobbler med överlägsen kvalitet och rörelsemönster." },
                    new Product { Name = "Strike King Red Eye Shad", Image = "strike_king_red_eye_shad.jpg", Price = 6.99m, Category = sjunkandeCategory, Description = "Effektivt lipless crankbete för aggressivt fiske." },
                    new Product { Name = "Zoom Super Fluke", Image = "zoom_super_fluke.jpg", Price = 4.99m, Category = mjukplastCategory, Description = "Mjukplastbete med realistiska rörelser och attraktiva färger." },
                    new Product { Name = "Rapala Original Floating", Image = "rapala_original_floating.jpg", Price = 8.49m, Category = flytandeCategory, Description = "Klassiskt wobblerbete med enkelt och effektivt design." },
                    new Product { Name = "Megabass PopMax", Image = "megabass_popmax.jpg", Price = 29.99m, Category = popperCategory, Description = "Högkvalitativt popperbete med unika ljud och rörelser." },
                    new Product { Name = "Booyah Pond Magic", Image = "booyah_pond_magic.jpg", Price = 3.99m, Category = spinnerbaitCategory, Description = "Små och effektiva spinnerbaits för kastning i mindre vatten." },
                    new Product { Name = "Rebel Pop-R", Image = "rebel_pop_r.jpg", Price = 5.49m, Category = popperCategory, Description = "Klassiskt popperbete med högkvalitativa komponenter och utmärkta rörelser." },
                    new Product { Name = "Keitech Swing Impact", Image = "keitech_swing_impact.jpg", Price = 6.99m, Category = mjukplastCategory, Description = "Mjukplastbete med realistiska rörelser och hög attraktion för fisk." },
                    new Product { Name = "Spro Bronzeye Frog", Image = "spro_bronzeye_frog.jpg", Price = 9.79m, Category = hoppandeCategory, Description = "Effektivt hoppande bete med realistiskt utseende för rovfiskfiske." },
                    new Product { Name = "Z-Man ChatterBait", Image = "zman_chatterbait.jpg", Price = 7.29m, Category = jiggCategory, Description = "Innovativt jiggbete med blad som skapar lockande vibrationer under vattnet." },
                    new Product { Name = "Megabass Dark Sleeper", Image = "megabass_dark_sleeper.jpg", Price = 6.99m, Category = jiggCategory, Description = "Kompakt jiggbete med realistisk form som fungerar utmärkt för bottenfiske." },
                    new Product { Name = "Yamamoto Senko", Image = "yamamoto_senko.jpg", Price = 6.49m, Category = mjukplastCategory, Description = "Enkel men effektiv design av mjukplastbete för bassfiske." },
                    new Product { Name = "LIVETARGET Hollow Body Frog", Image = "livetarget_hollow_body_frog.jpg", Price = 9.99m, Category = hoppandeCategory, Description = "Realistiskt designat hoppande bete med ihåligt kropp för bättre krokning." },
                    new Product { Name = "Heddon Super Spook Jr.", Image = "heddon_super_spook_jr.jpg", Price = 7.99m, Category = walkTheDogCategory, Description = "Klassiskt ytbete med walk-the-dog-rörelse som attraherar rovfisk." },
                    new Product { Name = "Gary Yamamoto Fat Baby Craw", Image = "gary_yamamoto_fat_baby_craw.jpg", Price = 6.49m, Category = mjukplastCategory, Description = "Kompakt mjukplastbete med realistiskt kräftutseende och attraktiva rörelser." },
                    new Product { Name = "Strike King KVD Square Bill", Image = "strike_king_kvd_square_bill.jpg", Price = 6.99m, Category = flytandeCategory, Description = "KVD-signaturklenodium med effektiv fiskning i grunda områden." },
                    new Product { Name = "Zoom Brush Hog", Image = "zoom_brush_hog.jpg", Price = 4.99m, Category = mjukplastCategory, Description = "Stort mjukplastbete med attraktiv rörelse för basfiske." },
                    new Product { Name = "Booyah Boo Jig", Image = "booyah_boo_jig.jpg", Price = 4.79m, Category = jiggCategory, Description = "Jiggbete med realistiskt huvud och mångsidigt användningsområde." },
                    new Product { Name = "Savage Gear 3D Bluegill", Image = "savage_gear_3d_bluegill.jpg", Price = 12.99m, Category = mjukplastCategory, Description = "Realistiskt designad mjukplastbete som imiterar en blågädda." },
                    new Product { Name = "Strike King Bitsy Bug Jig", Image = "strike_king_bitsy_bug_jig.jpg", Price = 2.99m, Category = jiggCategory, Description = "Små jiggar med realistiskt utseende för effektivt fiske i mindre vatten." },
                    new Product { Name = "Rapala Skitter V", Image = "rapala_skitter_v.jpg", Price = 8.99m, Category = flytandeCategory, Description = "Ytbete med realistiska rörelser och attraktiva färger för ytvattenfiske." },
                    new Product { Name = "Z-Man TRD CrawZ", Image = "zman_trd_crawz.jpg", Price = 3.49m, Category = mjukplastCategory, Description = "Kompakt mjukplastbete med realistiskt kräftutseende för basfiske." },
                    new Product { Name = "Strike King KVD Jerkbait", Image = "strike_king_kvd_jerkbait.jpg", Price = 7.49m, Category = sjunkandeCategory, Description = "KVD-signaturklenodium med lockande rörelser för jerkbaitfiske." },
                    new Product { Name = "Keitech Easy Shiner", Image = "keitech_easy_shiner.jpg", Price = 5.99m, Category = mjukplastCategory, Description = "Mjukplastbete med lockande rörelser och realistiskt utseende för olika fiskarter." },
                    new Product { Name = "Megabass Magdraft", Image = "megabass_magdraft.jpg", Price = 19.99m, Category = flytandeCategory, Description = "Stor mjukplastjigg med realistiska rörelser för stora rovfiskar." },
                    new Product { Name = "Booyah Flex II", Image = "booyah_flex_ii.jpg", Price = 6.79m, Category = spinnerbaitCategory, Description = "Smidig och flexibel spinnerbait för effektivt fiske i olika förhållanden." }
                );
            }
            database.SaveChanges();

        }
    }
}
