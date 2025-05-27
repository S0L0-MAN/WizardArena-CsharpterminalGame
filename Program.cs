using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardApp
{
    internal class Program
    {
        class WizardClass
        {
            public string WizardName;
            public string WizardElement;
            public int HP;
            public int XP;
            public int MP;
            public SpellsClass[] slots = new SpellsClass[4];

            public WizardClass(string WizardName,string WizardElement,int HP,int XP,int MP, SpellsClass[] slots)
            {
                this.WizardName = WizardName;
                this.WizardElement = WizardElement;
                this.HP = HP;
                this.XP = XP;
                this.MP = MP;
                this.slots = slots;

            }

        }

        class SpellsClass
        {
            public string spellName;
            public string spellType;
            public int manaCost;
            public int DamageHeal;
            public int unlockXp;

            public SpellsClass(string spellName,string spellType,int manaCost,int DamageHeal,int unlockXp)
            {
                this.spellName = spellName;
                this.spellType = spellType;
                this.manaCost = manaCost;
                this.DamageHeal = DamageHeal;
                this.unlockXp = unlockXp;
            }

        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            SpellsClass[] SpellSlots = new SpellsClass[4];

            SpellsClass FireSpell = new SpellsClass("BomboClat", "Offensive", 50, 35, 15);
            SpellsClass WaterSpell = new SpellsClass("Drink", "Healing", 10, 20, 15);
            SpellsClass EarthSpell = new SpellsClass("Meteor Strike", "Offensive", 100, 100, 55);
            SpellsClass AirSpell = new SpellsClass("Fart", "Offensive", 50, 15, 15);
            SpellsClass NullSpell = new SpellsClass("Null", "Offensive", 50, 15, 15);

            SpellsClass[] SpellOptions = new SpellsClass[5] { FireSpell,WaterSpell,EarthSpell,AirSpell,NullSpell };

            

            WizardClass FireClass = new WizardClass("Ariel", "Fire", 100, 0, 0, SpellSlots);
            WizardClass EarthClass = new WizardClass("Taurus", "Earth", 100, 0, 0, SpellSlots);
            WizardClass WaterClass = new WizardClass("Pisces", "Water", 100, 0, 0, SpellSlots);
            WizardClass AirClass = new WizardClass("Libra", "Air", 100, 0, 0, SpellSlots);

            WizardClass[] wizards = new WizardClass[] { FireClass, WaterClass, EarthClass, AirClass };


            //Wizard Choosing function  
            WizardPrint op = WizardChoosing;
            int optionChoice = op(wizards);
            Console.Clear();
            WizardClass selectedCharacter = wizards[optionChoice];
            Random rnd = new Random();
            int opponentCode = rnd.Next(0, 3);
            WizardClass opponentWizard = wizards[opponentCode];


            Battle(wizards[optionChoice], wizards[opponentCode],opponentCode,optionChoice,SpellOptions);
            //spellChoosing(SpellOptions, wizards[optionChoice]);
        }

        private delegate int WizardPrint(WizardClass[] wizards);
        private static int WizardChoosing(WizardClass[] wizards)
        {
            Console.WriteLine("Choose Your Wizard");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(i + 1 + ":" + wizards[i].WizardName);
            }
            Console.WriteLine("Enter the number representing the wizard ");
            int optionChoice = Convert.ToInt32(Console.ReadLine());
            optionChoice = optionChoice - 1;
            if (optionChoice >= 0 && optionChoice <= 4)
            {
                return optionChoice;
                
            }

            else
            {
                return 000;
            }
        }
        private static void attack(WizardClass wizard,WizardClass opponent)
        {
            Console.WriteLine("Choose your attack");
            foreach (var item in wizard.slots)
            {
                if (item!=null)
                {
                    Console.WriteLine(item.spellName);
                }
                else
                {
                    Console.WriteLine("No Spells");
                }
            }
            int spellChoice = Convert.ToInt32(Console.ReadLine());
            spellChoice = spellChoice - 1;
            int opponentInit = opponent.HP;
            opponent.HP = opponent.HP - wizard.slots[spellChoice].DamageHeal;
            Console.WriteLine("Opponent HP"+opponentInit+"->"+opponent.HP);

        }
        private static void spellChoosing(SpellsClass[] spells,WizardClass wizard)
        {

            
            
            Console.WriteLine("Choose your spell");
                foreach (var item in spells)
                {
                    Console.WriteLine(item.spellName+"("+item.unlockXp+")");
                }
                int spellChoice = Convert.ToInt32(Console.ReadLine());
                spellChoice = spellChoice - 1;
                if (wizard.MP == spells[spellChoice].manaCost)
                {
                wizard.slots[0] = spells[spellChoice];
                }
            else
            {
                Console.WriteLine("Not enough Mana Points");
                wizard.slots[0] = spells[4];
            }


        } private static void OpspellChoosing(SpellsClass[] spells,WizardClass wizard)
        {
            Random rnd = new Random();
            wizard.slots[0] = spells[rnd.Next(0, 3)];
            
        }
        private static void pointDeduction(WizardClass wizard, WizardClass opponent, int opponentCode, int optionChoice)
        {
            if (wizard.WizardElement == "Fire" && opponent.WizardElement == "Air")
            {
                wizard.HP = wizard.HP - 20;
            }
            else if (wizard.WizardElement == "Air" && opponent.WizardElement == "Fire")
            {
                opponent.HP = opponent.HP - 20;
            }
            else if (opponentCode == optionChoice + 1)
            {
                wizard.HP = wizard.HP - 20;
            }
            else if (optionChoice == opponentCode + 1)
            {
                opponent.HP = opponent.HP - 20;
            }
            
        }
        private static void Battle(WizardClass wizard, WizardClass opponent, int opponentCode, int optionChoice, SpellsClass[] spellOptions)
        {
            Console.WriteLine("Player chosen Wizard is "+ wizard.WizardName);
            Console.WriteLine("CPU chosen Wizard is "+ opponent.WizardName);
            Console.WriteLine(wizard.WizardElement);
            opponent.XP = 60;
            spellChoosing(spellOptions, wizard);

            OpspellChoosing(spellOptions, opponent);

            //Console.Clear();
            //pointDeduction(wizard, opponent,opponentCode,optionChoice);
            CardGenerator(wizard, optionChoice);


            CardGenerator(opponent, opponentCode);
            Console.Clear();
            attack(wizard,opponent);

        }
        private static string ElementEmoji(int choice)
        {
            if (choice == 0)
            {
                return "🔥";

            }
            else if (choice == 1)
            {
                return "💧";

            }
            else if (choice == 2)
            {
                return "🌀";

            }
            else if (choice == 3)
            {
                return "🌍";

            }
            else
            {
                return " ";
            }
            
        }

        private static void CardGenerator(WizardClass wizard,int option)
        {
            string title = "Wizard";
            string Element = $"{ElementEmoji(option)}";
            string WizardName = wizard.WizardName;
            int HP = wizard.HP;
            int MP = wizard.MP;
            int XP = wizard.XP;
            SpellsClass[] spells = wizard.slots;
            //SpellsClass slots = wizard.slots;
            string content = "" ;

            string UpperBorder = "|``````````````````````|";
            string TitlePos = $"| {title.PadRight(13)}{WizardName.PadLeft(1)}{Element.PadLeft(1)} |";
            //string NamePos = $"| {WizardName.PadLeft(20)} |";
            //string ElementPos = $"| {Element.PadRight(20)} |";
            string HPPos = $"| HP{HP.ToString().PadLeft(18)} |";
            string XPPos = $"| XP{XP.ToString().PadLeft(18)} |";
            string MPPos = $"| MP{MP.ToString().PadLeft(18)} |";
            string SpellPos = $"| Spells{spells[0].spellName.ToString().PadLeft(14)} |";
            //string TitlePos = $"| {WizardName.PadRight(20)} |";

            string LowerBorder = "|______________________|";

            Console.WriteLine(UpperBorder);
            Console.WriteLine(TitlePos);
            //Console.WriteLine(NamePos);
            //Console.WriteLine(ElementPos);
            Console.WriteLine(HPPos);
            Console.WriteLine(XPPos);
            Console.WriteLine(MPPos);
            Console.WriteLine(SpellPos);
            Console.WriteLine(LowerBorder);

            

        }
    }
}
