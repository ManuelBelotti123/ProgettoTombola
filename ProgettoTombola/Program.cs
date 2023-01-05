using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgettoTombola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //dichiarazione
            Random random = new Random();
            int n = 0, riga = 0, colonna = 0;
            bool c = false;
            string n1 = "";
            string[,] cart = new string[3, 9], tab = new string[9, 10];
            //generazione della cartella
            cart = GenerazioneCartella(cart, riga, colonna);
            Console.WriteLine("Cartella 1");
            riga = 1;
            colonna = 0;
            CartellaStampa(cart, riga, colonna);
            //stampa del tabellone
            tab = Tabellone(tab, riga, colonna);
            Console.WriteLine("\n\n\nTabellone");
            riga = 7;
            TabelloneStampa(tab, riga, colonna);
            //estrazione del numero casuale
            riga = 7;
            while (true)
            {
                Thread.Sleep(2);
                n = random.Next(1, 91);
                if (n < 10)
                {
                    n1 = " " + n.ToString();
                }
                else
                {
                    n1 = n.ToString();
                }
                Console.SetCursorPosition(80, 21);
                Console.WriteLine(n);
                //aggiornamento del tabellone
                TabelloneAggiornamento(tab, n, riga, colonna);
                //verifica della corrisponenza nelle cartelle
                CartellaVerificaCorrispondenza(cart, n1, riga, colonna);
                //verifica cinquine
                VerificaCinquina(cart, riga, colonna);
                //verifica tombola
                c = VerificaTombola(cart, riga, colonna, c);
                if (c)
                {
                    break;
                }
            }
        }

        static string[,] GenerazioneCartella(string[,] cartella, int riga, int colonna)
        {
            //dichiarazione
            Random random = new Random();
            cartella = new string[3, 9];
            int n = 1, m = 11;
            //generazione della cartella
            string numerogen = "0";
            for (int i = 0; i < 3; i++)
            {
                n = 1;
                m = 11;
                numerogen = "0";
                for (int j = 0; j < 9; j++)
                {
                    //generazione di numeri non ripetuti nella cartella
                    numerogen = random.Next(n, m).ToString();
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 9; l++)
                        {
                            while (cartella[k, l] == numerogen)
                            {
                                numerogen = random.Next(n, m).ToString();
                            }
                        }
                    }
                    //correzione dell'allineamento dei numeri in colonna
                    if (n == 1 && m == 11 && numerogen != "10")
                    {
                        numerogen = " " + numerogen;
                    }
                    cartella[i, j] = numerogen;
                    n = n + 10;
                    m = m + 10;
                }
            }
            //ciclo di inserimento delle caselle vuote
            int randi = 0;
            for (int i = 0; i < 3; i++)
            {
                randi = 0;
                for (int j = 0; j < 4; j++)
                {
                    do
                    {
                        randi = random.Next(0, 9);
                    }
                    while (cartella[i, randi] == "--");
                    cartella[i, randi] = "--";
                }
            }
            return cartella;
        }

        static void CartellaStampa(string[,] cartella, int riga, int colonna)
        {
            //stampa della cartella
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(colonna, riga);
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(cartella[i, j] + "   ");
                }
                riga++;
            }
        }

        static string[,] Tabellone(string[,] tabellone, int riga, int colonna)
        {
            //matrice tabellone
            tabellone = new string[9, 10];
            int n = 1;
            //ciclo per la generazione del tabellone
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    tabellone[i, j] = n.ToString();
                    n++;
                }
            }
            return tabellone;
        }

        static void TabelloneStampa(string[,] tabellone, int riga, int colonna)
        {
            //stampa del tabellone
            for (int i = 0; i < 9; i++)
            {
                Console.SetCursorPosition(colonna, riga);
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(tabellone[i, j] + "  ");
                }
                riga++;
            }
        }

        static void TabelloneAggiornamento(string[,] tabellone, int numero, int riga, int colonna)
        {
            //stampa del tabellone
            for (int i = 0; i < 9; i++)
            {
                Console.SetCursorPosition(colonna, riga);
                for (int j = 0; j < 10; j++)
                {
                    if (tabellone[i, j] == numero.ToString())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(tabellone[i, j] + "  ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                riga++;
            }
        }

        static void CartellaVerificaCorrispondenza(string[,] cartella, string numero, int riga, int colonna)
        {
            //stampa della cartella con corrispondenza
            riga = 1;
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(colonna, riga);
                for (int j = 0; j < 9; j++)
                {
                    if (cartella[i, j] == numero)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Thread.Sleep(1000);
                        cartella[i, j] = "^^";
                    }
                    Console.Write(cartella[i, j] + "   ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                riga++;
            }
        }

        static void VerificaCinquina(string[,] cartella, int riga, int colonna)
        {
            //dichiarazione
            int contatore = 0;
            //ciclo
            riga = 25;
            for (int i = 0; i < 3; i++)
            {
                contatore = 0;
                for (int j = 0; j < 9; j++)
                {
                    if (cartella[i, j] == "^^")
                    {
                        contatore++;
                    }
                }
                if (contatore == 5)
                {
                    Console.SetCursorPosition(colonna, riga);
                    Console.WriteLine("Cinquina!");
                }
            }
        }

        static bool VerificaTombola(string[,] cartella, int riga, int colonna, bool c)
        {
            //dichiarazione
            int contatore = 0;
            //ciclo
            riga = 25;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (cartella[i, j] == "^^")
                    {
                        contatore++;
                    }
                }
            }
            if (contatore == 15)
            {
                Console.SetCursorPosition(colonna, riga);
                Console.WriteLine("Tombola!");
                c = true;
            }
            return c;
        }
    }
}
