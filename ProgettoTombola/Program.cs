using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
            int[,] cart = new int[3, 9], tab = new int[9, 10];
            //generazione della cartella
            cart = GenerazioneCartella(cart);
            //stampa del tabellone
            tab = TabelloneStampa(tab);
        }

        static int[,] GenerazioneCartella(int[,] cartella)
        {
            //dichiarazione
            Random random = new Random();
            cartella = new int[3, 9];
            int n = 1, m = 11, sup = 0;
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
    }
}
