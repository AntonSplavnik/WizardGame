using System;

namespace Wizard_Class_Practice
{

    public class Wizard
    {
        public string name;
        public string spellName;
        public int health;
        public int mana;
        public int damage;
        public float expirience;

        public static void RegenMana(Wizard wizard)
        {
            Random mana = new Random();
            int manaRegened = mana.Next(1, 10);
            wizard.mana += manaRegened;
            Console.WriteLine($"Mana points restored: {manaRegened}");
        }

        public void CastSpell (Wizard wizard, Enemy enemy)
        {
            // Random damage = new Random();
            // int generatetDamage = damage.Next(1, wizard.damage);
            // enemy.health -= generatetDamage;
            enemy.health -= wizard.damage;
            wizard.mana -= 10;
        }
    }

    public class Enemy
    {
        public string name;
        public int health;
        public float expirience;
        public int damage;

        public void Attack(Wizard wizard, Enemy enemy)
        {
            Random damage = new Random();
            int generatetDamage = damage.Next(1, enemy.damage);
            wizard.health -= generatetDamage;
        }    
    }

    class GameManager
    { 
        public static void Respawn(Wizard wizard)
        {
            if ( wizard.health <= 0)
            {
                wizard.health = 25;
                wizard.mana = 50;
                wizard.expirience = wizard.expirience - (wizard.expirience * 0.05f);
            }
        }
        
        public static void Fight(Wizard wizard, Enemy orc)
        {
            var count = 0;

            while (wizard.health != 0 && orc.health != 0)
            {
                
                while (wizard.mana >= 0 && orc.health >=0) //wizard attacks enemy (while mana >= 0)
                {
                    
                    if (wizard.mana < 10)  
                    {
                        //Console.WriteLine("_______________________________");
                        System.Console.WriteLine("Not enough mana!");
                        break;
                    }

                    count++;
                    
                    if (orc.health <=0)
                    {
                        break;
                    }

                    wizard.CastSpell(wizard, orc); 
                    orc.Attack(wizard, orc);

                    Console.WriteLine("_______________________________");
                    Console.WriteLine($"Action #{count}. Wizard Health: {wizard.health}");
                    Console.WriteLine($"Action #{count}. Wizard Mana: {wizard.mana}");
                    Console.WriteLine($"Action #{count}. Orc Health : {orc.health}");
                    Console.WriteLine("_______________________________");
                }

                if (wizard.health <= 0) // check if alive (if not respawn)
                {
                    System.Console.WriteLine("You died. You will be respawned.");
                    GameManager.Respawn(wizard);
                    break;
                }
                if (orc.health <=0)
                    {
                        break;
                    }  

                count++;
                orc.Attack(wizard, orc);
                Console.WriteLine("_______________________________");
                Console.WriteLine($"Action #{count}. Wizard Health: {wizard.health}");
                Console.WriteLine($"Action #{count}. Orc Health : {orc.health}");
                Console.WriteLine("_______________________________");   

                if (wizard.health <= 0) // check if alive (if not respawn)
                {
                    System.Console.WriteLine("You died. You will be respawned.");
                    GameManager.Respawn(wizard);
                    break;
                }
                
            }

            // if wizard alive and enemy is dead give exp
            if (wizard.health > 0 && orc.health <= 0)
            {
                wizard.expirience += orc.expirience;
            }
        
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            
            Wizard wizard01 = new Wizard() {health = 100, expirience = 0f, damage = 10, mana = 100};
            Enemy Orc = new Enemy() {health = 100, expirience = 14f, damage = 7 };
            Enemy Orc02 = new Enemy() {health = 50, expirience = 5f, damage = 3 };

            Console.WriteLine("\n\n\n_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_");
            GameManager.Fight(wizard01, Orc);
            System.Console.WriteLine($"Wizard Health: {wizard01.health}\nWizard Mana: {wizard01.mana}\nWizard EXP: {wizard01.expirience}");
            
            Console.WriteLine("\n\n\n_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_");
            GameManager.Fight(wizard01, Orc);
            System.Console.WriteLine($"Wizard Health: {wizard01.health}\nWizard Mana: {wizard01.mana}\nWizard EXP: {wizard01.expirience}");
            
            Console.WriteLine("\n\n\n_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_");
            Wizard.RegenMana(wizard01);
            System.Console.WriteLine($"Wizard Health: {wizard01.health}\nWizard Mana: {wizard01.mana}\nWizard EXP: {wizard01.expirience}");
            
            Console.WriteLine("\n\n\n_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_");
            GameManager.Fight(wizard01, Orc02);
            System.Console.WriteLine($"Wizard Health: {wizard01.health}\nWizard Mana: {wizard01.mana}\nWizard EXP: {wizard01.expirience}");
        }    

    }
}
