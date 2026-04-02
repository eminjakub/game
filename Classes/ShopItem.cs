using System.Collections.Generic;

namespace game
{
    public class ShopItem
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public int StatBonus { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }

        public static List<ShopItem> GetShopItems()
        {
            return new List<ShopItem>
            {
                new ShopItem { Name = "Hůl", Price = 100, Type = "Weapon", StatBonus = 3, ImagePath = "./fotky/hul.png", Description = "Obyčejná hůl. Lepší než nic." },
                new ShopItem { Name = "Mačeta", Price = 150, Type = "Weapon", StatBonus = 5, ImagePath = "./fotky/macheta.png", Description = "Ostrá mačeta na politické protivníky." },
                new ShopItem { Name = "Pero", Price = 250, Type = "Weapon", StatBonus = 8, ImagePath = "./fotky/pero.png", Description = "Pero je mocnější než meč!" },
                new ShopItem { Name = "Kyvadlo", Price = 400, Type = "Weapon", StatBonus = 12, ImagePath = "./fotky/kyvadlo.png", Description = "Hypnotické kyvadlo. Zmatek zaručen." },
                new ShopItem { Name = "Cibule", Price = 30, Type = "Consumable", StatBonus = 10, ImagePath = "./fotky/cibule.png", Description = "Léčivá cibule. Obnoví 10 HP." },
                new ShopItem { Name = "Rybička", Price = 40, Type = "Consumable", StatBonus = 15, ImagePath = "./fotky/rybicka.png", Description = "Česká rybička. Obnoví 15 HP." },
                new ShopItem { Name = "Půllitr", Price = 50, Type = "Consumable", StatBonus = 20, ImagePath = "./fotky/pullitr.png", Description = "Plný půllitr. Obnoví 20 HP." },
                new ShopItem { Name = "Strojek", Price = 200, Type = "Armor", StatBonus = 5, ImagePath = "./fotky/strojek.png", Description = "Mechanická ochrana. +5 Armor." },
                new ShopItem { Name = "Pultík", Price = 300, Type = "Armor", StatBonus = 8, ImagePath = "./fotky/pultik.png", Description = "Neprůstřelný pultík. +8 Armor." },
            };
        }

        public static List<ShopItem> GetDealerItems()
        {
            return new List<ShopItem>
            {
                new ShopItem { Name = "Kouzelná houbička", Price = 20, Type = "Consumable", StatBonus = 25, ImagePath = "./fotky/cibule.png", Description = "\"Speciální\" houbička od Dealera. Obnoví 25 HP. Neptej se odkud jsou." },
                new ShopItem { Name = "Tajemný odvar", Price = 35, Type = "Consumable", StatBonus = 40, ImagePath = "./fotky/pullitr.png", Description = "Dealerova tajná receptura. Obnoví 40 HP. Záruky neposkytujeme." },
                new ShopItem { Name = "Rybička z mostu", Price = 15, Type = "Consumable", StatBonus = 12, ImagePath = "./fotky/rybicka.png", Description = "Rybička chycená přímo z Vltavy. Čerstvá (skoro). +12 HP." },
                new ShopItem { Name = "Politická klobása", Price = 55, Type = "Consumable", StatBonus = 50, ImagePath = "./fotky/pullitr.png", Description = "Speciální výběr z nejlepší části Prahy. +50 HP. 100% maso (pravděpodobně)." },
                new ShopItem { Name = "Dealerovo pero", Price = 180, Type = "Weapon", StatBonus = 6, ImagePath = "./fotky/pero.png", Description = "Podepisovací pero na falešné smlouvy. +6 Str. Ode mě nic neslyšels." },
                new ShopItem { Name = "Kontakty", Price = 500, Type = "Armor", StatBonus = 10, ImagePath = "./fotky/pultik.png", Description = "Kdo má kontakty, ten vládne. +10 Armor. Dealer ti zařídí cokoliv." },
            };
        }
    }
}
