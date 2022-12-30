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
            int n = 0;
            int[,] cart = new int[3, 9], tab = new int[9, 10];
            //generazione della cartella
            cart = GenerazioneCartella(cart);
            //stampa del tabellone
            tab = TabelloneStampa(tab);
            //estrazione del numero casuale
            while (true)
            {
                Thread.Sleep(2000);
                n = random.Next(1, 91);
                Console.SetCursorPosition(80, 21);
                Console.WriteLine(n);
                //verifica della corrisponenza nelle cartelle
                VerificaCorrispondenza(cart, tab, n);
            }
        }

        static int[,] GenerazioneCartella(int[,] cartella)
        {
            //dichiarazione
            Random random = new Random();
            cartella = new int[3, 9];
            int n = 1, m = 11;
            //generazione della cartella
            for (int i = 0; i < 3; i++)
            {
                n = 1;
                m = 11;
                for (int j = 0; j < 9; j++)
                {
                    cartella[i, j] = random.Next(n, m);
                    n = n + 10;
                    m = m + 10;
                }
            }
            //ciclo di modifica dei numeri ripetuti nella cartella
         
            //ciclo di inserimento delle caselle vuote
            for (int i = 0; i < 3; i++)
            {
                for (int j = random.Next(0, 2); j < 9; j = j + 2)
                {
                    cartella[i, j] = 0;
                }
            }
            //stampa della cartella
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(0, i);
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(cartella[i, j] + "   ");
                }
            }
            return cartella;
        }

        static int[,] TabelloneStampa(int[,] tabellone)
        {
            //matrice tabellone
            tabellone = new int[9, 10];
            int n = 1, riga = 4;
            //ciclo per la generazione del tabellone
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    tabellone[i, j] = n;
                    n++;
                }
            }
            //stampa del tabellone
            for (int i = 0; i < 9; i++)
            {
                Console.SetCursorPosition(0, riga);
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(tabellone[i, j] + "  ");
                }
                riga++;
            }
            return tabellone;
        }

        static void VerificaCorrispondenza(int[,] cartella, int[,] tabellone, int numero)
        {
            //dichiarazioni
            int riga = 4;
            //stampa del tabellone
            for (int i = 0; i < 9; i++)
            {
                Console.SetCursorPosition(0, riga);
                for (int j = 0; j < 10; j++)
                {
                    if (tabellone[i, j] == numero)
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
                    if (cartella[i, j] == numero)
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
