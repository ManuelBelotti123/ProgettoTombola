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
            int n = 0, riga = 0;
            string[,] cart = new string[3, 9], tab = new string[9, 10];
            //generazione della cartella
            cart = GenerazioneCartella(cart);
            //stampa della cartella
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(0, i);
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(cart[i, j] + "   ");
                }
            }
            //stampa del tabellone
            tab = Tabellone(tab);
            //stampa del tabellone
            riga = 6;
            for (int i = 0; i < 9; i++)
            {
                Console.SetCursorPosition(0, riga);
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(tab[i, j] + "  ");
                }
                riga++;
            }
            //estrazione del numero casuale
            riga = 6;
            while (true)
            {
                Thread.Sleep(2000);
                n = random.Next(1, 91);
                Console.SetCursorPosition(80, 21);
                Console.WriteLine(n);
                //verifica della corrisponenza nelle cartelle
                VerificaCorrispondenza(cart, tab, n, riga);
            }
        }

        static string[,] GenerazioneCartella(string[,] cartella)
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

        static string[,] Tabellone(string[,] tabellone)
        {
            //matrice tabellone
            tabellone = new string[9, 10];
            int n = 1, riga = 4;
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

        static void VerificaCorrispondenza(string[,] cartella, string[,] tabellone, int numero, int riga)
        {
            //dichiarazioni
            //stampa del tabellone
            for (int i = 0; i < 9; i++)
            {
                Console.SetCursorPosition(0, riga);
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
            //stampa della cartella con corrisppndenza
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(0, i);
                for (int j = 0; j < 9; j++)
                {
                    if (cartella[i, j] == numero.ToString())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(cartella[i, j] + "   ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
